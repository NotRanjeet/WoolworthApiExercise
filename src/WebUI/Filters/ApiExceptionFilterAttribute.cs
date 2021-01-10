using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Woolworth.Application.Common.Exceptions;
using Woolworth.Domain.Exceptions;

namespace Woolworth.WebUI.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(DomainException), HandleDomainException},
                {typeof(HttpResponseException), HandleHttpResponseException}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
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

        private void HandleValidationException(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                var details = new ValidationProblemDetails(exception.Errors)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                context.Result = new BadRequestObjectResult(details);

                context.ExceptionHandled = true;
            }
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

        private void HandleNotFoundException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException exception)
            {

                var details = new ProblemDetails()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    Title = "The specified resource was not found.",
                    Detail = exception.Message
                };

                context.Result = new NotFoundObjectResult(details);

                context.ExceptionHandled = true;
            }
        }

        private void HandleDomainException(ExceptionContext context)
        {
            if (context.Exception is DomainException exception)
            {
                var details = new ProblemDetails()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    Title = "Business Logic validation failed.",
                    Detail = exception.Message
                };

                context.Result = new UnprocessableEntityObjectResult(details);

                context.ExceptionHandled = true;
            }
        }

        private void HandleHttpResponseException(ExceptionContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                var responseMessage = exception.ResponseMessage;
                var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                var errorResponse = new ProblemDetails
                {
                    Type = context.HttpContext?.Request?.GetDisplayUrl(),
                    Status = (int)exception.ResponseMessage.StatusCode,
                    Title = exception.ResponseMessage.ReasonPhrase,
                    Detail = responseContent,
                };
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = errorResponse.Status,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
