using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksToDo.Domain.Models;

namespace TasksToDo.Infrastructure.Data.Configuration;

public class UserConfiguration : BaseEntityConfiguration<UserModel>
{
    public override void Configure(EntityTypeBuilder<UserModel> builder)
    {
        base.Configure(builder);

        builder.Property(i => i.Email)
            .IsRequired();
        
        builder.Property(i => i.Password)
            .IsRequired()
            .HasMaxLength(500);
    }
}