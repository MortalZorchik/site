using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Domain;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Helpers;

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
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasData(new User
                {
                    Id = 1, Name = name,
                    Password = HashPasswordHelper.HashPassword(password), Role = Role.Admin//password need to hash
                });

                builder.ToTable("Users").HasKey(x => x.Id);
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();
            });
        }
        
        /*if (name != null && password != null)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, Name = name,
                    Password = password, Role = Role.Admin
                }
            );*/
    }
}