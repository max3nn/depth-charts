using Application.Commands;
using Application.Queries;
using DepthChart.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    [ControllerName("chart")]
    public class ChartController : ControllerBase
    {
        private readonly ILogger<ChartController> _logger;
        private readonly ISender _sender;

        public ChartController(ILogger<ChartController> logger, ISender sender)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpGet("{league}/{team}")]
        [Produces("application/json")]
        public async Task<IResult> GetFullDepthChart(string league, string team)
        {

            var results = await _sender.Send(
                new GetFullDepthChartQuery
                {
                    League = league,
                    Team = team
                });

            return results is not null ? Results.Ok(results) : Results.NotFound();
        }

        [HttpPost("{league}/{team}/add")]
        [Produces("application/json")]
        public async Task<IResult> AddPlayerToDepthChart([FromBody] AddPlayerRequest request, string league, string team)
        {
            var results = await _sender.Send(new AddPlayerToDepthChartCommand
            {
                League = league,
                Team = team,
                Position = request.Position,
                Name = request.Name,
                Depth = request.Depth,
                Number = request.Number
            });

            return results ? Results.NoContent() : Results.Problem();
        }

        [HttpDelete("{league}/{team}/remove")]
        [Produces("application/json")]
        public async Task<IResult> RemovePlayerFromDepthChart([FromBody] RemovePlayerRequest request, string league, string team)
        {
            var player = await _sender.Send(new RemovePlayerFromDepthChartCommand
            {
                League = league,
                Team = team,
                Position = request.Position,
                Name = request.Name
            });

            return player is not null ? Results.Ok(player) : Results.Problem();
        }

        [HttpPost("{league}/{team}/backups")]
        [Produces("application/json")]

        public async Task<IResult> GetBackups(GetBackupsRequest request, string league, string team)
        {
            var result = await _sender.Send(
                    new GetBackupsQuery
                    {
                        Position = request.Position,
                        Name = request.Name,
                        League = league,
                        Team = team
                    });

            return result is not null ? Results.Ok(result) : Results.Problem();
        }
    }
}
