using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using DepthChart.Application;
using DepthChart.Domain.Constants;
using Domain.Common.Players;
using MediatR;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
