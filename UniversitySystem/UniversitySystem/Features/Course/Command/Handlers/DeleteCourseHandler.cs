using AutoMapper;
using MediatR;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Repositories.Interfaces;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Command.Handlers
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseDto, Response>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public DeleteCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(DeleteCourseDto request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);

            if (course == null)
            {
                return new Response
                {
                    Message = $"Course with id {request.Id} not found",
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            await _courseRepository.DeleteAsync(course, cancellationToken);

            var responseData = _mapper.Map<object>(course);

            return new Response
            {
                Data = responseData,
                Message = $"Course with id {request.Id} deleted successfully",
                Status = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}