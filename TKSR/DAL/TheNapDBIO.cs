using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class TheNapDBIO
    {
        mydb mdb = new mydb();
        public List<NhaMang> GetAllNhaMang()
        {
            return mdb.Database.SqlQuery<NhaMang>(
                "select * from NhaMang"
                ).ToList();
        }
        public List<ChietKhau> GetAllChietKhau()
        {
            return mdb.Database.SqlQuery<ChietKhau>(
                "select * from ChietKhau"
                ).ToList();
        }
        public List<YeuCauGachThe> GetYeuCauGachThes(string TenTaiKhoan, int soluong)
        {
            return mdb.Database.SqlQuery<YeuCauGachThe>(
                "select * from YeuCauGachThe where TenTaiKhoan = @uid order by(NgayNap) desc",
                new SqlParameter("@uid", TenTaiKhoan)
                ).Take(soluong).ToList();
        }
        public List<YeuCauNapATM> GetYeuCauNapTienATM(string TenTaiKhoan, int soluong)
        {
            return mdb.Database.SqlQuery<YeuCauNapATM>(
                "select * from YeuCauNapATM where TenTaiKhoan = @uid order by(NgayNap) desc",
                new SqlParameter("@uid", TenTaiKhoan)
                ).Take(soluong).ToList();
        }
        public List<RutTienATM> GetYeuCauRutTienATM(string TenTaiKhoan, int soluong)
        {
            return mdb.Database.SqlQuery<RutTienATM>(
                "select * from RutTienATM where TenTaiKhoan = @uid order by(NgayRut) desc",
                new SqlParameter("@uid", TenTaiKhoan)
                ).Take(soluong).ToList();
        }
        public List<ChuyenTien> GetYeuCauChuyenTien(string TenTaiKhoan, int soluong)
        {
            return mdb.Database.SqlQuery<ChuyenTien>(
                "select * from ChuyenTien where TenTaiKhoanChuyen = @uid order by(NgayChuyen) desc",
                new SqlParameter("@uid", TenTaiKhoan)
                ).Take(soluong).ToList();
        }
        public List<ChuyenTien> GetYeuCauNhanTien(string TenTaiKhoan, int soluong)
        {
            return mdb.Database.SqlQuery<ChuyenTien>(
                "select * from ChuyenTien where TenTaiKhoanNhan = @uid order by(NgayChuyen) desc",
                new SqlParameter("@uid", TenTaiKhoan)
                ).Take(soluong).ToList();
        }
        public List<YeuCauNapATM> GetAllYeuCauNapTienATM(int soluong)
        {
            return mdb.Database.SqlQuery<YeuCauNapATM>(
                "select * from YeuCauNapATM where TrangThai = N'Chờ duyệt' order by(NgayNap) asc"
                ).Take(soluong).ToList();
        }
        public List<YeuCauNapATM> GetAllYeuCauNapTienATMTheoIUD(string id)
        {
            return mdb.Database.SqlQuery<YeuCauNapATM>(
                "select * from YeuCauNapATM where TrangThai = N'Chờ duyệt' and TenTaiKhoan LIKE N'%" + id + "%' order by(NgayNap) asc"
                ).ToList();
        }
        public List<RutTienATM> GetAllYeuCauRutTienATM(int soluong)
        {
            return mdb.Database.SqlQuery<RutTienATM>(
                "select * from RutTienATM where TrangThai = N'Chờ duyệt' order by(NgayRut) asc"
                ).Take(soluong).ToList();
        }
        public List<ThongBao> GetAllDSThongBao()
        {
            return mdb.Database.SqlQuery<ThongBao>(
                "select * from ThongBao order by(NgayThem) asc"
                ).ToList();
        }
        public List<RutTienATM> GetAllYeuCauRutTienATMTheoIUD(string id)
        {
            return mdb.Database.SqlQuery<RutTienATM>(
                "select * from RutTienATM where TrangThai = N'Chờ duyệt' and TenTaiKhoan LIKE N'%" + id + "%' order by(NgayRut) asc"
                ).ToList();
        }
        public List<YeuCauGachThe> GetAllYeuCauGachThes(int soluong)
        {
            return mdb.Database.SqlQuery<YeuCauGachThe>(
                "select * from YeuCauGachThe where TrangThai = N'Chờ duyệt' order by(NgayNap) asc"
                ).Take(soluong).ToList();
        }
        public List<YeuCauGachThe> GetLoadYeuCauGachTheTheoIUD(string id)
        {
            return mdb.Database.SqlQuery<YeuCauGachThe>(
                "select * from YeuCauGachThe where TrangThai = N'Chờ duyệt' and TenTaiKhoan LIKE N'%" + id + "%' order by(NgayNap) asc"
                ).ToList();
        }
        public int GetSLYeuCauGachThes()
        {
            int sl = 0;
            sl = mdb.Database.SqlQuery<YeuCauGachThe>(
                "select * from YeuCauGachThe where TrangThai = N'Chờ duyệt'"
                ).ToList().Count;
            return sl;
        }
        public int GetSLYeuCauNapATM()
        {
            int sl = 0;
            sl = mdb.Database.SqlQuery<YeuCauNapATM>(
                "select * from YeuCauNapATM where TrangThai = N'Chờ duyệt'"
                ).ToList().Count;
            return sl;
        }
        public int GetSLYeuCauRutATM()
        {
            int sl = 0;
            sl = mdb.Database.SqlQuery<RutTienATM>(
                "select * from RutTienATM where TrangThai = N'Chờ duyệt'"
                ).ToList().Count;
            return sl;
        }
        public YeuCauGachThe GetYeuCau(string id)
        {
            return mdb.YeuCauGachThes.Where(c => c.MaHoaDon == id).FirstOrDefault();
        }
        public YeuCauNapATM GetYeuCauATM(string id)
        {
            return mdb.YeuCauNapATMs.Where(c => c.MaHoaDon == id).FirstOrDefault();
        }
        public RutTienATM GetYeuCauRutATM(string id)
        {
            return mdb.RutTienATMs.Where(c => c.MaHoaDon == id).FirstOrDefault();
        }
        public ChietKhau GetChietKhau(string NhaMang, string MenhGia)
        {
            return mdb.ChietKhaus.Where(c => c.NhaMang == NhaMang && c.MenhGia == MenhGia).FirstOrDefault();
        }
        public NhaMang GetNhaMang(string NhaMang)
        {
            return mdb.NhaMangs.Where(c => c.TenNhaMang == NhaMang).FirstOrDefault();
        }
        public void PostNhaMang<NhaMang>(NhaMang dichvu)
        {
            mdb.Set(dichvu.GetType()).Add(dichvu);
        }
        public void PostThongBao<ThongBao>(ThongBao SP)
        {
            mdb.Set(SP.GetType()).Add(SP);
        }
        public void PostGachThe<YeuCauGachThe>(YeuCauGachThe SP)
        {
            mdb.Set(SP.GetType()).Add(SP);
        }
        public void PostNapATM<YeuCauNapATM>(YeuCauNapATM SP)
        {
            mdb.Set(SP.GetType()).Add(SP);
        }
        public void PostRutATM<RutTienATM>(RutTienATM SP)
        {
            mdb.Set(SP.GetType()).Add(SP);
        }
        public void PostChuyenTien<ChuyenTien>(ChuyenTien SP)
        {
            mdb.Set(SP.GetType()).Add(SP);
        }
        public TheNap GetCheckNhaMangThe(string id)
        {
            return mdb.TheNaps.Where(c => c.NhaMang == id).FirstOrDefault();
        }
        public ThongBao GetThongBao(string id)
        {
            return mdb.ThongBaos.Where(c => c.MaThongBao == id).FirstOrDefault();
        }
        public void DeleteNhaMang<NhaMang>(NhaMang nhamang)
        {
            mdb.Set(nhamang.GetType()).Remove(nhamang);
        }
        public void DeleteThongBao<ThongBao>(ThongBao tb)
        {
            mdb.Set(tb.GetType()).Remove(tb);
        }
        public void Save()
        {
            mdb.SaveChanges();
        }
    }
}
