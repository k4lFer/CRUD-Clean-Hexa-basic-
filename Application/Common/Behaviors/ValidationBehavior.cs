using System.Net;
using Application.DTOs;
using Application.DTOs.Common;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result<object>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .GroupBy(f => f.PropertyName)
                .ToList();

            if (failures.Any())
            {
                var messageDto = new MessageDto { Type = "error" };

                foreach (var group in failures)
                {
                    foreach (var error in group)
                    {
                        messageDto.AddMessage($"{error.PropertyName}: {error.ErrorMessage}");
                    }
                }

                var result = Result<object>.Failure([messageDto], HttpStatusCode.BadRequest);
                return (TResponse)(object)result;
            }

            return await next();
        }
    }
}