using AutoMapper;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Models;

namespace UniversitySystem.Features.Student.Command.Handlers
{
    public class UpdateStudentHandler(IStudentRepository repo, IMapper mapper)
    {
        public async Task<UniversitySystem.Models.Student> Handle(UpdateStudentDto dto)
        {
            var student = await repo.GetByIdAsync(dto.Id);
            if (student == null)
                throw new Exception("Student not found");

            mapper.Map(dto, student);
            await repo.UpdateAsync(student);

            return student;
        }
    }
}