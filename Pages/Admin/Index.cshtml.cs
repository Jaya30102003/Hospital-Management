using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Notifications.Model;


namespace HospitalManagementSystem.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int PatientCount { get; set; }
        public int DoctorCount { get; set; }
        public int TodayAppointmentCount { get; set; }

        public List<Notification> RecentNotifications { get; set; }

        public async Task OnGetAsync()
        {
            PatientCount = await _context.Patients.CountAsync();
            DoctorCount = await _context.Doctors.CountAsync();
            TodayAppointmentCount = await _context.Appointments
                .Where(a => a.TimeSlot.Date == DateTime.Today).CountAsync();

            RecentNotifications = await _context.Notifications
                .Where(n => n.Recipient == RecipientType.Admin)
                .OrderByDescending(n => n.CreatedAt)
                .Take(5)
                .ToListAsync();
        }
    }
}
