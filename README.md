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

------------------------------

mssql basic
C# basic

console app
ado.net
dapper
efcore
efcore database first
northwind database
asp.net core web api (dot net 7)
minimal api / [ ado.net / dapper => custom service ]

.net 7
.net core 3.1

---------------------------------

File.json
Read => Convert Object [] => Json => Write

--------------------------------

Kpay 

Mobile No
Me - Another one


Id
Login full-name
Mobile Number 
Balance
pin

# API
- Deposite (in) => + (mobile number, balance) => input (+)
- Withdraw (out) => - (mobile number, balance) => input (+) (minimum amount at account = 10k)

- Transfer API (requirement) => FromMobileNo ToMobileNo Amount Pin
Notes
FromMobileNo != ToMobileNo
check FromMobileNo and ToMobileNo
Pin check
Balance
Note
FromBalance -
ToBalance + 
Message (Complete)
Transition History
Transition Receive and Send Message

Balance 

User Account Register
Login
Change Pin
Phone Change
Forget Password and Reset Password
First Time Login Pin Set Up

## Case
- Validation error
- Logic error
- CRUD error
- Success case

{
	"RespCode", "RespDesp", "RespType", "IsSuccess", "IsError"
} // RespType<Success OR SystemError OR Warning OR ValidatoinError>
