using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.ViewModels
{
    public class RegistrationViewModel
    {
        [MinLength(2, ErrorMessage = "Имя не может быть короче двух символов")]
        [MaxLength(150, ErrorMessage = "Максимальная длина имени составляет 150 знаков")]
        [RegularExpression(@"[A-Za-zА-Яа-я]*",
        ErrorMessage = "Имя пользователя должно содержать только латинские символы или ")]
        [Display(Name = "Полное имя:")]
        public string Fullname { get; set; }

        [MinLength(3, ErrorMessage = "Имя пользователя должно состоять из 3х или более символов")]
        [MaxLength(150, ErrorMessage = "Имя пользователя не должно превышать 150 символов")]
        [RegularExpression(@"[A-Za-z0-9_]*",
        ErrorMessage = "Логин пользователя должен содержать только латинские символы, цифры и символ подчеркивания")]
        [Display(Name = "Имя пользователя:")]

        public string Username { get; set; }
        
        /*[Display(Name = "Ваш возраст (полных лет):")]
        public int UserAge {  get; set; }*/

        [MinLength(8, ErrorMessage = "Пароль должен быть длиной не меньше 8ми символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторить пароль:")]
        public string RepeatPassword { get; set; }
    }
}
