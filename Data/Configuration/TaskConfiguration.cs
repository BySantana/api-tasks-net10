using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksToDo.Models;

namespace TasksToDo.Data.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskModel>
{
    public void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(i => i.Description)
            .HasMaxLength(500);
        
        builder.HasOne(i => i.Tag)
            .WithMany(t => t.Tasks)
            .HasForeignKey(i => i.TagId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}