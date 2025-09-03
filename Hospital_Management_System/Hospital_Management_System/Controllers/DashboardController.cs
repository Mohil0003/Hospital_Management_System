using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class DashboardController : Controller
    {
        private readonly string _connectionString;
        public DashboardController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public IActionResult Index ()
        {
            DashboardModel stats = GetDashboardStats();
            return View("Dashboard", stats);
        }




        

        public IActionResult GetStatics()
        {
            DashboardModel stats = GetDashboardStats();
            return View(stats);
        }

        // Your requested method
        public DashboardModel GetDashboardStats()
        {
            DashboardModel stats = new DashboardModel();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PR_Dashboard_GetStatics", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    stats.TotalUsers = Convert.ToInt32(reader["TotalUsers"]);
                    stats.TotalDoctors = Convert.ToInt32(reader["TotalDoctors"]);
                    stats.TotalPatients = Convert.ToInt32(reader["TotalPatients"]);
                    stats.TotalAppointments = Convert.ToInt32(reader["TotalAppointments"]);
                    stats.TotalDepartments = Convert.ToInt32(reader["TotalDepartments"]);

                    // Dynamic KPIs
                    stats.TodaysAppointments = Convert.ToInt32(reader["TodaysAppointments"]);
                    stats.NewPatientsThisMonth = Convert.ToInt32(reader["NewPatientsThisMonth"]);
                    stats.PendingAppointments = Convert.ToInt32(reader["PendingAppointments"]);
                    stats.CompletedAppointments = SafeReadInt(reader, "CompletedAppointments");
                    stats.CancelledAppointments = SafeReadInt(reader, "CancelledAppointments");
                    stats.CompletionRatePercent = Convert.ToInt32(reader["CompletionRatePercent"]);

                    // Financial Data (derived from appointments)
                    stats.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    stats.AvgRevenuePerPatient = Convert.ToDecimal(reader["AvgRevenuePerPatient"]);
                    stats.AvgRevenuePerAppointment = Convert.ToDecimal(reader["AvgRevenuePerAppointment"]);
                }
            }

            return stats;
        }

        private void TryReadInt(SqlDataReader reader, string column, Action<int> setter)
        {
            try { var ord = reader.GetOrdinal(column); if (ord >= 0 && !reader.IsDBNull(ord)) setter(Convert.ToInt32(reader.GetValue(ord))); }
            catch { }
        }

        private void TryReadDecimal(SqlDataReader reader, string column, Action<decimal> setter)
        {
            try { var ord = reader.GetOrdinal(column); if (ord >= 0 && !reader.IsDBNull(ord)) setter(Convert.ToDecimal(reader.GetValue(ord))); }
            catch { }
        }
        private int SafeReadInt(SqlDataReader reader, string column)
        {
            try { var ord = reader.GetOrdinal(column); return reader.IsDBNull(ord) ? 0 : Convert.ToInt32(reader.GetValue(ord)); }
            catch { return 0; }
        }
        public IActionResult GetChartData()
        {
            List<object> patientsGrowth = new List<object>();
            List<object> appointmentsByDept = new List<object>();
            List<object> revenueAnalysis = new List<object>();
            List<object> appointmentStatus = new List<object>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PR_Dashboard_GetStatics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Skip first result set (main stats)
                        dr.NextResult();
                        
                        // Patients Growth (2nd result set)
                        while (dr.Read())
                        {
                            patientsGrowth.Add(new { Month = dr["Month"], Count = dr["TotalPatients"] });
                        }

                        // Move to next result set (3rd)
                        dr.NextResult();

                        // Appointments by Department
                        while (dr.Read())
                        {
                            appointmentsByDept.Add(new { Department = dr["DepartmentName"], Count = dr["TotalAppointments"] });
                        }

                        // Move to next result set (4th)
                        dr.NextResult();

                        // Revenue Analysis (uses column alias 'Revenue' from proc)
                        while (dr.Read())
                        {
                            revenueAnalysis.Add(new { Quarter = dr["Quarter"], Revenue = dr["Revenue"] });
                        }

                        // Move to next result set (5th)
                        dr.NextResult();

                        // Appointment Status Overview
                        while (dr.Read())
                        {
                            appointmentStatus.Add(new { Status = dr["Status"], Count = dr["Count"] });
                        }
                    }
                }
            }

            return Json(new
            {
                patientsGrowth,
                appointmentsByDept,
                revenueAnalysis,
                appointmentStatus
            });
        }

    }

}
