using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public SelectList PatientList { get; set; }
        public List<SelectListItem> DoctorList { get; set; }
        public List<string> Specializations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Specializations = await _context.Doctors
                .Select(d => d.Specialization)
                .Distinct()
                .ToListAsync();

            PatientList = new SelectList(
                await _context.Patients
                    .Select(p => new {
                        p.PatientId,
                        Display = p.Name + " (" + p.PatientId + ")"
                    }).ToListAsync(),
                "PatientId", "Display"
            );

            DoctorList = await _context.Doctors
                .Select(d => new SelectListItem
                {
                    Value = d.DoctorID,
                    Text = d.Name + " – " + d.Specialization
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
{
    Console.WriteLine("🚨 RazorPage OnPostAsync triggered");

    if (!ModelState.IsValid)
    {
        Console.WriteLine("❌ ModelState invalid.");
        await OnGetAsync(); // reload dropdowns
        return Page();
    }

    try
    {
        var client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5120"); // 👈 use the exact port your API listens on

var response = await client.PostAsJsonAsync("/api/Appointment", Appointment);


        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("✅ Appointment posted to API and notification should fire.");
            return RedirectToPage("Index");
        }

        Console.WriteLine("❌ API call failed: " + await response.Content.ReadAsStringAsync());
        ModelState.AddModelError(string.Empty, "API error: " + await response.Content.ReadAsStringAsync());
    }
    catch (Exception ex)
    {
        Console.WriteLine("🔥 Exception posting appointment: " + ex.Message);
        ModelState.AddModelError(string.Empty, "Something went wrong: " + ex.Message);
    }

    await OnGetAsync(); // keep dropdowns live
    return Page();
}

    }
}
