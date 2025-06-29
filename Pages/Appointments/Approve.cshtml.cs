using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Appointments
{
    public class ApproveModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpFactory;

        public ApproveModel(ApplicationDbContext context, IHttpClientFactory httpFactory)
        {
            _context = context;
            _httpFactory = httpFactory;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var app = await _context.Appointments.FindAsync(id);
            if (app == null) return NotFound();

            app.IsApproved = true;
            app.IsCancelled = false;

            var client = _httpFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5120"); // adjust port if needed

            var result = await client.PutAsJsonAsync($"/api/Appointment/{id}", app);

            if (!result.IsSuccessStatusCode)
                Console.WriteLine("❌ API call failed: " + await result.Content.ReadAsStringAsync());

            return RedirectToPage("Index");
        }
    }
}
