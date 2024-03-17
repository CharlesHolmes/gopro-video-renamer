using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoProVideoRenamer.File.VideoFile.Interfaces;

namespace GoProVideoRenamer.File.VideoFile
{
    public class VideoFileFactory : IVideoFileFactory
    {
        public IVideoFile Create(IFileInfo fileInfo) => new VideoFile(fileInfo);
    }
}
