using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sol_Input_UI.Models.FileUploads;

namespace Sol_Input_UI.Controllers
{
    public class FileUploadDemoController : Controller
    {
        #region Constructor

        public FileUploadDemoController()
        {
        }

        #endregion Constructor

        #region Actions

        public IActionResult Index()
        {
            ViewBag.Path = "http://localhost:52340/Files/Google_Adwards_Sep_2019.pdf";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OnSubmit(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("files not selected");

            foreach (var file in files)
            {
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Files",
                        file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Index");
        }

        #endregion Actions
    }
}