using GoProVideoRenamer.File.VideoFiles.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFiles.Renamed.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProVideoRenamer.File.VideoFiles.Renamed
{
    public class RenamedVideoFileFactory : IRenamedVideoFileFactory
    {
        public IRenamedVideoFile Create(string newName, INumberedVideoFile file) => new RenamedVideoFile(newName, file);
    }
}
