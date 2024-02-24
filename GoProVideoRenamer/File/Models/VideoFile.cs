using System.IO.Abstractions;

namespace GoProVideoRenamer.File.Models
{
    public class VideoFile
    {
        protected readonly IFileInfo _fileInfo;

        public int FileNumber
        {
            get
            {
                return int.Parse(_fileInfo.Name.Substring(4, 4));
            }
        }

        public int ChapterNumber
        {
            get
            {
                return int.Parse(_fileInfo.Name.Substring(2, 2));
            }
        }

        public string Name => _fileInfo.Name;

        public string FileExtension => _fileInfo.Extension;

        public VideoFile(IFileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public VideoFile(VideoFile other)
        {
            _fileInfo = other._fileInfo;
        }
    }
}
