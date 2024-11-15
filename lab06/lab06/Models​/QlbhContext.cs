using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab06.Models​;

public partial class QlbhContext : DbContext
{
    public QlbhContext()
    {
    }

    public QlbhContext(DbContextOptions<QlbhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ctdondh> Ctdondhs { get; set; }

    public virtual DbSet<Ctpnhap> Ctpnhaps { get; set; }

    public virtual DbSet<Ctpxuat> Ctpxuats { get; set; }

    public virtual DbSet<Dondh> Dondhs { get; set; }

    public virtual DbSet<Nhacc> Nhaccs { get; set; }

    public virtual DbSet<Pnhap> Pnhaps { get; set; }

    public virtual DbSet<Pxuat> Pxuats { get; set; }

    public virtual DbSet<Tonkho> Tonkhos { get; set; }

    public virtual DbSet<Vattu> Vattus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ADMIN\\MSSQLSERVER01;Database=QLBH;uid=sa;pwd=123456;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ctdondh>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CTDONDH");

            entity.Property(e => e.MaVtu)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaVTu");
            entity.Property(e => e.SoDh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaVtuNavigation).WithMany()
                .HasForeignKey(d => d.MaVtu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTDONDH_VATTU");
        });

        modelBuilder.Entity<Ctpnhap>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CTPNHAP");

            entity.Property(e => e.Dgnhap)
                .HasColumnType("money")
                .HasColumnName("DGNhap");
            entity.Property(e => e.MaVtu)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaVTu");
            entity.Property(e => e.Slnhap).HasColumnName("SLNhap");
            entity.Property(e => e.SoPn)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaVtuNavigation).WithMany()
                .HasForeignKey(d => d.MaVtu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPNHAP_VATTU");
        });

        modelBuilder.Entity<Ctpxuat>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CTPXUAT");

            entity.Property(e => e.DgXuat).HasColumnType("money");
            entity.Property(e => e.MaVtu)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaVTu");
            entity.Property(e => e.SoPx)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaVtuNavigation).WithMany()
                .HasForeignKey(d => d.MaVtu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPXUAT_VATTU");
        });

        modelBuilder.Entity<Dondh>(entity =>
        {
            entity.HasKey(e => e.SoDh);

            entity.ToTable("DONDH");

            entity.Property(e => e.SoDh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaNhaCc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNhaCC");
            entity.Property(e => e.NgayDh).HasColumnType("datetime");

            entity.HasOne(d => d.MaNhaCcNavigation).WithMany(p => p.Dondhs)
                .HasForeignKey(d => d.MaNhaCc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DONDH_NHACC");
        });

        modelBuilder.Entity<Nhacc>(entity =>
        {
            entity.HasKey(e => e.MaNhaCc);

            entity.ToTable("NHACC");

            entity.Property(e => e.MaNhaCc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNhaCC");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.DienThoai).HasMaxLength(50);
            entity.Property(e => e.TenNhaCc)
                .HasMaxLength(100)
                .HasColumnName("TenNhaCC");
        });

        modelBuilder.Entity<Pnhap>(entity =>
        {
            entity.HasKey(e => e.SoPn);

            entity.ToTable("PNHAP");

            entity.Property(e => e.SoPn)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            entity.Property(e => e.SoDh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.SoDhNavigation).WithMany(p => p.Pnhaps)
                .HasForeignKey(d => d.SoDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PNHAP_DONDH");
        });

        modelBuilder.Entity<Pxuat>(entity =>
        {
            entity.HasKey(e => e.SoPx);

            entity.ToTable("PXUAT");

            entity.Property(e => e.SoPx)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NgayXuat).HasColumnType("datetime");
            entity.Property(e => e.TenKh).HasMaxLength(100);
        });

        modelBuilder.Entity<Tonkho>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TONKHO");

            entity.Property(e => e.MaVtu)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaVTu");
            entity.Property(e => e.NamThang)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Slcuoi).HasColumnName("SLCuoi");
            entity.Property(e => e.Sldau).HasColumnName("SLDau");
            entity.Property(e => e.TonSlx).HasColumnName("TonSLX");
            entity.Property(e => e.TongSln).HasColumnName("TongSLN");

            entity.HasOne(d => d.MaVtuNavigation).WithMany()
                .HasForeignKey(d => d.MaVtu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TONKHO_VATTU");
        });

        modelBuilder.Entity<Vattu>(entity =>
        {
            entity.HasKey(e => e.MaVtu);

            entity.ToTable("VATTU");

            entity.Property(e => e.MaVtu)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaVTu");
            entity.Property(e => e.DvTinh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenVtu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TenVTu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
