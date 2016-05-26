using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalTask.Models
{
    public class UploadedFileModel
    {
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Файл для загрузки не выбран.")]
        public HttpPostedFileBase UploadedFile { get; set; }

        public UploadedFileModel() { }
    }
}