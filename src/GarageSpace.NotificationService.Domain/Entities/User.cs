using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageSpace.NotificationService.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public required string Nickname { get; set; }

        [Required]
        public required string Email { get; set; }
    }
}
