using System;
namespace RunbeckCodeExercise
{
    public interface ITextFile
    {
        bool ValidateFilePath();
        void RecordRecords();
        void OutputFile(string output, string filename);
    }
}
