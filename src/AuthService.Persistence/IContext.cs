using MongoDB.Driver;
using AuthService.Persistence.Entities;
using System.Threading.Tasks;

namespace AuthService.Persistence
{
    public interface IContext
    {
        Task<IClientSessionHandle> StartSessionAsync();

        IMongoCollection<User> Users { get; }
    }
}
