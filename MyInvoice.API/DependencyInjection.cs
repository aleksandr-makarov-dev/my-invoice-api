using MyInvoice.API.Infrastructure;

namespace MyInvoice.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) => services
        .AddCoreServices()
        .AddExceptionHandler();

    private static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
