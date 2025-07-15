using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class AppointmentController : Controller
    {
        private IConfiguration _configuration;
        public AppointmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("AppointmentAddEdit");
        }

        public IActionResult AppointmentList()
        {
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "PR_Appointment_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        public IActionResult AppointmnentDelete(int AppointmentID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Appointment_DeleteByPK";
                command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = AppointmentID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("AppointmentList");
        }
    }
}
