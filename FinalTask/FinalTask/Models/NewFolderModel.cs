using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalTask.Models
{
    public class NewFolderModel
    {
        public string OwnerName { get; set; }
        public string SavePath { get; set; }

        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов.")]
        [RegularExpression("^[^!/\\\"<>*?]+$", ErrorMessage = "Символы \\ / ? * < > \" ! недопустимы.")]
        [Required(ErrorMessage = "Укажите имя будущей папки.")]
        public string Name { get; set; } = "Новая папка";

        public NewFolderModel() { }

        public NewFolderModel(string ownerName, string savePath)
        {
            OwnerName = ownerName;
            SavePath = savePath;
        }
    }
}