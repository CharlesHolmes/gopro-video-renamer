using Cocona.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.ParameterLogging
{
    public class ParameterLoggingCommandFilterFactory : IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new ParameterLoggingCommandFilter(
                serviceProvider.GetRequiredService<ILogger<ParameterLoggingCommandFilter>>());
        }
    }
}
