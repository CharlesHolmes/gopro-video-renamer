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
        public static Func<ICoconaCommandsBuilder, Type, CommandTypeConventionBuilder> AddCommand = CommandsBuilderExtensions.AddCommands;

        public static void RegisterAllCommands(ICoconaCommandsBuilder app)
        {
            AddCommand(app, typeof(RenameCommand));
        }
    }
}
