using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management_System.Controllers
{
    public class UserLoginController : Controller
    {
        private IConfiguration _configuration;
        public UserLoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction("Login", "User",model);
        }
        public IActionResult ValidateLogin(UserLoginModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    // If the model is not valid (e.g., empty username/password),
            //    // return the user to the login page to see the errors.
            //    return View("Login");
            //}
            if (ModelState.IsValid)
            {


                try
                {
                    string connectionString = this._configuration.GetConnectionString("ConnectionString");
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        SqlCommand sqlCommand = sqlConnection.CreateCommand();
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "PR_User_ValidateLogin";
                        sqlCommand.Parameters.AddWithValue("@Username", model.Username);
                        sqlCommand.Parameters.AddWithValue("@Password", model.Password); // Your SP should handle password comparison

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        if (sqlDataReader.HasRows)
                        {
                            // If the database returns a user, the login is successful
                            while (sqlDataReader.Read())
                            {
                                // Store user details in the session
                                HttpContext.Session.SetString("UserID", sqlDataReader["UserID"].ToString());
                                HttpContext.Session.SetString("UserName", sqlDataReader["UserName"].ToString());
                                HttpContext.Session.SetString("EmailAddress", sqlDataReader["Email"].ToString());
                            }

                            return RedirectToAction("Index", "Dashboard"); // Redirect to your main dashboard
                        }
                        else
                        {
                            // If no user is found, show an error message
                            TempData["ErrorMessage"] = "Invalid username or password.";
                            return RedirectToAction("Login","User");
                        }
                    }
                }
                catch (Exception e)
                {
                    // Handle any database or other errors
                    TempData["ErrorMessage"] = "An error occurred during login: " + e.Message;
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login","UserLogin", model);
        }

        public IActionResult SignUp(UserModel model)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("SignUp", "User" , model);
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

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
