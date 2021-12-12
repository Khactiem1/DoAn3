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
        public List<YeuCauGachThe> GetAllYeuCauGachThes(int soluong)
        {
            return mdb.Database.SqlQuery<YeuCauGachThe>(
                "select * from YeuCauGachThe where TrangThai = N'Chờ duyệt' order by(NgayNap) asc"
                ).Take(soluong).ToList();
        }
        public int GetSLYeuCauGachThes()
        {
            int sl = 0;
            sl = mdb.Database.SqlQuery<YeuCauGachThe>(
                "select * from YeuCauGachThe where TrangThai = N'Chờ duyệt'"
                ).ToList().Count;
            return sl;
        }
        public YeuCauGachThe GetYeuCau(string id)
        {
            return mdb.YeuCauGachThes.Where(c => c.MaHoaDon == id).FirstOrDefault();
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
        public void PostGachThe<YeuCauGachThe>(YeuCauGachThe SP)
        {
            mdb.Set(SP.GetType()).Add(SP);
        }
        public TheNap GetCheckNhaMangThe(string id)
        {
            return mdb.TheNaps.Where(c => c.NhaMang == id).FirstOrDefault();
        }
        public void DeleteNhaMang<NhaMang>(NhaMang nhamang)
        {
            mdb.Set(nhamang.GetType()).Remove(nhamang);
        }
        public void Save()
        {
            mdb.SaveChanges();
        }
    }
}
