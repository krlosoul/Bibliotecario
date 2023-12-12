namespace PruebaIngresoBibliotecario.Api.Filters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using PruebaIngresoBibliotecario.Business.Common.Exceptions;
    using System.Collections.Generic;

    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException validationEx:
                    HandleValidationException(context, validationEx);
                    break;
                case NotFoundException notFoundEx:
                    HandleNotFoundException(context, notFoundEx);
                    break;
                case BadRequestException badRequestEx:
                    HandleBadRequestException(context, badRequestEx);
                    break;
                default:
                    HandleUnknownException(context);
                    break;
            }

            base.OnException(context);
        }


        private void HandleValidationException(ExceptionContext context, ValidationException exception)
        {
            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private static void HandleNotFoundException(ExceptionContext context, NotFoundException exception)
        {
            Dictionary<string, string> message = new Dictionary<string, string>
            {
                { "mensaje", exception.Message }
            };

            context.Result = new NotFoundObjectResult(message);

            context.ExceptionHandled = true;
        }

        private void HandleBadRequestException(ExceptionContext context, BadRequestException exception)
        {
            Dictionary<string, string> message = new Dictionary<string, string>
            {
                { "mensaje", exception.Message }
            };
            context.Result = new BadRequestObjectResult(message);

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
