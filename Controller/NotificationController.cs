using Microsoft.AspNetCore.Mvc;
using Notifications.DTO;
using Notifications.Repository;
using Notifications.Request;
using Notifications.Service;

namespace Notifications.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _service;

    public NotificationController(INotificationService service)
    {
        _service = service;
    }

    // Retrieval of all Notifications 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    // Getting Notifications based on Recipient : Doctor / Patient
    [HttpGet("recipient/{recipient}")]
    public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetByRecipient(string recipient)
    {
        return Ok(await _service.GetAllByRecipientAsync(recipient));
    }

    // Doctor notification creation
    [HttpPost("doctor")]
    public async Task<IActionResult> CreateForDoctor([FromBody] DoctorNotificationRequest request)
    {
        await _service.CreateForDoctor(request.AppointmentId, request.Message);
        return Ok("Doctor notification created.");
    }

    // Patient Notification creation
    [HttpPost("patient")]
    public async Task<IActionResult> CreateForPatient([FromBody] PatientNotificationRequest request)
    {
        await _service.CreateForPatient(request.AppointmentId, request.Message);
        return Ok("Patient notification created.");
    }

    // Notification deletion based on appointment id
    [HttpDelete("appointment/{appointmentId}")]
    public async Task<IActionResult> DeleteByAppointmentId(Guid appointmentId)
    {
        await _service.DeleteByAppointmentIdAsync(appointmentId);
        return NoContent();
    }

    // Notification deletion based on Notification id
    [HttpDelete("{notificationId}")]
    public async Task<IActionResult> DeleteByNotificationId(Guid notificationId)
    {
        await _service.DeleteByNotificationIdAsync(notificationId);
        return NoContent();
    }
    
    [HttpGet("recipient/{recipient}/id/{recipientId}")]
public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetByRecipientAndId(string recipient, string recipientId)
{
    var notifications = await _service.GetByRecipientAndIdAsync(recipient, recipientId);
    return Ok(notifications);
}

}
