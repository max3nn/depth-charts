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
        public async Task<IActionResult> GetFullDepthChart(string league, string team)
        {
            // var result = await _sender.Send(
            //     new GetFullDepthChartQuery
            //     {
            //         League = league,
            //         Team = team
            //     });

            return Ok();
        }

        [HttpPost("{league}/{team}/add")]
        public async Task<IActionResult> AddPlayerToDepthChart([FromBody] AddPlayerRequest request)
        {
            // Params should be like this: addPlayerToDepthChart(“LWR”, MikeEvans, 0);

            // var result = await _sender.Send(new AddPlayerToDepthChartCommand(request));
            return Ok();
        }

        [HttpDelete("{league}/{team}/remove")]
        public async Task<IActionResult> RemovePlayerFromDepthChart([FromBody] RemovePlayerRequest request)
        {
            // Params should be like this: removePlayerFromDepthChart(“WR”, MikeEvans)

            // var result = await _sender.Send(new RemovePlayerFromDepthChartCommand(request));
            return Ok();
        }

        [HttpGet("backups")]
        public async Task<IActionResult> GetBackups(GetBackupsRequest request)
        {
            // Params should be like this: getBackups(“QB”, Kyle Trask);

            // var result = await _sender.Send(new GetBackupsQuery(request));
            return Ok();
        }
    }
}
