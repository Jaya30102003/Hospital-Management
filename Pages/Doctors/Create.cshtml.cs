using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Doctors
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doctor Doctor { get; set; }

        public List<SelectListItem> Specializations { get; set; }

        public void OnGet()
        {
            LoadSpecializations();
        }

        public async Task<IActionResult> OnPostAsync()
{
    ModelState.Clear(); // ✅ Bypass ALL validation temporarily

    var last = _context.Doctors.OrderByDescending(d => d.DoctorID).FirstOrDefault();
    int nextId = 1;
    if (last != null && int.TryParse(last.DoctorID.Substring(3), out int lastNum))
        nextId = lastNum + 1;

    Doctor.DoctorID = "DOC" + nextId.ToString("D3");

    _context.Doctors.Add(Doctor);
    await _context.SaveChangesAsync();

    return RedirectToPage("Index");
}



        private void LoadSpecializations()
        {
            Specializations = new List<SelectListItem>
            {
                new SelectListItem { Value = "Cardiology", Text = "Cardiology" },
                new SelectListItem { Value = "Neurology", Text = "Neurology" },
                new SelectListItem { Value = "Orthopedics", Text = "Orthopedics" },
                new SelectListItem { Value = "Pediatrics", Text = "Pediatrics" },
                new SelectListItem { Value = "General Medicine", Text = "General Medicine" }
            };
        }
    }
}
