using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public class AddPlayerToDepthChartCommand : IRequest<bool>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
        public required string Position { get; set; }
        public required string Name { get; set; }
        public required int Depth { get; set; }
        public required int Number { get; set; }
    }

    public class AddPlayerToDepthChartCommandHandler : IRequestHandler<AddPlayerToDepthChartCommand, bool>
    {
        protected IChartService _chartService;
        protected ILogger<AddPlayerToDepthChartCommandHandler> _logger;

        public AddPlayerToDepthChartCommandHandler(IChartService chartService, ILogger<AddPlayerToDepthChartCommandHandler> logger)
        {
            _chartService = chartService;
            _logger = logger;
        }

        public async Task<bool> Handle(AddPlayerToDepthChartCommand command, CancellationToken cancellationToken)
        {

            try
            {
                await _chartService.AddPlayerToDepthChart(command.League, command.Team, command.Position, command.Name, command.Number, command.Depth);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Adding {} to {} in position {} at depth ", command.Name, command.Team, command.Position, command.Depth);
                return false;
            }

        }
    }
}
