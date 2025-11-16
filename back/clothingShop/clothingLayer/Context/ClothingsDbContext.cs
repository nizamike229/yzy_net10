using Microsoft.EntityFrameworkCore;
using clothingLayer.Entities;
using System.IO;

namespace clothingLayer.Context;

public partial class ClothingsDbContext : DbContext
{
    public ClothingsDbContext()
    {
    }

    public ClothingsDbContext(DbContextOptions<ClothingsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clothing> Clothings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory+"../../../", "products.sqlite")}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clothing>(entity =>
        {
            entity.ToTable("clothings");

            entity.Property(e => e.Id)
                .HasColumnType("string(36)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text(500)")
                .HasColumnName("description");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Sizes)
                .HasColumnType("text(400)")
                .HasColumnName("sizes");
            entity.Property(e => e.Title)
                .HasColumnType("text(100)")
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
