using GoProVideoRenamer.File.VideoFiles.Interfaces;

namespace GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces
{
    public interface INumberedVideoFile : IVideoFile
    {
        int NewIndex { get; }
    }
}