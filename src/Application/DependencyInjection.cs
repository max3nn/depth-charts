using Application.Interfaces;
using DepthChart.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DepthChart.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddTransient<IChartService, ChartService>();

            return services;
        }
    }
}