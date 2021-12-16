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
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<PhanHoi> PhanHois { get; set; }
        public virtual DbSet<ChietKhau> ChietKhaus { get; set; }
        public virtual DbSet<NhaMang> NhaMangs { get; set; }
        public virtual DbSet<YeuCauGachThe> YeuCauGachThes { get; set; }
        public virtual DbSet<YeuCauNapATM> YeuCauNapATMs { get; set; }
        public virtual DbSet<RutTienATM> RutTienATMs { get; set; }
        public virtual DbSet<ChuyenTien> ChuyenTiens { get; set; }
    }
}
