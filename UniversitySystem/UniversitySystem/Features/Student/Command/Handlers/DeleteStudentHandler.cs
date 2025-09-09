using MediatR;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Repositories.Interfaces;

namespace UniversitySystem.Features.Student.Command.Handlers
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentDto, bool>
    {
        private readonly IStudentRepository _repo;

        public DeleteStudentHandler(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteStudentDto request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetByIdAsync(request.Id);
            if (student == null)
                return false;

            await _repo.DeleteAsync(student);
            return true;
        }
    }
}