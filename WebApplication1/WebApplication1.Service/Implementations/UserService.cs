using WebApplication1.DAL.Interfaces;
using WebApplication1.Domain;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Extentions;
using WebApplication1.Domain.Helpers;
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
            
            baseResponse.StatusCode = StatusCode.OK;
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
            baseResponse.StatusCode = StatusCode.OK;
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
    

    public async Task<IBaseResponse<User>> UpdateUser(int id, UserViewModel userModel)
    {
        var baseResponse = new BaseResponse<User>();

        try
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                baseResponse.StatusCode = StatusCode.UserNotFound;
                baseResponse.Description = "Пользователь не найден";
                return baseResponse;
            }
            
            user.Name = userModel.Name;
            user.Password = HashPasswordHelper.HashPassword(userModel.Password);
            user.Role = (Role)Convert.ToInt32(userModel.Role);
            var user1 = await _userRepository.Update(user);
            baseResponse.Data = user1; 
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Description = "Пользователь добавлен";
            return baseResponse;

        }
        catch (Exception e)
        {
            return new BaseResponse<User>()
            {
                Description = $"[UpdateUser] : {e.Message}",
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

    public async Task<BaseResponse<User>> DeleteUser(int id)
    {
        var baseResponse = new BaseResponse<User>();
        try
        {
            var user =  await _userRepository.Get(id);
            if (user == null)
            {
                baseResponse.Description = "Пользователь не найден";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                
                return baseResponse;
            }

            var deletedUser = await _userRepository.Delete(user);
            baseResponse.Data = deletedUser;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<User>()
            {
                Description = $"[DeleteUser] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<User>> CreateUser(UserViewModel userViewModel)
    {
        var baseResponse = new BaseResponse<User>();
        try
        {
            var user = new User()
            {
                Name = userViewModel.Name,
                Password = HashPasswordHelper.HashPassword(userViewModel.Password),
                Role = (Role)Convert.ToInt32(userViewModel.Role)
            };
            
            var createdUser = await _userRepository.Create(user);
            baseResponse.Data = createdUser;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<User>()
            {
                Description = $"[CreateUser] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        
    }
    
    public BaseResponse<Dictionary<int, string>> GetTypes()
    {
        try
        {
            var types = ((Role[])Enum.GetValues(typeof(Role))).ToDictionary(k => (int)k, t => t.GetDisplayName());

            return new BaseResponse<Dictionary<int, string>>()
            {
                Data = types,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Dictionary<int, string>>()
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    /*public async Task<IBaseResponse<bool>> UpdateUser(User userData)
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
            baseResponse.StatusCode = StatusCode.OK;
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
    }*/
}