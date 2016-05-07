using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalTask.Models
{
    public class LoggedUserModel
    {
        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Длина логина должна быть в пределах от 5 до 40 символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть в пределах от 6 до 40 символов.")]
        public string Password { get; set; }
    }
}