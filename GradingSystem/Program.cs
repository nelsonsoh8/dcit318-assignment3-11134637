using System;
using System.IO;

namespace GradingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "student_scores.txt";
            string outputFile = "student_grades_report.txt";

            var processor = new StudentResultProcessor();

            try
            {
                Console.WriteLine($"Reading student data from {inputFile}...");
                var students = processor.ReadStudentsFromFile(inputFile);

                Console.WriteLine($"Generating report to {outputFile}...");
                processor.WriteReportToFile(students, outputFile);

                Console.WriteLine("Report generated successfully!");
                Console.WriteLine($"Total students processed: {students.Count}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The input file '{inputFile}' was not found.");
            }
            catch (Exceptions.MissingFieldException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exceptions.InvalidScoreFormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}