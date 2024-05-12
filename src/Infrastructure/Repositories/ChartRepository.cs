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
            // IMPORTANT NOTES: This is not following best practice and it done this way for simplicity.
            // The InMemory DbContext doesn't quite work the same as an SQL database.

            return Task.Run(async () =>
            {
                var allDepthCharts = await _db.Chart.ToListAsync();

                var teamDepthChart = allDepthCharts.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault();

                if (teamDepthChart is null) throw new Exception("Chart not found.");

                var playersInPosition = teamDepthChart.Chart[position].ToList(); // ToList returns a new Reference, so we can modify it and then reassign it later.

                playersInPosition.Insert(depth, new Player(name, number));
                playersInPosition.Take(5); // TODO: This should be removed and controlled in the Domain/Application layer.

                allDepthCharts.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault().Chart[position] = playersInPosition;

                _db.Chart.UpdateRange(allDepthCharts);

                await _db.SaveChangesAsync();

                return Task.CompletedTask;
            });
        }

        async Task<IEnumerable<Player>> IChartRepository.GetBackups(string league, string team, string position, string name)
        {
            var teamDepthChart = await _db.Chart.Where(dc => dc.League.ToUpper() == league.ToUpper() && dc.Team.ToUpper() == team.ToUpper()).FirstOrDefaultAsync();

            if (teamDepthChart is null) throw new Exception("Chart not found.");

            var results = teamDepthChart.Chart[position].SkipWhile(x => x.Name.ToUpper() == name.ToUpper());

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
                // This will force enumeration upfront, so we can start modifying the resulting List.
                var allDepthCharts = await _db.Chart.ToListAsync();

                var teamDepthChart = allDepthCharts.Where(dc => dc.League.ToUpper() == league.ToUpper() && dc.Team.ToUpper() == team.ToUpper()).FirstOrDefault();

                var playersInPosition = teamDepthChart.Chart[position].ToList(); // ToList returns a new Reference, so we can modify it and then reassign it later.
                playersInPosition.RemoveAll(p => p.Name.ToUpper() == name.ToUpper());

                allDepthCharts.Where(dc => dc.League.ToUpper() == league.ToUpper() && dc.Team.ToUpper() == team.ToUpper()).FirstOrDefault().Chart[position] = playersInPosition;

                _db.Chart.UpdateRange(allDepthCharts);

                await _db.SaveChangesAsync();

                return Task.CompletedTask;
            });
        }
    }
}