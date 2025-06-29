using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;

namespace HospitalManagementSystem.Pages.Appointments
{
    public class ApproveModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ApproveModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var app = await _context.Appointments.FindAsync(id);
            if (app == null) return NotFound();

            app.IsApproved = true;
            app.IsCancelled = false;

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
