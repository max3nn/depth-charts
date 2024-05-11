using DepthChart.Domain.Constants;
using Domain.Common.Players;
using Domain.Common.PlayersPositionsDict;
using Domain.NFL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChart.Domain.Common
{
    public class DepthChart
    {
        public required string League { get; init; }

        public required string Team { get; init; }

        public Dictionary<string, IEnumerable<Player>> Chart { get; set; } = new();
    }
}
