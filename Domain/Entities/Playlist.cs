using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.Domain.Entities
{
    public class Playlist : Entity
    {
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [StringLength(500)]
        public string? Description { get; set; }
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        public DateTime? CreateDate { get; set; }        
        public DateTime? Time { get; set;}
        public User User { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
