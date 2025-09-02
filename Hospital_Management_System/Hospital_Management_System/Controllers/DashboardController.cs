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
                  
                }
            }

            return stats;
        }
        public IActionResult GetChartData()
        {
            List<object> patientsGrowth = new List<object>();
            List<object> appointmentsByDept = new List<object>();
            List<object> revenueAnalysis = new List<object>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PR_Dashboard_GetStatics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.NextResult();
                        // Patients Growth
                        while (dr.Read())
                        {
                            patientsGrowth.Add(new { Month = dr["Month"], Count = dr["TotalPatients"] });
                        }

                        // Move to next result set
                        dr.NextResult();

                        // Appointments by Department
                        while (dr.Read())
                        {
                            appointmentsByDept.Add(new { Department = dr["DepartmentName"], Count = dr["TotalAppointments"] });
                        }

                        dr.NextResult();

                        // Revenue Analysis
                        while (dr.Read())
                        {
                            revenueAnalysis.Add(new { Quarter = dr["Quarter"], Revenue = dr["TotalRevenue"] });
                        }
                    }
                }
            }

            return Json(new
            {
                patientsGrowth,
                appointmentsByDept,
                revenueAnalysis
            });
        }

    }

}
