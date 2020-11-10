using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AuthService.Persistence.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly IContext _context;

        public UserRepository(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> Get(ObjectId id)
        {
            return (await _context
                .Users
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync())?.MapToDomainEntity();
        }

        public async Task<User> GetUsingEmail(Email email)
        {
            return (await _context
                .Users
                .Find(p => p.Email == email)
                .FirstOrDefaultAsync())?.MapToDomainEntity();
        }

        public async Task<User> GetUsingPhone(Phone phone)
        {
            return (await _context
                .Users
                .Find(p => p.Phone == phone)
                .FirstOrDefaultAsync())?.MapToDomainEntity();
        }

        public async Task<User> Insert(User user)
        {
            var userEntity = Entities.User.Load(user);

            await _context
                .Users
                .InsertOneAsync(userEntity);

            return userEntity.MapToDomainEntity();
        }

        public async Task Update(User user)
        {
            var userEntity = Entities.User.Load(user);

            var updateResult = await _context
                .Users
                .ReplaceOneAsync(
                    filter: g => g.Id == userEntity.Id,
                    replacement: userEntity,
                    options: new ReplaceOptions { IsUpsert = false });

            if (updateResult.MatchedCount == 0)
                throw new NotFoundException($"No user with id {user.Id}");
        }
    }
}
