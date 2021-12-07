using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TKSR.Controllers
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string UserAvatarBase64String { get; set; }
    }
    public class UserSupportController : ApiController
    {
        /// <summary>
        /// Save image to Folder's Avatar
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>@Created by tungnt.net - 6/2015</returns>
        [Route("UploadImgSupport")]
        public bool Post([FromBody] UserProfile userProfile,string url,string imageTail)
        {
            bool result = false;
            try
            {
                //For demo purpose I only use jpg file and save file name by userId
                if (!string.IsNullOrEmpty(userProfile.UserAvatarBase64String))
                {
                    using (Image image = Base64ToImage(userProfile.UserAvatarBase64String))
                    {
                    string strFileName = "~/"+url+"/" + userProfile.UserId + imageTail;
                    image.Save(HttpContext.Current.Server.MapPath(strFileName), ImageFormat.Jpeg);
                    result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Bitmap tempBmp;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                using (Image image = Image.FromStream(ms, true))
                {
                    //Create another object image for dispose old image handler
                    tempBmp = new Bitmap(image.Width, image.Height);
                    Graphics g = Graphics.FromImage(tempBmp);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
            }
            return tempBmp;
        }
    }
}
