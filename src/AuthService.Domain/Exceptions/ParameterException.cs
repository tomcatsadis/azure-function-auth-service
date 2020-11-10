namespace AuthService.Domain.Exceptions
{
    public class ParameterException : DomainException
    {
        public ParameterException(string message = "Request parameter is not valid.")
               : base(message)
        {
        }
    }
}
