using System.Threading.Tasks;
using DepthChart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DepthChart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DepthChart.Domain.Contracts;
using DepthChart.Infrastructure.Repositories;

namespace Application.UnitTests.Services
{
    public class ChartServiceTest
    {
        // TODO: Add private services

        private static DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "DepthChartTest").Options;

        ApplicationDbContext _db;

        public ChartServiceTest()
        {
            _db = new ApplicationDbContext(_dbContextOptions);
            _db.Database.EnsureDeleted();
            SeedDatabase();
        }

        [Fact]
        async public Task Test_SeedDatabase_Successful()
        {
            var results = await _db.Chart.ToListAsync();

            Assert.NotEmpty(results);
        }

        [Fact]
        async public Task AddPlayerToDepthChart_Should_AddPlayerToDepthChart()
        {
            // Arrange
            // TODO: Create an instance of ChartService and mock the dependencies

            // Act
            // TODO: Call the AddPlayerToDepthChart method with test data

            // Assert
            // TODO: Assert that the player is added to the depth chart correctly
        }

        [Fact]
        async public Task GetBackups_Should_ReturnBackupsForPlayer()
        {
            // Arrange
            // TODO: Create an instance of ChartService and mock the dependencies

            // Act
            // TODO: Call the GetBackups method with test data

            // Assert
            // TODO: Assert that the correct backups are returned
        }

        [Fact]
        async public Task GetFullDepthChart_Should_ReturnFullDepthChart()
        {
            // Arrange
            // TODO: Create an instance of ChartService and mock the dependencies

            // Act
            // TODO: Call the GetFullDepthChart method with test data

            // Assert
            // TODO: Assert that the complete depth chart is returned
        }

        [Fact]
        async public Task RemovePlayerFromDepthChart_Should_RemovePlayerFromDepthChart()
        {
            // Arrange
            // TODO: Create an instance of ChartService and mock the dependencies

            // Act
            // TODO: Call the RemovePlayerFromDepthChart method with test data

            // Assert
            // TODO: Assert that the player is removed from the depth chart correctly
        }

        // Edge cases

        // Player Name casing doesn't affected the retrieval of backups, or removal players

        private void SeedDatabase()
        {
            new ApplicationDbContextInitialiser(null, _db).SeedAsync().Wait();
        }
    }
}
