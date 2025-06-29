using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HospitalManagementSystem.Models
{
    public class Doctor
    {
        [Key] 
        [BindNever]
    public string DoctorID { get; set; }

    public string Name { get; set; }

    public string Specialization { get; set; }

    public int Experience { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
    }
}
