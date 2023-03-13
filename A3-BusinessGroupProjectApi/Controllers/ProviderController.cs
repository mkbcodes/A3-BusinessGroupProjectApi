using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A3_BusinessGroupProjectApi.Data;
using A3_BusinessGroupProjectApi.Models;
using Newtonsoft.Json.Linq;

namespace A3_BusinessGroupProjectApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProviderController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /Provider
        [HttpPost]
        public async Task<ActionResult<Provider>> CreateProvider(Provider provider)
        {
            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProvider), new { id = provider.Id }, provider);
        }
        // GET: api/Immunization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvider([FromQuery] string? FirstName, [FromQuery] string? LastName, [FromQuery] uint? LicenseNumber)
        {
            if (FirstName != null)
            {
                return await _context.Providers.Where(i => i.FirstName == FirstName).ToListAsync();
            }
            else if (LastName != null)
            {
                return await _context.Providers.Where(i => i.LastName == LastName).ToListAsync();
            }
            else if (LicenseNumber != null)
            {
                return await _context.Providers.Where(i => i.LicenseNumber == LicenseNumber).ToListAsync();
            }

            return await _context.Providers.ToListAsync();
        }

        // GET: /Provider/{providerId}
        [HttpGet("{providerId}")]
        public async Task<ActionResult<Provider>> GetProviderById(Guid providerId)
        {
            var provider = await _context.Providers.FindAsync(providerId);

            if (provider == null)
            {
                return NotFound(CreateErrorMessage("Provider not found", StatusCodes.Status404NotFound));
            }

            return provider;
        }

        // PUT: /Provider/{providerId}
        [HttpPut("{providerId}")]
        public async Task<IActionResult> UpdateProvider(Guid providerId, Provider provider)
        {
            if (providerId != provider.Id)
            {
                return BadRequest(CreateErrorMessage("The provider id provided in the request body does not match the id in the URL", StatusCodes.Status400BadRequest));
            }

            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(providerId))
                {
                    return NotFound(CreateErrorMessage("Provider not found", StatusCodes.Status404NotFound));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: /Provider/{providerId}
        [HttpDelete("{providerId}")]
        public async Task<IActionResult> DeleteProvider(Guid providerId)
        {
            var provider = await _context.Providers.FindAsync(providerId);

            if (provider == null)
            {
                return NotFound(CreateErrorMessage("Provider not found", StatusCodes.Status404NotFound));
            }

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderExists(Guid id)
        {
            return _context.Providers.Any(e => e.Id == id);
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
