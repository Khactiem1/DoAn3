using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using DAL;
using System.Net.Mail;
using System.Net;
using System.Text;
using TKSR.Models;

namespace TKSR.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DBIO db = new DBIO();
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                ClassModels model = new ClassModels();
                ViewBag.Message = "active__login";
                return View(model);
            }
            else
            {
                ClassModels model = new ClassModels();
                ViewBag.Message = "none";
                TaiKhoan user = (TaiKhoan)Session["user"];
                TaiKhoan UserEdit = db.GetOneTaiKhoan(user.tenTK);
                string ip = Request.UserHostAddress;
                UserEdit.DacDiem = ip;
                db.Save();
                model.user = UserEdit;
                if (model.user.TrangThai == "lock")
                {
                    model.user = null;
                    Session["user"] = null;
                    ViewBag.Message = "active__login";
                    return View(model);
                }
                return View(model);
            }
        }
        [Route("GetTaiKhoan")]
        public string GetTaiKhoan()
        {
            if (Session["user"] == null)
            {
                return "false";
            }
            else
            {
                TaiKhoan user = (TaiKhoan)Session["user"];
                TaiKhoan userGet = db.GetOneTaiKhoan(user.tenTK);
                return userGet.SoDu + ""; 
            }
        }
        public void Logout()
        {
            Session["user"] = null;
            Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Authority);
        }
        //Kiểm tra đăng nhập
        [HttpPost]
        public JsonResult CheckLogin(FormCollection collection)
        {
            JsonResult jr = new JsonResult();
            try
            {
                string uid = collection["uid"];
                string pass = collection["pass"];
                TaiKhoan tk = db.GetOneTaiKhoan(uid);
                if (tk == null)
                {
                    jr.Data = new
                    {
                        status = "F"
                    };
                }
                else
                {
                    if (tk.TrangThai == "lock")
                    {
                        jr.Data = new
                        {
                            status = "lock"
                        };
                    }
                    else if (tk.MatKhau == pass && tk.tenTK == uid)
                    {
                        Session["user"] = tk;
                        Session.Timeout = 60;
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
        //Gửi mã về mail
        public int Rd(List<Register> listNumbers1)
        {
            int number;
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < listNumbers1.Count; i++)
            {
                listNumbers.Add(int.Parse(listNumbers1[i].Code));
            }
            Random rand = new Random();
            do
            {
                number = rand.Next(1000, 9999);
            } while (listNumbers.Contains(number));
            return number;
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        /// <summary>
        /// Tạo ra một chuỗi ngẫu nhiên với độ dài cho trước
        /// </summary>
        /// <param name="size">Kích thước của chuỗi </param>
        /// <param name="lowerCase">Nếu đúng, tạo ra chuỗi chữ thường</param>
        /// <returns>Random string</returns>
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public string RdPass(string PassFor)
        {
            string pass = "";
            do
            {
                pass = GetPassword();
            } while (pass == PassFor);
            return pass;
        }
        public void SenMail(string toMail, string title, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient sever = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("nguyenkhactiem2k1@gmail.com");
            mail.To.Add(toMail);
            mail.Subject = title;
            mail.Body = body;
            sever.Port = 587;
            sever.Credentials = new NetworkCredential("nguyenkhactiem2k1@gmail.com", "KhacTiemNa");
            sever.EnableSsl = true;
            sever.Send(mail);
        }
        [HttpPost]
        public void Register(FormCollection collection)
        {
            try
            {
                List<Register> request = new List<Register>();
                request = db.GetAllRegister();
                string uid = collection["uid"];
                string email = collection["email"];
                for (int i = 0; i < request.Count; i++)
                {
                    if (uid == request[i].TaiKhoan)
                    {
                        db.DeleteRegister(request[i]);
                        db.Save();
                    }
                }
                int code = Rd(request);
                Register tk = new Register();
                DateTime nowh = DateTime.Now;
                tk.TaiKhoan = uid;
                tk.Email = email;
                tk.Code = code+"";
                tk.NgayThem = nowh;
                db.PostRegister(tk);
                db.Save();
                string title = "Mã xác nhận đăng ký tài khoản TKSR";
                string body = "Chào bạn, bạn đang đăng ký sử dụng dịch vụ TKSR Chúng tôi gửi bạn mã kích hoạt tài khoản: "+ code + " Mã có tác dụng 3 phút Không cung cấp mã này cho bất kỳ ai Cảm ơn bạn đã sử dụng dịch vụ";
                SenMail(email,title,body);
            }
            catch
            {
            }
        }
        [HttpPost]
        public JsonResult RegisterConfirm(FormCollection collection)
        {
            JsonResult jr = new JsonResult();
            try
            {
                List<Register> request = new List<Register>();
                request = db.GetAllRegister();
                string uid = collection["uid"];
                string email = collection["email"];
                string confirm = collection["confirm"];
                bool check = false;
                for(int i = 0; i < request.Count; i++)
                {
                    DateTime nowh = DateTime.Now;
                    TimeSpan difference = nowh - request[i].NgayThem;
                    if (difference.Days == 0)
                    {
                        string Day1 = request[i].NgayThem.ToString("dd/HH/mm/ss");
                        string Day2 = nowh.ToString("dd/HH/mm/ss");
                        string[] arr1 = Day1.Split('/');
                        string[] arr2 = Day2.Split('/');
                        int[] arr3 = new int[4];
                        arr3[0] = int.Parse(arr2[0]) - int.Parse(arr1[0]);
                        arr3[1] = int.Parse(arr2[1]) - int.Parse(arr1[1]);
                        arr3[2] = int.Parse(arr2[2]) - int.Parse(arr1[2]);
                        arr3[3] = int.Parse(arr2[3]) - int.Parse(arr1[3]);
                        int second = arr3[0] * 86400 + arr3[1] * 3600 + arr3[2] * 60 + arr3[3];
                        if (second > 190)
                        {
                            db.DeleteRegister(request[i]);
                            db.Save();
                        }
                        else
                        {
                            if (request[i].TaiKhoan == uid && request[i].Email == email && request[i].Code == confirm)
                            {
                                string pass1 = RdPass("");
                                string pass2 = RdPass(pass1);
                                string ip = Request.UserHostAddress;
                                TaiKhoan NewUser = new TaiKhoan();
                                NewUser.tenTK = uid;
                                NewUser.Email = email;
                                NewUser.LoaiTK = "Thuong";
                                NewUser.ngayLap = nowh;
                                NewUser.sdt = "Chưa cập nhật";
                                NewUser.DacDiem = ip;
                                NewUser.SoDu = 0;
                                NewUser.MatKhau = pass1;
                                NewUser.MatKhauC2 = pass2;
                                NewUser.TrangThai = "active";
                                NewUser.NumberLock = 0;
                                NewUser.IP = ip;
                                db.PostTaiKhoan(NewUser);
                                db.Save();
                                string title = "Thông báo đăng ký thành công TKSR";
                                string body = "Chào bạn, bạn đã đăng ký thành công tài khoản " + uid + " Mật khẩu đăng nhập của bạn là: " + pass1 + " Mật khẩu giao dịch của bạn là: " + pass2 + " Hãy bảo mật thông tin này, đăng nhập và thay đổi mật khẩu của bạn, cảm ơn bạn đã tin dùng sử dụng dịch vụ";
                                SenMail(email, title, body);
                                Session["user"] = NewUser;
                                Session.Timeout = 60;
                                check = true;
                                db.DeleteRegister(request[i]);
                                db.Save();
                            }
                        }
                    }
                    else
                    {
                        db.DeleteRegister(request[i]);
                        db.Save();
                    }
                }
                if(check == false)
                {
                    jr.Data = new
                    {
                        status = "F"
                    };
                }
                else
                {
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
                    status = "F"
                };
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public void ForgotPass(FormCollection collection)
        {
            try
            {
                string email = collection["email"];
                TaiKhoan TK = db.GetCheckEmail(email);
                string pass1 = RdPass("");
                string pass2 = RdPass(pass1);
                TK.MatKhau = pass1;
                TK.MatKhauC2 = pass2;
                db.Save();
                string title = "Thông báo lấy lại mật khẩu TKSR";
                string body = "Chào bạn, đã thực hiện lấy lại mật khẩu tài khoản " + TK.tenTK + ", Chúng tôi đã đổi mật khẩu của bạn, mật khẩu đăng nhập của bạn là: " + pass1 + " Mật khẩu giao dịch của bạn là: " + pass2 + " Hãy bảo mật thông tin này, đăng nhập và thay đổi mật khẩu của bạn, cảm ơn bạn đã tin dùng sử dụng dịch vụ";
                SenMail(email, title, body);
            }
            catch
            {
            }
        }
    }
}