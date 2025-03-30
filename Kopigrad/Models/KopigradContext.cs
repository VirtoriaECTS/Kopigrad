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

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Miniservice> Miniservices { get; set; }

    public virtual DbSet<Pagepdf> Pagepdfs { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Requstсontent> Requstсontents { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Storypdf> Storypdfs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Viewcategory> Viewcategories { get; set; }

    public virtual DbSet<Viewmaterial> Viewmaterials { get; set; }

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

            entity.HasIndex(e => e.IdMiniService, "idMiniService");

            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.IdMiniService)
                .HasColumnType("int(11)")
                .HasColumnName("idMiniService");
            entity.Property(e => e.NameCategory).HasColumnType("text");

            entity.HasOne(d => d.IdMiniServiceNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.IdMiniService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_ibfk_1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Miniservice>(entity =>
        {
            entity.HasKey(e => e.IdMiniService).HasName("PRIMARY");

            entity.ToTable("miniservice");

            entity.HasIndex(e => e.IdService, "idService");

            entity.Property(e => e.IdMiniService)
                .HasColumnType("int(11)")
                .HasColumnName("idMiniService");
            entity.Property(e => e.IdService)
                .HasColumnType("int(11)")
                .HasColumnName("idService");
            entity.Property(e => e.NameMunuSercise).HasColumnName("nameMunuSercise");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Miniservices)
                .HasForeignKey(d => d.IdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("miniservice_ibfk_1");
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

            entity.HasIndex(e => e.IdView, "IdView");

            entity.Property(e => e.IdPrice).HasColumnType("int(11)");
            entity.Property(e => e.IdView).HasColumnType("int(11)");
            entity.Property(e => e.Price1).HasColumnName("Price");

            entity.HasOne(d => d.IdViewNavigation).WithMany(p => p.Prices)
                .HasForeignKey(d => d.IdView)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("price_ibfk_1");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.IdRequest).HasName("PRIMARY");

            entity.ToTable("request");

            entity.HasIndex(e => e.IdStatus, "idStatus");

            entity.Property(e => e.IdRequest).HasColumnType("int(11)");
            entity.Property(e => e.DataRequst).HasColumnType("datetime");
            entity.Property(e => e.Email).HasColumnType("text");
            entity.Property(e => e.IdStatus)
                .HasColumnType("int(11)")
                .HasColumnName("idStatus");
            entity.Property(e => e.Name).HasColumnType("text");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_ibfk_1");
        });

        modelBuilder.Entity<Requstсontent>(entity =>
        {
            entity.HasKey(e => e.IdRequstContent).HasName("PRIMARY");

            entity.ToTable("requstсontent");

            entity.HasIndex(e => e.IdRequst, "IdRequst");

            entity.HasIndex(e => e.IdViewCategory, "IdViewCategory");

            entity.Property(e => e.IdRequstContent)
                .HasColumnType("int(11)")
                .HasColumnName("idRequstContent");
            entity.Property(e => e.Count).HasColumnType("int(11)");
            entity.Property(e => e.IdRequst).HasColumnType("int(11)");
            entity.Property(e => e.IdViewCategory).HasColumnType("int(11)");
            entity.Property(e => e.Price).HasColumnType("int(11)");

            entity.HasOne(d => d.IdRequstNavigation).WithMany(p => p.Requstсontents)
                .HasForeignKey(d => d.IdRequst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("requstсontent_ibfk_1");

            entity.HasOne(d => d.IdViewCategoryNavigation).WithMany(p => p.Requstсontents)
                .HasForeignKey(d => d.IdViewCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("requstсontent_ibfk_2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PRIMARY");

            entity.ToTable("service");

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
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.IdStatus)
                .HasColumnType("int(11)")
                .HasColumnName("idStatus");
            entity.Property(e => e.NameStatus).HasColumnType("text");
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

            entity.HasIndex(e => e.IdView, "idView");

            entity.Property(e => e.IdViewCategory)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("idViewCategory");
            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.IdView)
                .HasColumnType("int(100)")
                .HasColumnName("idView");
            entity.Property(e => e.NameStobets).HasColumnName("nameStobets");

            entity.HasOne(d => d.IdViewNavigation).WithMany(p => p.Viewcategories)
                .HasForeignKey(d => d.IdView)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("viewcategory_ibfk_2");

            entity.HasOne(d => d.IdViewCategoryNavigation).WithOne(p => p.Viewcategory)
                .HasForeignKey<Viewcategory>(d => d.IdViewCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("viewcategory_ibfk_1");
        });

        modelBuilder.Entity<Viewmaterial>(entity =>
        {
            entity.HasKey(e => e.IdView).HasName("PRIMARY");

            entity.ToTable("viewmaterial");

            entity.Property(e => e.IdView).HasColumnType("int(11)");
            entity.Property(e => e.Count).HasColumnType("int(11)");
            entity.Property(e => e.NameView).HasColumnName("nameView");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
