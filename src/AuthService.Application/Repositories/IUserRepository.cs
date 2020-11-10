using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace AuthService.Application.Repositories
{
    public interface IUserRepository
    {
        //
        // Summary:
        //  Get user
        //
        // Parameters:
        //  id
        //
        // Returns:
        //  user
        Task<User> Get(ObjectId id);

        //
        // Summary:
        //  Get user using email
        //
        // Parameters:
        //  email
        //
        // Returns:
        //  user
        Task<User> GetUsingEmail(Email email);

        //
        // Summary:
        //  Get user using mobile phone
        //
        // Parameters:
        //  phone
        //
        // Returns:
        //  user
        Task<User> GetUsingPhone(Phone phone);

        //
        // Summary:
        //  Insert a new user
        //
        // Parameters:
        //  user
        //
        // Returns:
        //  User with the generated user id
        Task<User> Insert(User user);

        //
        // Summary:
        //   Update user
        //
        // Parameters:
        //   user
        //
        // Exceptions:
        //   T:AuthService.Domain.Exceptions.NotFoundException:
        //     If user id is not exist
        Task Update(User user);
    }
}