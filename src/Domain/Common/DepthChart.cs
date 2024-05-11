using DepthChart.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class DepthChart<T>
    {

        public Guid Id { get; init; }

        public League league { get; private set; } = League.NFL;

        public string Code { get; private set; }

        public string Name { get; private set; }
    }
}
