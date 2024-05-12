using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using DepthChart.Domain;
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
        protected readonly IChartService _service;

        public GetBackupsQueryHandler(IChartService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Player>> Handle(GetBackupsQuery query, CancellationToken cancellationToken)
        {
            return await _service.GetBackups(query.League, query.Team, query.Position, query.Name);
        }
    }
}