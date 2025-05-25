# Skill-Assessment
Skill Assessment - Livekha

# Skill Assessment - ASP.NET Razor Pages CRUD App

This is a simple **CRUD (Create, Read, Update, Delete)** web application built using **ASP.NET Core Razor Pages**. The application manages a list of users and allows registration, editing, deletion, and viewing of user data.

## 👤 Developed By
**Livekha Gunasekar**

---

## 📌 Features

- ✅ Register new users  
- ✅ View a user list with sorting  
- ✅ Edit user information  
- ✅ Delete users  
- ✅ Stylish and responsive UI with Bootstrap  
- ✅ SQL Server database integration  

---

## 💻 Technologies Used

- ASP.NET Core Razor Pages  
- Entity Framework Core  
- SQL Server  
- Bootstrap 5  
- C#  
- Git & GitHub  

---

## 📂 Project Structure
Assessment/
│
├── Pages/
│ ├── Index.cshtml # Home page
│ ├── Index.cshtml.cs
│ ├── Register.cshtml # User registration form
│ ├── Register.cshtml.cs
│ ├── UsersList.cshtml # List of registered users
│ ├── UsersList.cshtml.cs
│ ├── Edit.cshtml # Edit user details
│ ├── Edit.cshtml.cs
│
├── Models/
│ └── User.cs # User entity model
│
├── Data/
│ └── AppDbContext.cs # EF Core DB context
│
├── wwwroot/
│ ├── css/
│ │ └── site.css # Custom styles
│ └── lib/
│ └── bootstrap/ # Bootstrap files
│
├── appsettings.json # Configuration for database connection
├── SkillAssessment.bak # 📦 SQL Server database backup file
├── README.md # Project documentation
├── Program.cs # App startup logic
├── Startup.cs # Middleware, routing, DB setup
└── .gitignore # Git ignored files


---

## 🔧 How to Run the Project

1. **Clone the repository:**
   ```bash
   git clone https://github.com/livekhagunasekar/Skill-Assessment.git
   cd Skill-Assessment
2. Open the project in Visual Studio.

3. Restore NuGet packages (automatically triggered in Visual Studio or run dotnet restore).

4. Set the connection string in appsettings.json:
   "ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=SkillAssessmentDb;Trusted_Connection=True;"
}
5. Run the application using Visual Studio or:
   dotnet run

🗃️ Restore Database from Backup
1. Open SQL Server Management Studio (SSMS).
2. Right-click on Databases > Restore Database...
3. Choose Device and browse to SkillAssessment.bak.
4. Click OK to restore.
5. Make sure the database name matches the one in appsettings.json.
   

🔗 GitHub Repository : https://github.com/livekhagunasekar/Skill-Assessment.git




