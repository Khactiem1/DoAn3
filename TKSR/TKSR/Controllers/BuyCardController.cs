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
        // GET: BuyCard
        public ActionResult Index()
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
                ViewBag.Link = "TheNap";
                TaiKhoan user = (TaiKhoan)Session["user"];
                model.user = db.GetOneTaiKhoan(user.tenTK);
                model.DSTheMua = db.Get10MuaTheNap(model.user.tenTK);
                return View(model);
            }
            return View();
        }
        public ActionResult NapTien()
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
                ViewBag.Link = "NapTien";
                TaiKhoan user = (TaiKhoan)Session["user"];
                model.user = db.GetOneTaiKhoan(user.tenTK);
                //model.DSTheMua = db.Get10MuaTheNap(model.user.tenTK);
                return View(model);
            }
            return View();
        }

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
                if (pass == userEdit.MatKhauC2)
                {
                    string NhaMang = collection["NhaMang"];
                    string MenhGia = collection["MenhGia"];
                    string SoLuong = collection["SoLuong"];
                    bool Confirm = bool.Parse(collection["Confirm"]);
                    List<TheNap> DSThe = new List<TheNap>();
                    DSThe = db.GetBuyThe(NhaMang,int.Parse(MenhGia),int.Parse(SoLuong));
                    if(DSThe.Count < int.Parse(SoLuong))
                    {
                        jr.Data = new
                        {
                            status = "KhongDu"
                        };
                    }
                    else
                    {
                        double Price = int.Parse(MenhGia) * int.Parse(SoLuong) * 0.96;
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
                                user.SoDu -= int.Parse(str);
                                userEdit.SoDu -= int.Parse(str);
                                db.Save();
                                jr.Data = DSTheMua;
                            }

                        }
                        
                    }
                }
                else
                {
                    jr.Data = new
                    {
                        status = "F"
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
    }
}