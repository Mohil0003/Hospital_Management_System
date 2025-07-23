using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class DoctorModel
    {
        public int? DoctorID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Qualification is required")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public string Specialization { get; set; }

        public bool IsActive { get; set; }

        public DateTime Modified { get; set; }

        [Required(ErrorMessage = "User selection is required")]
        public int UserID { get; set; }
    }
}
