using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DoctorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
    {
        return await _context.Doctors.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(string id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();
        return doctor;
    }

    [HttpPost]
public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        return BadRequest(errors);
    }

    // Auto-generate DoctorID
    var last = await _context.Doctors.OrderByDescending(d => d.DoctorID).FirstOrDefaultAsync();
    int nextId = 1;
    if (last != null && int.TryParse(last.DoctorID.Substring(3), out int lastNum))
        nextId = lastNum + 1;

    doctor.DoctorID = "DOC" + nextId.ToString("D3");

    _context.Doctors.Add(doctor);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorID }, doctor);
}


    [HttpPut("{id}")]
    public async Task<IActionResult> PutDoctor(string id, Doctor doctor)
    {
        if (id != doctor.DoctorID) return BadRequest();
        _context.Entry(doctor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(string id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
