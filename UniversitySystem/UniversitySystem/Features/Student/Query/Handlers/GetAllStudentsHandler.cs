using MediatR;
using UniversitySystem.Features.Student.Query.Models;
using UniversitySystem.Global;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Net;
using AutoMapper;
using UniversitySystem.Features.Student.Command.Models;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, Response>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetAllStudentsHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var page = Math.Max(1, request.Page ?? 1);
            var pageSize = Math.Min(50, Math.Max(1, request.PageSize ?? 10));
            var skip = (page - 1) * pageSize;

            var spec = new AllStudentsSpecification(skip, pageSize);
            var students = await _studentRepository.GetListAsync(spec, cancellationToken);

            var totalCount = await _studentRepository.CountAsync(new AllStudentsSpecification(), cancellationToken);

            var studentsData = _mapper.Map<List<StudentDto>>(students);

            var responseData = new
            {
                Students = studentsData,
                Pagination = new
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 0,
                    HasNextPage = page < Math.Ceiling((double)totalCount / pageSize),
                    HasPreviousPage = page > 1
                }
            };

            return Response.SuccessResponse(
                responseData,
                $"Students retrieved successfully. Page {page} of {(int)Math.Ceiling((double)totalCount / pageSize)}",
                HttpStatusCode.OK
            );
        }
    }
}