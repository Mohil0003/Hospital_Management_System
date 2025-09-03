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

        // Dynamic, frequently-changing KPIs
        public int TodaysAppointments { get; set; }
        public int NewPatientsThisMonth { get; set; }
        public int PendingAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int CancelledAppointments { get; set; }
        public int CompletionRatePercent { get; set; }

        // Financial insights
        public decimal AvgRevenuePerPatient { get; set; }
        public decimal AvgRevenuePerAppointment { get; set; }
        public int PaymentClearanceRatePercent { get; set; }
    }

}
