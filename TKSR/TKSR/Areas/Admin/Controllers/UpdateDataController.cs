using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;
using DAL;
using TKSR.Controllers;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace TKSR.Areas.Admin.Controllers
{
    public class UpdateDataController : ApiController
    {
        DBIO db = new DBIO();
        TheNapDBIO TheNapDB = new TheNapDBIO();
        [Route("GetLoadYeuCauGachThe")]
        public List<YeuCauGachThe> GetLoadYeuCauGachThe(string sl) 
        {
            return TheNapDB.GetAllYeuCauGachThes(int.Parse(sl));
        }
        [Route("PostConfirmCardTrue")]
        public bool PostConfirmCardTrue(string id)
        {
            try
            {
                YeuCauGachThe yeuCau = TheNapDB.GetYeuCau(id);
                TaiKhoan editTK = db.GetOneTaiKhoan(yeuCau.TenTaiKhoan);
                yeuCau.TrangThai = "Hoàn thành";
                editTK.SoDu += double.Parse(yeuCau.TienThucNhan);
                TheNapDB.Save();
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("PostConfirmCardFalse")]
        public bool PostConfirmCardFalse(string id)
        {
            try
            {
                YeuCauGachThe yeuCau = TheNapDB.GetYeuCau(id);
                yeuCau.TrangThai = "Thẻ không đúng";
                TheNapDB.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("GetLoadYeuCauNapATM")]
        public List<YeuCauNapATM> GetLoadYeuCauNapATM(string sl)
        {
            return TheNapDB.GetAllYeuCauNapTienATM(int.Parse(sl));
        }
        [Route("PostConfirmATMTrue")]
        public bool PostConfirmATMTrue(string id)
        {
            try
            {
                YeuCauNapATM yeuCau = TheNapDB.GetYeuCauATM(id);
                TaiKhoan editTK = db.GetOneTaiKhoan(yeuCau.TenTaiKhoan);
                yeuCau.TrangThai = "Hoàn thành";
                editTK.SoDu += double.Parse(yeuCau.SoTien + "");
                TheNapDB.Save();
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("PostConfirmATMFalse")]
        public bool PostConfirmATMFalse(string id)
        {
            try
            {
                YeuCauNapATM yeuCau = TheNapDB.GetYeuCauATM(id);
                yeuCau.TrangThai = "AD không nhận được tiền";
                TheNapDB.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }




        [Route("GetLoadYeuCauRutATM")]
        public List<RutTienATM> GetLoadYeuCauRutATM(string sl)
        {
            return TheNapDB.GetAllYeuCauRutTienATM(int.Parse(sl));
        }
        [Route("PostConfirmRutATMTrue")]
        public bool PostConfirmRutATMTrue(string id)
        {
            try
            {
                RutTienATM yeuCau = TheNapDB.GetYeuCauRutATM(id);
                TaiKhoan editTK = db.GetOneTaiKhoan(yeuCau.TenTaiKhoan);
                yeuCau.TrangThai = "Hoàn thành";
                TheNapDB.Save();
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("PostConfirmRutATMFalse")]
        public bool PostConfirmRutATMFalse(string id)
        {
            try
            {
                RutTienATM yeuCau = TheNapDB.GetYeuCauRutATM(id);
                TaiKhoan editTK = db.GetOneTaiKhoan(yeuCau.TenTaiKhoan);
                editTK.SoDu += double.Parse(yeuCau.SoTien + "");
                yeuCau.TrangThai = "Tài khoản gửi AD sai";
                TheNapDB.Save();
                db.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }




        [Route("PostOneNhaMang")]
        public bool PostOneNhaMang([FromBody] ImageCard value)
        {
            try
            {
                UserSupportController Upload = new UserSupportController();
                UserProfile file = new UserProfile();
                file.UserId = value.TenNhaMang;
                file.UserAvatarBase64String = value.Logo;
                Upload.Post(file, "ImgCard", ".png");
                NhaMang NewNhaMang = new NhaMang();
                NewNhaMang.TenNhaMang = value.TenNhaMang;
                NewNhaMang.Logo = value.TenNhaMang + ".png";
                TheNapDB.PostNhaMang(NewNhaMang);
                TheNapDB.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("RemoveNhaMang")]
        public bool PostRemoveNhaMang(string id)
        {
            try
            {
                TheNap DV = TheNapDB.GetCheckNhaMangThe(id);
                if (DV != null)
                {
                    return false;
                }
                else
                {
                    NhaMang NM = TheNapDB.GetNhaMang(id);
                    TheNapDB.DeleteNhaMang(NM);
                    TheNapDB.Save();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
    public class ImageCard
    {
        public string TenNhaMang { get; set; }
        public string Logo { get; set; }
    }
}
