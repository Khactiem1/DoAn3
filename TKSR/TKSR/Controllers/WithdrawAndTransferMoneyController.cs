using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using TKSR.Models;
using DTO;

namespace TKSR.Controllers
{
    public class WithdrawAndTransferMoneyController : Controller
    {
        DBIO db = new DBIO();
        TheNapDBIO CardDB = new TheNapDBIO();
        [Route("GetCheckTaiKhoanChuyen")]
        public string GetCheckTaiKhoanChuyen(string id)
        {
            try
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                TaiKhoan DV = db.GetOneTaiKhoan(id);
                if (DV != null)
                {
                    if (DV.tenTK == user.tenTK)
                    {
                        return "Trùng";
                    }
                    else
                    {
                        return "true";
                    }
                }
                return "false";
            }
            catch
            {
                return "false";
            }
        }
        // GET: WithdrawAndTransferMoney
        public ActionResult RutTien()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                ClassModels model = new ClassModels();
                ViewBag.Message = "none";
                ViewBag.Link = "RutTien";
                TaiKhoan user = (TaiKhoan)Session["user"];
                ViewBag.RutTienATM = CardDB.GetYeuCauRutTienATM(user.tenTK, 10);
                model.user = db.GetOneTaiKhoan(user.tenTK);
                if (model.user.TrangThai == "lock")
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
                return View(model);
            }
            return View();
        }
        public ActionResult ChuyenTien()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                ClassModels model = new ClassModels();
                ViewBag.Message = "none";
                ViewBag.Link = "ChuyenTien";
                TaiKhoan user = (TaiKhoan)Session["user"];
                ViewBag.ChuyenTien = CardDB.GetYeuCauChuyenTien(user.tenTK, 10);
                ViewBag.NhanTien = CardDB.GetYeuCauNhanTien(user.tenTK, 10);
                model.user = db.GetOneTaiKhoan(user.tenTK);
                if (model.user.TrangThai == "lock")
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
                return View(model);
            }
            return View();
        }
        //Update lịch sử rút tiền
        [Route("UpdateLSRut")]
        public JsonResult UpdateLSRut()
        {
            JsonResult jr = new JsonResult();
            if (Session["user"] == null)
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                List<RutTienATM> YeuCauNapATM = CardDB.GetYeuCauRutTienATM(user.tenTK, 10);
                jr.Data = new
                {
                    YeuCauNapATM = YeuCauNapATM,
                    status = "OK"
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        //Update lịch sử Nhận tiền
        [Route("UpdateLSNhanTien")]
        public JsonResult UpdateLSNhanTien()
        {
            JsonResult jr = new JsonResult();
            if (Session["user"] == null)
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                List<ChuyenTien> YeuCauNapATM = CardDB.GetYeuCauNhanTien(user.tenTK, 10);
                jr.Data = new
                {
                    YeuCauNapATM = YeuCauNapATM,
                    status = "OK"
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        // Xử lý rút tiền 
        [Route("GetRutTien")]
        public JsonResult GetRutTien(FormCollection collection)
        {
            JsonResult jr = new JsonResult();
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            try
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                TaiKhoan userEdit = db.GetOneTaiKhoan(user.tenTK);
                string Pass = collection["Pass"];
                if (userEdit.TrangThai == "lock")
                {
                    jr.Data = new
                    {
                        status = "lock"
                    };
                }
                else if (Pass == userEdit.MatKhauC2)
                {
                    userEdit.NumberLock = 0;
                    db.Save();
                    string NganHang = collection["NganHang"];
                    string STK = collection["STK"];
                    string ChuTK = collection["ChuTK"];
                    string SoTien = collection["SoTien"];
                    bool Confirm = bool.Parse(collection["Confirm"]);
                    if (int.Parse(SoTien) > userEdit.SoDu)
                    {
                        jr.Data = new
                        {
                            status = "ThieuTien"
                        };
                    }
                    else
                    {
                        if (Confirm == false)
                        {
                            jr.Data = new
                            {
                                status = "OK"
                            };
                        }
                        else
                        {
                            jr.Data = new
                            {
                                status = "OK"
                            };
                            DateTime ngayRut = DateTime.Now;
                            user.SoDu -= double.Parse(SoTien);
                            userEdit.SoDu -= double.Parse(SoTien);
                            userEdit.NumberLock = 0;
                            RutTienATM YC = new RutTienATM();
                            YC.MaHoaDon = Guid.NewGuid().ToString();
                            YC.TenTaiKhoan = userEdit.tenTK;
                            YC.NganHang = NganHang;
                            YC.STK = STK;
                            YC.ChuTK = ChuTK;
                            YC.SoTien = int.Parse(SoTien);
                            YC.NgayRut = ngayRut;
                            YC.TrangThai = "Chờ duyệt";
                            CardDB.PostRutATM(YC);
                            CardDB.Save();
                            db.Save();
                        }

                    }
                }
                else
                {
                    if (userEdit.NumberLock >= 4)
                    {
                        userEdit.TrangThai = "lock";
                        userEdit.NumberLock++;
                        db.Save();
                        jr.Data = new
                        {
                            status = "lock",
                            numberLock = 0
                        };
                    }
                    else
                    {
                        userEdit.NumberLock++;
                        int number = 5 - userEdit.NumberLock;
                        db.Save();
                        jr.Data = new
                        {
                            status = "F",
                            numberLock = number
                        };
                    }
                }
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jr.Data = new
                {
                    status = "ER"
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
        // Xử lý chuyển tiền
        [Route("GetChuyenTien")]
        public JsonResult GetChuyenTien(FormCollection collection)
        {
            JsonResult jr = new JsonResult();
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            try
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                TaiKhoan userEdit = db.GetOneTaiKhoan(user.tenTK);
                string Pass = collection["Pass"];
                if (userEdit.TrangThai == "lock")
                {
                    jr.Data = new
                    {
                        status = "lock"
                    };
                }
                else if (Pass == userEdit.MatKhauC2)
                {
                    userEdit.NumberLock = 0;
                    db.Save();
                    string TenTaiKhoanNhan = collection["TenTaiKhoanNhan"];
                    string LoiNhan = collection["LoiNhan"];
                    string SoTien = collection["SoTien"];
                    bool Confirm = bool.Parse(collection["Confirm"]);
                    if (int.Parse(SoTien) > userEdit.SoDu)
                    {
                        jr.Data = new
                        {
                            status = "ThieuTien"
                        };
                    }
                    else
                    {
                        if (Confirm == false)
                        {
                            jr.Data = new
                            {
                                status = "OK"
                            };
                        }
                        else
                        {
                            TaiKhoan TKNhan = db.GetOneTaiKhoan(TenTaiKhoanNhan);
                            if (TKNhan.tenTK == user.tenTK || TKNhan == null)
                            {
                                jr.Data = new
                                {
                                    status = "ER"
                                };
                            }
                            else
                            {
                                DateTime ngayChuyen = DateTime.Now;
                                user.SoDu -= double.Parse(SoTien);
                                userEdit.SoDu -= double.Parse(SoTien);
                                userEdit.NumberLock = 0;
                                TKNhan.SoDu += double.Parse(SoTien);
                                ChuyenTien YC = new ChuyenTien();
                                YC.MaHoaDon = Guid.NewGuid().ToString();
                                YC.TenTaiKhoanChuyen = userEdit.tenTK;
                                YC.TenTaiKhoanNhan = TKNhan.tenTK;
                                YC.SoTien = int.Parse(SoTien);
                                YC.NgayChuyen = ngayChuyen;
                                YC.LoiNhan = LoiNhan;
                                CardDB.PostChuyenTien(YC);
                                CardDB.Save();
                                db.Save();
                                jr.Data = new
                                {
                                    status = "OK",
                                    YC = YC
                                };
                            }
                        }

                    }
                }
                else
                {
                    if (userEdit.NumberLock >= 4)
                    {
                        userEdit.TrangThai = "lock";
                        userEdit.NumberLock++;
                        db.Save();
                        jr.Data = new
                        {
                            status = "lock",
                            numberLock = 0
                        };
                    }
                    else
                    {
                        userEdit.NumberLock++;
                        int number = 5 - userEdit.NumberLock;
                        db.Save();
                        jr.Data = new
                        {
                            status = "F",
                            numberLock = number
                        };
                    }
                }
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jr.Data = new
                {
                    status = "ER"
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
    }
}