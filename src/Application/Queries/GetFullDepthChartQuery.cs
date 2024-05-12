using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries
{
    public class GetFullDepthChartQuery : IRequest<DepthChart.Domain.Common.DepthChart?>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
    }

    public class GetFullDepthChartQueryHandler : IRequestHandler<GetFullDepthChartQuery, DepthChart.Domain.Common.DepthChart?>
    {
        protected IChartService _chartService;
        protected ILogger<GetFullDepthChartQueryHandler> _logger;

        public GetFullDepthChartQueryHandler(IChartService ChartService, ILogger<GetFullDepthChartQueryHandler> logger)
        {
            _chartService = ChartService;
            _logger = logger;
        }

        public async Task<DepthChart.Domain.Common.DepthChart?> Handle(GetFullDepthChartQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _chartService.GetFullDepthChart(query.League, query.Team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting {} full depth chart for {}", query.League, query.Team);
                return null;
            }
        }
    }
}