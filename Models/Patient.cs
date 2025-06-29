using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HospitalManagementSystem.Models
{
public class Patient
{
    [Key]
    [BindNever]
    public string PatientId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    [Range(0, 120)]
    public int Age { get; set; }

    [Required]
    public string Gender { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
}

}
