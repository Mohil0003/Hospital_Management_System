using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private List<SelectListItem> GetDoctorList()
        {
            List<SelectListItem> list = new();
            using SqlConnection conn = new(_configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new("SELECT DoctorID, Name FROM Doctor", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new SelectListItem { Value = dr["DoctorID"].ToString(), Text = dr["Name"].ToString() });
            }
            return list;
        }

        private List<SelectListItem> GetPatientList()
        {
            List<SelectListItem> list = new();
            using SqlConnection conn = new(_configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new("SELECT PatientID, Name FROM Patient", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new SelectListItem { Value = dr["PatientID"].ToString(), Text = dr["Name"].ToString() });
            }
            return list;
        }

        private List<SelectListItem> GetUserList()
        {
            List<SelectListItem> list = new();
            using SqlConnection conn = new(_configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new("SELECT UserID, UserName FROM [User] WHERE IsActive = 1", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new SelectListItem { Value = dr["UserID"].ToString(), Text = dr["UserName"].ToString() });
            }
            return list;
        }
        public IActionResult Index()
        {
            ViewBag.DoctorList = GetDoctorList();
            ViewBag.PatientList = GetPatientList();
            ViewBag.UserList = GetUserList();
            return View("AppointmentAddEdit", new AppointmentModel());
        }
        public IActionResult Edit(int? AppointmentID)
        {
            AppointmentModel model = new AppointmentModel();

            if (AppointmentID != null)
            {
                string cs = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("PR_Appointment_SelectByPK", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            model.AppointmentID = Convert.ToInt32(dr["AppointmentID"]);
                            model.DoctorID = Convert.ToInt32(dr["DoctorID"]);
                            model.PatientID = Convert.ToInt32(dr["PatientID"]);
                            model.AppointmentDate = Convert.ToDateTime(dr["AppointmentDate"]);
                            model.AppointmentStatus = dr["AppointmentStatus"].ToString();
                            model.Description = dr["Description"].ToString();
                            model.SpecialRemarks = dr["SpecialRemarks"].ToString();
                            model.Modified = Convert.ToDateTime(dr["Modified"]);
                            model.UserID = Convert.ToInt32(dr["UserID"]);
                            model.TotalConsultedAmount = Convert.ToDecimal(dr["TotalConsultedAmount"]);
                        }
                    }
                }
            }

            // Dropdown population
            ViewBag.DoctorList = GetDoctorList();     // List<SelectListItem>
            ViewBag.PatientList = GetPatientList();   // List<SelectListItem>
            ViewBag.UserList = GetUserList();         // List<SelectListItem>

            return View("AppointmentAddEdit", model);
        }


        [HttpPost]
        public IActionResult SaveAppointment(AppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DoctorList = GetDoctorList();
                ViewBag.PatientList = GetPatientList();
                ViewBag.UserList = GetUserList();
                return View("AppointmentAddEdit", model);
            }

            using SqlConnection conn = new(_configuration.GetConnectionString("ConnectionString"));
            conn.Open();

            SqlCommand cmd;

            if (model.AppointmentID == null || model.AppointmentID == 0)
            {
                cmd = new SqlCommand("PR_Appointment_Add", conn);
            }
            else
            {
                cmd = new SqlCommand("PR_Appointment_Edit", conn);
                cmd.Parameters.AddWithValue("@AppointmentID", model.AppointmentID); // ✅ Important!
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DoctorID", model.DoctorID);
            cmd.Parameters.AddWithValue("@PatientID", model.PatientID);
            cmd.Parameters.AddWithValue("@AppointmentDate", model.AppointmentDate);
            cmd.Parameters.AddWithValue("@AppointmentStatus", model.AppointmentStatus);
            cmd.Parameters.AddWithValue("@Description", (object?)model.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SpecialRemarks", (object?)model.SpecialRemarks ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
            cmd.Parameters.AddWithValue("@UserID", model.UserID);
            cmd.Parameters.AddWithValue("@TotalConsultedAmount", (object?)model.TotalConsultedAmount ?? DBNull.Value);

            cmd.ExecuteNonQuery();
            return RedirectToAction("AppointmentList");
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
        public IActionResult AppointmentDelete(int AppointmentID)
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
