using System.IO.Abstractions;

namespace GoProVideoRenamer.File.VideoFiles.Interfaces
{
    public interface IVideoFile
    {
        int ChapterNumber { get; }
        string FileExtension { get; }
        int FileNumber { get; }
        string Name { get; }
        IFileInfo FileInfo { get; }
    }
}