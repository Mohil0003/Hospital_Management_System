using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class AppointmentModel
    {
        public int? AppointmentID { get; set; }

        [Required(ErrorMessage = "Doctor is required")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Patient is required")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string AppointmentStatus { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? SpecialRemarks { get; set; }

        public decimal? TotalConsultedAmount { get; set; }

        public DateTime Modified { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserID { get; set; }
    }
}
