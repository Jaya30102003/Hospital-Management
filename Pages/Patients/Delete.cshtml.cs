using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var patientToDelete = await _context.Patients.FindAsync(id);
            if (patientToDelete == null)
                return NotFound();

            _context.Patients.Remove(patientToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
