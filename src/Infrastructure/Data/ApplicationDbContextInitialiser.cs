using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using DepthChart.Domain;
using System.Text.Json;

namespace DepthChart.Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this IApplicationBuilder webApplication)
    {
        using var scope = webApplication.ApplicationServices.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.TrySeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _dbContext;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task TrySeedAsync(string file = "../../SeedData.json")
    {
        try
        {
            var json = await File.ReadAllTextAsync(file);
            IEnumerable<Domain.Common.DepthChart> seedDepthCharts = JsonSerializer.Deserialize<IEnumerable<Domain.Common.DepthChart>>(json);

            _dbContext.Chart.AddRange(seedDepthCharts);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}
