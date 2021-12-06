using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DTO
{
    public partial class mydb : DbContext
    {
        public mydb()
            : base("name=mydb")
        {
        }

        public virtual DbSet<DichVu> DichVus { get; set; }
        public virtual DbSet<TheNap> TheNaps { get; set; }
        public virtual DbSet<MuaThe> MuaThes { get; set; }
        public virtual DbSet<Register> Registers { get; set; }
        public virtual DbSet<DichVuThanhToan> DichVuThanhToans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<PhanHoi> PhanHois { get; set; }
        public virtual DbSet<ChietKhau> ChietKhaus { get; set; }
        public virtual DbSet<NhaMang> NhaMangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.TaiKhoan1)
                .HasForeignKey(e => e.TaiKhoan);
        }
    }
}
