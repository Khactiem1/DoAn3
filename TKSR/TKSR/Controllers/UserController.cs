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
    public class UserController : Controller
    {
        DBIO db = new DBIO();
        [Route("GetCheckSecurity")]
        public bool GetCheckSecurity(string id)
        {
            bool hasUpper = false; bool hasLower = false; bool hasDigit = false;
            for (int i = 0; i < id.Length && !(hasUpper && hasLower && hasDigit); i++)
            {
                char c = id[i];
                if (!hasUpper) hasUpper = char.IsUpper(c);
                if (!hasLower) hasLower = char.IsLower(c);
                if (!hasDigit) hasDigit = char.IsDigit(c);
            }
            return hasUpper && hasLower && hasDigit;
        }
        // GET: User
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                ViewBag.Message = "none";
                ViewBag.Link = "User";
                ViewBag.Index = "bar__list-text__active";
                ClassModels model = new ClassModels();
                TaiKhoan user = (TaiKhoan)Session["user"];
                model.user = db.GetOneTaiKhoan(user.tenTK);
                if (model.user.TrangThai == "lock")
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
                return View(model);
            }
            return View();
        }
        public ActionResult Support()
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            else
            {
                ViewBag.Message = "none";
                ViewBag.Link = "Support";
                ClassModels model = new ClassModels();
                TaiKhoan user = (TaiKhoan)Session["user"];
                model.user = db.GetOneTaiKhoan(user.tenTK);
                if (model.user.TrangThai == "lock")
                {
                    Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
                }
                return View(model);
            }
            return View();
        }
        [Route("GetCheckPass1")]
        public bool GetCheckPass1(string id)
        {
            TaiKhoan user = (TaiKhoan)Session["user"];
            if(user.MatKhau == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [Route("GetCheckPass2")]
        public bool GetCheckPass2(string id)
        {
            TaiKhoan user = (TaiKhoan)Session["user"];
            if (user.MatKhauC2 == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [Route("PostEditSDT")]
        public bool PostEditSDT(string id)
        {
            if (Session["user"] == null)
            {
                Session["user"] = null;
                Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
            }
            try
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                if (user.sdt == id)
                {
                    return false;
                }
                else
                {
                    TaiKhoan setUser = db.GetOneTaiKhoan(user.tenTK);
                    setUser.sdt = id;
                    db.Save();
                    user.sdt = id;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        [Route("PostEditPass1")]
        public JsonResult PostEditPass1(FormCollection collection)
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
                string pass = collection["pass"];
                string Newpass = collection["newPass"];
                if(pass == user.MatKhau)
                {
                    if (Newpass != user.MatKhauC2)
                    {
                        TaiKhoan setUser = db.GetOneTaiKhoan(user.tenTK);
                        setUser.MatKhau = Newpass;
                        db.Save();
                        user.MatKhau = Newpass;
                        jr.Data = new
                        {
                            status = "OK"
                        };
                    }
                    else
                    {
                        jr.Data = new
                        {
                            status = "F"
                        };
                    }
                }
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jr.Data = new
                {
                    status = "F"
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("PostEditPass2")]
        public JsonResult PostEditPass2(FormCollection collection)
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
                string pass = collection["pass"];
                string Newpass = collection["newPass"];
                if (pass == user.MatKhauC2)
                {
                    if (Newpass != user.MatKhau)
                    {
                        TaiKhoan setUser = db.GetOneTaiKhoan(user.tenTK);
                        setUser.MatKhauC2 = Newpass;
                        db.Save();
                        user.MatKhauC2 = Newpass;
                        jr.Data = new
                        {
                            status = "OK"
                        };
                    }
                    else
                    {
                        jr.Data = new
                        {
                            status = "F"
                        };
                    }
                }
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jr.Data = new
                {
                    status = "F"
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("PostRequestSupport")]
        public JsonResult PostRequestSupport(FormCollection collection)
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
                string title = collection["title"];
                string content = collection["content"];
                string img = collection["img"];
                DateTime nowh = DateTime.Now;
                List<PhanHoi> dsPhanHoi = new List<PhanHoi>();
                dsPhanHoi = db.GetPhanHoiUser(user.tenTK);
                int src = Rd(dsPhanHoi);
                PhanHoi newPH = new PhanHoi();
                newPH.TaiKhoan = user.tenTK;
                newPH.TieuDe = title;
                newPH.NoiDung = content;
                if (img == "NotValue")
                {
                    newPH.Img = "NotValue";
                }
                else
                {
                    newPH.Img = user.tenTK + src + ".jpg";
                    UserSupportController Upload = new UserSupportController();
                    UserProfile file = new UserProfile();
                    file.UserId = user.tenTK + src;
                    file.UserAvatarBase64String = img;
                    Upload.Post(file, "ImgSupport", ".jpg");
                }
                newPH.NgayPhanHoi = nowh;
                db.PostPhanHoi(newPH);
                jr.Data = new
                {
                    status = "OK"
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public int Rd(List<PhanHoi> listNumbers1)
        {
            int number;
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < listNumbers1.Count; i++)
            {
                if (listNumbers1[i].Img != "NotValue")
                {
                    string src = listNumbers1[i].Img.Substring(listNumbers1[i].TaiKhoan.Length, listNumbers1[i].Img.Length - listNumbers1[i].TaiKhoan.Length - 4);
                    listNumbers.Add(int.Parse(src)); 
                }
            }
            Random rand = new Random();
            do
            {
                number = rand.Next(1000, 999999999);
            } while (listNumbers.Contains(number));
            return number;
        }
    }
}