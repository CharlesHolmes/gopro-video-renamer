using GoProVideoRenamer.File.VideoFile.Numbered;
using GoProVideoRenamer.File.VideoFile.Numbered.Interfaces;
using GoProVideoRenamer.File.VideoFile.Renamed.Interfaces;

namespace GoProVideoRenamer.File.VideoFile.Renamed
{
    public class RenamedVideoFile : NumberedVideoFile, IRenamedVideoFile
    {
        public string NewName { get; private init; }

        public RenamedVideoFile(string newName, INumberedVideoFile file) : base(file)
        {
            NewName = newName;
        }

        public void CommitRenameToDisk()
        {
            FileInfo.MoveTo($"{FileInfo.Directory!.FullName}\\{NewName}");
        }
    }
}
