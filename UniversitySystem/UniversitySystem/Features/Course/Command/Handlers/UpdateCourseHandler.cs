using AutoMapper;
using MediatR;
using UniversitySystem.Features.Course.Command.Models; 
using UniversitySystem.Global;
using UniversitySystem.Repositories.Interfaces;


namespace UniversitySystem.Features.Course.Command.Handler
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseDto, Response>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateCourseDto request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);

            if (course == null)
            {
                return new Response
                {
                    Message = $"Course with ID {request.Id} not found",
                    Status = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            
            _mapper.Map(request, course);
            
            await _courseRepository.UpdateAsync(course, cancellationToken);
            return new Response
            {
                Data = course,
                Message = "Course updated successfully",
                Status = true,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

    }
}