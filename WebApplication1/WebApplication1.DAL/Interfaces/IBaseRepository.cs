using WebApplication1.Domain.Entity;

namespace WebApplication1.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<User> Create(T entity);

    Task<T> Get(int id);

    Task<List<T>> SelectAll();

    Task<T> Delete(T entity);

    Task<T> Update(T entity);
}