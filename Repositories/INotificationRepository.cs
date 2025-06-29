using Notifications.DTO;

namespace Notifications.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationDTO>> GetAll();
        Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient);
        Task CreateForDoctor(Guid appointmentId, string doctorId, string message);
        Task CreateForPatient(Guid appointmentId, string patientId, string message);
        Task DeleteByAppointmentId(Guid appointmentId);
        Task DeleteByNotificationId(Guid notificationId);
        Task<IEnumerable<NotificationDTO>> GetAllByRecipientAndIdAsync(string recipientType, string recipientId);

    }
}
