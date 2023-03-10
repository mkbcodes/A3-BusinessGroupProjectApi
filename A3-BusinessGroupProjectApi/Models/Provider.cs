using System.ComponentModel.DataAnnotations;

namespace A3_BusinessGroupProjectApi.Models
{
    public class Provider
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset CreationTime { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        [Required]
        public uint LicenseNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
