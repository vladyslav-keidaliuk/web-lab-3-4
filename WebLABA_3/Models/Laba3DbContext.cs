using Microsoft.EntityFrameworkCore;

namespace WebLABA_3.Models;

public class Laba3DbContext : DbContext
{
    public Laba3DbContext(DbContextOptions<Laba3DbContext> options) : base(options) { }
       
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
}