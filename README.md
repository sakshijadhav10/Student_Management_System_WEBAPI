# Student_Management_System_WEBAPI
This project is a Student Management System built using ASP.NET Core Web API (.NET 8). It follows a clean and scalable 3-layer architecture with SQL Server, JWT Authentication, Serilog Logging, and Global Exception Handling.


PROJECT STRUCTURE

Student_Management_System/
├── Student_Management_System_API (API Layer)
│ Controllers
│ Middleware
│ Program.cs
│ appsettings.json
│
├── Student_Management_System_BLL (Business Logic Layer)
│ DTOs
│ Interfaces
│ Services
│
└── Student_Management_System_DAL (Data Access Layer)
Data
Models
Interfaces
Repositories

REQUIRED NUGET PACKAGES

API Project:

Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Authentication.JwtBearer
Serilog.AspNetCore
Swashbuckle.AspNetCore

BLL & DAL:

Microsoft.Extensions.Configuration.Abstractions
BCrypt.Net-Next
SETUP AND INSTALLATION
Clone the repository:
git clone <repo-url>
Open the solution in Visual Studio or VS Code.
Create a SQL Server database named:
Student_API_DB
Update the connection string in appsettings.json:
"ConnectionStrings": {
"DefaultConnection": "Server=YOUR_SERVER;Database=Student_API_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Update JWT settings:
"Jwt": {
"Key": "YOUR_SECRET_KEY",
"Issuer": "yourapp.com",
"Audience": "yourapp.com"
}
Run migrations:
In Package Manager Console:
Set "Default Project" = Student_Management_System_API
Run:
Add-Migration InitialCreate
Update-Database
Run the application:
dotnet run
or press F5 in Visual Studio.
Open Swagger UI in browser:
https://localhost:7039/swagger
AUTHENTICATION USAGE (SWAGGER)

Step 1: Register user
Endpoint: POST /api/auth/register
Response contains a JWT token.

Step 2: Login user
Endpoint: POST /api/auth/register
Response contains a JWT token use this token to Authorize.

Step 3: Click the "Authorize" button in Swagger.
Paste token as:
Bearer <your_jwt_token>

Step 3: Access protected Student APIs:

GET /api/student/getall
POST /api/student/add
PUT /api/student/update
GET /api/student/get/{id}
DELETE /api/student/delete/{id}
