using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{


    public class UserController : Controller
    {
        private IConfiguration _configuration;

         public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("UserAddEdit", new UserModel());
        }
        public IActionResult Edit(int? UserID)
        {
            UserModel model = new UserModel();

            if (UserID != null)
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                using SqlCommand cmd = new SqlCommand("PR_User_SelectByPK", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.UserName = dr["UserName"].ToString();
                    model.Password = dr["Password"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.MobileNo = dr["MobileNo"].ToString();
                    model.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    model.Modified = Convert.ToDateTime(dr["Modified"]);
                }
            }

            return View("UserAddEdit", model);
        }

        // Save (Insert or Update) User
        [HttpPost]
        public IActionResult SaveUser(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("UserAddEdit", model);
            }

            string connectionString = _configuration.GetConnectionString("ConnectionString");
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd;

            if (model.UserID == 0)
            {
                // INSERT
                cmd = new SqlCommand("PR_User_AddUser", conn);
            }
            else
            {
                // UPDATE
                cmd = new SqlCommand("PR_User_EditUser", conn);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", model.UserName);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@MobileNo", model.MobileNo);
            cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
            cmd.Parameters.AddWithValue("@Modified", model.Modified);

            cmd.ExecuteNonQuery();

            return RedirectToAction("UserList");
        }
        public IActionResult UserList()
        {
            string ConnectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[PR_User_SelectAll]";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult UserDelete(int UserID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_User_DeleteByPK";
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("UserList");
        }
    }
}
