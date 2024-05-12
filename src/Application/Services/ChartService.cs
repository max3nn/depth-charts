using Application.Interfaces;
using DepthChart.Domain;
using DepthChart.Domain.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepthChart.Application.Services
{
    public class ChartService(IChartRepository _repository, ILogger<ChartService> _logger) : IChartService
    {
        public async Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth)
        {
            try
            {
                await _repository.AddPlayerToDepthChart(league, team, position, name, number, depth);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<Player>> GetBackups(string league, string team, string position, string name)
        {
            try
            {
                return _repository.GetBackups(league, team, position, name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Domain.Common.DepthChart> GetFullDepthChart(string league, string team)
        {
            try
            {
                return await _repository.GetFullDepthChart(league, team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task RemovePlayerFromDepthChart(string league, string team, string position, string name)
        {
            try
            {
                await _repository.RemovePlayerFromDepthChart(league, team, position, name);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
