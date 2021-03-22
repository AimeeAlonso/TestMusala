using System.ComponentModel.DataAnnotations;

namespace Domain
{
  public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
