using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksToDo.Domain.Models;

namespace TasksToDo.Infrastructure.Data.Configuration;

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