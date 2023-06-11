using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Domain.Response;
using WebApplication1.Domain.ViewModels;

namespace WebApplication1.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

    }
}

