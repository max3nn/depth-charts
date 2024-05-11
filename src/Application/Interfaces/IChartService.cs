using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common.Players;

namespace Application.Interfaces
{
    public interface IChartService
    {
        Task<Domain.Common.DepthChart> GetFullDepthChart(string league, string team);
        Task AddPlayerToDepthChart(string positions, string name, int depth);
        Task RemovePlayerFromDepthChart(string positions, string name);
        Task<IEnumerable<Player>> GetBackups(string positions, string name);
    }
}