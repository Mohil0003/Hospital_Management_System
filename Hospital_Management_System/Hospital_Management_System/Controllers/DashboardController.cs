using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index ()
        {
            DashboardModel stats = GetDashboardStats();
            return View("Dashboard", stats);
        }


        private readonly string _connectionString;

        public DashboardController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
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
                    //stats.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    //stats.TotalReports = Convert.ToInt32(reader["TotalReports"]);
                    //stats.ActiveBeds = Convert.ToInt32(reader["ActiveBeds"]);
                    //stats.StaffMembers = Convert.ToInt32(reader["StaffMembers"]);
                }
            }

            return stats;
        }
    }

}
