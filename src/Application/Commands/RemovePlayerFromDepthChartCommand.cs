using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using DepthChart.Application;
using DepthChart.Domain.Constants;
using Domain.Common.Players;
using MediatR;

namespace Application.Commands
{
    public class RemovePlayerFromDepthChartCommand : IRequest<bool>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
        public required string Position { get; set; }
        public required string Name { get; set; }
    }

    public class RemovePlayerFromDepthChartCommandHandler : IRequestHandler<AddPlayerToDepthChartCommand, bool>
    {
        public async Task<bool> Handle(AddPlayerToDepthChartCommand command, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
    }
}
