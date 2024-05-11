using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DepthChart.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof().Module.Assembly)); // TODO for each assembly
        }
    }
}
