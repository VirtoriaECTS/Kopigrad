using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Kopigrad.Models;

public partial class KopigradContext : DbContext
{
    public KopigradContext()
    {
    }

    public KopigradContext(DbContextOptions<KopigradContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Categoryservice> Categoryservices { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Pagepdf> Pagepdfs { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Priceview> Priceviews { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Storypdf> Storypdfs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Viewcategory> Viewcategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=kopigrad;user=viktoria;password=17092002Ol!", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.NameCategory).HasColumnType("text");
        });

        modelBuilder.Entity<Categoryservice>(entity =>
        {
            entity.HasKey(e => e.IdCategoryService).HasName("PRIMARY");

            entity.ToTable("categoryservice");

            entity.HasIndex(e => e.IdCategory, "idCategory");

            entity.HasIndex(e => e.IdServie, "idServie");

            entity.Property(e => e.IdCategoryService)
                .HasColumnType("int(11)")
                .HasColumnName("idCategoryService");
            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.IdServie)
                .HasColumnType("int(11)")
                .HasColumnName("idServie");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Categoryservices)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categoryservice_ibfk_1");

            entity.HasOne(d => d.IdServieNavigation).WithMany(p => p.Categoryservices)
                .HasForeignKey(d => d.IdServie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categoryservice_ibfk_2");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.IdImage).HasName("PRIMARY");

            entity.ToTable("image");

            entity.HasIndex(e => e.IdCategory, "idCategory");

            entity.Property(e => e.IdImage)
                .HasColumnType("int(11)")
                .HasColumnName("idImage");
            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.Image1)
                .HasColumnType("text")
                .HasColumnName("Image");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Images)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("image_ibfk_1");
        });

        modelBuilder.Entity<Pagepdf>(entity =>
        {
            entity.HasKey(e => e.IdPage).HasName("PRIMARY");

            entity.ToTable("pagepdf");

            entity.HasIndex(e => e.IdStory, "idStory");

            entity.Property(e => e.IdPage)
                .HasColumnType("int(11)")
                .HasColumnName("idPage");
            entity.Property(e => e.IdStory)
                .HasColumnType("int(11)")
                .HasColumnName("idStory");
            entity.Property(e => e.Size).HasColumnType("text");

            entity.HasOne(d => d.IdStoryNavigation).WithMany(p => p.Pagepdfs)
                .HasForeignKey(d => d.IdStory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagepdf_ibfk_1");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.IdPrice).HasName("PRIMARY");

            entity.ToTable("price");

            entity.Property(e => e.IdPrice)
                .HasColumnType("int(11)")
                .HasColumnName("idPrice");
            entity.Property(e => e.Price1).HasColumnName("Price");
        });

        modelBuilder.Entity<Priceview>(entity =>
        {
            entity.HasKey(e => e.IdPriceCondition).HasName("PRIMARY");

            entity.ToTable("priceview");

            entity.HasIndex(e => e.IdPrice, "idPrice");

            entity.HasIndex(e => e.IdPriceCondition, "idPriceCondition");

            entity.HasIndex(e => new { e.IdPriceCondition, e.IdView }, "idPriceCondition_2");

            entity.HasIndex(e => e.IdView, "idView");

            entity.Property(e => e.IdPriceCondition)
                .HasColumnType("int(11)")
                .HasColumnName("idPriceCondition");
            entity.Property(e => e.IdPrice)
                .HasColumnType("int(11)")
                .HasColumnName("idPrice");
            entity.Property(e => e.IdView)
                .HasColumnType("int(11)")
                .HasColumnName("idView");

            entity.HasOne(d => d.IdPriceNavigation).WithMany(p => p.Priceviews)
                .HasForeignKey(d => d.IdPrice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("priceview_ibfk_1");

            entity.HasOne(d => d.IdViewNavigation).WithMany(p => p.Priceviews)
                .HasForeignKey(d => d.IdView)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("priceview_ibfk_2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PRIMARY");

            entity.ToTable("service");

            entity.Property(e => e.IdService)
                .HasColumnType("int(11)")
                .HasColumnName("idService");
            entity.Property(e => e.ConditionText).HasMaxLength(1000);
            entity.Property(e => e.NameService)
                .HasMaxLength(100)
                .HasColumnName("nameService");
            entity.Property(e => e.Time).HasMaxLength(100);
        });

        modelBuilder.Entity<Storypdf>(entity =>
        {
            entity.HasKey(e => e.IdStory).HasName("PRIMARY");

            entity.ToTable("storypdf");

            entity.Property(e => e.IdStory)
                .HasColumnType("int(11)")
                .HasColumnName("idStory");
            entity.Property(e => e.NameFile)
                .HasColumnType("text")
                .HasColumnName("nameFile");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

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

        modelBuilder.Entity<Viewcategory>(entity =>
        {
            entity.HasKey(e => e.IdViewCategory).HasName("PRIMARY");

            entity.ToTable("viewcategory");

            entity.Property(e => e.IdViewCategory)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("idViewCategory");
            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.NameView)
                .HasMaxLength(100)
                .HasColumnName("nameView");

            entity.HasOne(d => d.IdViewCategoryNavigation).WithOne(p => p.Viewcategory)
                .HasForeignKey<Viewcategory>(d => d.IdViewCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("viewcategory_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
