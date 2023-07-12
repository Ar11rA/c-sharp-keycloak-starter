using Microsoft.EntityFrameworkCore;
using Sample.Api.Models;

namespace Sample.Api.Config;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Fruit> Fruits { get; set; }
}
