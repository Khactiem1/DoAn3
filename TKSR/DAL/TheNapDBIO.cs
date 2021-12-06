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
    }
}
