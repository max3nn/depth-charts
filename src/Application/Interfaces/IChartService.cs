using System.Collections.Generic;
using System.Threading.Tasks;
using DepthChart.Domain.Common;
using Domain.Common.Players;

namespace Application.Interfaces
{
    public interface IChartService
    {
        Task<DepthChart.Domain.Common.DepthChart> GetFullDepthChart(string league, string team);
        Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth);
        Task RemovePlayerFromDepthChart(string league, string team, string position, string name);
        Task<IEnumerable<Player>> GetBackups(string league, string team, string position, string name);
    }
}