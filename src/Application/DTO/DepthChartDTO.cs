
using System.Collections.Generic;
using Domain.Common.Players;

namespace Application.DTO
{
    public class DepthChartDTO
    {
        public required Dictionary<string, IEnumerable<Player>> Chart { get; set; }
    }
}