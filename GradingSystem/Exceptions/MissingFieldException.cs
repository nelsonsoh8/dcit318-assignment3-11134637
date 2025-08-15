namespace GradingSystem.Exceptions
{
    public class MissingFieldException : System.Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }
}