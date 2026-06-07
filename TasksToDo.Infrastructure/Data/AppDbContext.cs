using Microsoft.EntityFrameworkCore;
using TasksToDo.Domain.Models;
using TasksToDo.Application.Abstractions;

namespace TasksToDo.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<TaskModel> Tasks => Set<TaskModel>();
    public DbSet<TagModel> Tags => Set<TagModel>();
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}