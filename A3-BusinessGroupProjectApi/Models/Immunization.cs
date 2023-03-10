using System.ComponentModel.DataAnnotations;

namespace A3_BusinessGroupProjectApi.Models
{
    public class Immunization
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset CreationTime { get; set; }

        [Required]
        [MaxLength(128)]
        public string? OfficialName { get; set; }

        [MaxLength(128)]
        public string TradeName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LotNumber { get; set; }

        [Required]
        public DateTimeOffset ExpirationDate { get; set; }

        public DateTimeOffset? UpdatedTime { get; set; }
    }
}
