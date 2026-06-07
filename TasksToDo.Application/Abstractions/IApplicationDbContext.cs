using Microsoft.EntityFrameworkCore;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Abstractions;

public interface IApplicationDbContext
{
    public DbSet<UserModel> Users { get; }
    public DbSet<TaskModel> Tasks { get; }
    public DbSet<TagModel> Tags { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}