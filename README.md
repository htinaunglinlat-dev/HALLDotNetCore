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

