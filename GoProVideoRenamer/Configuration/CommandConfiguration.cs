using Cocona.Builder;
using Cocona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.Configuration
{
    public static class CommandConfiguration
    {
        public static void RegisterAllCommands(ICoconaCommandsBuilder app)
        {
            app.AddCommands<RenameCommand>();
        }
    }
}
