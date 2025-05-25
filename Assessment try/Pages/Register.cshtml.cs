using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assessment_try.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace Assessment_try.Pages
{
    public class Index1Model : PageModel
    {
        private readonly IConfiguration _configuration;

        public Index1Model(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public User User { get; set; }
        public string? Message { get; set; }

        public void OnGet(int? id)
        {
            if (id.HasValue)
            {
                string connectionString = _configuration.GetConnectionString("Assessment_tryContext");
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.GetUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", id.Value);

                    con.Open();
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        User = new User
                        {
                            Id = reader.GetInt32(0),
                            user_name = reader.GetString(1),
                            user_no = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            create_date = reader.GetDateTime(3)
                        };
                    }
                }
            }
            else
            {

                User = new User();
            }
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string connectionString = _configuration.GetConnectionString("Assessment_tryContext");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                if (User.Id > 0)
                {
                    // Update User
                    cmd = new SqlCommand("dbo.UpdateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", User.Id);
                    cmd.Parameters.AddWithValue("@UserName", User.user_name);
                    cmd.Parameters.AddWithValue("@UserNo", (object)User.user_no ?? DBNull.Value);
                }
                else
                {
                    // Add new user
                    cmd = new SqlCommand("dbo.AddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", User.user_name);
                    cmd.Parameters.AddWithValue("@UserNo", (object)User.user_no ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage("UsersList");
        }
    }
}
