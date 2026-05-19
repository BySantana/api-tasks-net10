using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksToDo.Models;

namespace TasksToDo.Data.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<TagModel>
{
    public void Configure(EntityTypeBuilder<TagModel> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Navigation(i => i.Tasks)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}