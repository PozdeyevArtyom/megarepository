using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalTask.Models
{
    public class ChangeEmailModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(60, ErrorMessage = "Максимальная длина - 60 символов")]
        [RegularExpression(@"^[A-Za-z0-9]+([-._][A-Za-z0-9]+)*\@[a-z]+\.[a-z]+$", ErrorMessage = "Неверный e-mail.")]
        public string OldEmail { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(60, ErrorMessage = "Максимальная длина - 60 символов")]
        [RegularExpression(@"^[A-Za-z0-9]+([-._][A-Za-z0-9]+)*\@[a-z]+\.[a-z]+$", ErrorMessage = "Неверный e-mail.")]
        public string NewEmail { get; set; }
    }
}