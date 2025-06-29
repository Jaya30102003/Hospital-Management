using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Pages.Appointments
{
    public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DeleteModel(ApplicationDbContext context) { _context = context; }

    [BindProperty]
    public Appointment Appointment { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);
        return Appointment == null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        var app = await _context.Appointments.FindAsync(id);
        if (app == null) return NotFound();

        _context.Appointments.Remove(app);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}

}
