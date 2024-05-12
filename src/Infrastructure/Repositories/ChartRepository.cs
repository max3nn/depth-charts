using DepthChart.Domain.Constants;
using DepthChart.Domain.Contracts;
using DepthChart.Infrastructure.Data;
using Domain.Common.Players;
using Domain.Common.PlayersPositionsDict;
using Microsoft.EntityFrameworkCore;

namespace DepthChart.Infrastructure.Repositories
{
    public class ChartRepository(ApplicationDbContext _db) : IChartRepository
    {
        public Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth)
        {

            //return Task.Run(async () =>
            //{
            //    var newPlayer = new Player(name, number);

            //    var entityToUpdate = await _db.Chart
            //        .Where(x => x.League == league && x.Team == team)
            //        .Select(x => x.Chart.Where( position].ToList())
            //        .FirstOrDefaultAsync();

            //    // Most likely will exist, so this should be the fastest way.
            //    if (entityToUpdate is not null)
            //    {
            //        entityToUpdate.Insert(depth, newPlayer);

            //        // This is an Assumption based on research
            //        entityToUpdate = entityToUpdate.Take(5).ToList();

            //        _db.Update(entityToUpdate);
            //        await _db.SaveChangesAsync();
            //        return;
            //    }


            //    entityToUpdate = new List<Player>();

            //    // TODO: Add Support for Position doesn't exist yet.
            //    var chart = _db.Chart.Where(x => x.League == league && x.Team == team).FirstOrDefault();
            //    if (chart is null)
            //    {
            //        // TODO: Add Support for League and Team doesn't exist yet.
            //    }

            //    await _db.SaveChangesAsync();
            //    return;
            //});

            return Task.CompletedTask;
        }

        Task<IEnumerable<Player>> IChartRepository.GetBackups(string league, string team, string position, string name)
        {
            //return Task.FromResult(_db.Chart
            //    .Where(x => x.League == league && x.Team == team)
            //    .Select(x => x.Chart._items[position])
            //    .SkipWhile(x => x.Any(p => p.Name != name))
            //    .Skip(1));
            
            throw new System.NotImplementedException();
        }

        public async Task<Domain.Common.DepthChart> GetFullDepthChart(string league, string team)
        {
            return await _db.Chart.Where(x => x.League == league && x.Team == team).FirstOrDefaultAsync();
        }

        public Task RemovePlayerFromDepthChart(string league, string team, string position, string name)
        {
            //return Task.Run(async () =>
            //{
            //    var playerToRemove = _db.Chart
            //        .Where(x => x.League == league && x.Team == team)
            //        .Select(x => x.Chart._items[position])
            //        .SelectMany(x => x.Where(p => p.Name == name));

            //    _db.Remove(playerToRemove);

            //    await _db.SaveChangesAsync();
            //    return Task.CompletedTask;
            //});

            return Task.CompletedTask;
        }
    }
}