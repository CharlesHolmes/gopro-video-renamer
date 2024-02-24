using Cocona;
using GoProVideoRenamer.Directory;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File;
using GoProVideoRenamer.File.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace GoProVideoRenamer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var builder = CoconaApp.CreateBuilder();
            builder.Services.AddTransient<IConsoleWrapper, ConsoleWrapper>();
            builder.Services.AddTransient<IFileSystem, FileSystem>();
            builder.Services.AddTransient<IVideoDirectory, VideoDirectory>();
            builder.Services.AddTransient<IVideoDirectoryFactory, VideoDirectoryFactory>();
            builder.Services.AddTransient<IFileFilter, FileFilter>();
            builder.Services.AddTransient<IFileSort, FileSort>();
            builder.Services.AddTransient<IFileRename, FileRename>();
            var app = builder.Build();
            app.UseFilter(new ParameterLoggingCommandFilterFactory());
            app.AddCommands<RenameCommand>();
            app.Run();
        }
    }
}
