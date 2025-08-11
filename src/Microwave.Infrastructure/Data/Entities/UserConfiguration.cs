using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microwave.Domain.Models;
using Microwave.Domain.Models.User;

namespace Microwave.Infrastructure.Data.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        #region :: Id ::
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();
        #endregion

        #region :: Username ::
        builder.OwnsOne(u => u.Name, d =>
        {
            d.Property(un => un.Value)
                .HasColumnName("UserName")
                .IsRequired();
        });
        #endregion
        
        #region :: Password ::
        builder.OwnsOne(u => u.Password, d =>
        {
            d.Property(p => p.Value)
                .HasColumnName("Password")
                .IsRequired();
        });
        #endregion
    }
}