namespace Notifications.DTO;

public class PatientNotificationRequest
{
    public Guid AppointmentId { get; set; }
    public string Message { get; set; }
}
