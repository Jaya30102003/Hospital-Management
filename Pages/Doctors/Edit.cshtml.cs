using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Doctors
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doctor Doctor { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Doctor = await _context.Doctors.FindAsync(id);
            if (Doctor == null)
                return NotFound();
            
            // Console.WriteLine("🧠 Loaded doctor: " + Doctor?.DoctorID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
{
    var existing = await _context.Doctors.FindAsync(id);
if (existing == null) return NotFound();

// Update fields manually
existing.Name = Doctor.Name;
existing.Specialization = Doctor.Specialization;
existing.Email = Doctor.Email;
existing.Password = Doctor.Password;
existing.Experience = Doctor.Experience;

await _context.SaveChangesAsync();
return RedirectToPage("Index");

}

    }
}
