using FluentValidation;
using MediatR;
using MyInvoice.Application.Common.Messaging;

namespace MyInvoice.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Count != 0)
            .SelectMany(validationResult => validationResult.Errors)
            .ToList();

        if (validationErrors.Count != 0)
        {
            throw new ValidationException(validationErrors);
        }

        return await next(cancellationToken);
    }
}
