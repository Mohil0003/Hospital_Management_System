namespace Hospital_Management_System.Models
{
   public class DashboardModel
    {
        public int TotalUsers { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalPatients { get; set; }
        public int TotalAppointments { get; set; }
        public int TotalDepartments { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalReports { get; set; }
        public int ActiveBeds { get; set; }
        public int StaffMembers { get; set; }
    }

}
