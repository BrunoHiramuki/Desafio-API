using Desafio.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Penaltie> Penalties { get; set; }
    }
}
