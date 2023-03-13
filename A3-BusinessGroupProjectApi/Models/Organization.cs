using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
        [BindProperty(Name = "Name", SupportsGet = true)]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(OrganizationType))]
        [BindProperty(Name = "Type", SupportsGet = true)]
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
