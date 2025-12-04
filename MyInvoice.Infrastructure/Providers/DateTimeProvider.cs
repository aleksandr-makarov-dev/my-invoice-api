using MyInvoice.Application.Interfaces;

namespace MyInvoice.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
