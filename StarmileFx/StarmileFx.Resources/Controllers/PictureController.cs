using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Net.Http.Headers;
using static StarmileFx.Resources.Service.FileService;
using Microsoft.AspNetCore.Cors;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Resources.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PicturesController : Controller
    {
        private IHostingEnvironment hostingEnv;

        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };

        public PicturesController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        [HttpPost]
        public IActionResult Post()
        {
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 1048576 * 2)
            {
                return Json(FileHelper.ErrorMsg("上传的图片不能大于2MB！"));
            }

            List<string> filePathResultList = new List<string>();

            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                string filePath = hostingEnv.WebRootPath + $@"\Files\Pictures\";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string suffix = fileName.Split('.')[1];

                if (!pictureFormatArray.Contains(suffix))
                {
                    return Json(FileHelper.ErrorMsg("上传的图片格式不支持，后缀名必须为：'png','jpg','jpeg','bmp','gif','ico'！"));
                }

                fileName = Guid.NewGuid().ToString().Replace("-", "") + "." + suffix;

                string fileFullName = filePath + fileName;

                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                filePathResultList.Add($"/src/Pictures/{fileName}");
            }

            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            return Json(FileHelper.SuccessMsg(message, filePathResultList));
        }
    }
}
