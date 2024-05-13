using Application.Interfaces;
using DepthChart.Domain;
using DepthChart.Domain.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepthChart.Application.Services
{
    public class ChartService(IChartRepository _repository, ILogger<ChartService> _logger) : IChartService
    {
        public async Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int positionDepth)
        {
            await _repository.AddPlayerToDepthChart(league, team, position, name, number, positionDepth);
        }

        public Task<IEnumerable<Player>> GetBackups(string league, string team, string position, string name)
        {
            return _repository.GetBackups(league, team, position, name);
        }

        public async Task<Domain.Common.DepthChart> GetFullDepthChart(string league, string team)
        {
            return await _repository.GetFullDepthChart(league, team);
        }

        public async Task<Player> RemovePlayerFromDepthChart(string league, string team, string position, string name)
        {
            return await _repository.RemovePlayerFromDepthChart(league, team, position, name);
        }
    }
}
