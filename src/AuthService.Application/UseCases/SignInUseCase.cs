using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Settings;
using AuthService.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public sealed class SignInUseCase : ISignInUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenSettings _tokenSettings;
        private readonly ILogger<SignInUseCase> _logger;

        public SignInUseCase(IUserRepository userRepository, IAccessTokenSettings tokenSettings, ILogger<SignInUseCase> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenSettings = tokenSettings;
        }

        public async Task<Token> SignInByEmail(Email email, Password password)
        {
            var user = await _userRepository.GetUsingEmail(email);

            if (user == null || !user.VerifyPassword(password))
            {
                throw new UnauthorizedException("Invalid email or password");
            }

            var token = Token.GenerateToken(user, _tokenSettings);

            try
            {
                user.UpdateLastLoginUsingEmail();

                await _userRepository.Update(user);
            }
            catch { }

            return token;
        }

        public async Task<Token> SignInByPhone(Phone phone, Password password)
        {
            var user = await _userRepository.GetUsingPhone(phone);

            if (user == null || !user.VerifyPassword(password))
            {
                throw new UnauthorizedException("Invalid phone or password");
            }

            var token = Token.GenerateToken(user, _tokenSettings);

            try
            {
                user.UpdateLastLoginUsingPhone();

                await _userRepository.Update(user);
            }
            catch { }

            return token;
        }
    }
}
