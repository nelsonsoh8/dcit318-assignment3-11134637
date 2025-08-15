using System;
using System.Collections.Generic;
using System.IO;

namespace GradingSystem
{
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();
            int lineNumber = 0;

            using (var reader = new StreamReader(inputFilePath))
            {
                while (!reader.EndOfStream)
                {
                    lineNumber++;
                    string? line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    try
                    {
                        string[] fields = line.Split(',');
                        if (fields.Length != 3)
                            throw new Exceptions.MissingFieldException($"Line {lineNumber}: Expected 3 fields but found {fields.Length}");

                        if (!int.TryParse(fields[0].Trim(), out int id))
                            throw new Exceptions.InvalidScoreFormatException($"Line {lineNumber}: Invalid ID format");

                        string fullName = fields[1].Trim();
                        if (string.IsNullOrEmpty(fullName))
                            throw new Exceptions.MissingFieldException($"Line {lineNumber}: Missing student name");

                        if (!int.TryParse(fields[2].Trim(), out int score))
                            throw new Exceptions.InvalidScoreFormatException($"Line {lineNumber}: Invalid score format");

                        students.Add(new Student(id, fullName, score));
                    }
                    catch (Exception ex) when (ex is Exceptions.MissingFieldException || ex is Exceptions.InvalidScoreFormatException)
                    {
                        Console.WriteLine($"Skipping line {lineNumber}: {ex.Message}");
                    }
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
                }
            }
        }
    }
}