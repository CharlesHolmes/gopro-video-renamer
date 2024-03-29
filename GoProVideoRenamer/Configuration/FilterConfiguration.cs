using Cocona.Builder;
using Cocona.Filters;
using Cocona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoProVideoRenamer.ParameterLogging;

namespace GoProVideoRenamer.Configuration
{
    public static class FilterConfiguration
    {
        public static Func<ICoconaCommandsBuilder, IFilterFactory, ICoconaCommandsBuilder> RegisterFilter = CommandsBuilderExtensions.UseFilter;

        public static void RegisterAllFilters(ICoconaCommandsBuilder app)
        {
            RegisterFilter(app, new ParameterLoggingCommandFilterFactory());
        }
    }
}
