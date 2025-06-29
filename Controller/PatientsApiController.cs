using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        // GET: api/Patients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            return patient;
        }

        // POST: api/Patients
        [HttpPost]
public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var last = await _context.Patients
        .OrderByDescending(p => p.PatientId)
        .FirstOrDefaultAsync();

    int nextId = 1;
    if (last != null && last.PatientId.StartsWith("PAT") &&
        int.TryParse(last.PatientId.Substring(3), out int lastNum))
    {
        nextId = lastNum + 1;
    }

    patient.PatientId = "PAT" + nextId.ToString("D3");

    _context.Patients.Add(patient);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
}


        // PUT: api/Patients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(string id, Patient patient)
        {
            if (id != patient.PatientId)
                return BadRequest("Patient ID mismatch");

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Patients.Any(p => p.PatientId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Patients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
