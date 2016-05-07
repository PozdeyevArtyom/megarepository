using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalTask.Models
{
    public class UploadedFileModel
    {
        public string OwnerName { get; set; }
        public string SavePath { get; set; }

        [Required(ErrorMessage = "Файл для загрузки не выбран.")]
        public HttpPostedFileBase UploadedFile { get; set; }

        public UploadedFileModel() { }
        
        public UploadedFileModel(string ownerName, string savePath)
        {
            OwnerName = ownerName;
            SavePath = savePath;
        }
    }
}