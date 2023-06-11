using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL.Interfaces;
using WebApplication1.Domain.Entity;

namespace WebApplication1.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<User> Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<User> Get(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<User>> SelectAll()
    {
        return _db.Users.ToListAsync();
    }

    public async Task<User> Delete(User entity)
    {
        _db.Users.Remove(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<User> Update(User entity)
    {
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<User> GetByName(string name)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Name == name);
    }
}