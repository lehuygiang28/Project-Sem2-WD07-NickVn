using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class NickVn_ProjectContext : DbContext
    {
        public NickVn_ProjectContext()
        {
        }

        public NickVn_ProjectContext(DbContextOptions<NickVn_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=ConnectionStrings:NickVn_Project", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Action)
                    .HasColumnType("text")
                    .HasColumnName("action");

                entity.Property(e => e.ImgSaleOff)
                    .HasColumnType("text")
                    .HasColumnName("img_sale_off");

                entity.Property(e => e.ImgSrc)
                    .HasColumnType("text")
                    .HasColumnName("img_src");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.Title)
                    .HasColumnType("text")
                    .HasColumnName("title");

                entity.Property(e => e.Total)
                    .HasColumnType("int(11)")
                    .HasColumnName("total");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("product_category");

                entity.Property(e => e.Action)
                    .HasColumnType("text")
                    .HasColumnName("action");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasColumnType("text")
                    .HasColumnName("category_name");

                entity.Property(e => e.ImgSrc)
                    .HasColumnType("text")
                    .HasColumnName("img_src");

                entity.Property(e => e.Note)
                    .HasColumnType("int(11)")
                    .HasColumnName("note");

                entity.Property(e => e.Total)
                    .HasColumnType("int(11)")
                    .HasColumnName("total");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnType("text")
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasColumnType("text")
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasColumnType("text")
                    .HasColumnName("last_name");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.Password)
                    .HasColumnType("text")
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnType("int(11)")
                    .HasColumnName("role")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UserName)
                    .HasColumnType("text")
                    .HasColumnName("user_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
