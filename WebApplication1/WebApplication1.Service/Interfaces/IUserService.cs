using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Response;
using WebApplication1.Domain.ViewModels;

namespace WebApplication1.Service.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<IEnumerable<User>>> GetAllUsers();

    Task<IBaseResponse<User>> GetUser(int id);
    
    Task<IBaseResponse<User>> CreateUser(UserViewModel userViewModel);
    
    Task<BaseResponse<User>> DeleteUser(int id);
    
    Task<IBaseResponse<User>> GetUserByName(string name);
    
    Task<IBaseResponse<User>> UpdateUser(int id, UserViewModel user);

    BaseResponse<Dictionary<int, string>> GetTypes();
}