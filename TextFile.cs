using System;
using System.IO;

namespace RunbeckCodeExercise
{
    class TextFile : ITextFile
    {
        private string location;
        private string delimeter;
        private string TsvPattern = "\t";
        private string CsvPattern = ",";
        private string pattern;
        private int fieldsPerRecord;
        private string correctOutputFile;
        private string incorrectOutputFile;

        public TextFile(string filePathLocation)
        {
            location = filePathLocation;
            correctOutputFile = null;
            incorrectOutputFile = null;
        }

        public bool ValidateFilePath()
        {
            return File.Exists(location);
        }

        public void RecordRecords()
        {
            var lines = File.ReadAllLines(location);
            string dateTime = DateTime.Now.ToString("-dd-MM-yyyy-(hh-mm-ss)");
            correctOutputFile = "CorrectRecords_" + dateTime + ".txt";
            incorrectOutputFile = "IncorrectRecords_" + dateTime + ".txt";

            foreach (string line in lines)
            {
                if (!line.Contains("First Name"))
                {
                    if (line.Split(pattern).Length == fieldsPerRecord)
                        OutputFile(line, correctOutputFile);
                    else
                        OutputFile(line, incorrectOutputFile);
                }
            }
        }

        public void OutputFile(string output, string filename)
        {
            var outputLocation = Directory.GetParent(location).ToString() + $"\\{filename}";
            File.AppendAllText(outputLocation, output + Environment.NewLine);
        }

        #region getter/setter
        public string Delimeter
        {
            get { return delimeter; }
            set
            {
                delimeter = value;
                setPattern(delimeter);
            }
        }

        public int FieldsPerRecords
        {
            get { return fieldsPerRecord; }
            set { fieldsPerRecord = value; }
        }

        protected void setPattern(string delimeter)
        {
            switch (delimeter)
            {
                case "TSV":
                    pattern = TsvPattern;
                    break;
                case "CSV":
                    pattern = CsvPattern;
                    break;
                default:
                    break;
            }
        }

        public string CorrectOutputFile
        { get { return correctOutputFile; } }

        public string IncorrectOutputFile
        { get { return incorrectOutputFile; } }
        #endregion
    }

    enum FileFormat
    {
        CSV,
        TSV
    }
}
