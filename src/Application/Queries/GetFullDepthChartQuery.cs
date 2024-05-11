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
    public class GetFullDepthChartQuery : IRequest<DepthChartDTO>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
    }

    public class GetFullDepthChartQueryHandler : IRequestHandler<GetFullDepthChartQuery, DepthChartDTO>
    {
        public async Task<DepthChartDTO> Handle(GetFullDepthChartQuery query, CancellationToken cancellationToken)
        {
            var depthChart =
            await Task.FromResult(new DepthChartDTO
            {
                Chart = new Dictionary<string, IEnumerable<Player>>(){
                    {"QB", new List<Player>(){new Player("Tom Brady", 12)}},
                    {"RB", new List<Player>(){new Player("Ronald Jones II", 27)}},
                    {"WR", new List<Player>(){new Player("Mike Evans", 13)}},
                    {"TE", new List<Player>(){new Player("Rob Gronkowski", 87)}},
                    {"OL", new List<Player>(){new Player("Ali Marpet", 74)}},
                    {"DL", new List<Player>(){new Player("Ndamukong Suh", 93)}},
                    {"LB", new List<Player>(){new Player("Lavonte David", 54)}},
                    {"CB", new List<Player>(){new Player("Carlton Davis", 24)}},
                    {"S", new List<Player>(){new Player("Jordan Whitehead", 33)}}
                }
            });

            return depthChart;
        }
    }
}