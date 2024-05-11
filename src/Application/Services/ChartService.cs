using Application.Interfaces;
using DepthChart.Domain.Contracts;
using Domain.Common.Players;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChart.Application.Services
{
    public class ChartService(IChartRepository _repository, ILogger<ChartService> _logger) : IChartService
    {
        public Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth)
        {
            try
            {
                return _repository.AddPlayerToDepthChart(league, team, position, name, number, depth);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<Player>> GetBackups(string league, string team, string position, string name)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Common.DepthChart> GetFullDepthChart(string league, string team)
        {
            throw new NotImplementedException();
        }

        public Task RemovePlayerFromDepthChart(string league, string team, string position, string name)
        {
            throw new NotImplementedException();
        }
    }
}
