using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

using Microsoft.AspNetCore.Authorization;


namespace HospitalManagementSystem.Pages.Patients
{

    // [Authorize(Roles = "Admin")]
    // [Authorize(Roles = "Doctor")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Patient> PatientList { get; set; }

        public async Task OnGetAsync()
        {
            PatientList = await _context.Patients.ToListAsync();
        }
    }
}
