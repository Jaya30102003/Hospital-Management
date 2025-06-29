using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime GeneratedOn { get; set; } = DateTime.Now;

        public string FilePath { get; set; }
    }
}
