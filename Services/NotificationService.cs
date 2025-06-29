using Notifications.DTO;
using Notifications.Model;
using Notifications.Repository;
using HospitalManagementSystem.Data;

namespace Notifications.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context, INotificationRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task CreateForDoctor(Guid appointmentId, string message)
        {
            Console.WriteLine($"🟡 Searching for appointment in CreateForDoctor: {appointmentId}");
            var appointment = await _context.Appointments.FindAsync(appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("❌ Appointment not found. Skipping doctor notification.");
                return;
            }

            var fullMessage = $"{message} Scheduled at: {appointment.TimeSlot:f}";
            Console.WriteLine($"✅ Creating notification for Doctor ID: {appointment.DoctorId}");

            await _repository.CreateForDoctor(appointmentId, appointment.DoctorId, fullMessage);
        }

        public async Task CreateForPatient(Guid appointmentId, string message)
        {
            Console.WriteLine($"🟡 Searching for appointment in CreateForPatient: {appointmentId}");
            var appointment = await _context.Appointments.FindAsync(appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("❌ Appointment not found. Skipping patient notification.");
                return;
            }

            var fullMessage = $"{message} Scheduled at: {appointment.TimeSlot:f}";
            Console.WriteLine($"✅ Creating notification for Patient ID: {appointment.PatientId}");

            await _repository.CreateForPatient(appointmentId, appointment.PatientId, fullMessage);
        }

        public async Task<IEnumerable<NotificationDTO>> GetAll()
        {
            Console.WriteLine("📦 Fetching ALL notifications");
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient)
        {
            return await _repository.GetAllByRecipientAsync(recipient);
        }

        public async Task<IEnumerable<NotificationDTO>> GetByRecipientAndIdAsync(string recipientType, string recipientId)
        {
            return await _repository.GetAllByRecipientAndIdAsync(recipientType, recipientId);
        }

        public async Task DeleteByAppointmentIdAsync(Guid appointmentId)
        {
            Console.WriteLine($"🗑️ Deleting notifications for Appointment: {appointmentId}");
            await _repository.DeleteByAppointmentId(appointmentId);
        }

        public async Task DeleteByNotificationIdAsync(Guid notificationId)
        {
            Console.WriteLine($"🗑️ Deleting notification: {notificationId}");
            await _repository.DeleteByNotificationId(notificationId);
        }
    }
}
