using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.ViewModels
{
    public class PlaylistViewModel
    {
        [MinLength(2)]
        [MaxLength(150)]
        [Display(Name = "Название плейлиста")]
        public string Title { get; set; } = string.Empty;

        [MinLength(2)]
        [MaxLength(500)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Изображение")]
        [Required]
        public IFormFile? Photo { get; set; }
    }
}
