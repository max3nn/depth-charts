using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Interfaces;
using DepthChart.Application.Services;
using DepthChart.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Queries
{
    public class GetBackupsQuery : IRequest<IEnumerable<Player>?>
    {
        public required string League { get; set; }
        public required string Team { get; set; }
        public required string Position { get; set; }
        public required string Name { get; set; }
    }

    public class GetBackupsQueryHandler : IRequestHandler<GetBackupsQuery, IEnumerable<Player>?>
    {
        protected readonly IChartService _service;
        protected ILogger<GetBackupsQueryHandler> _logger;

        public GetBackupsQueryHandler(IChartService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Player>?> Handle(GetBackupsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.GetBackups(query.League, query.Team, query.Position, query.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Backups from the {} in position {}", query.Team, query.Position);
                return null;
            }
        }
    }
}