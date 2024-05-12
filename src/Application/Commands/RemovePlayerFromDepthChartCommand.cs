using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public class RemovePlayerFromDepthChartCommand : IRequest<bool>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
        public required string Position { get; set; }
        public required string Name { get; set; }
    }

    public class RemovePlayerFromDepthChartCommandHandler : IRequestHandler<RemovePlayerFromDepthChartCommand, bool>
    {
        protected IChartService _chartService;
        protected ILogger<RemovePlayerFromDepthChartCommandHandler> _logger;

        public RemovePlayerFromDepthChartCommandHandler(IChartService chartService, ILogger<RemovePlayerFromDepthChartCommandHandler> logger)
        {
            _chartService = chartService;
            _logger = logger;
        }

        public async Task<bool> Handle(RemovePlayerFromDepthChartCommand command, CancellationToken cancellationToken)
        {

            try
            { 
                await _chartService.RemovePlayerFromDepthChart(command.League, command.Team, command.Position, command.Name);    
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
