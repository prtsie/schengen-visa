using ApplicationLayer.GeneralExceptions;
using ApplicationLayer.Services.AuthServices.LoginService.Exceptions;
using ApplicationLayer.Services.VisaApplications.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchengenVisaApi.ExceptionFilters;

/// Handles <see cref="ApiException"/>
public class GlobalExceptionsFilter : IAsyncExceptionFilter
{
    /// <inheritdoc cref="IExceptionFilter.OnException"/>
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case ValidationException validationException:
                problemDetails.Extensions.Add("errors", validationException.Errors.Select(e => e.ErrorMessage));
                problemDetails.Detail = "Validation errors occured";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Bad request";
                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                break;
            case ApiException:
                problemDetails.Detail = exception.Message;
                switch (exception)
                {
                    case EntityNotFoundException:
                        problemDetails.Status = StatusCodes.Status404NotFound;
                        problemDetails.Title = "Requested entity not found";
                        problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
                        break;
                    case IncorrectLoginDataException:
                        problemDetails.Status = StatusCodes.Status403Forbidden;
                        problemDetails.Title = "Auth failed";
                        problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3";
                        break;
                    case AlreadyExistsException:
                        problemDetails.Status = StatusCodes.Status409Conflict;
                        problemDetails.Title = "Already exists";
                        problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
                        break;
                    case ApplicationAlreadyProcessedException:
                        problemDetails.Status = StatusCodes.Status409Conflict;
                        problemDetails.Title = "Already processed";
                        problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
                        break;
                    default:
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "Bad request";
                        problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                        break;
                }

                break;
            default:
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "An unhandled error occured";
                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                break;
        }

        await Results.Problem(problemDetails).ExecuteAsync(context.HttpContext);
        context.ExceptionHandled = true;
    }
}
