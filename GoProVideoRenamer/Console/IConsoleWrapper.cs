﻿namespace GoProVideoRenamer.ConsoleWrapping
{
    public interface IConsoleWrapper
    {
        void WriteErrorLine(string line);
        void WriteLine(string line);
    }
}