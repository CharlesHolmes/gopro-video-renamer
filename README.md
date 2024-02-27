# gopro-video-renamer
Renames GoPro chapter videos so that they appear in the correct order when sorted alphanumerically.

## Usage

```text
~$ GoProVideoRenamer --help

Usage: GoProVideoRenamer [--file-location <String>] [--prefix <String>] [--suffix <String>] [--starting-number <Int32>] [--digit-count <Int32>] [--dry-run] [--help] [--version]

GoProVideoRenamer

Options:
  --file-location <String>     Directory where the GoPro videos are stored (Required)
  --prefix <String>            Text that should appear before each file's number
  --suffix <String>            Text that should appear after each file's number
  --starting-number <Int32>    What number the renamed files should start at
  --digit-count <Int32>        The number of digits to include in each file number
  --dry-run                    Print a list of the files to be renamed, but do not rename them
  -h, --help                   Show help message
  --version                    Show version
```