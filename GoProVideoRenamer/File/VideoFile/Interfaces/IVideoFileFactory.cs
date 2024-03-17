using System.IO.Abstractions;

namespace GoProVideoRenamer.File.VideoFile.Interfaces
{
    public interface IVideoFileFactory
    {
        IVideoFile Create(IFileInfo fileInfo);
    }
}