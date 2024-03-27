using GoProVideoRenamer.File.VideoFiles;
using GoProVideoRenamer.File.VideoFiles.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.UnitTests.File
{
    internal class IVideoFileMocking
    {
        public static IVideoFile GetMockedIVideoFile(string fileName)
        {
            var result = new Mock<IVideoFile>();
            result.Setup(m => m.Name).Returns(fileName);
            return result.Object;
        }

        public static IVideoFile GetIVideoFileWithMockedIFileInfo(string fileName)
        {
            var fileInfo = new Mock<IFileInfo>();
            fileInfo.Setup(m => m.Name).Returns(fileName);
            return new VideoFile(fileInfo.Object);
        }
    }
}
