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
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /Patient
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }

        // GET: /Patient/{patientId}
        [HttpGet("{patientId}")]
        public async Task<ActionResult<Patient>> GetPatientById(Guid patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
            {
                return NotFound(CreateErrorMessage("Patient not found", StatusCodes.Status404NotFound));
            }

            return patient;
        }

        // PUT: /Patient/{patientId}
        [HttpPut("{patientId}")]
        public async Task<IActionResult> UpdatePatient(Guid patientId, Patient patient)
        {
            if (patientId != patient.Id)
            {
                return BadRequest(CreateErrorMessage("The patient id provided in the request body does not match the id in the URL", StatusCodes.Status400BadRequest));
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(patientId))
                {
                    return NotFound(CreateErrorMessage("Patient not found", StatusCodes.Status404NotFound));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: /Patient?firstName={value}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByFirstName(string value)
        {
            var patients = await _context.Patients.Where(p => p.FirstName == value).ToListAsync();

            if (!patients.Any())
            {
                return NotFound(CreateErrorMessage("No patients found for the specified first name", StatusCodes.Status404NotFound));
            }

            return patients;
        }

        // GET: /Patient?lastName={value}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByLastName(string value)
        {
            var patients = await _context.Patients.Where(p => p.LastName == value).ToListAsync();

            if (!patients.Any())
            {
                return NotFound(CreateErrorMessage("No patients found for the specified last name", StatusCodes.Status404NotFound));
            }

            return patients;
        }

        // GET: /Patient?dateOfBirth={value}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByDateOfBirth(DateTimeOffset value)
        {
            var patients = await _context.Patients.Where(p => p.DateOfBirth == value).ToListAsync();

            if (!patients.Any())
            {
                return NotFound(CreateErrorMessage("No patients found for the specified date of birth", StatusCodes.Status404NotFound));
            }

            return patients;
        }

        // DELETE: /Patient/{patientId}
        [HttpDelete("{patientId}")]
        public async Task<IActionResult> DeletePatient(Guid patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
            {
                return NotFound(CreateErrorMessage("Patient not found", StatusCodes.Status404NotFound));
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(Guid id)
        {
            return _context.Patients.Any(e => e.Id == id);
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
