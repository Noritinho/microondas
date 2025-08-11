using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microwave.Domain.Models;
using Microwave.Domain.Models.Heating;
using Microwave.Domain.Models.User;
using Microwave.Infrastructure.Data.Entities;

namespace Microwave.Infrastructure.Data;

public class MicroWaveDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<HeatingPreset> HeatingPreset { get; set; }
    
    public MicroWaveDbContext(DbContextOptions<MicroWaveDbContext> options)
        : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) //TODO: testar depois sem o config das entidades.
    {
        base.OnModelCreating(modelBuilder);

        #region :: User ::
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();
            
            builder.OwnsOne(u => u.Name, d =>
            {
                d.Property(un => un.Value)
                    .HasColumnName("UserName")
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Password, d =>
            {
                d.Property(p => p.Value)
                    .HasColumnName("Password")
                    .IsRequired();
            });
        });
        #endregion
        
        #region :: HeatingPreset ::
        modelBuilder.Entity<HeatingPreset>(builder =>
        {
            builder.HasKey(hp => hp.Id);
            builder.Property(hp => hp.Id)
                .ValueGeneratedOnAdd();
            
            builder.OwnsOne(hp => hp.Identifier, d =>
            {
                d.Property(x => x.Value)
                    .HasColumnName("Identifier")
                    .IsRequired();
            });

            builder.OwnsOne(hp => hp.Duration, d =>
            {
                d.Property(x => x.Value)
                    .HasColumnName("Duration")
                    .IsRequired();
            });

            builder.OwnsOne(hp => hp.Potency, d =>
            {
                d.Property(x => x.Value)
                    .HasColumnName("Potency")
                    .IsRequired();
            });
        });
        #endregion
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}