using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kopigrad.Models
{
    public partial class KopigradContext : DbContext
    {
        public KopigradContext()
        {
        }

        public KopigradContext(DbContextOptions<KopigradContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Columnname> Columnnames { get; set; } = null!;
        public virtual DbSet<Contacttype> Contacttypes { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Miniservice> Miniservices { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderitem> Orderitems { get; set; } = null!;
        public virtual DbSet<Pagepdf> Pagepdfs { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Storypdf> Storypdfs { get; set; } = null!;
        public virtual DbSet<Tableminiservice> Tableminiservices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=5.129.192.42;port=3306;database=copygrad;user=root;password=111111", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.22-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Columnname>(entity =>
            {
                entity.HasKey(e => e.IdColumnNames)
                    .HasName("PRIMARY");

                entity.ToTable("columnnames");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdMiniService, "columnnames_ibfk_1");

                entity.Property(e => e.IdColumnNames)
                    .HasColumnType("int(11)")
                    .HasColumnName("idColumnNames");

                entity.Property(e => e.IdMiniService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idMiniService");

                entity.Property(e => e.NameColumn)
                    .HasColumnType("text")
                    .HasColumnName("nameColumn");

                entity.HasOne(d => d.IdMiniServiceNavigation)
                    .WithMany(p => p.Columnnames)
                    .HasForeignKey(d => d.IdMiniService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("columnnames_ibfk_1");
            });

            modelBuilder.Entity<Contacttype>(entity =>
            {
                entity.ToTable("contacttype");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.ContactTypeId).HasColumnType("int(11)");

                entity.Property(e => e.ContactTypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.IdMaterial)
                    .HasName("PRIMARY");

                entity.ToTable("material");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdMiniService, "idMiniService");

                entity.Property(e => e.IdMaterial).HasColumnType("int(11)");

                entity.Property(e => e.IdMiniService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idMiniService");

                entity.Property(e => e.NameMaterial).HasColumnName("nameMaterial");

                entity.HasOne(d => d.IdMiniServiceNavigation)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.IdMiniService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("material_ibfk_1");
            });

            modelBuilder.Entity<Miniservice>(entity =>
            {
                entity.HasKey(e => e.IdMiniService)
                    .HasName("PRIMARY");

                entity.ToTable("miniservice");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdService, "idService");

                entity.Property(e => e.IdMiniService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idMiniService");

                entity.Property(e => e.BottomName).HasColumnType("text");

                entity.Property(e => e.IdService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idService");

                entity.Property(e => e.NameMiniServise).HasColumnName("nameMiniServise");

                entity.Property(e => e.TopName).HasColumnType("text");

                entity.HasOne(d => d.IdServiceNavigation)
                    .WithMany(p => p.Miniservices)
                    .HasForeignKey(d => d.IdService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("miniservice_ibfk_1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.ContactTypeId, "fk_contacttype");

                entity.HasIndex(e => e.IdStatus, "idStatus");

                entity.HasIndex(e => e.IdTableMiniService, "idx_idTableMiniService");

                entity.Property(e => e.IdOrder).HasColumnType("int(11)");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.ContactTypeId).HasColumnType("int(11)");

                entity.Property(e => e.Count).HasColumnType("int(11)");

                entity.Property(e => e.DataOrder).HasColumnType("datetime");

                entity.Property(e => e.IdStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("idStatus");

                entity.Property(e => e.IdTableMiniService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idTableMiniService");

                entity.Property(e => e.NameUser).HasColumnType("text");

                entity.Property(e => e.Price).HasPrecision(10, 2);

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ContactTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contacttype");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_1");

                entity.HasOne(d => d.IdTableMiniServiceNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdTableMiniService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_tableminiservice");
            });

            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.HasKey(e => e.IdOrderItems)
                    .HasName("PRIMARY");

                entity.ToTable("orderitems");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdOrder, "IdRequst");

                entity.Property(e => e.IdOrderItems)
                    .HasColumnType("int(11)")
                    .HasColumnName("idOrderItems");

                entity.Property(e => e.IdOrder).HasColumnType("int(11)");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("fk_orderitems_order");
            });

            modelBuilder.Entity<Pagepdf>(entity =>
            {
                entity.HasKey(e => e.IdPage)
                    .HasName("PRIMARY");

                entity.ToTable("pagepdf");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdStory, "idStory");

                entity.Property(e => e.IdPage)
                    .HasColumnType("int(11)")
                    .HasColumnName("idPage");

                entity.Property(e => e.IdStory)
                    .HasColumnType("int(11)")
                    .HasColumnName("idStory");

                entity.Property(e => e.Size).HasColumnType("text");

                entity.HasOne(d => d.IdStoryNavigation)
                    .WithMany(p => p.Pagepdfs)
                    .HasForeignKey(d => d.IdStory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pagepdf_ibfk_1");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdService)
                    .HasName("PRIMARY");

                entity.ToTable("service");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idService");

                entity.Property(e => e.ConditionText).HasMaxLength(1000);

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.NameService)
                    .HasMaxLength(100)
                    .HasColumnName("nameService");

                entity.Property(e => e.Time).HasMaxLength(100);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PRIMARY");

                entity.ToTable("status");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("idStatus");

                entity.Property(e => e.NameStatus).HasColumnType("text");
            });

            modelBuilder.Entity<Storypdf>(entity =>
            {
                entity.HasKey(e => e.IdStory)
                    .HasName("PRIMARY");

                entity.ToTable("storypdf");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdStory)
                    .HasColumnType("int(11)")
                    .HasColumnName("idStory");

                entity.Property(e => e.NameFile)
                    .HasColumnType("text")
                    .HasColumnName("nameFile");
            });

            modelBuilder.Entity<Tableminiservice>(entity =>
            {
                entity.HasKey(e => e.IdTableMiniService)
                    .HasName("PRIMARY");

                entity.ToTable("tableminiservice");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdColumnName, "idColumnName");

                entity.HasIndex(e => e.IdMaterial, "idMaterial");

                entity.HasIndex(e => e.IdMiniService, "idMiniService");

                entity.Property(e => e.IdTableMiniService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idTableMiniService");

                entity.Property(e => e.IdColumnName)
                    .HasColumnType("int(11)")
                    .HasColumnName("idColumnName");

                entity.Property(e => e.IdMaterial)
                    .HasColumnType("int(100)")
                    .HasColumnName("idMaterial");

                entity.Property(e => e.IdMiniService)
                    .HasColumnType("int(11)")
                    .HasColumnName("idMiniService");

                entity.Property(e => e.Price).HasPrecision(10, 2);

                entity.HasOne(d => d.IdColumnNameNavigation)
                    .WithMany(p => p.Tableminiservices)
                    .HasForeignKey(d => d.IdColumnName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tableminiservice_ibfk_3");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.Tableminiservices)
                    .HasForeignKey(d => d.IdMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tableminiservice_ibfk_2");

                entity.HasOne(d => d.IdMiniServiceNavigation)
                    .WithMany(p => p.Tableminiservices)
                    .HasForeignKey(d => d.IdMiniService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tableminiservice_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("idUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.HashPassworld)
                    .HasMaxLength(500)
                    .HasColumnName("hashPassworld");

                entity.Property(e => e.NameUser)
                    .HasMaxLength(50)
                    .HasColumnName("nameUser");

                entity.Property(e => e.Salt).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
