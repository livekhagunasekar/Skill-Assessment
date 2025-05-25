using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Assessment_try.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;

namespace Assessment_try.Pages
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(int? userId)
        {
            if (!userId.HasValue)
                return NotFound();

            string connectionString = _configuration.GetConnectionString("Assessment_tryContext");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.GetUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                con.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: User found in DB, assigning values...");

                    User = new User
                    {
                        Id = (int)reader.GetInt64(0),
                        user_name = reader.GetString(1),
                        user_no = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        create_date = reader.GetDateTime(3)
                    };
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: No user found with that ID.");
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string connectionString = _configuration.GetConnectionString("Assessment_tryContext");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.UpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", User.Id);
                cmd.Parameters.AddWithValue("@UserName", User.user_name);
                cmd.Parameters.AddWithValue("@UserNo", User.user_no.HasValue ? (object)User.user_no.Value : DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("UsersList");
        }
    }
}
