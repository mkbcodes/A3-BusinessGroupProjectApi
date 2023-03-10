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

            return CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, provider);
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

        // GET: /Provider?firstName={value}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvidersByFirstName(string value)
        {
            var providers = await _context.Providers.Where(p => p.FirstName == value).ToListAsync();

            if (!providers.Any())
            {
                return NotFound(CreateErrorMessage("No providers found for the specified first name", StatusCodes.Status404NotFound));
            }

            return providers;
        }

        // GET: /Provider?lastName={value}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvidersByLastName(string value)
        {
            var providers = await _context.Providers.Where(p => p.LastName == value).ToListAsync();

            if (!providers.Any())
            {
                return NotFound(CreateErrorMessage("No providers found for the specified last name", StatusCodes.Status404NotFound));
            }

            return providers;
        }

        // GET: /Provider?licenseNumber={value}
        [HttpGet]
        public async Task<ActionResult<Provider>> GetProvidersByLicenseNumber(uint value)
        {
            var provider = await _context.Providers.SingleOrDefaultAsync(p => p.LicenseNumber == value);

            if (provider == null)
            {
                return NotFound(CreateErrorMessage("Provider not found for the specified license number", StatusCodes.Status404NotFound));
            }

            return provider;
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
