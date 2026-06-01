using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksToDo.Models;

namespace TasksToDo.Data.Configuration;

public class TagConfiguration : BaseEntityConfiguration<TagModel>
{
    public override void Configure(EntityTypeBuilder<TagModel> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Navigation(i => i.Tasks)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}