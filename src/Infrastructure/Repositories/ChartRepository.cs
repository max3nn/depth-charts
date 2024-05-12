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

            //return Task.Run(async () =>
            //{
            //    var newPlayer = new Player(name, number);

            //    var entityToUpdate = await _db.Chart
            //        .Where(x => x.League == league && x.Team == team)
            //        .Select(x => x.Chart)
            //        .Select(x => x[position])
            //        .FirstOrDefaultAsync();

            //    entityToUpdate.ToList();
            //    entityToUpdate.



            //    // Most likely will exist, so this should be the fastest way.
            //    if (entityToUpdate is not null)
            //    {
            //        var EntityUpdatedList = entityToUpdate;
            //        EntityUpdatedList.ToList();
            //        EntityUpdatedList.Insert(depth, newPlayer);
            //        EntityUpdatedList.Take(5);

            //        entityToUpdate.ToList().A ;

            //        _db.Update(EntityUpdatedList);
            //        await _db.SaveChangesAsync();
            //        return;
            //    }


            //TODO: Support Empty | null Positions => Assume validation is done in the service layer.

            return Task.CompletedTask;
            //});
        }

        async Task<IEnumerable<Player>> IChartRepository.GetBackups(string league, string team, string position, string name)
        {
            var result = await _db.Chart
                .Where(x => x.League == league && x.Team == team)
                .Select(x => x.Chart)
                .Select(x => x[position])
                .Select(x => x.SkipWhile(x => x.Name == name))
                .SelectMany(x => x)
                .ToListAsync();

            return result;
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

                var individualDepthChart = updatedData.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault();

                var playersInPosition = individualDepthChart.Chart[position].ToList();
                playersInPosition.RemoveAll(p => p.Name == name);

                updatedData.Where(dc => dc.League == league && dc.Team == team).FirstOrDefault().Chart[position] = playersInPosition;

                await _db.Chart.AddRangeAsync(updatedData);
                await _db.SaveChangesAsync();


                return Task.CompletedTask;
            });
        }
    }
}