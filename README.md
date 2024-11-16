# hello

# AsNoTracting()
SELECT * FROM tbl_blog with (nolock);
// AsNoTracking()

## commit data/ uncommit data

INSERT INTO tbl_blog;
UPDATE tbl_blog;

efcore 
database first (manual, auto)

=> code first  
/ build the code and the table is auto created after execution the code

dotnet ef dbcontext scaffold "Server=DESKTOP-UST9CM1\SQLEXPRESS;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f
"Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123"

Request Model
Response Model
DTO

----------------------------

5 weeks

Visual Studio Installation
Microsoft SQL Server 2022
SSMS (SQL Server Management System)

C# Basic 
SQL Basic

Console App (Create Project)
DTO (data transfer object)
Nuget Package (package manager)
Dapper
- ORM
- Data Model
- AsNoTracking
ADO.NET
EFCore
- AppDbContext
- Database First
REST API (ASP.NET Core Web Api)
- POST man
- HTTP Method
- Swagger
- HTTP Status Code

----------------------------

Backend API

data model (data access, database) 10 columns
view model (frontend return data) 2 columns