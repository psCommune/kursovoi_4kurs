using System.ComponentModel.DataAnnotations;

namespace eLibrary.Domain.Entities
{
    public class Role:Entity
    {
        [StringLength(150)]
        public string Name { get; set; } = null!;
    }
}
