using GoProVideoRenamer.ConsoleWrapping;
using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.Directory;
using GoProVideoRenamer.File.Interfaces;
using GoProVideoRenamer.File;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Numbered;
using GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Renamed;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using GoProVideoRenamer.File.VideoFiles;

namespace GoProVideoRenamer.Configuration
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection collection)
        {
            collection
                .AddTransient<IConsoleWrapper, ConsoleWrapper>()
                .AddTransient<IFileSystem, FileSystem>()
                .AddTransient<IVideoDirectoryFactory, VideoDirectoryFactory>()
                .AddTransient<IVideoFileFactory, VideoFileFactory>()
                .AddTransient<INumberedVideoFileFactory, NumberedVideoFileFactory>()
                .AddTransient<IRenamedVideoFileFactory, RenamedVideoFileFactory>()
                .AddTransient<IFileFilter, FileFilter>()
                .AddTransient<IFileSort, FileSort>()
                .AddTransient<IFileRename, FileRename>();
        }
    }
}
