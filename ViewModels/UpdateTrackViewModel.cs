using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eLibrary.ViewModels
{
    public class UpdateTrackViewModel
    {
        public int Id { get; set; }


        [MinLength(2)]
        [MaxLength(150)]
        [Display(Name = "Название аудио")]
        public string Title { get; set; } = string.Empty;

        [MinLength(2)]
        [MaxLength(150)]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Изображение")]
        public IFormFile? Photo { get; set; } = null!;
        public string? PhotoString { get; set; }

        [Display(Name = "Файл аудио (mp3)")]
        public IFormFile? File { get; set; }
        public string? FileString { get; set; }

        [Required]
        [Display(Name = "Плейлист")]
        public int? PlaylistId { get; set; }
        public List<SelectListItem>? Playlists { get; set; } = new();
    }
}
