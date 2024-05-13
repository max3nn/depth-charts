using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using DepthChart.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public class RemovePlayerFromDepthChartCommand : IRequest<Player?>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
        public required string Position { get; set; }
        public required string Name { get; set; }
    }

    public class RemovePlayerFromDepthChartCommandHandler : IRequestHandler<RemovePlayerFromDepthChartCommand, Player?>
    {
        protected IChartService _chartService;
        protected ILogger<RemovePlayerFromDepthChartCommandHandler> _logger;

        public RemovePlayerFromDepthChartCommandHandler(IChartService chartService, ILogger<RemovePlayerFromDepthChartCommandHandler> logger)
        {
            _chartService = chartService;
            _logger = logger;
        }

        public async Task<Player?> Handle(RemovePlayerFromDepthChartCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _chartService.RemovePlayerFromDepthChart(command.League, command.Team, command.Position, command.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing {Name} from {Team} in position {Qb}", command.Name, command.Team, command.Position);
                return null;
            }
        }
    }
}
