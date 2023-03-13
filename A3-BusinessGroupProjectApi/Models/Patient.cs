using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace A3_BusinessGroupProjectApi.Models
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset CreationTime { get; set; }

        [Required]
        [MaxLength(128)]
        [BindProperty(Name = "FirstName", SupportsGet = true)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128)]
        [BindProperty(Name = "LastName", SupportsGet = true)]
        public string LastName { get; set; }

        [Required]
        [BindProperty(Name = "DateOfBirth", SupportsGet = true)]
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
