using Microsoft.AspNetCore.Http;
using Sol_Input_UI.Models.FileUploads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Input_UI.ViewModel
{
    public class FileUploadsViewModel
    {
        //public FileUploadModel FileUploads { get; set; }

        public IFormFile FileToUpload { get; set; }
    }
}