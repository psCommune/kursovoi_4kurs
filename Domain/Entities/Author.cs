using System.ComponentModel.DataAnnotations;

namespace eLibrary.Domain.Entities
{
    public class Author : Entity
    {
        [StringLength(150)]
        public string Pseudonym { get; set; } = null!;
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public List<Track> Tracks { get; set; }

    }
}
