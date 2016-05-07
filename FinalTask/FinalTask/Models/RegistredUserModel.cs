using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalTask.Models
{
    public class RegistredUserModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Длина логина должна быть в пределах от 5 до 40 символов.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть в пределах от 6 до 40 символов.")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [Compare("Password")]
        public string Repeat { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(60, ErrorMessage = "Максимальная длина - 60 символов")]
        [RegularExpression(@"^[A-Za-z0-9]+([-._][A-Za-z0-9]+)*\@[a-z]+\.[a-z]+$", ErrorMessage = "Неверный e-mail.")]
        public string Email { get; set; }
    }
}