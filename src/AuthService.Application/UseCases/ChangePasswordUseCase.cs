using AuthService.Application.Repositories;
using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public sealed class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ChangePasswordUseCase> _logger;

        public ChangePasswordUseCase(IUserRepository userRepository, ILogger<ChangePasswordUseCase> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task ChangePassword(ObjectId userId, Password oldPassword, Password newPassword)
        {
            var user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            user.ChangePassword(oldPasswordConfirmation: oldPassword, 
                newPassword: newPassword);

            await _userRepository.Update(user);
        }
    }
}
