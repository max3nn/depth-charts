namespace DepthChart.Domain.Common
{
    public class DepthChart
    {
        public required string League { get; init; }

        public required string Team { get; init; }

        public Dictionary<string, IEnumerable<Player>> Chart { get; set; } = new();
    }
}
