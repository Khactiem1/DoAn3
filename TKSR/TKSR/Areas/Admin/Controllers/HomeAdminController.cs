using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using DAL;

namespace TKSR.Areas.Admin.Controllers
{ 
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        DBIO db = new DBIO();
        TheNapDBIO CardDB = new TheNapDBIO();
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                if (user.LoaiTK == "admin")
                {
                    ViewBag.Index = "bar__list-text__active";
                    ViewBag.Name = user.tenTK;
                    ViewBag.Number = 1;
                    DateTime dateSearch = new DateTime();
                    List<TaiKhoan> DSTK = db.GetAllTaiKhoan(15 , 0,true,"", dateSearch);
                    return View(DSTK);
                }
                else
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
            }
            return View();
        }
        public ActionResult QLDichVu()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                if (user.LoaiTK == "admin")
                {
                    ViewBag.QLDichVu = "bar__list-text__active";
                    ViewBag.Name = user.tenTK;
                    ViewBag.Number = 1;
                    List<DichVu> DSDV = db.GetAllDichVu(10,0,"","","","",true,0);
                    return View(DSDV);
                }
                else
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
            }
            return View();
        }
        public ActionResult QLTheNap()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                if (user.LoaiTK == "admin")
                {
                    ViewBag.DSNhaMang = CardDB.GetAllNhaMang();
                    ViewBag.QLTheNap = "bar__list-text__active";
                    ViewBag.Name = user.tenTK;
                    ViewBag.Number = 1;
                    List<TheNap> DSTN = db.GetAllTheNap(10, 0, "", "", "", "", true, 0);
                    return View(DSTN);
                }
                else
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
            }
            return View();
        }
        public ActionResult PhanHoi()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                if (user.LoaiTK == "admin")
                {
                    ViewBag.QLPhanHoi = "bar__list-text__active";
                    ViewBag.Name = user.tenTK;
                    ViewBag.Number = 1;
                    List<PhanHoi> DSPH = db.GetAllPhanHoiUser();
                    return View(DSPH);
                }
                else
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
            }
            return View();
        }
        public ActionResult ChietKhau()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                if (user.LoaiTK == "admin")
                {
                    ViewBag.DSNhaMang = CardDB.GetAllNhaMang();
                    ViewBag.DSChietKhau = CardDB.GetAllChietKhau();
                    ViewBag.ChietKhau = "bar__list-text__active";
                    ViewBag.Name = user.tenTK;
                    return View();
                }
                else
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
            }
            return View();
        }
    }
}