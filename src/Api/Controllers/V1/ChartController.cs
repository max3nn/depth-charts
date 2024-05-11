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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFullDepthChart(string league, string team)
        {

            var result = await _sender.Send(
                new GetFullDepthChartQuery
                {
                    League = league,
                    Team = team
                });

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{league}/{team}/add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPlayerToDepthChart([FromBody] AddPlayerRequest request, string league, string team)
        {
            var result = await _sender.Send(new AddPlayerToDepthChartCommand
            {
                League = league,
                Team = team,
                Position = request.Position,
                Name = request.Name,
                Depth = request.Depth
            });

            return Ok(result);
        }

        [HttpDelete("{league}/{team}/remove")]
        public async Task<IActionResult> RemovePlayerFromDepthChart([FromBody] RemovePlayerRequest request, string league, string team)
        {
            // Params should be like this: removePlayerFromDepthChart(“WR”, MikeEvans)

            var result = await _sender.Send(new RemovePlayerFromDepthChartCommand
            {
                League = league,
                Team = team,
                Position = request.Position,
                Name = request.Name
            });

            return Ok(result);
        }

        [HttpPost("{league}/{team}/backups")]
        public async Task<IActionResult> GetBackups(GetBackupsRequest request, string league, string team)
        {
            // Params should be like this: getBackups(“QB”, Kyle Trask);

            var result = await _sender.Send(new GetBackupsQuery
            {
                Position = request.Position,
                Name = request.Name,
                League = league,
                Team = team
            });
            return Ok(result);
        }
    }
}
