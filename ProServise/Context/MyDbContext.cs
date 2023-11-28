using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProServise.Models;

namespace ProServise.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<RepairHistory> RepairHistories { get; set; }

    public virtual DbSet<RepairsSpare> RepairsSpares { get; set; }

    public virtual DbSet<RepairsWork> RepairsWorks { get; set; }

    public virtual DbSet<Spare> Spares { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;user id=postgres;password=toor;database=ProServise;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Clients_pk");

            entity.Property(e => e.IdClient).UseIdentityAlwaysColumn();
            entity.Property(e => e.Address).HasMaxLength(40);
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("EMail");
            entity.Property(e => e.Fname)
                .HasColumnType("character varying")
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasColumnType("character varying")
                .HasColumnName("LName");
            entity.Property(e => e.PhoneNumber).HasMaxLength(13);
            entity.Property(e => e.Sname)
                .HasColumnType("character varying")
                .HasColumnName("SName");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.IdDevice).HasName("Devices_pk");

            entity.Property(e => e.IdDevice).UseIdentityAlwaysColumn();
            entity.Property(e => e.Manufacturer).HasMaxLength(40);
            entity.Property(e => e.Model).HasMaxLength(40);
            entity.Property(e => e.SerialNumber).HasMaxLength(30);
            entity.Property(e => e.Type).HasColumnType("character varying");
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.IdRepair).HasName("Repairs_pk");

            entity.Property(e => e.IdRepair).UseIdentityAlwaysColumn();
            entity.Property(e => e.DateReception).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DescriptionBreaking).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Repairs_Clients_IdClient_fk");

            entity.HasOne(d => d.IdDeviceNavigation).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.IdDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Repairs_Devices_IdDevice_fk");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Repairs_Users_IdUser_fk");
        });

        modelBuilder.Entity<RepairHistory>(entity =>
        {
            entity.HasKey(e => e.IdRepairHistory).HasName("RepairHistory_pk");

            entity.ToTable("RepairHistory");

            entity.Property(e => e.IdRepairHistory).UseIdentityAlwaysColumn();
            entity.Property(e => e.DateTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Status).HasColumnType("character varying");

            entity.HasOne(d => d.IdRepairsNavigation).WithMany(p => p.RepairHistories)
                .HasForeignKey(d => d.IdRepairs)
                .HasConstraintName("RepairHistory_Repairs_IdRepair_fk");
        });

        modelBuilder.Entity<RepairsSpare>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("RepairsSpares_pk");

            entity.Property(e => e.IdList).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdRepairNavigation).WithMany(p => p.RepairsSpares)
                .HasForeignKey(d => d.IdRepair)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RepairsSpares_Repairs_IdRepair_fk");

            entity.HasOne(d => d.IdSpareNavigation).WithMany(p => p.RepairsSpares)
                .HasForeignKey(d => d.IdSpare)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RepairsSpares_Spares_IdSpare_fk");
        });

        modelBuilder.Entity<RepairsWork>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("RepairsWorks_pk");

            entity.Property(e => e.IdList).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdRepairNavigation).WithMany(p => p.RepairsWorks)
                .HasForeignKey(d => d.IdRepair)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RepairsWorks_Repairs_IdRepair_fk");

            entity.HasOne(d => d.IdWorkNavigation).WithMany(p => p.RepairsWorks)
                .HasForeignKey(d => d.IdWork)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RepairsWorks_Works_IdWork_fk");
        });

        modelBuilder.Entity<Spare>(entity =>
        {
            entity.HasKey(e => e.IdSpare).HasName("Spares_pk");

            entity.Property(e => e.IdSpare).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pk");

            entity.Property(e => e.IdUser).UseIdentityAlwaysColumn();
            entity.Property(e => e.Fname)
                .HasColumnType("character varying")
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasColumnType("character varying")
                .HasColumnName("LName");
            entity.Property(e => e.Login).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.Sname)
                .HasColumnType("character varying")
                .HasColumnName("SName");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.IdWork).HasName("Works_pk");

            entity.Property(e => e.IdWork).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
