using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;
using DAL;

namespace TKSR.Areas.Admin.Controllers
{
    public class QLTaiKhoanController : ApiController
    {
        DBIO db = new DBIO();
        TheNapDBIO TheNapDB = new TheNapDBIO();
        [Route("Search")]
        public List<TaiKhoan> GetOneUser(string Name)
        {
            return db.GetSearchTaiKhoan(Name);
        }
        [Route("GetCheckTaiKhoan")]
        public bool GetCheckTaiKhoan(string id)
        {
            try
            {
                TaiKhoan DV = db.GetOneTaiKhoan(id);
                if (DV != null)
                {
                    if (DV.tenTK == id)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return true;
            }
        }


        [Route("OpenAccount")]
        public bool PostOpenAccount(string id)
        {
            try
            {
                TaiKhoan DV = db.GetOneTaiKhoan(id);
                DV.TrangThai = "active";
                DV.NumberLock = 0;
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("LockAccount")]
        public bool PostLockAccount(string id)
        {
            try
            {
                TaiKhoan DV = db.GetOneTaiKhoan(id);
                DV.TrangThai = "lock";
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [Route("GetCheckEmail")]
        public bool GetCheckEmail(string id)
        {
            try
            {
                TaiKhoan DV = db.GetCheckEmail(id);
                if (DV != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
        [Route("SearchOneDichVu")]
        public bool GetCheckDichVu(string id)
        {
            try
            {
                DichVu DV = db.GetCheckIdDichVu(id);
                if (DV != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
        [Route("SearchOneThe")]
        public bool GetCheckThe(string id)
        {
            try
            {
                TheNap The = db.GetCheckMaThe(id);
                if (The != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        [Route("SearchOneNhaMang")]
        public bool GetCheckNhaMang(string id)
        {
            try
            {
                NhaMang The = TheNapDB.GetNhaMang(id);
                if (The != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        // GET: api/QLTaiKhoan/5
        [Route("GetDSTaiKhoan")]
        public List<TaiKhoan> Get(string id)
        {
            string[] str = id.Split('z');
            DateTime dateSearch = new DateTime();
            try
            {
                if (str[3] != "")
                {
                    string[] strDate = str[3].Split('-');
                    dateSearch = new DateTime(day: int.Parse(strDate[0]), month: int.Parse(strDate[1]), year: int.Parse(strDate[2]));
                }
            }
            catch
            {
                dateSearch = new DateTime();
            }
            return db.GetAllTaiKhoan(int.Parse(str[4]),int.Parse(str[0]),bool.Parse(str[1]),str[2],dateSearch);
        }

        [Route("GetAllDichVu")]
        public List<DichVu> GetAllDichVu(string id)
        {
            string[] str = id.Split('*'); //0 là trang hiện tại, 1 là id 2 là tên 3 là ncc 4 là dịch vụ 5 là trạng thái
            string strDichVu = "";
            if(str[4] == "dien")
            {
                strDichVu = "Điện";
            }
            else if (str[4] == "nuoc")
            {
                strDichVu = "Nước";
            }
            else if (str[4] == "vaytieudung")
            {
                strDichVu = "vay tiêu dùng";
            }
            else if (str[4] == "truyenhinh")
            {
                strDichVu = "Truyền hình";
            }
            else if (str[4] == "internet")
            {
                strDichVu = "Internet";
            }
            else if (str[4] == "chungcu")
            {
                strDichVu = "Chung cư";
            }
            int delete = 0;
            try
            {
                delete = int.Parse(str[6]);
                delete = int.Parse(str[7]) - delete;
            }
            catch
            {
                delete = 0;
            }
            return db.GetAllDichVu(int.Parse(str[7]), int.Parse(str[0]),str[1], str[2], str[3], strDichVu, bool.Parse(str[5]),delete);
        }

        [Route("PostOneDichVu")]
        public bool PostDichVu([FromBody]DichVu value)
        {
            try
            {
                db.PostDichVu(value);
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Route("PostEditDichVu")]
        public bool PostEditDichVu([FromBody]DichVu value)
        {
            try
            {
                DichVu DV = db.GetCheckIdDichVu(value.MaDichVu);
                if (DV == null)
                {
                    return false;
                }
                else
                {
                    DV.TenDichVu = value.TenDichVu;
                    DV.TenKhachHang = value.TenKhachHang;
                    DV.TienThanhToan = value.TienThanhToan;
                    DV.DoiTac = value.DoiTac;
                    db.Save();
                    return true;
                }
            }
            catch
            {
                return false;
            }        
        }
        // PUT: api/QLTaiKhoan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/QLTaiKhoan/5
        [Route("RemoveDichVu")]
        public bool PostRemoveDichVu(string id)
        {
            try
            {
                DichVu DV = db.GetCheckIdDichVu(id);
                if (DV == null)
                {
                    return false;
                }
                else
                {
                    if(DV.MaDichVu == id)
                    {
                        db.DeleteDichVu(DV);
                        db.Save();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        [Route("PostOneTheNap")]
        public bool PostOneTheNap([FromBody]TheNap value)
        {
            try
            {
                db.PostTheNap(value);
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("GetAllTheNap")]
        public List<TheNap> GetAllTheNap(string id)
        {
            string[] str = id.Split('*'); //0 là trang hiện tại, 1 là mã 2 là seri 3 là menhgia 4 là Nhà mạng 5 là trạng thái
            int delete = 0;
            try
            {
                delete = int.Parse(str[6]);
                delete = int.Parse(str[7]) - delete;
            }
            catch
            {
                delete = 0;
            }
            return db.GetAllTheNap(int.Parse(str[7]), int.Parse(str[0]), str[1], str[2], str[3], str[4], bool.Parse(str[5]), delete);
        }
        [Route("PostEditTheNap")]
        public bool PostEditTheNap([FromBody]TheNap value)
        {
            try
            {
                TheNap DV = db.GetCheckMaThe(value.MaThe);
                if (DV == null)
                {
                    return false;
                }
                else
                {
                    DV.Seri = value.Seri;
                    DV.MenhGia = value.MenhGia;
                    DV.NhaMang = value.NhaMang;
                    db.Save();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        [Route("RemoveTheNap")]
        public bool PostRemoveTheNap(string id)
        {
            try
            {
                TheNap DV = db.GetCheckMaThe(id);
                if (DV == null)
                {
                    return false;
                }
                else
                {
                    if (DV.MaThe == id)
                    {
                        db.DeleteTheNap(DV);
                        db.Save();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        [Route("PostEditChietKhau")]
        public bool PostEditChietKhau([FromBody] Customer value)
        {
            try
            {
                string[] DSNhaMang = value.NhaMang;
                string[] DSChietKhauBan = value.Ban;
                string[] DSChietKhauNhap = value.Nhap;
                bool check = true;
                foreach(string items in DSChietKhauBan)
                {
                    if(double.Parse(items) <= 0.3 && double.Parse(items) >= 2)
                    {
                        check = false;
                        break;
                    }
                }
                foreach (string items in DSChietKhauNhap)
                {
                    if (double.Parse(items) <= 0.3 && double.Parse(items) >= 2)
                    {
                        check = false;
                        break;
                    }
                }
                if(check == false)
                {
                    return false;
                }
                else
                {
                    string[] MenhGia = new string[] { "10000", "20000", "30000", "50000", "100000", "200000", "300000", "500000", "1000000"};
                    int indexChietKhauBan = 0;
                    int quantityChietKhauBan = 0;
                    foreach (string items in DSNhaMang)
                    {
                        int indexNhaMang = -1;
                        indexChietKhauBan += 1;
                        for(int i = quantityChietKhauBan; i < indexChietKhauBan * 9; i++)
                        {
                            indexNhaMang++;
                            ChietKhau updateChietKhau = TheNapDB.GetChietKhau(items,MenhGia[indexNhaMang]);
                            quantityChietKhauBan++;
                            updateChietKhau.ChietKhauBan = double.Parse(DSChietKhauBan[i]);
                            TheNapDB.Save();
                        }
                    }
                    int indexChietKhauNhap = 0;
                    int quantityChietKhauNhap = 0;
                    foreach (string items in DSNhaMang)
                    {
                        int indexNhaMang = -1;
                        indexChietKhauNhap += 1;
                        for (int i = quantityChietKhauNhap; i < indexChietKhauNhap * 9; i++)
                        {
                            indexNhaMang++;
                            ChietKhau updateChietKhau = TheNapDB.GetChietKhau(items, MenhGia[indexNhaMang]);
                            quantityChietKhauNhap++;
                            updateChietKhau.ChietKhauNap = double.Parse(DSChietKhauNhap[i]);
                            TheNapDB.Save();
                        }
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
    public class Customer
    {
        public string[] NhaMang { get; set; }
        public string[] Ban { get; set; }
        public string[] Nhap { get; set; }
    }
}
