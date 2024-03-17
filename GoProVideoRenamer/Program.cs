using Cocona;
using GoProVideoRenamer.ConsoleWrapping;
using GoProVideoRenamer.Directory;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.ParameterLogging;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace GoProVideoRenamer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var builder = CoconaApp.CreateBuilder();
            builder.Services
                .AddTransient<IConsoleWrapper, ConsoleWrapper>()
                .AddTransient<IFileSystem, FileSystem>()
                .AddTransient<IVideoDirectory, VideoDirectory>()
                .AddTransient<IVideoDirectoryFactory, VideoDirectoryFactory>()
                .AddTransient<IFileFilter, FileFilter>()
                .AddTransient<IFileSort, FileSort>()
                .AddTransient<IFileRename, FileRename>();
            var app = builder.Build();
            app.UseFilter(new ParameterLoggingCommandFilterFactory());
            app.AddCommands<RenameCommand>();
            app.Run();
        }
    }
}
