using System.ComponentModel.DataAnnotations;

namespace eLibrary.Domain.Entities
{
    public class Track : Entity
    {
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        [StringLength(300)]
        public string Filename { get; set; } = null!;
        
    }
}
