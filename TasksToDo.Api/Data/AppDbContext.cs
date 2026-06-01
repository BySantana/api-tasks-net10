using Microsoft.EntityFrameworkCore;
using TasksToDo.Models;

namespace TasksToDo.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskModel> Tasks => Set<TaskModel>();
    public DbSet<TagModel> Tags => Set<TagModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}