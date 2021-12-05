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
    public class DBIO
    {
        mydb mdb = new mydb();
        public List<Register> GetAllRegister()
        {
            return mdb.Registers.ToList();
        }
        public List<PhanHoi> GetPhanHoiUser(string uid)
        {
            return mdb.Database.SqlQuery<PhanHoi>(
                "select * from PhanHoi where TaiKhoan = @uid",
                new SqlParameter("@uid", uid)
                ).ToList();
        }
        public List<PhanHoi> GetAllPhanHoiUser()
        {
            return mdb.Database.SqlQuery<PhanHoi>(
                "select * from PhanHoi order by(NgayPhanHoi) desc"
                ).ToList();
        }
        public void PostPhanHoi(PhanHoi phanhoi)
        {
            mdb.Database.ExecuteSqlCommand(
                "insert into PhanHoi(TaiKhoan,TieuDe,NoiDung,Img,NgayPhanHoi) values(@tk,@td,@nd,@img,@date)",
                new SqlParameter("@tk",phanhoi.TaiKhoan),
                new SqlParameter("@td", phanhoi.TieuDe),
                new SqlParameter("@nd", phanhoi.NoiDung),
                new SqlParameter("@img", phanhoi.Img),
                new SqlParameter("@date", phanhoi.NgayPhanHoi)
                );
            //mdb.Set(phanhoi.GetType()).Add(phanhoi);
        }
        public void PostMuaThe(MuaThe muathe)
        {
            mdb.Database.ExecuteSqlCommand(
                "insert into MuaThe(tenTK,MaThe,MenhGia,Seri,NhaMang,NgayMua) values(@tk,@ma,@menhgia,@seri,@nhamang,@date)",
                new SqlParameter("@tk", muathe.tenTK),
                new SqlParameter("@ma", muathe.MaThe),
                new SqlParameter("@menhgia", muathe.MenhGia),
                new SqlParameter("@seri", muathe.Seri),
                new SqlParameter("@nhamang", muathe.NhaMang),
                new SqlParameter("@date", muathe.NgayMua)
                );
            //mdb.Set(muathe.GetType()).Add(muathe);
        }
        public List<MuaThe> Get10MuaTheNap(string uid)
        {
            List<MuaThe> dsmt = new List<MuaThe>();
            dsmt = mdb.Database.SqlQuery<MuaThe>(
                "select * from MuaThe where tenTK = @uid",
                new SqlParameter("@uid", uid)
                ).ToList();
            //dsmt = mdb.MuaThes.Where(c => c.tenTK == uid).ToList();
            dsmt = dsmt.OrderByDescending(x => x.NgayMua.Date).ThenByDescending(x => x.NgayMua.TimeOfDay).ToList();
            return dsmt;
        }
        public void PostRegister<Register>(Register user)
        {
            mdb.Set(user.GetType()).Add(user);
        }
        public void DeleteRegister<Register>(Register dichvu)
        {
            mdb.Set(dichvu.GetType()).Remove(dichvu);
        }
        public TaiKhoan GetOneTaiKhoan(string userName)
        {
            return mdb.TaiKhoans.Where(c => c.tenTK == userName).FirstOrDefault();
        }
        public TaiKhoan GetCheckEmail(string Email)
        {
            return mdb.TaiKhoans.Where(c => c.Email == Email).FirstOrDefault();
        }
        public List<TaiKhoan> GetSearchTaiKhoan(string userName)
        {
            List<TaiKhoan> dstk = new List<TaiKhoan>();
            dstk = mdb.TaiKhoans.Where(c => c.tenTK.Contains(userName)).ToList();
            dstk = dstk.OrderByDescending(x => x.ngayLap.Date).ThenByDescending(x => x.ngayLap.TimeOfDay).ToList();
            return dstk;
        }
        public void PostTaiKhoan<TaiKhoan>(TaiKhoan user)
        {
            mdb.Set(user.GetType()).Add(user);
        }
        public List<DichVu> GetAllDichVu(int soluong, int trang,string id,string name,string ncc,string dichvu,bool trangthai,int Delete) //soluong là số lượg cần lấy ra 0 là trang hiện tại, 1 là id 2 là tên 3 là ncc 4 là dịch vụ 5 là trạng thái
        {
            List<DichVu> dsdv = new List<DichVu>();
            try
            {
                dsdv = mdb.DichVus.Where(c => c.MaDichVu.Contains(id) && c.TenKhachHang.Contains(name) && c.DoiTac.Contains(ncc) && c.TenDichVu.Contains(dichvu)).ToList();
                dsdv = dsdv.OrderByDescending(x => x.NgayThem.Date).ThenByDescending(x => x.NgayThem.TimeOfDay).ToList();
                if (trangthai == true)
                {
                    int start = (trang * soluong) - Delete;
                    int stop = (trang * soluong) + soluong - Delete;
                    return SearchDichVu(start, stop, dsdv, soluong);
                }
                else
                {
                    int stop = (trang * soluong) - soluong;
                    int start = stop - soluong;
                    return SearchDichVu(start, stop, dsdv, soluong);
                }
            }
            catch
            {
                return dsdv;
            }
        }
        public List<TheNap> GetAllTheNap(int soluong, int trang, string ma, string seri, string menhgia, string nhamang, bool trangthai, int Delete)
        {
            List<TheNap> dstn = new List<TheNap>();
            try
            {
                if(menhgia == "")
                {
                    dstn = mdb.TheNaps.Where(c => c.MaThe.Contains(ma) && c.Seri.Contains(seri) && c.NhaMang.Contains(nhamang)).ToList();
                }
                else
                {
                    dstn = mdb.TheNaps.Where(c => c.MaThe.Contains(ma) && c.Seri.Contains(seri) && c.MenhGia == menhgia && c.NhaMang.Contains(nhamang)).ToList();
                }
                dstn = dstn.OrderByDescending(x => x.NgayThem.Date).ThenByDescending(x => x.NgayThem.TimeOfDay).ToList();
                if (trangthai == true)
                {
                    int start = (trang * soluong) - Delete;
                    int stop = (trang * soluong) + soluong - Delete;
                    return SearchTheNap(start, stop, dstn, soluong);
                }
                else
                {
                    int stop = (trang * soluong) - soluong;
                    int start = stop - soluong;
                    return SearchTheNap(start, stop, dstn, soluong);
                }
            }
            catch
            {
                return dstn;
            }
        }
        public List<TheNap> SearchTheNap(int start, int stop, List<TheNap> dsdv, int soluong)
        {
            List<TheNap> rdstk = new List<TheNap>();
            try
            {
                int quantity = 0;
                for (int i = start; i < stop; i++)
                {
                    if (i >= dsdv.Count)
                    {
                        break;
                    }
                    quantity++;
                    rdstk.Add(dsdv[i]);
                    if (quantity == soluong)
                    {
                        break;
                    }
                }
                return rdstk;
            }
            catch
            {
                return rdstk;
            }
        }
        public List<DichVu> SearchDichVu(int start, int stop, List<DichVu> dsdv, int soluong)
        {
            List<DichVu> rdstk = new List<DichVu>();
            try
            {
                int quantity = 0;
                for (int i = start; i < stop; i++)
                {
                    if (i >= dsdv.Count)
                    {
                        break;
                    }
                    quantity++;
                    rdstk.Add(dsdv[i]);
                    if (quantity == soluong)
                    {
                        break;
                    }
                }
                return rdstk;
            }
            catch
            {
                return rdstk;
            }
        }

        public TheNap GetCheckMaThe(string id)
        {
            return mdb.TheNaps.Where(c => c.MaThe == id).FirstOrDefault();
        }
        public List<TheNap> GetBuyThe(string NhaMang, int MenhGia, int SoLuong)
        {
            return mdb.TheNaps.Where(c => c.NhaMang == NhaMang && c.MenhGia == MenhGia + "").Take(SoLuong).ToList();
        }
        public DichVu GetCheckIdDichVu(string id)
        {
            return mdb.DichVus.Where(c => c.MaDichVu == id).FirstOrDefault();
        }
        public List<TaiKhoan> search(int start,int stop,List<TaiKhoan> dstk,int soluong)
        {
            List<TaiKhoan> rdstk = new List<TaiKhoan>();
            try
            {

                int quantity = 0;
                for (int i = start; i < stop; i++)
                {
                    if (i >= dstk.Count)
                    {
                        break;
                    }
                    quantity++;
                    rdstk.Add(dstk[i]);
                    if (quantity == soluong)
                    {
                        break;
                    }
                }
                return rdstk;
            }
            catch
            {
                return rdstk;
            }
        }
        public List<TaiKhoan> GetAllTaiKhoan(int soluong,int trang,bool trangThai,string select,DateTime daySearch)
        {
            List<TaiKhoan> dstk = new List<TaiKhoan>();
            try
            {
                if (daySearch.Year < 1900)
                {
                    if (select == "")
                    {
                        dstk = mdb.TaiKhoans.ToList();
                        dstk = dstk.OrderByDescending(x => x.ngayLap.Date).ThenByDescending(x => x.ngayLap.TimeOfDay).ToList();
                    }
                    else if (select != "admin")
                    {
                        dstk = mdb.TaiKhoans.Where(c => c.LoaiTK != "admin").ToList();
                        dstk = dstk.OrderByDescending(x => x.ngayLap.Date).ThenByDescending(x => x.ngayLap.TimeOfDay).ToList();
                    }
                    else
                    {
                        dstk = mdb.TaiKhoans.Where(c => c.LoaiTK == "admin").ToList();
                        dstk = dstk.OrderByDescending(x => x.ngayLap.Date).ThenByDescending(x => x.ngayLap.TimeOfDay).ToList();
                    }
                }
                else
                {
                    if (select == "")
                    {
                        dstk = mdb.TaiKhoans.Where(c => c.ngayLap.Day == daySearch.Day && c.ngayLap.Month == daySearch.Month && c.ngayLap.Year == daySearch.Year).OrderBy(c => c.ngayLap).ToList();
                        dstk = dstk.OrderByDescending(x => x.ngayLap.Date).ThenByDescending(x => x.ngayLap.TimeOfDay).ToList();
                    }
                    else if (select != "admin")
                    {
                        dstk = mdb.TaiKhoans.Where(c => c.LoaiTK != "admin" && c.ngayLap.Day == daySearch.Day && c.ngayLap.Month == daySearch.Month && c.ngayLap.Year == daySearch.Year).OrderBy(c => c.ngayLap).ToList();
                        dstk = dstk.OrderByDescending(x => x.ngayLap.Date).ThenByDescending(x => x.ngayLap.TimeOfDay).ToList();
                    }
                    else
                    {
                        dstk = mdb.TaiKhoans.Where(c => c.LoaiTK == "admin" && c.ngayLap.Day == daySearch.Day && c.ngayLap.Month == daySearch.Month && c.ngayLap.Year == daySearch.Year).OrderBy(c => c.ngayLap).ToList();
                        dstk = dstk.OrderByDescending(x => x.ngayLap.Date).ThenByDescending(x => x.ngayLap.TimeOfDay).ToList();
                    }
                }
                if (trangThai == true)
                {
                    int start = trang * soluong;
                    int stop = (trang * soluong) + soluong;
                    return search(start, stop, dstk, soluong);
                }
                else
                {
                    int stop = (trang * soluong) - soluong;
                    int start = stop - soluong;
                    return search(start, stop, dstk, soluong);
                }
            }
            catch
            {
                return dstk;
            }
        }
        public void PostDichVu<DichVu>(DichVu dichvu)
        {
            mdb.Set(dichvu.GetType()).Add(dichvu);
        }
        public void PostTheNap<TheNap>(TheNap thenap)
        {
            mdb.Set(thenap.GetType()).Add(thenap);
        }
        public void DeleteDichVu<DichVu>(DichVu dichvu)
        {
            mdb.Set(dichvu.GetType()).Remove(dichvu);
        }
        public void DeleteTheNap<TheNap>(TheNap thenap)
        {
            mdb.Set(thenap.GetType()).Remove(thenap);
        }
        public void Save()
        {
            mdb.SaveChanges();
        }
    }
}
