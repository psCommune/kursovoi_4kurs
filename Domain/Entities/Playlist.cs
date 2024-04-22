using System.ComponentModel.DataAnnotations;

namespace eLibrary.Domain.Entities
{
    public class Playlist : Entity
    {
        [StringLength(150)]
        public string Title { get; set; } = null!;
        public List<Track> Tracks { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        public DateTime? CreateDate { get; set; }        
        public DateTime? Time { get; set;}
    }
}
