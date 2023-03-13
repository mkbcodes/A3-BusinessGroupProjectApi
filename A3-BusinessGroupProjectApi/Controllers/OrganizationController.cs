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

            return CreatedAtAction(nameof(GetOrganization), new { id = organization.Id }, organization);

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
        // GET: api/Organization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganization([FromQuery] string? Name, [FromQuery] string? Type)
        {
            if (Name != null)
            {
                return await _context.Organizations.Where(i => i.Name == Name).ToListAsync();
            }
            else if (Type != null)
            {
                return await _context.Organizations.Where(i => i.Type == Type).ToListAsync();
            }

            return await _context.Organizations.ToListAsync();
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
