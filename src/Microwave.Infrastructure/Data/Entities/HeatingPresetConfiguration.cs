using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microwave.Domain.Models;
using Microwave.Domain.Models.Heating;

namespace Microwave.Infrastructure.Data.Entities;

public class HeatingPresetConfiguration : IEntityTypeConfiguration<HeatingPreset>
{
    public void Configure(EntityTypeBuilder<HeatingPreset> builder)
    {
        builder.ToTable("HeatingPreset");

        #region :: Id ::
        builder.HasKey(hp => hp.Id);
        builder.Property(hp => hp.Id)
            .ValueGeneratedOnAdd();
        #endregion

        #region :: Identifier ::
        builder.OwnsOne(hp => hp.Identifier, id =>
        {
            id.Property(p => p.Value)
                .HasColumnName("Identifier")
                .HasMaxLength(64)
                .IsRequired();
        });
        #endregion

        #region :: Name ::
        builder.Property(hp => hp.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(64);
        #endregion
        
        #region :: Food ::
        builder.Property(hp => hp.Food)
            .IsRequired()
            .HasMaxLength(64);
        #endregion
        
        #region :: Duration ::
        builder.OwnsOne(hp => hp.Duration, d =>
        {
            d.Property(x => x.Value)
                .HasColumnName("Duration")
                .IsRequired();
        });
        #endregion

        #region :: Potency ::
        builder.OwnsOne(hp => hp.Potency, p =>
        {
            p.Property(x => x.Value)
                .HasColumnName("Potency")
                .IsRequired();
        });
        #endregion

        #region :: Instructions ::
        builder.Property(hp => hp.Instructions)
            .IsRequired()
            .HasMaxLength(256);
        #endregion
    }
}

public class HeatingPresetIdentifierConverter : ValueConverter<HeatingPresetIdentifier, string>
{
    public HeatingPresetIdentifierConverter()
        : base(
            v => v.Value,
            v => new HeatingPresetIdentifier(v)
        )
    {
    }
}