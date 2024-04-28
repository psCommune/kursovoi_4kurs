using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.ViewModels
{
    public class PlaylistViewModel
    {
        [MinLength(1, ErrorMessage = "Название должно содержать минимум 1 символ")]
        [MaxLength(150, ErrorMessage = "Максимальная длина названия составляет 150 знаков")]
        [Display(Name = "Название плейлиста")]
        public string Title { get; set; } = string.Empty;

        [MinLength(1)]
        [MaxLength(500, ErrorMessage = "Максимальная длина описания составляет 500 знаков")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Изображение")]
        [Required]
        public IFormFile? Photo { get; set; }
    }
}
