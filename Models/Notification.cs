using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalManagementSystem.Models;

namespace Notifications.Model;

[Table("Notifications")]
public class Notification
{
    [Key]
    public Guid NotificationId { get; set; }

    [Required]
    [ForeignKey("Appointments")]
    public Guid AppointmentId { get; set; }
    [Required]
    public string RecipientId { get; set; } // store doctorId or patientId


    [MaxLength(100)]
    public string NotificationTitle { get; set; }

    [MaxLength(300)]
    public string NotificationMessage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public RecipientType Recipient { get; set; }

    public Appointment Appointment { get; set; }
}
