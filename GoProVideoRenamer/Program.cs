using Cocona;
using Cocona.Builder;
using Cocona.Filters;
using GoProVideoRenamer.Configuration;
using GoProVideoRenamer.ConsoleWrapping;
using GoProVideoRenamer.Directory;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.ParameterLogging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;

namespace GoProVideoRenamer
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        static void Main(string[] args)
        {
            var builder = CoconaApp.CreateBuilder();
            ServiceConfiguration.Configure(builder.Services);
            var app = builder.Build();
            FilterConfiguration.RegisterAllFilters(app);
            CommandConfiguration.RegisterAllCommands(app);
            app.Run();
        }
    }
}
