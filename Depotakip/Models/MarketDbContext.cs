using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Depotakip.Models
{
    public partial class MarketDbContext : DbContext
    {
        public MarketDbContext()
        {
        }

        public MarketDbContext(DbContextOptions<MarketDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; } = null!;
        public virtual DbSet<Depo> Depolar{ get; set; } = null!;
        public virtual DbSet<Kategori> Kategoriler { get; set; } = null!;
        public virtual DbSet<Personel> Personeller { get; set; } = null!;
        public virtual DbSet<Reyon> Reyonlar { get; set; } = null!;
        public virtual DbSet<Tedarikci> Tedarikciler { get; set; } = null!;
        public virtual DbSet<Urun> Urunler { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;initial Catalog=MarketDb;trusted_connection=yes ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Depo>(entity =>
            {
                entity.Property(e => e.DepoId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.Property(e => e.KategoriId).ValueGeneratedNever();

                entity.HasOne(d => d.Reyon)
                    .WithMany(p => p.Kategoris)
                    .HasForeignKey(d => d.ReyonId)
                    .HasConstraintName("FK__Kategori__ReyonI__286302EC");
            });

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.Property(e => e.PersonelId).ValueGeneratedNever();

                entity.HasOne(d => d.Depo)
                    .WithMany(p => p.Personels)
                    .HasForeignKey(d => d.DepoId)
                    .HasConstraintName("FK__Personel__DepoId__300424B4");
            });

            modelBuilder.Entity<Reyon>(entity =>
            {
                entity.Property(e => e.ReyonId).ValueGeneratedNever();

                entity.HasOne(d => d.Depo)
                    .WithMany(p => p.Reyons)
                    .HasForeignKey(d => d.DepoId)
                    .HasConstraintName("FK__Reyon__DepoId__25869641");
            });

            modelBuilder.Entity<Tedarikci>(entity =>
            {
                entity.Property(e => e.TedarikciId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Urun>(entity =>
            {
                entity.Property(e => e.UrunId).ValueGeneratedNever();

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.Uruns)
                    .HasForeignKey(d => d.KategoriId)
                    .HasConstraintName("FK__Urun__KategoriId__2B3F6F97");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
