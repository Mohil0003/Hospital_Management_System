using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class DoctorDepartmentModel
    {
        public int? DoctorDepartmentID { get; set; }

        [Required(ErrorMessage = "Please select a Doctor")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Please select a Department")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Modified date is required")]
        public DateTime Modified { get; set; }

        [Required(ErrorMessage = "Please select a User")]
        public int UserID { get; set; }

        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public string? UserName { get; set; }
        public DateTime? Created { get; set; }
    }
}
