using WebApplication1.Domain.Entity;

namespace WebApplication1.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);

    Task<T> Get(int id);

    Task<List<T>> SelectAll();

    Task<bool> Delete(T entity);

    Task<bool> Update();
}