using System.IO.Abstractions;
using GoProVideoRenamer.File.VideoFile.Interfaces;

namespace GoProVideoRenamer.File.VideoFile
{
    public class VideoFile : IVideoFile
    {
        public IFileInfo FileInfo { get; private init; }

        public int FileNumber
        {
            get
            {
                return int.Parse(FileInfo.Name.Substring(4, 4));
            }
        }

        public int ChapterNumber
        {
            get
            {
                return int.Parse(FileInfo.Name.Substring(2, 2));
            }
        }

        public string Name => FileInfo.Name;

        public string FileExtension => FileInfo.Extension;

        public VideoFile(IFileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public VideoFile(IVideoFile other)
        {
            FileInfo = other.FileInfo;
        }
    }
}
