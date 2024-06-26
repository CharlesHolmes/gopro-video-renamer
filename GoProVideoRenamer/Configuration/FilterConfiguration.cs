﻿using Cocona.Builder;
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
        public static void RegisterAllFilters(ICoconaCommandsBuilder app)
        {
            app.UseFilter(new ParameterLoggingCommandFilterFactory());
        }
    }
}
