using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notifications.Model;

namespace HospitalManagementSystem.Pages.Notifications
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Notification Notification { get; set; }

        public List<SelectListItem> RoleOptions { get; set; } = new()
        {
            new SelectListItem { Value = "All", Text = "All" },
            new SelectListItem { Value = "Doctor", Text = "Doctor" },
            new SelectListItem { Value = "Patient", Text = "Patient" }
        };

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Notification.CreatedAt = DateTime.Now;
            _context.Notifications.Add(Notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

