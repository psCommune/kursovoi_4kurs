﻿using System.ComponentModel.DataAnnotations;

namespace eLibrary.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Укажите имя пользователя")]
        [Display(Name = "Имя пользователя:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]

        public string Password { get; set; }
    }
}