using MediatR;

namespace MyInvoice.Application.Common.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
