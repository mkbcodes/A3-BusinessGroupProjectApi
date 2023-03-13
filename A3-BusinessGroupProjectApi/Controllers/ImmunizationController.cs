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
    [BindProperties]
    public class ImmunizationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImmunizationController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /Immunization
        [HttpPost]
        public async Task<ActionResult<Immunization>> CreateImmunization(Immunization immunization)
        {
            _context.Immunizations.Add(immunization);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImmunization), new { id = immunization.Id }, immunization);
        }

        // GET: api/Immunization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Immunization>>> GetImmunization([FromQuery]DateTimeOffset? CreationTime, [FromQuery]string? OfficialName, [FromQuery] string? TradeName, [FromQuery]string? LotNumber)
        {
            if (CreationTime != null)
            {
                return await _context.Immunizations.Where(i => i.CreationTime == CreationTime).ToListAsync();
            }
            else if (OfficialName != null)
            {
                return await _context.Immunizations.Where(i => i.OfficialName == OfficialName).ToListAsync();
            }
            else if (LotNumber != null)
            {
                return await _context.Immunizations.Where(i => i.LotNumber == LotNumber).ToListAsync();
            }
            else if (TradeName != null)
            {
                return await _context.Immunizations.Where(i => i.TradeName == TradeName).ToListAsync();
            }
            // params
            // params with switch statements with multuple returns
            Console.WriteLine($"OfficialName {OfficialName}");
            Console.WriteLine($"TradeName {TradeName}");
            Console.WriteLine($"LotNumber {LotNumber}");
            Console.WriteLine($"CreationTime {CreationTime}");
            return await _context.Immunizations.ToListAsync();
        }

        // GET: /Immunization/{immunizationId}
        [HttpGet("{immunizationId}")]
        public async Task<ActionResult<Immunization>> GetImmunizationById(Guid immunizationId)
        {
            var immunization = await _context.Immunizations.FindAsync(immunizationId);

            if (immunization == null)
            {
                return NotFound(CreateErrorMessage("Immunization not found", StatusCodes.Status404NotFound));
            }

            return immunization;
        }


        //// GET: /Immunization?creationTime={value}
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Immunization>>> GetImmunizationsByCreationTime(DateTimeOffset value)
        //{
        //    var immunizations = await _context.Immunizations.Where(i => i.CreationTime == value).ToListAsync();

        //    if (!immunizations.Any())
        //    {
        //        return NotFound(CreateErrorMessage("No immunizations found for the specified creation time", StatusCodes.Status404NotFound));
        //    }

        //    return immunizations;
        //}

        // PUT: /Immunization/{immunizationId}
        [HttpPut("{immunizationId}")]
        public async Task<IActionResult> UpdateImmunization(Guid immunizationId, Immunization immunization)
        {
            if (immunizationId != immunization.Id)
            {
                return BadRequest(CreateErrorMessage($"The immunization id provided in the request body does not match the id in the URL; id1={immunizationId} id2={immunization.Id}", StatusCodes.Status400BadRequest));
            }

            _context.Entry(immunization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImmunizationExists(immunizationId))
                {
                    return NotFound(CreateErrorMessage("Immunization not found", StatusCodes.Status404NotFound));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: /Immunization/{immunizationId}
        [HttpDelete("{immunizationId}")]
        public async Task<IActionResult> DeleteImmunization(Guid immunizationId)
        {
            var immunization = await _context.Immunizations.FindAsync(immunizationId);

            if (immunization == null)
            {
                return NotFound(CreateErrorMessage("Immunization not found", StatusCodes.Status404NotFound));
            }

            _context.Immunizations.Remove(immunization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImmunizationExists(Guid id)
        {
            return _context.Immunizations.Any(e => e.Id == id);
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
