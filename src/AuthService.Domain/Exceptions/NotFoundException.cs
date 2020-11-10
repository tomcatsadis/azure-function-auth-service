namespace AuthService.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message = "Data Not Found.")
               : base(message)
        {
        }
    }
}
