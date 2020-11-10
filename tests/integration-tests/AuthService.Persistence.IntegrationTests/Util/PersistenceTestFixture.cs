using AuthService.Application.Repositories;
using AuthService.Domain.ValueObjects;
using AuthService.Persistence.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Persistence.IntegrationTests.Util
{
    public class PersistenceTestFixture : IDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public IServiceScope ServiceScope { get; private set; }

        public IContext Context { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        private static readonly Random random = new Random();

        public PersistenceTestFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            IConfiguration configuration = builder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped(_ => configuration);

            serviceCollection = serviceCollection.AddPersistence();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            ServiceScope = ServiceProvider.CreateScope();

            UserRepository = ServiceScope.ServiceProvider.GetService<IUserRepository>();

            Context = ServiceScope.ServiceProvider.GetService<IContext>();
        }

        /// <summary>
        /// Get user by id, directly from database.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>
        /// User
        /// </returns>
        public Domain.Entities.User GetUser(ObjectId id)
        {
            return Context
                .Users
                .Find(p => p.Id == id)
                .FirstOrDefault()
                ?.MapToDomainEntity();
        }

        /// <summary>
        /// Get user by email, directly from database.
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>
        /// User
        /// </returns>
        public Domain.Entities.User GetUserByEmail(Email email)
        {
            return Context
                .Users
                .Find(p => p.Email == email)
                .FirstOrDefault()
                ?.MapToDomainEntity();
        }

        /// <summary>
        /// Get user by phone, directly from database.
        /// </summary>
        /// <param name="phone">user phone</param>
        /// <returns>
        /// User
        /// </returns>
        public Domain.Entities.User GetUserByPhone(Phone phone)
        {
            return Context
                .Users
                .Find(p => p.Phone == phone)
                .FirstOrDefault()
                ?.MapToDomainEntity();
        }

        /// <summary>
        /// Inject (insert directly) user to database.
        /// </summary>
        /// <param name="user">User to be injected.</param>
        /// <returns>
        /// User with the generated user id.
        /// </returns>
        public async Task<Domain.Entities.User> InjectUser(Domain.Entities.User user)
        {
            var userEntity = User.Load(user);

            await Context.Users.InsertOneAsync(userEntity);

            return userEntity.MapToDomainEntity();
        }

        /// <summary>
        /// Delete data directly to database
        /// </summary>
        /// <param name="id">user id to be deleted</param>
        public async Task DeleteUser(ObjectId id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.Id, id);

            var deleteResult = await Context
                .Users
                .DeleteOneAsync(filter);
        }

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomStringWithNumber(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public virtual void Dispose()
        {
            ServiceScope.Dispose();

            ServiceProvider.Dispose();
        }
    }
}
