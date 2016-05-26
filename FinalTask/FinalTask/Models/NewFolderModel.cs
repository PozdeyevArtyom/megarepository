using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace FinalTask.Models
{
    public class NewFolderModel
    {
        public int ParentId { get; set; }
        //public FileEntity ParentFolder { get; set; }

        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов.")]
        [RegularExpression("^[^!/\\\"<>*?]+$", ErrorMessage = "Символы \\ / ? * < > \" ! недопустимы.")]
        [Required(ErrorMessage = "Укажите имя будущей папки.")]
        public string Name { get; set; } = "Новая папка";

        public NewFolderModel() { }
    }
}