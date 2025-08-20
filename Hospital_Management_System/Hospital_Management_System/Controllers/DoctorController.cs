using ClosedXML.Excel;
using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IConfiguration _configuration;

        public DoctorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public IActionResult ExportToExcel()
        {
            DataTable dt = RetrieveData("PR_Doctor_SelectAll");

            using (var workbook = new XLWorkbook())
            {
                // Add the DataTable to a worksheet
                workbook.Worksheets.Add(dt, "Doctors");

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Doctors.xlsx"
                    );
                }
            }
        }


        public DataTable RetrieveData(String SP)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = SP;
            //if (PKID != 0)
            //{
            //    cmd.Parameters.AddWithValue("@" + PKName, PKID);
            //}
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            conn.Close();

            return dt;
        }

        // Dropdown: Get list of active users
        private List<SelectListItem> GetUserList()
        {
            List<SelectListItem> userList = new List<SelectListItem>();
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
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

        // Show Add Doctor Form
        public IActionResult Index()
        {
            ViewBag.UserList = GetUserList();
            return View("DoctorAddEdit", new DoctorModel());
        }

        // Edit Existing Doctor
        public IActionResult Edit(int? DoctorID)
        {
            DoctorModel model = new DoctorModel();

            if (DoctorID != null)
            {
                string cs = _configuration.GetConnectionString("ConnectionString");
                using SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                using SqlCommand cmd = new SqlCommand("PR_Doctor_SelectByPK", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model.DoctorID = Convert.ToInt32(dr["DoctorID"]);
                    model.Name = dr["Name"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.Qualification = dr["Qualification"].ToString();
                    model.Specialization = dr["Specialization"].ToString();
                    model.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    model.Modified = Convert.ToDateTime(dr["Modified"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                }
            }

            ViewBag.UserList = GetUserList();
            return View("DoctorAddEdit", model);
        }

        // Save (Add or Update Doctor)
        [HttpPost]
        public IActionResult SaveDoctor(DoctorModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserList = GetUserList();
                return View("DoctorAddEdit", model);
            }

            string cs = _configuration.GetConnectionString("ConnectionString");
            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            SqlCommand cmd;

            if (model.DoctorID == null || model.DoctorID == 0)
            {
                cmd = new SqlCommand("PR_Doctor_Add", conn);
            }
            else
            {
                cmd = new SqlCommand("PR_Doctor_Edit", conn);
                cmd.Parameters.AddWithValue("@DoctorID", model.DoctorID);
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Qualification", model.Qualification);
            cmd.Parameters.AddWithValue("@Specialization", model.Specialization);
            cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
            cmd.Parameters.AddWithValue("@Modified", model.Modified);
            cmd.Parameters.AddWithValue("@UserID", model.UserID);

            cmd.ExecuteNonQuery();

            return RedirectToAction("DoctorList");
        }

        // List All Doctors
        public IActionResult DoctorList()
        {
            string cs = _configuration.GetConnectionString("ConnectionString");
            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_Doctor_SelectAll", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            return View(dt);
        }

        // Delete Doctor
        public IActionResult DoctorDelete(int DoctorID)
        {
            try
            {
                string cs = _configuration.GetConnectionString("ConnectionString");
                using SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                SqlCommand cmd = new SqlCommand("PR_Doctor_Delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("DoctorList");
        }
    }
}
