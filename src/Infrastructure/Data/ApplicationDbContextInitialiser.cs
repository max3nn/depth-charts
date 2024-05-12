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
                    { "QB", new List<Player> { new Player("BRADY TOM", 21), new Player("SMITH JOHN", 21)}},
                    { "RB", new List<Player> { new Player("James Conner", 6)}},
                    { "WR", new List<Player> { new Player("DeAndre Hopkins", 10)}},
                    { "TE", new List<Player> { new Player("Maxx Williams", 87)}},
                    { "OL", new List<Player> { new Player("Justin Pugh", 67)}},
                    { "DL", new List<Player> { new Player("J.J. Watt", 99)}},
                    { "LB", new List<Player> { new Player("Chandler Jones", 55)}},
                    { "CB", new List<Player> { new Player("Byron Murphy Jr.", 33)}},
                    { "S", new List<Player> { new Player("Budda Baker", 32)}}

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
