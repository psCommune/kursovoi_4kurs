using System.ComponentModel.DataAnnotations;

namespace eLibrary.Domain.Entities
{
    public class Playlist : Entity
    {
        [StringLength(150)]
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<Track> Tracks { get; set; }
        public DateTime? Time { get; set;}
    }
}
