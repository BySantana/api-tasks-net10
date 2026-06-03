using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksToDo.Domain.Models;

namespace TasksToDo.Infrastructure.Data.Configuration;

public class TaskConfiguration : BaseEntityConfiguration<TaskModel>
{
    public override void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(i => i.Description)
            .HasMaxLength(500);
        
        builder.HasMany(i => i.Tags)
            .WithMany(t => t.Tasks)
            .UsingEntity(j => j.ToTable("TaskTags")); 
        
        builder.Navigation(t => t.Tags)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}