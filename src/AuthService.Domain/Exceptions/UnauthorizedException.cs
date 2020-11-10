namespace AuthService.Domain.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message = "Unauthorized Request.")
               : base(message)
        {
        }
    }
}
