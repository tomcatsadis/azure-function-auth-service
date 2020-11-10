using AuthService.Domain.Entities;
using System.Threading.Tasks;

namespace AuthService.Application.Repositories
{
    public interface IEventPublisher
    {
        Task SendNewUserEvent(User user);
    }
}
