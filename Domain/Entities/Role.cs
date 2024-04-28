using System.ComponentModel.DataAnnotations;

namespace kursovoi_4kurs.Domain.Entities
{
    public class Role:Entity
    {
        [StringLength(150)]
        public string Name { get; set; } = null!;
    }
}
