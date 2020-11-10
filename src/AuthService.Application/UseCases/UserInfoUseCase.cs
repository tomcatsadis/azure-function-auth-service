using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public sealed class UserInfoUseCase : IUserInfoUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserInfoUseCase> _logger;

        public UserInfoUseCase(IUserRepository userRepository, ILogger<UserInfoUseCase> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> GetUserInfo(ObjectId userId)
        {
            var user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return user;
        }
    }
}
