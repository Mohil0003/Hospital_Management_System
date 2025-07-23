using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class PatientModel
    {
        public int? PatientID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public bool IsActive { get; set; }

        public DateTime? Modified { get; set; }

        [Required(ErrorMessage = "User selection is required")]
        public int? UserID { get; set; }

        // 🔽 Add this for dropdown
        public List<SelectListItem> UserDropdownList { get; set; } = new List<SelectListItem>();
    }
}
