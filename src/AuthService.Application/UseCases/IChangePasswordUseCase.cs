using AuthService.Domain.ValueObjects;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace AuthService.Application.UseCases
{
    public interface IChangePasswordUseCase
    {
        Task ChangePassword(ObjectId userId, Password oldPassword, Password newPassword);
    }
}
