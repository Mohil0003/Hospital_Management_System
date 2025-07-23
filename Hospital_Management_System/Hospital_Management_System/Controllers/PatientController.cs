using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class PatientController : Controller
    {
        private readonly IConfiguration _configuration;

        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private List<SelectListItem> GetUserList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string cs = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserID, UserName FROM [User] WHERE IsActive = 1", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = dr["UserID"].ToString(),
                        Text = dr["UserName"].ToString()
                    });
                }
            }

            return list;
        }

        public IActionResult Index()
        {
            ViewBag.UserList = GetUserList();
            return View("PatientAddEdit", new PatientModel());
        }

        public IActionResult Edit(int? PatientID)
        {
            PatientModel model = new PatientModel();

            if (PatientID != null)
            {
                string cs = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("PR_Patient_SelectByPK", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientID", PatientID);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        model.PatientID = Convert.ToInt32(dr["PatientID"]);
                        model.Name = dr["Name"].ToString();
                        model.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                        model.Gender = dr["Gender"].ToString();
                        model.Email = dr["Email"].ToString();
                        model.Phone = dr["Phone"].ToString();
                        model.Address = dr["Address"].ToString();
                        model.City = dr["City"].ToString();
                        model.State = dr["State"].ToString();
                        model.UserID = Convert.ToInt32(dr["UserID"]);
                        model.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        model.Modified = Convert.ToDateTime(dr["Modified"]);
                    }
                }
            }

            ViewBag.UserList = GetUserList();
            return View("PatientAddEdit", model);
        }

        [HttpPost]
        public IActionResult SavePatient(PatientModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserList = GetUserList();
                return View("PatientAddEdit", model);
            }

            string cs = _configuration.GetConnectionString("ConnectionString");
            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            SqlCommand cmd;
            if (model.PatientID == 0)
                cmd = new SqlCommand("PR_Patient_Add", conn);
            else
            {
                cmd = new SqlCommand("PR_Patient_Edit", conn);
                cmd.Parameters.AddWithValue("@PatientID", model.PatientID);
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", model.Gender);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Address", model.Address);
            cmd.Parameters.AddWithValue("@City", model.City);
            cmd.Parameters.AddWithValue("@State", model.State);
            cmd.Parameters.AddWithValue("@UserID", model.UserID);
            cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
            cmd.Parameters.AddWithValue("@Modified", model.Modified);

            cmd.ExecuteNonQuery();
            return RedirectToAction("PatientList");
        }

        public IActionResult PatientList()
        {
            string cs = _configuration.GetConnectionString("ConnectionString");
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            SqlCommand cmd = new SqlCommand("PR_Patient_SelectAll", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            return View(table);
        }

        public IActionResult PatientDelete(int PatientID)
        {
            try
            {
                string cs = _configuration.GetConnectionString("ConnectionString");
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                SqlCommand cmd = new SqlCommand("PR_Patient_DeleteByPK", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientID", PatientID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("PatientList");
        }
    }
}
