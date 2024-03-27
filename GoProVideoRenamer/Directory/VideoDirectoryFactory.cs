using GoProVideoRenamer.Directory.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;

namespace GoProVideoRenamer.Directory
{
    public class VideoDirectoryFactory : IVideoDirectoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public VideoDirectoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IVideoDirectory Create(string directoryPath)
        {
            return new VideoDirectory(
                _serviceProvider.GetRequiredService<ILogger<VideoDirectory>>(),
                _serviceProvider.GetRequiredService<IFileSystem>(),
                _serviceProvider.GetRequiredService<IVideoFileFactory>(),
                directoryPath);
        }
    }
}
