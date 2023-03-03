using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalProject.Models
{
    public partial class FinalProjectDBContext : DbContext
    {
        public FinalProjectDBContext()
        {
        }

        public FinalProjectDBContext(DbContextOptions<FinalProjectDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<MyList> MyLists { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=FinalProjectDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategryName)
                    .HasMaxLength(50)
                    .HasColumnName("categryName");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.Property(e => e.ListId).HasColumnName("listId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.ListName)
                    .HasMaxLength(50)
                    .HasColumnName("listName");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.AdSoyad).HasMaxLength(50);

                entity.Property(e => e.EPosta)
                    .HasMaxLength(50)
                    .HasColumnName("ePosta");

                entity.Property(e => e.Sifre)
                    .HasMaxLength(50)
                    .HasColumnName("sifre");
            });

            modelBuilder.Entity<MyList>(entity =>
            {
                entity.HasKey(e => e.CurrentListId);

                entity.ToTable("myList");

                entity.Property(e => e.CurrentListId).HasColumnName("currentListId");

                entity.Property(e => e.ListId).HasColumnName("listId");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.MyLists)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_myList_Lists");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.ProductUrl).HasColumnName("productUrl");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
