using CrudMysqlVSCODE.Shared;
using Microsoft.EntityFrameworkCore;

namespace crudMysqlVSCODE.Server
{
     public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<User> Produtos { get; set; }
    }
}