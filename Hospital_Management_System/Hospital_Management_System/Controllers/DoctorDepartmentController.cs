    using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private List<SelectListItem> GetDoctorList()
        {
            List<SelectListItem> doctorList = new List<SelectListItem>();
            string cs = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using SqlCommand cmd = new SqlCommand("SELECT DoctorID, Name FROM Doctor WHERE IsActive = 1", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    doctorList.Add(new SelectListItem
                    {
                        Value = reader["DoctorID"].ToString(),
                        Text = reader["Name"].ToString()
                    });
                }
            }

            return doctorList;
        }

        private List<SelectListItem> GetDepartmentList()
        {
            List<SelectListItem> departmentList = new List<SelectListItem>();
            string cs = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using SqlCommand cmd = new SqlCommand("SELECT DepartmentID, DepartmentName FROM Department WHERE IsActive = 1", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    departmentList.Add(new SelectListItem
                    {
                        Value = reader["DepartmentID"].ToString(),
                        Text = reader["DepartmentName"].ToString()
                    });
                }
            }

            return departmentList;
        }

        private List<SelectListItem> GetUserList()
        {
            List<SelectListItem> userList = new List<SelectListItem>();
            string cs = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                using SqlCommand cmd = new SqlCommand("SELECT UserID, UserName FROM [User] WHERE IsActive = 1", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new SelectListItem
                    {
                        Value = reader["UserID"].ToString(),
                        Text = reader["UserName"].ToString()
                    });
                }
            }

            return userList;
        }

        public IActionResult Index()
        {
            ViewBag.DoctorList = GetDoctorList();
            ViewBag.DepartmentList = GetDepartmentList();
            ViewBag.UserList = GetUserList();
            return View("DoctorDepartmentAddEdit", new DoctorDepartmentModel());
        }
        public IActionResult Edit(int? DoctorDepartmentID)
        {
            DoctorDepartmentModel model = new DoctorDepartmentModel();

            if (DoctorDepartmentID != null)
            {
                string cs = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("PR_DoctorDepartment_SelectByPK", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DoctorDepartmentID", DoctorDepartmentID);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            model.DoctorDepartmentID = Convert.ToInt32(dr["DoctorDepartmentID"]);
                            model.DoctorID = Convert.ToInt32(dr["DoctorID"]);
                            model.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                            model.Modified = Convert.ToDateTime(dr["Modified"]);
                            model.UserID = Convert.ToInt32(dr["UserID"]);
                            model.Created = Convert.ToDateTime(dr["Created"]);
                            model.DoctorName = dr["DoctorName"].ToString();
                            model.UserName = dr["UserName"].ToString();
                            model.DepartmentName = dr["DepartmentName"].ToString();

                        }
                    }
                }
            }

            // Populate dropdowns
            ViewBag.DoctorList = GetDoctorList();       // List<SelectListItem>
            ViewBag.DepartmentList = GetDepartmentList(); // List<SelectListItem>
            ViewBag.UserList = GetUserList();           // List<SelectListItem>

            return View("DoctorDepartmentAddEdit", model);
        }

        [HttpPost]
        public IActionResult SaveDoctorDepartment(DoctorDepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DoctorList = GetDoctorList();
                ViewBag.DepartmentList = GetDepartmentList();
                ViewBag.UserList = GetUserList();
                return View("DoctorDepartmentAddEdit", model);
            }

            using SqlConnection conn = new(_configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.DoctorDepartmentID == 0 || model.DoctorDepartmentID == null)
            {
                cmd.CommandText = "PR_DoctorDepartment_Add";
                cmd.Parameters.AddWithValue("@DoctorName", model.DoctorName);
                cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
                cmd.Parameters.AddWithValue("@Created", model.Created);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);

            }
            else
            {
                cmd.CommandText = "[dbo].[PR_DoctorDepartment_Edit]";
                cmd.Parameters.AddWithValue("@DoctorDepartmentID", model.DoctorDepartmentID);
            }
            //cmd.Parameters.AddWithValue("@DoctorDepartmentID", model.DoctorDepartmentID);
            cmd.Parameters.AddWithValue("@DoctorID", model.DoctorID);
            cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
            cmd.Parameters.AddWithValue("@Modified", model.Modified);
            cmd.Parameters.AddWithValue("@UserID", model.UserID);


            cmd.ExecuteNonQuery();

            return RedirectToAction("DoctorDepartmentList");
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
