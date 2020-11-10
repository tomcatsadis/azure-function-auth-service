using AuthService.Domain.Entities;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public interface IUserInfoUseCase
    {
        Task<User> GetUserInfo(ObjectId userId);
    }
}
