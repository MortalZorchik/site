using WebApplication1.DAL.Interfaces;
using WebApplication1.Domain;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Response;
using WebApplication1.Domain.ViewModels;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IBaseResponse<User>> GetUser(int id)
    {
        var baseResponse = new BaseResponse<User>();
        try
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                baseResponse.Description = "Пользователь не найден";
                baseResponse.StatusCode = StatusCode.UserNotFound;

                return baseResponse;
            }

            baseResponse.Data = user;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<User>()
            {
                Description = $"[GetUser] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    public async Task<IBaseResponse<User>> GetUserByName(string name)
    {
        var baseResponse = new BaseResponse<User>();
        try
        {
            var user = await _userRepository.GetByName(name);
            if (user == null)
            {
                baseResponse.Description = "Пользователь не найден";
                baseResponse.StatusCode = StatusCode.UserNotFound;

                return baseResponse;
            }

            baseResponse.Data = user;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<User>()
            {
                Description = $"[GetUserByName] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
                
            };
        }
    }
    public async Task<IBaseResponse<IEnumerable<User>>> GetAllUsers()
    {
        var baseResponse = new BaseResponse<IEnumerable<User>>();
        try
        {
            var users = await _userRepository.SelectAll();
            if (users.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.Data = users;
            baseResponse.StatusCode = StatusCode.OK;

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<User>>()
            {
                Description = $"[GetAllUsers] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteUser(int id)
    {
        var baseResponse = new BaseResponse<bool>()
        {
            Data = true
        };
        try
        {
            var user =  await _userRepository.Get(id);
            if (user == null)
            {
                baseResponse.Description = "Пользователь не найден";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                baseResponse.Data = false;
                
                return baseResponse;
            }

            await _userRepository.Delete(user);

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteUser] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<UserViewModel>> CreateUser(UserViewModel userViewModel)
    {
        var baseResponse = new BaseResponse<UserViewModel>();
        try
        {
            var user = new User()
            {
                Name = userViewModel.Name,
                Password = userViewModel.Password,
                Role = (Role)Convert.ToInt32(userViewModel.Role)
            };

            await _userRepository.Create(user);
            
        }
        catch (Exception e)
        {
            return new BaseResponse<UserViewModel>()
            {
                Description = $"[CreateUser] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        return baseResponse;
    }

    public async Task<IBaseResponse<bool>> UpdateUser(User userData)
    {
        var baseResponse = new BaseResponse<bool>()
        {
            Data = true
        };
        try
        {
            var user =  await _userRepository.Get(userData.Id);
            if (user == null)
            {
                baseResponse.Description = "Пользователь не найден";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                baseResponse.Data = false;
                
                return baseResponse;
            }

            user.Role = userData.Role;
            user.Name = userData.Name;
            await _userRepository.Update();

            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[UpdateUser] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}