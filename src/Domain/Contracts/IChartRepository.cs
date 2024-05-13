namespace DepthChart.Domain.Contracts
{
    public interface IChartRepository
    {
        Task<Common.DepthChart> GetFullDepthChart(string league, string team);
        Task AddPlayerToDepthChart(string league, string team, string position, string name, int number, int depth);
        Task<Player> RemovePlayerFromDepthChart(string league, string team, string position, string name);
        Task<IEnumerable<Player>> GetBackups(string league, string team, string position, string name);
    }
}
