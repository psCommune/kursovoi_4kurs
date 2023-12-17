using System.ComponentModel.DataAnnotations;

namespace eLibrary.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
