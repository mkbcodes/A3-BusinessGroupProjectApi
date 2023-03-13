using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace A3_BusinessGroupProjectApi.Models
{
    public class Immunization
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [BindProperty(Name = "CreationTime", SupportsGet = true)]
        public DateTimeOffset CreationTime { get; set; }

        [Required]
        [MaxLength(128)]
        [BindProperty(Name = "OfficialName", SupportsGet = true)]
        public string? OfficialName { get; set; }

        [MaxLength(128)]
        [BindProperty(Name = "TradeName", SupportsGet = true)]
        public string TradeName { get; set; }

        [Required]
        [MaxLength(255)]
        [BindProperty(Name = "LotNumber", SupportsGet = true)]
        public string LotNumber { get; set; }

        [Required]
        public DateTimeOffset ExpirationDate { get; set; }

        public DateTimeOffset? UpdatedTime { get; set; }
    }
}
