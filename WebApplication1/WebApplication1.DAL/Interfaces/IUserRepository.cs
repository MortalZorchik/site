using WebApplication1.Domain.Entity;

namespace WebApplication1.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByName(string name);
}