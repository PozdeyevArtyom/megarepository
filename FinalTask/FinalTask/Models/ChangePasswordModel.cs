using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalTask.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть в пределах от 6 до 40 символов.")]
        public string OldPass { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть в пределах от 6 до 40 символов.")]
        public string NewPass { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Compare("NewPass")]
        public string NewRepeat { get; set; }
    }
}