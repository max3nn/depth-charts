using DepthChart.Domain.Common;
using DepthChart.Domain.Constants;
using DepthChart.Domain.Contracts;
using DepthChart.Infrastructure.Data;
using Domain.Common.Players;
using Domain.Common.PlayersPositionsDict;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DepthChart.Infrastructure.Repositories
{
    public class ChartRepository(ApplicationDbContext _db) : IChartRepository
    {
        public Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth)
        {

            return Task.Run(async () =>
            {
                var allCharts = await _db.Chart.ToListAsync();
                var updatedData = allCharts;
                _db.Chart.RemoveRange(allCharts);
                await _db.SaveChangesAsync();

                var teamDepthChart = updatedData.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault();

                var playersInPosition = teamDepthChart.Chart[position].ToList();

                playersInPosition.Insert(depth, new Player(name, number));

                updatedData.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault().Chart[position] = playersInPosition;

                await _db.Chart.AddRangeAsync(updatedData);
                await _db.SaveChangesAsync();


                return Task.CompletedTask;
            });
        }

        async Task<IEnumerable<Player>> IChartRepository.GetBackups(string league, string team, string position, string name)
        {
            var teamDepthChart = await _db.Chart.Where(dc => dc.League == league && dc.Team == team).FirstOrDefaultAsync();

            var results = teamDepthChart.Chart[position].SkipWhile(x => x.Name == name);

            return results;
        }

        public async Task<Domain.Common.DepthChart> GetFullDepthChart(string league, string team)
        {
            return await _db.Chart.Where(x => x.League == league && x.Team == team).FirstOrDefaultAsync();
        }

        public Task RemovePlayerFromDepthChart(string league, string team, string position, string name)
        {
            return Task.Run(async () =>
            {
                var allCharts = await _db.Chart.ToListAsync();
                var updatedData = allCharts;
                _db.Chart.RemoveRange(allCharts);
                await _db.SaveChangesAsync();

                var teamDepthChart = updatedData.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault();

                var playersInPosition = teamDepthChart.Chart[position].ToList();

                playersInPosition.RemoveAll(p => p.Name == name);

                updatedData.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault().Chart[position] = playersInPosition;

                await _db.Chart.AddRangeAsync(updatedData);
                await _db.SaveChangesAsync();


                return Task.CompletedTask;
            });
        }
    }
}