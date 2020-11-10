using AuthService.Persistence.Entities;
using AuthService.Persistence.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace AuthService.Persistence
{
    public class Context : IContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public Context(IDatabaseSettings databaseSettings)
        {
            _mongoClient = new MongoClient(databaseSettings.ConnectionString);
            
            _database = _mongoClient.GetDatabase(databaseSettings.DatabaseName);

            Users = _database.GetCollection<User>(databaseSettings.UserCollectionName);
        }

        public Task<IClientSessionHandle> StartSessionAsync()
        {
            return _mongoClient.StartSessionAsync();
        }

        public IMongoCollection<User> Users { get; }
    }
}
