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
