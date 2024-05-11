using DepthChart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DepthChart.Domain.Contracts;
using DepthChart.Infrastructure.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseInMemoryDatabase(configuration.GetConnectionString("DefaultConnection") ?? ""));

        services.AddTransient<IChartRepository, ChartRepository>();
        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}