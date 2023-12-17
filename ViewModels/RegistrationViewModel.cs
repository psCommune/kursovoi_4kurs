using System.ComponentModel.DataAnnotations;

namespace eLibrary.ViewModels
{
    public class RegistrationViewModel
    {
        [MinLength(2, ErrorMessage = "Имя не может быть короче двух символов")]
        [MaxLength(50, ErrorMessage = "Максимальная длина имени составляет 50 знаков")]
        [Display(Name = "Полное имя:")]
        public string Fullname { get; set; }

        [MinLength(3, ErrorMessage = "Имя пользователя должно быть 3 или более символов")]
        [MaxLength(50, ErrorMessage = "Имя пользователя не должно превышать 50 символов")]
        [RegularExpression(@"[A-Za-z0-9_]*",
        ErrorMessage = "Имя пользователя должно содержать только латинские символы, цифры и символ подчеркивания")]
        [Display(Name = "Имя пользователя:")]

        public string Username { get; set; }

        [MinLength(8, ErrorMessage = "Пароль должен быть длиной 8 или более символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторить пароль:")]
        public string RepeatPassword { get; set; }
    }
}
