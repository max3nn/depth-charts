using Domain.Common.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChart.Domain.Contracts
{
    public interface IChartRepository
    {
        Task<IQueryable<Common.DepthChart>> GetFullDepthChart(string league, string team);
        Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth);
        Task RemovePlayerFromDepthChart(string league, string team, string position, string name);
        Task<IQueryable<IEnumerable<Player>>> GetBackups(string league, string team, string position, string name);
    }
}
