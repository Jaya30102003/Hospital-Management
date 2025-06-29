namespace Notifications.Request;
public class DoctorNotificationRequest
{
    public Guid AppointmentId { get; set; }
    public string Message { get; set; }
}
