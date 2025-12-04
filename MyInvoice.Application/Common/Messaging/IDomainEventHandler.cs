using MediatR;
using MyInvoice.Domain.Base;

namespace MyInvoice.Application.Common.Messaging;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;