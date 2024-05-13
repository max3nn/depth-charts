using System.Linq;
using System.Threading.Tasks;
using DepthChart.Application.Services;
using DepthChart.Infrastructure.Data;
using DepthChart.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.UnitTests.Services
{
    public class ChartServiceTest
    {
        // TODO: Add private services

        private static DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "DepthChartTest").Options;

        readonly ApplicationDbContext _db;

        private readonly ChartRepository _chartRepository;
        private readonly ChartService _chartService;
        private readonly ILogger<ChartService> _logger = Substitute.For<ILogger<ChartService>>();
        private readonly ILogger<ApplicationDbContextInitialiser> _loggerDbContext = Substitute.For<ILogger<ApplicationDbContextInitialiser>>();

        public ChartServiceTest()
        {
            _db = new ApplicationDbContext(_dbContextOptions);
            _db.Database.EnsureDeleted();
            SeedDatabase();

            _chartRepository = new ChartRepository(_db);
            _chartService = new ChartService(_chartRepository, _logger);
        }

        [Fact]
        async public Task Test_SeedDatabase_Successful()
        {
            // Act
            var results = await _db.Chart.ToListAsync();

            // Assert
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("NFL", "ArizonaCardinals", 9)]
        [InlineData("NFL", "AtlantaFalcons", 8)]
        [InlineData("NFL", "BaltimoreRavens", 4)]
        [InlineData("NFL", "BuffaloBills", 2)]
        async public Task GetFullDepthChart_Returns_DepthChart_WithAll_SeededPositions(string league, string team, int seededPositions)
        {
            // Act
            var chart = await _chartService.GetFullDepthChart(league, team);

            // Assert
            Assert.NotNull(chart);
            Assert.Equal(league, chart.League);
            Assert.Equal(team, chart.Team);
            Assert.Equal(seededPositions, chart.Chart.Count);
        }

        [Theory]
        [InlineData("AtlantaFalcons", "QB", "LAST FIRST", 10, 0)]
        [InlineData("AtlantaFalcons", "QB", "ABC CBA", 21, 4)]
        [InlineData("AtlantaFalcons", "QB", "AAA BBB", 21, 3)]
        async public Task Test_AddPlayerToDepthChart_At_Various_Depths_Successful(string team, string position, string name, int number, int positionDepth)
        {
            // Act
            await _chartService.AddPlayerToDepthChart("NFL", team, position, name, number, positionDepth);
            var chart = await _chartService.GetFullDepthChart("NFL", team);
            var playerInPosition = chart.Chart[position].ToList();
            var playerAtDepth = playerInPosition[positionDepth];

            // Assert
            Assert.Equal(name, playerAtDepth.Name);
            Assert.Equal(number, playerAtDepth.Number);
        }

        [Fact]
        async public Task Test_AddPlayerToDepthChart_Has_MaxDepth_Of_5_Player()
        {
            // Act
            await _chartService.AddPlayerToDepthChart("NFL", "AtlantaFalcons", "WR", "PERSON NAME", 51, 0);
            var chart = await _chartService.GetFullDepthChart("NFL", "AtlantaFalcons");

            // Assert
            Assert.Equal(5, chart.Chart["WR"].Count());
        }

        [Fact]
        async public Task Test_Can_AddPlayerToDepthChart_To_Empty_Position()
        {
            // Act
            await _chartService.AddPlayerToDepthChart("NFL", "AtlantaFalcons", "S", "PERSON NAME", 51, 0);
            var chart = await _chartService.GetFullDepthChart("NFL", "AtlantaFalcons");

            // Assert
            Assert.Equal(1, chart.Chart["S"].Count());
        }

        private void SeedDatabase()
        {
            new ApplicationDbContextInitialiser(_loggerDbContext, _db).TrySeedAsync("../../../../../SeedData.json").Wait();
        }
    }
}
