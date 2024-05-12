using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using DepthChart.Domain;

namespace DepthChart.Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this IApplicationBuilder webApplication)
    {
        using var scope = webApplication.ApplicationServices.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.SeedAsync();
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


    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        try
        {
            var chart = _dbContext.Chart;
            var ACDepthChart = new Domain.Common.DepthChart
            {
                League = "NFL",
                Team = "ArizonaCardinals",
                Chart  = new Dictionary<string, IEnumerable<Player>>
                {
                    { "QB", new List<Player> { new("BRADY TOM", 21), new Player("SMITH JOHN", 21)}},
                    { "RB", new List<Player> { new("James Conner", 6)}},
                    { "WR", new List<Player> { new("DeAndre Hopkins", 10)}},
                    { "TE", new List<Player> { new("Maxx Williams", 87)}},
                    { "OL", new List<Player> { new("Justin Pugh", 67)}},
                    { "DL", new List<Player> { new("J.J. Watt", 99)}},
                    { "LB", new List<Player> { new("Chandler Jones", 55)}},
                    { "CB", new List<Player> { new("Byron Murphy Jr.", 33)}},
                    { "S", new List<Player> { new("Budda Baker", 32)}}
                }
            };

                _dbContext.Chart.Add(ACDepthChart);
                await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}
