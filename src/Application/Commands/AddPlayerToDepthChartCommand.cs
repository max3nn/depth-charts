using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public class AddPlayerToDepthChartCommand : IRequest
    {
        public required string League { get; set; }
        public required string Team { get; set; }
        public required string Position { get; set; }
        public required string Name { get; set; }
        public required int Depth { get; set; }
        public required int Number { get; set; }
    }

    public class AddPlayerToDepthChartCommandHandler : IRequestHandler<AddPlayerToDepthChartCommand>
    {
        protected IChartService _chartService;
        protected ILogger<AddPlayerToDepthChartCommandHandler> _logger;

        public AddPlayerToDepthChartCommandHandler(IChartService chartService, ILogger<AddPlayerToDepthChartCommandHandler> logger)
        {
            _chartService = chartService;
            _logger = logger;
        }

        public async Task Handle(AddPlayerToDepthChartCommand command, CancellationToken cancellationToken)
        {

            await _chartService.AddPlayerToDepthChart(command.League, command.Team, command.Position, command.Name, command.Number, command.Depth);
            return;

        }
    }
}
