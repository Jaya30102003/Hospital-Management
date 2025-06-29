using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Doctors
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
{
    if (string.IsNullOrEmpty(id))
        return NotFound();

    var doctorToDelete = await _context.Doctors.FindAsync(id);
    if (doctorToDelete == null)
        return NotFound();

    _context.Doctors.Remove(doctorToDelete);
    await _context.SaveChangesAsync();

    return RedirectToPage("Index");
}


    }
}
