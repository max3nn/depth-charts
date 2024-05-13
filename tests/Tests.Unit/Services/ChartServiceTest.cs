using System.Linq;
using System.Threading.Tasks;
using DepthChart.Application.Services;
using DepthChart.Infrastructure.Data;
using DepthChart.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Application.UnitTests.Services
{
    public class ChartServiceTest
    {
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
            // Arrange -> Handled in constructor

            // Act
            var results = await _db.Chart.ToListAsync();

            // Assert
            Assert.NotEmpty(results);
        }

        #region GetFullDepthChart

        [Theory]
        [InlineData("NFL", "ArizonaCardinals", 9)]
        [InlineData("NFL", "AtlantaFalcons", 8)]
        [InlineData("NFL", "BaltimoreRavens", 4)]
        [InlineData("NFL", "BuffaloBills", 2)]
        async public Task GetFullDepthChart_Returns_DepthChart_WithAll_SeededPositions(string league, string team, int seededPositions)
        {
            // Arrange -> Handled in constructor

            // Act
            var chart = await _chartService.GetFullDepthChart(league, team);

            // Assert
            Assert.NotNull(chart);
            Assert.Equal(league, chart.League);
            Assert.Equal(team, chart.Team);
            Assert.Equal(seededPositions, chart.Chart.Count);
        }

        #endregion

        #region AddPlayerToDepthChart

        [Theory]
        [InlineData("AtlantaFalcons", "QB", "LAST FIRST", 10, 0)]
        [InlineData("AtlantaFalcons", "QB", "ABC CBA", 21, 4)]
        [InlineData("AtlantaFalcons", "QB", "AAA BBB", 21, 3)]
        async public Task Test_AddPlayerToDepthChart_At_Various_Depths_Successful(string team, string position, string name, int number, int positionDepth)
        {
            // Arrange -> Handled in constructor

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
            // Arrange -> Handled in constructor

            // The AtlantaFalcons has 0 players in the 'WR' position
            // Act
            await _chartService.AddPlayerToDepthChart("NFL", "AtlantaFalcons", "WR", "PERSON NAME", 51, 0);
            var chart = await _chartService.GetFullDepthChart("NFL", "AtlantaFalcons");

            // Assert
            Assert.Equal(5, chart.Chart["WR"].Count());
            Assert.Equal("PERSON NAME", chart.Chart["WR"].ElementAt(0).Name);
        }

        [Fact]
        async public Task Test_Can_AddPlayerToDepthChart_To_Empty_Position()
        {
            // Arrange

            // The AtlantaFalcons has 0 players in the 'S' position
            // Act
            await _chartService.AddPlayerToDepthChart("NFL", "AtlantaFalcons", "S", "PERSON NAME", 51, 0);
            var chart = await _chartService.GetFullDepthChart("NFL", "AtlantaFalcons");

            // Assert
            Assert.Equal(1, chart.Chart["S"]?.Count());
        }

        [Fact]
        public async Task Test_AddPlayerToDepthChart_Throws_Exception_When_Chart_Not_Found()
        {
            // Arrange -> Handled in constructor

            // Act
            var exception = await Record.ExceptionAsync(() => _chartService.AddPlayerToDepthChart("NFL", "NOT A REAL TEAM", "QB", "Matt Ryan", 2, 0));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Chart not found.", exception.Message);
        }   

        #endregion

        #region RemovePlayerFromDepthChart

        [Theory]
        [InlineData("AtlantaFalcons", "QB", "Matt Ryan")]
        [InlineData("AtlantaFalcons", "RB", "Cordarrelle Patterson")]
        [InlineData("AtlantaFalcons", "CB", "Chris Williamson")]
        async public Task Test_RemovePlayerFromDepthChart_At_Various_Depths_Successful(string team, string position, string name)
        {
            // Arrange -> Handled in constructor

            // Act
            await _chartService.RemovePlayerFromDepthChart("NFL", team, position, name);
            var chart = await _chartService.GetFullDepthChart("NFL", team);

            // Assert
            Assert.DoesNotContain(chart.Chart[position], p => p.Name == name);
            Assert.Equal(4, chart.Chart[position].Count());
        }

        [Fact]
        async public Task Test_RemovePlayerFromDepthChart_Throws_Exception_When_Player_Not_Found()
        {
            // Arrange -> Handled in constructor

            // Act
            var exception = await Record.ExceptionAsync(() => _chartService.RemovePlayerFromDepthChart("NFL", "AtlantaFalcons", "QB", "NOT A REAL PLAYER"));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Player not found.", exception.Message);
        }

        #endregion

        #region GetBackups

        [Theory]
        [InlineData("AtlantaFalcons", "QB", "Matt Ryan", 4)]
        [InlineData("AtlantaFalcons", "RB", "Cordarrelle Patterson", 3)]
        [InlineData("AtlantaFalcons", "CB", "Chris Williamson", 0)]
        async public Task Test_GetBackups_At_Various_Depths_Successful(string team, string position, string name, int backups)
        {
            // Arrange -> Handled in constructor

            // Act
            var results = await _chartService.GetBackups("NFL", team, position, name);

            // Assert
            Assert.Equal(backups, results.Count());
        }

        #endregion

        private void SeedDatabase()
        {
            new ApplicationDbContextInitialiser(_loggerDbContext, _db).TrySeedAsync("../../../../../SeedData.json").Wait();
        }
    }
}
