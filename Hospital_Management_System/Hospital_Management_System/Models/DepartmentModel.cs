using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        public string DepartmentName { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "User selection is required")]
        public int? UserID { get; set; }

        public bool IsActive { get; set; }

        public DateTime Modified { get; set; }
    }
}
