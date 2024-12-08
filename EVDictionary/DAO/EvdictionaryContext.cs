using System;
using System.Collections.Generic;
using EVDictionary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EVDictionary.DAO;

public partial class EvdictionaryContext : DbContext
{
    public EvdictionaryContext()
    {
    }

    public EvdictionaryContext(DbContextOptions<EvdictionaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Definition> Definitions { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    public virtual DbSet<WordType> WordTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Tạo đối tượng Configuration để đọc từ appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Đường dẫn tới thư mục hiện tại
                .AddJsonFile("appsettings.json")
                .Build();

            // Lấy chuỗi kết nối từ tệp cấu hình
            var connectionString = configuration.GetConnectionString("MyCnn");

            // Cấu hình DbContext sử dụng SQL Server
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Definition>(entity =>
        {
            entity.HasKey(e => e.DefinitionId).HasName("PK__Definiti__9D66553470018409");

            entity.ToTable("Definition");

            entity.Property(e => e.DefinitionId).HasColumnName("DefinitionID");
            entity.Property(e => e.WordId).HasColumnName("WordID");

            entity.HasOne(d => d.Word).WithMany(p => p.Definitions)
                .HasForeignKey(d => d.WordId)
                .HasConstraintName("FK__Definitio__WordI__5441852A");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF62F490F41");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WordText).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__UserID__02FC7413");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACB7B4B11F");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E4E3594B77").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleID__4CA06362");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE3AF5A29BCC");

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(e => e.WordId).HasName("PK__Word__2C20F046AFCD81F7");

            entity.ToTable("Word");

            entity.Property(e => e.WordId).HasColumnName("WordID");
            entity.Property(e => e.WordText).HasMaxLength(100);
            entity.Property(e => e.WordTypeId).HasColumnName("WordTypeID");

            entity.HasOne(d => d.WordType).WithMany(p => p.Words)
                .HasForeignKey(d => d.WordTypeId)
                .HasConstraintName("FK__Word__WordTypeID__5165187F");
        });

        modelBuilder.Entity<WordType>(entity =>
        {
            entity.HasKey(e => e.WordTypeId).HasName("PK__WordType__C3532A4D123D2DE4");

            entity.ToTable("WordType");

            entity.Property(e => e.WordTypeId).HasColumnName("WordTypeID");
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
