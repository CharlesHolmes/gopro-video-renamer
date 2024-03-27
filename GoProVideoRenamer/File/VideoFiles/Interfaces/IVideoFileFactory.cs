using System.IO.Abstractions;

namespace GoProVideoRenamer.File.VideoFiles.Interfaces
{
    public interface IVideoFileFactory
    {
        IVideoFile Create(IFileInfo fileInfo);
    }
}