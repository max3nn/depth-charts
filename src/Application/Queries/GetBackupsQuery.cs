using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using DepthChart.Application;
using DepthChart.Domain.Constants;
using Domain.Common.Players;
using MediatR;

namespace Application.Queries
{
    public class GetBackupsQuery : IRequest<IEnumerable<Player>>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
        public required string Position { get; set; }
        public required string Name { get; set; }
    }

    public class GetBackupsQueryHandler : IRequestHandler<GetBackupsQuery, IEnumerable<Player>>
    {
        public async Task<IEnumerable<Player>> Handle(GetBackupsQuery query, CancellationToken cancellationToken)
        {
            var depthChart =
            await Task.FromResult(new List<Player>
            {
                new Player("Player 1", 12),
                new Player("Player 2", 35),
                new Player("Player 3", 8)
            });

            return depthChart;
        }
    }
}