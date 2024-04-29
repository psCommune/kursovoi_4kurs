using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.Domain.Entities
{
    public class User:Entity
    {
        [StringLength(150)]
        public string Fullname { get; set; } = null!;

        [StringLength(150)]
        public string Login { get; set; } = null!;

        [StringLength(150)]
        public string Password { get; set; } = null!;

        [StringLength(100)]
        public string Salt { get; set; } = null!;

        [StringLength(500)]
        public string? Photo { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public ICollection<Playlist>? Playlists { get; set; }
    }
}
