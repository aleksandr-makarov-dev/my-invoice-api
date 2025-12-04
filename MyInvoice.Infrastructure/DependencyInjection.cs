using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyInvoice.Application.Interfaces;
using MyInvoice.Infrastructure.Database;
using MyInvoice.Infrastructure.Database.Interceptors;
using MyInvoice.Infrastructure.Providers;

namespace MyInvoice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection
        AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services
        .AddServices()
        .AddDatabase(configuration);

    // .AddDefaultDomainEvents();
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<PublishDomainEventsInterceptor>();

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            options.UseNpgsql(connectionString,
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .AddInterceptors(provider.GetRequiredService<PublishDomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        return services;
    }
}
