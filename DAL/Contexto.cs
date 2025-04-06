using Microsoft.EntityFrameworkCore;
using CyH_Techno_Store.Models;

namespace CyH_Techno_Store.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>().HasData(
            new List<UserAccount>()
            {
                new()
                {
                    Id = 1,
                    UserName = "Administrador",
                    Password = "Qwe123...",
                    Role = "Admin"
                },
                new()
                {
                    Id = 2,
                    UserName = "Usuario",
                    Password = "Asd123...",
                    Role = "User"
                }
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}