using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ADD View
        private List<SelectListItem> GetUserList()
        {
            List<SelectListItem> userList = new List<SelectListItem>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT UserID, UserName FROM [User] WHERE IsActive = 1", conn))
                {
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
            }
            return userList;
        }

        public IActionResult Index()
        {
            ViewBag.UserList = GetUserList();
            return View("DepartmentAddEdit", new DepartmentModel());
        }

        public IActionResult Edit(int? DepartmentID)
        {
            DepartmentModel model = new DepartmentModel();
            if (DepartmentID != null)
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                using SqlCommand cmd = new SqlCommand("PR_Department_SelectByPK", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                    model.DepartmentName = dr["DepartmentName"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    model.Modified = Convert.ToDateTime(dr["Modified"]);
                }
            }

            ViewBag.UserList = GetUserList();
            return View("DepartmentAddEdit", model);
        }

        // SAVE Department (Insert or Update)
        [HttpPost]
        [HttpPost]
        public IActionResult SaveDepartment(DepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                // Fetch user list again if form is invalid
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                using SqlCommand cmd = new SqlCommand("PR_User_SelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                List<SelectListItem> userList = new List<SelectListItem>();
                foreach (DataRow row in dt.Rows)
                {
                    userList.Add(new SelectListItem
                    {
                        Value = row["UserID"].ToString(),
                        Text = row["UserName"].ToString()
                    });
                }

                ViewBag.UserList = userList;
                return View("DepartmentAddEdit", model);
            }

            // Save logic (Insert or Update)
            string cs = _configuration.GetConnectionString("ConnectionString");
            using SqlConnection connSave = new SqlConnection(cs);
            connSave.Open();

            SqlCommand cmdSave;
            if (model.DepartmentID == 0)
            {
                cmdSave = new SqlCommand("PR_Department_Add", connSave);
            }
            else
            {
                cmdSave = new SqlCommand("PR_Department_Edit", connSave);
                cmdSave.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
            }

            cmdSave.CommandType = CommandType.StoredProcedure;
            cmdSave.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
            cmdSave.Parameters.AddWithValue("@Description", model.Description ?? "");
            cmdSave.Parameters.AddWithValue("@UserID", model.UserID);
            cmdSave.Parameters.AddWithValue("@IsActive", model.IsActive);
            cmdSave.Parameters.AddWithValue("@Modified", model.Modified);

            cmdSave.ExecuteNonQuery();

            return RedirectToAction("DepartmentList");
        }

        // LIST Departments
        public IActionResult DepartmentList()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_Department_SelectAll", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dr);

            return View(table);
        }

        // DELETE Department
        public IActionResult DepartmentDelete(int DepartmentID)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("PR_Department_DeleteByPK", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("DepartmentList");
        }
    }
}
