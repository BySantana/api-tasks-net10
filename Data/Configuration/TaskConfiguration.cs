using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksToDo.Models;

namespace TasksToDo.Data.Configuration;

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
        
        builder.HasOne(i => i.Tag)
            .WithMany(t => t.Tasks)
            .HasForeignKey(i => i.TagId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}