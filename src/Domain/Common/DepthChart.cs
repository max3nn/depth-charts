using Domain.Common.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class DepthChart // Chart need to be geneirc to support all sporting Leagues and their positions
    {
        public Dictionary<string, IEnumerable<Player>> _items = new Dictionary<string, IEnumerable<Player>>();
    }
}
