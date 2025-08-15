using GradingSystem.Exceptions;

namespace GradingSystem
{
    public class Student
    {
        public int Id { get; }
        public string FullName { get; }
        public int Score { get; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        public string GetGrade()
        {
            if (Score >= 80 && Score <= 100) return "A";
            if (Score >= 70 && Score < 80) return "B";
            if (Score >= 60 && Score < 70) return "C";
            if (Score >= 50 && Score < 60) return "D";
            if (Score < 50) return "F";
            throw new InvalidScoreFormatException($"Invalid score value: {Score}");
        }
    }
}