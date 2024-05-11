using Domain.Common.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.PlayersPositionsDict
{
    /// <summary>
    ///  I think this can be removed....
    /// </summary>
    public class PlayersPositionsDict
    {
        public Dictionary<string, IEnumerable<Player>> _items { get; set; } = new();
    }
}
