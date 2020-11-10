using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public sealed class SignUpUseCase : ISignUpUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger<SignUpUseCase> _logger;

        public SignUpUseCase(IUserRepository userRepository, IEventPublisher eventPublisher, ILogger<SignUpUseCase> logger)
        {
            _userRepository = userRepository;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public async Task<User> SignUpByEmail(Email email, Password password, Name firstName, Name lastName)
        {
            User user = User.NewUserUsingEmail(
                email: email, 
                password: password, 
                firstName: firstName, 
                lastName: lastName);

            if ((await _userRepository.GetUsingEmail(email)) != null)
            {
                throw new ConflictException("Email already exist.");
            }

            user = await _userRepository.Insert(user);

            try
            {
                await _eventPublisher.SendNewUserEvent(user);
            } catch (Exception e)
            {
                _logger.LogError($"Send new user event error: {e.Message}", e);
            }

            return user;
        }

        public async Task<User> SignUpByPhone(Phone phone, Password password, Name firstName, Name lastName)
        {
            User user = User.NewUserUsingPhone(
                phone: phone,
                password: password,
                firstName: firstName,
                lastName: lastName);

            if ((await _userRepository.GetUsingPhone(phone)) != null)
            {
                throw new ConflictException("Phone already exist.");
            }

            user = await _userRepository.Insert(user);

            try
            {
                await _eventPublisher.SendNewUserEvent(user);
            }
            catch (Exception e)
            {
                _logger.LogError($"Send new user event error: {e.Message}", e);
            }

            return user;
        }
    }
}
