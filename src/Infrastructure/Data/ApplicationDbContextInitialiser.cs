using Domain.Common.Players;
using Domain.Common.PlayersPositionsDict;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Builder;
using WebApplicationBuilder = Microsoft.AspNetCore.Builder;

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
        // Default data
        // Seed, if necessary
        try
        {
            var chart = _dbContext.Chart;
            //if (!_dbContext.Chart.Any())
            //{
            var tester = new Domain.Common.DepthChart
            {
                League = "NFL",
                Team = "ArizonaCardinals",
                Chart  = new Dictionary<string, IEnumerable<Player>>
                {
                    { "QB", new List<Player> { new Player("Kyler", 21)}}
                }
            };


                _dbContext.Chart.Add(tester);

                await _dbContext.SaveChangesAsync();
            //}

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}
