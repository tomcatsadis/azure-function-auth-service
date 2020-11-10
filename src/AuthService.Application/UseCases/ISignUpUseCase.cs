using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public interface ISignUpUseCase
    {
        Task<User> SignUpByEmail(Email email, Password password, Name firstName, Name lastName);

        Task<User> SignUpByPhone(Phone phone, Password password, Name firstName, Name lastName);
    }
}
