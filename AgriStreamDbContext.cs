using System;
using System.Collections.Generic;
using AgriStreamIoT.web.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriStreamIoT.web.Data;

public partial class AgriStreamDbContext : DbContext
{
    public AgriStreamDbContext()
    {
    }

    public AgriStreamDbContext(DbContextOptions<AgriStreamDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Crop> Crops { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<Recommendation> Recommendations { get; set; }

    public virtual DbSet<SensorReading> SensorReadings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Crop>(entity =>
        {
            entity.HasKey(e => e.CropId).HasName("PK__Crops__92356115E20651F2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CropName).HasMaxLength(100);
            entity.Property(e => e.CropStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Planted");

            entity.HasOne(d => d.Farm).WithMany(p => p.Crops)
                .HasForeignKey(d => d.FarmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Crops_Farms");
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.FarmId).HasName("PK__Farms__ED7BBAB9BF6FD192");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FarmName).HasMaxLength(150);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.SizeHectares).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Farms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Farms_Users");
        });

        modelBuilder.Entity<Recommendation>(entity =>
        {
            entity.HasKey(e => e.RecommendationId).HasName("PK__Recommen__AA15BEE459317E2E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .HasDefaultValue("Medium");
            entity.Property(e => e.RecommendationType).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Crop).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.CropId)
                .HasConstraintName("FK_Recommendations_Crops");

            entity.HasOne(d => d.Farm).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.FarmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recommendations_Farms");
        });

        modelBuilder.Entity<SensorReading>(entity =>
        {
            entity.HasKey(e => e.Reading).HasName("PK__SensorRe__BF349789219DA7BD");

            entity.Property(e => e.Humidity).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RecordedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.SoilMoisture).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SoilPh)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("SoilPH");
            entity.Property(e => e.Temparature).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Farm).WithMany(p => p.SensorReadings)
                .HasForeignKey(d => d.FarmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SensorReadings_Farms");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CC2FAD44E");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4FC4293A9").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PassWordHash).HasMaxLength(500);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
