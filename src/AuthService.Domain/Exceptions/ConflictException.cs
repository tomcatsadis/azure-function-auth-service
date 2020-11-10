namespace AuthService.Domain.Exceptions
{
    public class ConflictException : DomainException
    {
        public ConflictException(string message = "Data already exist.")
               : base(message)
        {
        }
    }
}
