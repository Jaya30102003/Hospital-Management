using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HospitalManagementSystem.Models
{
    [Table("Appointments")]
public class Appointment
{
    [Key]
    public Guid AppointmentId { get; set; }

    public string PatientId { get; set; }
    public string DoctorId { get; set; }

    [MaxLength(120)]
    public string? Remarks { get; set; }
    public string Reason { get; set; }

    public bool IsApproved { get; set; } = false;
    public bool IsCancelled { get; set; } = false;

    public DateTime TimeSlot { get; set; }
public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;



        // 👇 Required for .Include(a => a.Patient) to work
        [ValidateNever]
    public virtual Patient Patient { get; set; }
    [ValidateNever]
    public virtual Doctor Doctor { get; set; }

    public Appointment() { }
}


}
