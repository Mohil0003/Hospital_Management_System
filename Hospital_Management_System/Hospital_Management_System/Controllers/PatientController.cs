using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class PatientController : Controller
    {
        private IConfiguration _configuration;

        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("PatientAddEdit");
        }
        public IActionResult PatientList()
        {
            string connectionstring = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[PR_Patient_SelectAll]";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        public IActionResult PatientDelete(int PatientID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Patient_DeleteByPK";
                command.Parameters.Add("@PatientID", SqlDbType.Int).Value = PatientID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("PatientList");
        }

    }
}
