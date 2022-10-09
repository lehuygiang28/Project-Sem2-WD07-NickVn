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
        public virtual DbSet<ChargeHistory> ChargeHistories { get; set; } = null!;
        public virtual DbSet<Googlerecaptcha> Googlerecaptchas { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Lienminh> Lienminhs { get; set; } = null!;
        public virtual DbSet<Oder> Oders { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TheNapDatum> TheNapData { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=ConnectionStrings:NickVn_Project", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
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
                    .HasColumnName("action")
                    .HasDefaultValueSql("'#'");

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

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Title)
                    .HasColumnType("text")
                    .HasColumnName("title");

                entity.Property(e => e.Total)
                    .HasColumnType("int(11)")
                    .HasColumnName("total");
            });

            modelBuilder.Entity<ChargeHistory>(entity =>
            {
                entity.ToTable("charge_history");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(11)
                    .HasColumnName("amount");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.MoneyReceived)
                    .HasPrecision(11)
                    .HasColumnName("money_received");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.Pin)
                    .HasColumnType("text")
                    .HasColumnName("pin");

                entity.Property(e => e.Result)
                    .HasColumnType("text")
                    .HasColumnName("result");

                entity.Property(e => e.Serial)
                    .HasColumnType("text")
                    .HasColumnName("serial");

                entity.Property(e => e.Telecom)
                    .HasColumnType("text")
                    .HasColumnName("telecom");

                entity.Property(e => e.TypeCharge)
                    .HasColumnType("text")
                    .HasColumnName("type_charge");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");
            });

            modelBuilder.Entity<Googlerecaptcha>(entity =>
            {
                entity.ToTable("googlerecaptcha");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.HostName)
                    .HasColumnType("text")
                    .HasColumnName("host_name");

                entity.Property(e => e.Response).HasColumnType("text");

                entity.Property(e => e.SecretKey)
                    .HasColumnType("text")
                    .HasColumnName("secret_key");

                entity.Property(e => e.SiteKey)
                    .HasColumnType("text")
                    .HasColumnName("site_key");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PRIMARY");

                entity.ToTable("images");

                entity.Property(e => e.ImgId)
                    .HasColumnType("int(11)")
                    .HasColumnName("img_id");

                entity.Property(e => e.ImgLink)
                    .HasColumnType("text")
                    .HasColumnName("img_link");

                entity.Property(e => e.LienminhId)
                    .HasColumnType("int(11)")
                    .HasColumnName("lienminh_id");
            });

            modelBuilder.Entity<Lienminh>(entity =>
            {
                entity.ToTable("lienminh");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Champ)
                    .HasColumnType("int(11)")
                    .HasColumnName("champ");

                entity.Property(e => e.ImgSrc)
                    .HasColumnType("text")
                    .HasColumnName("img_src");

                entity.Property(e => e.ImgThumb)
                    .HasColumnType("text")
                    .HasColumnName("img_thumb");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Note)
                    .HasColumnType("int(11)")
                    .HasColumnName("note");

                entity.Property(e => e.PriceAtm)
                    .HasPrecision(10)
                    .HasColumnName("price_atm");

                entity.Property(e => e.ProductUserName)
                    .HasColumnType("text")
                    .HasColumnName("product_user_name");

                entity.Property(e => e.ProductUserPassword)
                    .HasColumnType("text")
                    .HasColumnName("product_user_password");

                entity.Property(e => e.Publisher)
                    .HasColumnType("text")
                    .HasColumnName("publisher");

                entity.Property(e => e.Rank)
                    .HasColumnType("text")
                    .HasColumnName("rank");

                entity.Property(e => e.Skin)
                    .HasColumnType("int(11)")
                    .HasColumnName("skin");

                entity.Property(e => e.Sold)
                    .HasColumnType("int(11)")
                    .HasColumnName("sold");

                entity.Property(e => e.Status)
                    .HasColumnType("text")
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Oder>(entity =>
            {
                entity.ToTable("oders");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.OderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("oder_id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("product_id");

                entity.Property(e => e.Status)
                    .HasColumnType("text")
                    .HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.IndexNo)
                    .HasName("PRIMARY");

                entity.ToTable("product_category");

                entity.Property(e => e.IndexNo)
                    .HasColumnType("int(11)")
                    .HasColumnName("index_no");

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

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Total)
                    .HasColumnType("int(11)")
                    .HasColumnName("total");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("role_id")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.RoleName)
                    .HasColumnType("text")
                    .HasColumnName("role_name")
                    .HasDefaultValueSql("'Thành Viên'");

                entity.Property(e => e.RoleNameEn)
                    .HasColumnType("text")
                    .HasColumnName("role_name_en");
            });

            modelBuilder.Entity<TheNapDatum>(entity =>
            {
                entity.ToTable("the_nap_data");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasPrecision(11);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.IsUse).HasColumnName("is_use");

                entity.Property(e => e.Pin)
                    .HasColumnType("text")
                    .HasColumnName("pin");

                entity.Property(e => e.Serial)
                    .HasColumnType("text")
                    .HasColumnName("serial");

                entity.Property(e => e.TelecomName)
                    .HasColumnType("text")
                    .HasColumnName("telecom_name");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_at");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CoverImgSrc)
                    .HasColumnType("text")
                    .HasColumnName("cover_img_src");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.Email)
                    .HasColumnType("text")
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasColumnType("text")
                    .HasColumnName("first_name");

                entity.Property(e => e.ImgSrc)
                    .HasColumnType("text")
                    .HasColumnName("img_src");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.LastName)
                    .HasColumnType("text")
                    .HasColumnName("last_name");

                entity.Property(e => e.Money)
                    .HasPrecision(10)
                    .HasColumnName("money");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.Password)
                    .HasColumnType("text")
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasColumnType("text")
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("role_id")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_at");

                entity.Property(e => e.UserName)
                    .HasColumnType("text")
                    .HasColumnName("user_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
