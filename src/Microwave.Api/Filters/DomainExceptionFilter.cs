using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microwave.Domain.Exceptions;

namespace Microwave.Api.Filters;

public class DomainValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DomainException domainEx)
        {
            var problemDetails = new ValidationProblemDetails(domainEx.Errors.ToDictionary())
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error"
            };

            context.Result = new BadRequestObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}