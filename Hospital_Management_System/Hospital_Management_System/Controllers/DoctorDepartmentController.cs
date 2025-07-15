using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class DoctorDepartmentController : Controller
    {
        private IConfiguration _configuration;

        public DoctorDepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("DoctorDepartmentAddEdit");
        }

        public IActionResult DoctorDepartmentList()
        {
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "PR_DoctorDepartment_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        public IActionResult DoctorDepartmentDelete(int DoctorDepartmentID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_DoctorDepartment_DeleteByPK";
                command.Parameters.Add("@DoctorDepartmentID", SqlDbType.Int).Value = DoctorDepartmentID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("DoctorDepartmentList");
        }
    }
}
