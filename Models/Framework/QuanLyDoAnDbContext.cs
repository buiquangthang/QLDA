namespace Models.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuanLyDoAnDbContext : DbContext
    {
        public QuanLyDoAnDbContext()
            : base("name=QuanLyDoAnDbContext")
        {
        }

        public virtual DbSet<DOAN> DOANs { get; set; }
        public virtual DbSet<DOCGIA> DOCGIAs { get; set; }
        public virtual DbSet<KHOA> KHOAs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHANLOAI> PHANLOAIs { get; set; }
        public virtual DbSet<PHIEUMUON> PHIEUMUONs { get; set; }
        //public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DOAN>()
                .HasMany(e => e.PHIEUMUONs)
                .WithRequired(e => e.DOAN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DOCGIA>()
                .HasMany(e => e.PHIEUMUONs)
                .WithRequired(e => e.DOCGIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.PHIEUMUONs)
                .WithRequired(e => e.NHANVIEN)
                .WillCascadeOnDelete(false);
        }
    }
}
