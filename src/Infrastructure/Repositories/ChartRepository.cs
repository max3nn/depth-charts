using DepthChart.Domain;
using DepthChart.Domain.Contracts;
using DepthChart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DepthChart.Infrastructure.Repositories
{
    public class ChartRepository(ApplicationDbContext _db) : IChartRepository
    {
        public async Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth)
        {
            // IMPORTANT NOTES: This is not following best practice and it done this way for simplicity.
            // The InMemory DbContext doesn't quite work the same as an SQL database.


            var allDepthCharts = await _db.Chart.ToListAsync();

            var teamDepthChart = allDepthCharts.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault();

            if (teamDepthChart is null) throw new Exception("Chart not found.");

            IEnumerable<Player> playersInPosition;

            teamDepthChart.Chart.TryGetValue(position, out playersInPosition);

            if (playersInPosition is not null)
            {
                List<Player> playersInPositionList = playersInPosition.ToList();
                playersInPositionList.Insert(depth, new(name, number));

                allDepthCharts.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault().Chart[position] = playersInPositionList.Take(5);
            }
            else
            {
                playersInPosition = new List<Player> { new(name, number) };
                allDepthCharts.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault().Chart[position] = playersInPosition;
            }

            _db.Chart.UpdateRange(allDepthCharts);
            await _db.SaveChangesAsync();
            return;
        }

        async Task<IEnumerable<Player>> IChartRepository.GetBackups(string league, string team, string position, string name)
        {
            var teamDepthChart = await _db.Chart.Where(dc => dc.League.ToUpper() == league.ToUpper() && dc.Team.ToUpper() == team.ToUpper()).FirstOrDefaultAsync();

            if (teamDepthChart is null) throw new Exception("Chart not found.");

            var results = teamDepthChart.Chart[position].SkipWhile(x => x.Name.ToUpper() != name.ToUpper()).Skip(1);

            return results;
        }

        public async Task<Domain.Common.DepthChart> GetFullDepthChart(string league, string team)
        {
            return await _db.Chart.Where(x => x.League == league && x.Team == team).FirstOrDefaultAsync() ?? throw new Exception("Chart not found.");
        }

        public Task<Player> RemovePlayerFromDepthChart(string league, string team, string position, string name)
        {
            return Task.Run(async () =>
            {
                // This will force enumeration upfront, so we can start modifying the resulting List.
                var allDepthCharts = await _db.Chart.ToListAsync();

                var teamDepthChart = allDepthCharts.Where(dc => dc.League.ToUpper() == league.ToUpper() && dc.Team.ToUpper() == team.ToUpper()).FirstOrDefault();

                if (teamDepthChart is null) throw new Exception("Chart not found.");

                var playersInPosition = teamDepthChart.Chart[position].ToList(); // ToList returns a new Reference, so we can modify it and then reassign it later.
                Player playerToRemove = playersInPosition.Where(p => p.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
                if (playerToRemove is null) throw new Exception("Player not found.");

                playersInPosition.Remove(playerToRemove);

                allDepthCharts.Where(dc => dc.League.ToUpper() == league.ToUpper() && dc.Team.ToUpper() == team.ToUpper()).FirstOrDefault().Chart[position] = playersInPosition;

                _db.Chart.UpdateRange(allDepthCharts);

                await _db.SaveChangesAsync();

                return playerToRemove;
            });
        }
    }
}