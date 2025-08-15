namespace GradingSystem.Exceptions
{
    public class InvalidScoreFormatException : System.Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }
}