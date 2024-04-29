using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.Domain.Entities
{
    public class Track : Entity
    {
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [StringLength(150)]
        public string Author { get; set; } = null!;
        [StringLength(500)]
        public string ImageUrl { get; set; }
        [StringLength(500)]
        public string Filename { get; set; }
        public int? PlaylistId { get; set; }
        public Playlist? Playlist { get; set; }
        public ICollection<TrackPlaylist> TrackPlaylists { get; set; }
    }
}
