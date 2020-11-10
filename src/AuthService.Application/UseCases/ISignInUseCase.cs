using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public interface ISignInUseCase
    {
        Task<Token> SignInByEmail(Email email, Password password);

        Task<Token> SignInByPhone(Phone phone, Password password);
    }
}
