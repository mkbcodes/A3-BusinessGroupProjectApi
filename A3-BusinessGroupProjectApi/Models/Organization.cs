using System.ComponentModel.DataAnnotations;

namespace A3_BusinessGroupProjectApi.Models
{
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset CreationTime { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(OrganizationType))]
        public string Type { get; set; }

        [Required]
        public string Address { get; set; }
    }

    public enum OrganizationType
    {
        Hospital,
        Clinic,
        Pharmacy
    }
}
