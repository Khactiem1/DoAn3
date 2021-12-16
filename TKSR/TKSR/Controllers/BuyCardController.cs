using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using TKSR.Models;
using DAL;

namespace TKSR.Controllers
{
    public class BuyCardController : Controller
    {
        DBIO db = new DBIO();
        TheNapDBIO CardDB = new TheNapDBIO();
        // GET: BuyCard
        //Trang chủ mua thẻ điện thoại
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                List<NhaMang> DSNhaMang = CardDB.GetAllNhaMang();
                ViewBag.DSNhaMang = DSNhaMang;
                ViewBag.ChietKhau = CardDB.GetAllChietKhau();
                ChietKhau PriceScreen = CardDB.GetChietKhau(DSNhaMang[0].TenNhaMang, "10000");
                ViewBag.PriceScreen = PriceScreen.ChietKhauBan * 10000;
                ClassModels model = new ClassModels();
                ViewBag.Message = "none";
                ViewBag.Link = "TheNap";
                TaiKhoan user = (TaiKhoan)Session["user"];
                model.user = db.GetOneTaiKhoan(user.tenTK);
                model.DSTheMua = db.Get10MuaTheNap(model.user.tenTK);
                if (model.user.TrangThai == "lock")
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
                return View(model);
            }
            return View();
        }
        // Lấy ra chiết khẩu của thẻ cần mua
        [Route("GetChietKhau")]
        public string GetChietKhau(string id)
        {
            string[] str = id.Split('-');
            ChietKhau PriceScreen = CardDB.GetChietKhau(str[0], str[1]);
            if(str[2] == "Ban" || str[2] == "ban")
            {
                return PriceScreen.ChietKhauBan + "";
            }
            else
            {
                return PriceScreen.ChietKhauNap + "";
            }
        }
        //Update chiết khấu của thẻ nạp mỗi 3s
        [Route("UpdateChietKhau")]
        public JsonResult UpdateChietKhau()
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
                List<NhaMang> DSNhaMang = CardDB.GetAllNhaMang();
                List<ChietKhau> DSChietKhau = CardDB.GetAllChietKhau();
                jr.Data = new
                {
                    DSNhaMang = DSNhaMang,
                    DSChietKhau = DSChietKhau,
                    status = "OK"
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        //Update tình trạng duyệt thẻ sau mỗi 3s
        [Route("UpdateTheNap")]
        public JsonResult UpdateTheNap()
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
                List<YeuCauGachThe> YeuCauGachThe = CardDB.GetYeuCauGachThes(user.tenTK, 10);
                jr.Data = new
                {
                    YeuCauGachThe = YeuCauGachThe,
                    status = "OK"
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        [Route("UpdateLSNap")]
        public JsonResult UpdateLSNap()
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
                List<YeuCauNapATM> YeuCauNapATM = CardDB.GetYeuCauNapTienATM(user.tenTK, 10);
                jr.Data = new
                {
                    YeuCauNapATM = YeuCauNapATM,
                    status = "OK"
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        //Trang chủ nạp tiền vào bằng Gạch thẻ
        public ActionResult NapTien()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                List<NhaMang> DSNhaMang = CardDB.GetAllNhaMang();
                ViewBag.DSNhaMang = DSNhaMang;
                ViewBag.ChietKhau = CardDB.GetAllChietKhau();
                ClassModels model = new ClassModels();
                ViewBag.Message = "none";
                ViewBag.Link = "NapTien";
                TaiKhoan user = (TaiKhoan)Session["user"];
                ViewBag.DSTheNap = CardDB.GetYeuCauGachThes(user.tenTK,10);
                model.user = db.GetOneTaiKhoan(user.tenTK);
                if (model.user.TrangThai == "lock")
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
                return View(model);
            }
            return View();
        }
        //Các xử lý mua thẻ nạp
        [Route("GetBuyCard")]
        public JsonResult GetBuyCard(FormCollection collection)
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
                string pass = collection["Pass"];
                if (userEdit.TrangThai == "lock")
                {
                    jr.Data = new
                    {
                        status = "lock"
                    };
                }
                else if (pass == userEdit.MatKhauC2)
                {
                    string NhaMang = collection["NhaMang"];
                    string MenhGia = collection["MenhGia"];
                    string SoLuong = collection["SoLuong"];
                    bool Confirm = bool.Parse(collection["Confirm"]);
                    List<TheNap> DSThe = new List<TheNap>();
                    DSThe = db.GetBuyThe(NhaMang,int.Parse(MenhGia),int.Parse(SoLuong));
                    userEdit.NumberLock = 0;
                    db.Save();
                    if (DSThe.Count < int.Parse(SoLuong))
                    {
                        jr.Data = new
                        {
                            status = "KhongDu"
                        };
                    }
                    else
                    {
                        string ChietKhau = GetChietKhau(NhaMang + "-" + MenhGia + "-Ban");
                        double Price = int.Parse(MenhGia) * int.Parse(SoLuong) * double.Parse(ChietKhau);
                        if (Price > userEdit.SoDu)
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
                                DateTime ngayMua = DateTime.Now;
                                List<MuaThe> DSTheMua = new List<MuaThe>();
                                for(int i = 0; i < DSThe.Count; i++)
                                {
                                    MuaThe the = new MuaThe();
                                    the.tenTK = userEdit.tenTK;
                                    the.MaThe = DSThe[i].MaThe;
                                    the.Seri = DSThe[i].Seri;
                                    the.NhaMang = DSThe[i].NhaMang;
                                    the.MenhGia = DSThe[i].MenhGia;
                                    the.NgayMua = ngayMua;
                                    DSTheMua.Add(the);
                                    db.PostMuaThe(the);
                                    db.DeleteTheNap(DSThe[i]);
                                }
                                string str = Price + "";
                                user.SoDu -= double.Parse(str);
                                userEdit.SoDu -= double.Parse(str);
                                userEdit.NumberLock = 0;
                                db.Save();
                                jr.Data = DSTheMua;
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
        //Các xử lý nạp tiền vào tài khoản bằng gạch thẻ
        [Route("GetRechargeCard")]
        public JsonResult GetRechargeCard(FormCollection collection)
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
                if (userEdit.TrangThai == "lock")
                {
                    jr.Data = new
                    {
                        status = "lock"
                    };
                }
                else
                {
                    string NhaMang = collection["NhaMang"];
                    string MaThe = collection["MaThe"];
                    string Seri = collection["Seri"];
                    string MenhGia = collection["MenhGia"];
                    bool Confirm = bool.Parse(collection["Confirm"]);
                    string ChietKhau = GetChietKhau(NhaMang + "-" + MenhGia + "-Nap");
                    double Monney = double.Parse(ChietKhau) * double.Parse(MenhGia);
                    if (Confirm == false)
                    {
                        jr.Data = new
                        {
                            status = Monney + ""
                        };
                    }
                    else
                    {
                        DateTime ngayNap = DateTime.Now;
                        YeuCauGachThe GachThe = new YeuCauGachThe();
                        GachThe.MaHoaDon = Guid.NewGuid().ToString();
                        GachThe.TenTaiKhoan = user.tenTK;
                        GachThe.NhaMang = NhaMang;
                        GachThe.MaThe = MaThe;
                        GachThe.Seri = Seri;
                        GachThe.MenhGia = int.Parse(MenhGia);
                        GachThe.TienThucNhan = Monney + "";
                        GachThe.NgayNap = ngayNap;
                        GachThe.TrangThai = "Chờ duyệt";
                        CardDB.PostGachThe(GachThe);
                        CardDB.Save();
                        jr.Data = new
                        {
                            status = Monney + "-" + ngayNap.ToString("dd/MM/yyyy HH:mm:ss")
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
        [Route("PostNapTienATM")]
        public JsonResult PostNapTienATM(FormCollection collection)
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
                if (userEdit.TrangThai == "lock")
                {
                    jr.Data = new
                    {
                        status = "lock"
                    };
                }
                else
                {
                    string NganHang = collection["NganHang"];
                    string SoTien = collection["SoTien"];
                    YeuCauNapATM YC = new YeuCauNapATM();
                    YC.MaHoaDon = Guid.NewGuid().ToString();
                    YC.TenTaiKhoan = userEdit.tenTK;
                    YC.NganHang = NganHang;
                    YC.SoTien = int.Parse(SoTien);
                    YC.NgayNap = DateTime.Now;
                    YC.TrangThai = "Chờ duyệt";
                    CardDB.PostNapATM(YC);
                    CardDB.Save();
                    jr.Data = new
                    {
                        status = "OK"
                    };
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
        public ActionResult NapTienBangATM()
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
                ViewBag.Link = "NapTienATM";
                TaiKhoan user = (TaiKhoan)Session["user"];
                ViewBag.NapTienATM = CardDB.GetYeuCauNapTienATM(user.tenTK, 10);
                model.user = db.GetOneTaiKhoan(user.tenTK);
                if (model.user.TrangThai == "lock")
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
                return View(model);
            }
            return View();
        }
    }
}