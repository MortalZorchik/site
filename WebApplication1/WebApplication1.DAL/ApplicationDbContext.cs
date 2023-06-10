using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Domain;
using WebApplication1.Domain.Entity;

namespace WebApplication1.DAL;

public class ApplicationDbContext : DbContext
{
    private IConfiguration _configuration;
    
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var name = _configuration.GetSection("AdminConfig:Name").Value;
        var password = _configuration.GetSection("AdminConfig:Password").Value;
        if (name != null && password != null)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, Name = name,
                    Password = password, Role = Role.Admin
                }
            );
    }
}