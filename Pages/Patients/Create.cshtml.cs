using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; }

        public void OnGet()
        {
        }

        //  public async Task<IActionResult> OnPostAsync()
        // {
        //     if (!ModelState.IsValid)
        //         return Page();

        //     // Auto-generate PatientID like PAT001
        //     var last = await _context.Patients
        //         .OrderByDescending(p => p.PatientId)
        //         .FirstOrDefaultAsync();

        //     int nextId = 1;
        //     if (last != null && last.PatientId?.StartsWith("PAT") == true &&
        //         int.TryParse(last.PatientId.Substring(3), out int lastNum))
        //     {
        //         nextId = lastNum + 1;
        //     }

        //     Patient.PatientId = "PAT" + nextId.ToString("D3");

        //     _context.Patients.Add(Patient);
        //     await _context.SaveChangesAsync();

        //     return RedirectToPage("Index");
        // }

        public async Task<IActionResult> OnPostAsync()
{
    ModelState.Clear(); // 👀 TEMPORARY – bypass validation if needed (only during debugging)

    // Get the last patient and calculate the next ID
    var last = _context.Patients
        .OrderByDescending(p => p.PatientId)
        .FirstOrDefault();

    int nextId = 1;
    if (last != null && last.PatientId != null &&
        last.PatientId.StartsWith("PAT") &&
        int.TryParse(last.PatientId.Substring(3), out int lastNum))
    {
        nextId = lastNum + 1;
    }

    Patient.PatientId = "PAT" + nextId.ToString("D3");

    _context.Patients.Add(Patient);
    await _context.SaveChangesAsync();

    return RedirectToPage("Index");
}


    }
}

