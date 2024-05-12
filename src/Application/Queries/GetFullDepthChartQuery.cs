using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetFullDepthChartQuery : IRequest<DepthChart.Domain.Common.DepthChart>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
    }

    public class GetFullDepthChartQueryHandler : IRequestHandler<GetFullDepthChartQuery, DepthChart.Domain.Common.DepthChart>
    {
        protected IChartService _chartService;

        public GetFullDepthChartQueryHandler(IChartService ChartService)
        {
            _chartService = ChartService;
        }

        public async Task<DepthChart.Domain.Common.DepthChart> Handle(GetFullDepthChartQuery query, CancellationToken cancellationToken)
        {
            var results = await _chartService.GetFullDepthChart(query.League, query.Team);

            return results;
        }
    }
}