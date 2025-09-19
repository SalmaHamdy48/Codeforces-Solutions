// Behaviour/ValidationBehaviour.cs (Backward Compatible Version)
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UniversitySystem.Global;
using System.Net;

namespace UniversitySystem.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;

        public ValidationBehaviour(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            return await HandleCore(request, next, cancellationToken);
        }

        //عشان اختلاف ال versions
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            return await HandleCore(request, next, cancellationToken);
        }

        private async Task<TResponse> HandleCore(TRequest request, RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    _logger.LogWarning("Validation failed for request {@Request} with errors: {@Errors}", 
                        request, failures.Select(f => f.ErrorMessage));

                    var errors = failures
                        .GroupBy(e => e.PropertyName)
                        .Select(g => $"{g.Key}: {string.Join(", ", g.Select(e => e.ErrorMessage))}")
                        .ToList();

                    return (TResponse)Response.ErrorResponse("Validation failed", errors, HttpStatusCode.BadRequest);
                }
            }

            return await next();
        }
    }
}