using FastReportAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FastReportAPI.Context;

public class AppDbContext : DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Products> ProductByCategoryName { get; set; } //ProductByCategoryName é uma view criada no banco de dados


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Products>().HasNoKey();
    }


}
