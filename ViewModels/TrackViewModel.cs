using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.ViewModels
{
    public class TrackViewModel
    {
        [MinLength(1, ErrorMessage = "Название должно содержать минимум 1 символ")]
        [MaxLength(150, ErrorMessage = "Максимальная длина названия составляет 150 знаков")]
        [Display(Name = "Название трека")]
        public string Title { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "Автор должено содержать минимум 1 символ")]
        [MaxLength(150, ErrorMessage = "Максимальная длина для автора составляет 150 знаков")]
        [Display(Name = "Автор")]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Изображение")]
        [Required]
        public IFormFile Photo { get; set; }

        [Display(Name = "Файл аудио (mp3)")]
        [Required]
        public IFormFile File { get; set; }

        [Display(Name = "Плейлист")]
        [Required]
        public int? PlaylistId { get; set; }
        public List<SelectListItem>? Playlists { get; set; } = new();
       
    }
}
