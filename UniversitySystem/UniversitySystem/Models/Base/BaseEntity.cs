using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}