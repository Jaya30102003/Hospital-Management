using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Patient = await _context.Patients.FindAsync(id);
            if (Patient == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var existing = await _context.Patients.FindAsync(id);
            if (existing == null)
                return NotFound();

            // Update fields manually
            existing.Name = Patient.Name;
            existing.Age = Patient.Age;
            existing.Gender = Patient.Gender;
            existing.Phone = Patient.Phone;
            existing.Email = Patient.Email;

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
