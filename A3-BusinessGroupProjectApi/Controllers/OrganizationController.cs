using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A3_BusinessGroupProjectApi.Data;
using A3_BusinessGroupProjectApi.Models;

namespace A3_BusinessGroupProjectApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganizationController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /Organization
        [HttpPost]
        public async Task<ActionResult<Organization>> CreateOrganization(Organization organization)
        {
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrganizationById), new { id = organization.Id }, organization);
        }

        // GET: /Organization/{organizationId}
        [HttpGet("{organizationId}")]
        public async Task<ActionResult<Organization>> GetOrganizationById(Guid organizationId)
        {
            var organization = await _context.Organizations.FindAsync(organizationId);

            if (organization == null)
            {
                return NotFound(CreateErrorMessage("Organization not found", StatusCodes.Status404NotFound));
            }

            return organization;
        }

        // PUT: /Organization/{organizationId}
        [HttpPut("{organizationId}")]
        public async Task<IActionResult> UpdateOrganization(Guid organizationId, Organization organization)
        {
            if (organizationId != organization.Id)
            {
                return BadRequest(CreateErrorMessage("The organization id provided in the request body does not match the id in the URL", StatusCodes.Status400BadRequest));
            }

            _context.Entry(organization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(organizationId))
                {
                    return NotFound(CreateErrorMessage("Organization not found", StatusCodes.Status404NotFound));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: /Organization?name={value}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizationsByName(string value)
        {
            var organizations = await _context.Organizations.Where(o => o.Name == value).ToListAsync();

            if (!organizations.Any())
            {
                return NotFound(CreateErrorMessage("No organizations found for the specified name", StatusCodes.Status404NotFound));
            }

            return organizations;
        }

        // GET: /Organization?type={value}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizationsByType(string value)
        {
            var organizations = await _context.Organizations.Where(o => o.Type == value).ToListAsync();

            if (!organizations.Any())
            {
                return NotFound(CreateErrorMessage("No organizations found for the specified type", StatusCodes.Status404NotFound));
            }

            return organizations;
        }

        // DELETE: /Organization/{organizationId}
        [HttpDelete("{organizationId}")]

        public async Task<IActionResult> DeleteOrganization(Guid organizationId)
        {
            var organization = await _context.Organizations.FindAsync(organizationId);

            if (organization == null)
            {
                return NotFound(CreateErrorMessage("Organization not found", StatusCodes.Status404NotFound));
            }

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationExists(Guid id)
        {
            return _context.Organizations.Any(e => e.Id == id);
        }

        private object CreateErrorMessage(string message, int statusCode)
        {
            return new
            {
                message = message,
                statusCode = statusCode,
                id = Guid.NewGuid()
            };
        }
    }

}
