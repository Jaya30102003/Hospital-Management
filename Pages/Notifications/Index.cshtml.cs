using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using Notifications.Model;

namespace HospitalManagementSystem.Pages.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Notification> NotificationList { get; set; } = new List<Notification>();

        public async Task OnGetAsync()
        {
            NotificationList = await _context.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .Take(100)
                .ToListAsync();

            Console.WriteLine($"📦 Loaded {NotificationList.Count} notifications.");
        }
    }
}
