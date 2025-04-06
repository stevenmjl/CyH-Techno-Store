using Microsoft.EntityFrameworkCore;
using CyH_Techno_Store.Models;

namespace CyH_Techno_Store.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<UserAccount> UserAccounts { get; set; }
}