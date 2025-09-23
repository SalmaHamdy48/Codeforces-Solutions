

using UniversitySystem.Global;
using MediatR;

namespace UniversitySystem.Features.Course.Command.Models
{
    public class UpdateCourseDto(int id , string code , string cname , int hours) : IRequest<Response>
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Cname { get; set; } = string.Empty;
        public int Hours { get; set; }
        
    }
    
    
}