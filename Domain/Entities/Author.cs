using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.Domain.Entities
{
    public class Author : Entity
    {
        [StringLength(150)]
        public string Pseudonym { get; set; } = null!;
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        public ICollection<Track> Tracks { get; set; }

    }
}
