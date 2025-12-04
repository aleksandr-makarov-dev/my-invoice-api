using MediatR;

namespace MyInvoice.Domain.Base;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
