using Microsoft.EntityFrameworkCore;

namespace MyInvoice.Application.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
