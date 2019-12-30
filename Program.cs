using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RunbeckCodeExercise
{
    class Program
    {
        public const string EXIT = "exit";

        static void Main(string[] args)
        {

            //public readonly IList<string> Delimeter = new List<string> { "CSV", "TSV" }.AsReadOnly();
            string fileLocation;
            do
            {
                bool fileFormat = false;
                Console.WriteLine("Where is the file located?(To exit type Exit)");
                fileLocation = Console.ReadLine();
                TextFile text = new TextFile(fileLocation);

                if (text.ValidateFilePath())
                {
                    Console.WriteLine("Is the file format CSV (comma-separated values) or TSV (tab-separated values)[Type CSV or TSV?");
                    var fileFormatEntry = Console.ReadLine().ToUpper();

                    foreach(string format in Enum.GetNames(typeof(FileFormat)))
                    {
                        if(format.Equals(fileFormatEntry))
                        {
                            fileFormat = true;
                            text.Delimeter = fileFormatEntry;
                            break;
                        }
                            
                    }

                    if (fileFormat)
                    {
                        Console.WriteLine("How many fields should each record contain?");
                        string recordCount = Console.ReadLine();

                        int i = 0;
                        bool canConvert = int.TryParse(recordCount, out i);
                        if (canConvert)
                        {
                            text.FieldsPerRecords = i;
                            text.RecordRecords();
                        }

                        if (File.Exists(Directory.GetParent(fileLocation) + "\\" + text.CorrectOutputFile))
                        {
                            Console.WriteLine("The correct files can be found here:");
                            Console.WriteLine(Directory.GetParent(fileLocation) + "\\" + text.CorrectOutputFile);
                        }

                        if (File.Exists(Directory.GetParent(fileLocation) + "\\" + text.IncorrectOutputFile))
                        {
                            Console.WriteLine("The incorrect files can be found here:");
                            Console.WriteLine(Directory.GetParent(fileLocation) + "\\" + text.IncorrectOutputFile);
                        }
                    }
                    else
                        Console.WriteLine("Please enter CSV or TSV. Please start over.");
                }
                else
                {
                    if(!fileLocation.Equals(EXIT))
                    Console.WriteLine("Unable to validate the file and location . Please enter a valid location and file.");
                }

            } while (!fileLocation.ToLower().Equals(EXIT));
        }
    }
}
