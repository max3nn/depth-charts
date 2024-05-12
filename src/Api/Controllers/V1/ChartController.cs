using Application.Commands;
using Application.Queries;
using DepthChart.Api.Models;
using DepthChart.Domain.Constants;
using Domain.NFL;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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

            return Results.Ok(
                await _sender.Send(
                new GetFullDepthChartQuery
                {
                    League = league,
                    Team = team
                })
            );
        }

        [HttpPost("{league}/{team}/add")]
        [Produces("application/json")]
        public async Task<IResult> AddPlayerToDepthChart([FromBody] AddPlayerRequest request, string league, string team)
        {
            await _sender.Send(new AddPlayerToDepthChartCommand
            {
                League = league,
                Team = team,
                Position = request.Position,
                Name = request.Name,
                Depth = request.Depth,
                Number = request.Number
            });

            return Results.NoContent();
        }

        [HttpDelete("{league}/{team}/remove")]
        [Produces("application/json")]
        public async Task<IResult> RemovePlayerFromDepthChart([FromBody] RemovePlayerRequest request, string league, string team)
        {
            await _sender.Send(new RemovePlayerFromDepthChartCommand
            {
                League = league,
                Team = team,
                Position = request.Position,
                Name = request.Name
            });

            return Results.NoContent();
        }

        [HttpPost("{league}/{team}/backups")]
        [Produces("application/json")]

        public async Task<IResult> GetBackups(GetBackupsRequest request, string league, string team)
        {
            return Results.Ok(
                await _sender.Send(
                    new GetBackupsQuery{
                    Position = request.Position,
                    Name = request.Name,
                    League = league,
                    Team = team
                    })
                );
        }
    }
}
