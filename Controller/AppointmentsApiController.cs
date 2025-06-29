using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Notifications.Service;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public AppointmentController(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            Console.WriteLine("📥 GET /api/Appointment");
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.TimeSlot)
                .ToListAsync();
        }

        // GET: api/Appointment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            return appointment == null ? NotFound() : appointment;
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            Console.WriteLine("🚨 POST /api/Appointment called");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            appointment.AppointmentId = Guid.NewGuid();
            appointment.CreatedAt = DateTimeOffset.UtcNow;
            appointment.IsApproved = false;
            appointment.IsCancelled = false;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(); // 🔑 Save FIRST

            try
            {
                await _notificationService.CreateForDoctor(appointment.AppointmentId, "Appointment Requested.");
                await _notificationService.CreateForPatient(appointment.AppointmentId, "Appointment Request Sent.");
                Console.WriteLine("✅ NotificationService triggered");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Notification failed: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId }, appointment);
        }

        // PUT: api/Appointment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(Guid id, Appointment updated)
        {
            if (id != updated.AppointmentId)
                return BadRequest("Appointment ID mismatch");

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            // Track original states
            bool wasApproved = appointment.IsApproved;
            bool wasCancelled = appointment.IsCancelled;

            // Update
            appointment.TimeSlot = updated.TimeSlot;
            appointment.Reason = updated.Reason;
            appointment.Remarks = updated.Remarks;
            appointment.DoctorId = updated.DoctorId;
            appointment.PatientId = updated.PatientId;
            appointment.IsApproved = updated.IsApproved;
            appointment.IsCancelled = updated.IsCancelled;

            Console.WriteLine($"🧠 WasApproved: {wasApproved}, New: {updated.IsApproved}");
Console.WriteLine($"🧠 WasCancelled: {wasCancelled}, New: {updated.IsCancelled}");


            await _context.SaveChangesAsync();

            try
            {
                if (!wasApproved && appointment.IsApproved)
                {
                    await _notificationService.CreateForDoctor(id, "Appointment Approved.");
                    await _notificationService.CreateForPatient(id, "Your appointment has been approved.");
                }

                if (!wasCancelled && appointment.IsCancelled)
                {
                    await _notificationService.CreateForDoctor(id, "Appointment Cancelled.");
                    await _notificationService.CreateForPatient(id, "Your appointment has been cancelled.");
                }

                Console.WriteLine("📢 Update notifications sent if applicable.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Notification failed on update: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Appointment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
