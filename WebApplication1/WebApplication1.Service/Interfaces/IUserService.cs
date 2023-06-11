using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Response;

namespace WebApplication1.Service.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<IEnumerable<User>>> GetAllUsers();
}