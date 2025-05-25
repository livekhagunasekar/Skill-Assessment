using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Assessment_try.Models;


public class UsersListModel : PageModel
{
	private readonly IConfiguration _configuration;
	public List<User> Users { get; set; } = new();

	public string CurrentSort { get; set; } = "";
	public string UserNameSort { get; set; } = "";
	public string UserNoSort { get; set; } = "";
	public string DateSort { get; set; } = "";


	public UsersListModel(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void OnGet(string sortBy, string sortDir)
	{
		CurrentSort = sortBy;
		UserNameSort = sortBy == "user_name" && sortDir == "asc" ? "UserName_desc" : "UserName";
		UserNoSort = sortBy == "user_no" && sortDir == "asc" ? "UserNo_desc" : "UserNo";
		DateSort = sortBy == "create_date" && sortDir == "asc" ? "CreateDate_desc" : "CreateDate";

		string connectionString = _configuration.GetConnectionString("Assessment_tryContext");
		using var conn = new SqlConnection(connectionString);
		using var cmd = new SqlCommand("dbo.GetUsers", conn);
		cmd.CommandType = CommandType.StoredProcedure;
		conn.Open();

		using var reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			Users.Add(new User
			{
                Id = (int)reader.GetInt64(0),
                user_name = reader.GetString(1),
				user_no = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
				create_date = reader.GetDateTime(3)
			});
		}

		// Sorting logic in-memory (or do it in SP)
		if (!string.IsNullOrEmpty(sortBy))
		{
			switch (sortBy)
			{
				case "user_name":
					Users = sortDir == "desc"
						? Users.OrderByDescending(u => u.user_name).ToList()
						: Users.OrderBy(u => u.user_name).ToList();
					break;
				case "user_no":
					Users = sortDir == "desc"
						? Users.OrderByDescending(u => u.user_no).ToList()
						: Users.OrderBy(u => u.user_no).ToList();
					break;
				case "create_date":
					Users = sortDir == "desc"
						? Users.OrderByDescending(u => u.create_date).ToList()
						: Users.OrderBy(u => u.create_date).ToList();
					break;
				case "Id":
				default:
					Users = sortDir == "desc"
						? Users.OrderByDescending(u => u.Id).ToList()
						: Users.OrderBy(u => u.Id).ToList();
					break;
			}

		}
	}

	public IActionResult OnPostDelete(int userId)
	{
		string connectionString = _configuration.GetConnectionString("Assessment_tryContext");
		using var conn = new SqlConnection(connectionString);
		using var cmd = new SqlCommand("dbo.DeleteUser", conn);
		cmd.CommandType = CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@UserID", userId);
		conn.Open();
		cmd.ExecuteNonQuery();

		return RedirectToPage();
	}
}
