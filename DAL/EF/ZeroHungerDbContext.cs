using System;
using System.Collections.Generic;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public partial class ZeroHungerDbContext : DbContext
{
    public ZeroHungerDbContext()
    {
    }

    public ZeroHungerDbContext(DbContextOptions<ZeroHungerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CollectRequest> CollectRequests { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CollectRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CollectR__3213E83F80E0E3E4");

            entity.ToTable("CollectRequest");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Empid).HasColumnName("empid");
            entity.Property(e => e.Maxtime)
                .HasColumnType("datetime")
                .HasColumnName("maxtime");
            entity.Property(e => e.Resid).HasColumnName("resid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("status");

            entity.HasOne(d => d.Emp).WithMany(p => p.CollectRequests)
                .HasForeignKey(d => d.Empid)
                .HasConstraintName("FK__CollectRe__empid__3E52440B");

            entity.HasOne(d => d.Res).WithMany(p => p.CollectRequests)
                .HasForeignKey(d => d.Resid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CollectRe__resid__3D5E1FD2");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FF2F5C386");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phno");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3213E83F7FE1B26F");

            entity.ToTable("Restaurant");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
