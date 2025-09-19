
using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Command.Handlers
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentDto, Response>
    {
        private readonly ApplicationDbContext _context;

        public DeleteStudentHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(DeleteStudentDto request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _context.Students.FindAsync(request.Id);
                if (student == null)
                {
                    return Response.ErrorResponse(
                        "Student not found",
                        statusCode: HttpStatusCode.NotFound
                    );
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync(cancellationToken);

                var responseData = new
                {
                    Id = request.Id,
                    Message = "Student deleted successfully"
                };

                return Response.SuccessResponse(
                    responseData,
                    "Student deleted successfully",
                    HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to delete student",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}