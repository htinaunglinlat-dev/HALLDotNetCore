// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

//Console.WriteLine("Hello, World!");
//Console.ReadLine();
//Console.ReadKey();

// md => MarkDown

// C# <=> Database

// ORM , C# -> SQL Query
// ADO.net (old school)
// Dapper
// EFCore / Entity Framework

// NuGet https://www.nuget.org/ (package for C#)

string connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";
Console.WriteLine("connection string = " + connectionString);
SqlConnection connection = new SqlConnection(connectionString);

Console.WriteLine("Connection is opening ....");
connection.Open();
Console.WriteLine("Connection is opened.");

string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[tbl_blog]";
SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd); 
//DataTable dt = new DataTable();
//adapter.Fill(dt);

// DataSet -> DataTable -> DataRow -> DataColumn
//foreach(DataRow dr in dt.Rows)
//{
//    Console.Write(dr["BlogId"] + "\t");
//    Console.Write(dr["BlogTitle"] + "\t");
//    Console.Write(dr["BlogAuthor"] + "\t");
//    Console.Write(dr["BlogContent"] + "\t");
//    Console.Write(dr["DeleteFlag"] + "\t");
//    // Console.WriteLine();
//}

SqlDataReader reader = cmd.ExecuteReader();
while(reader.Read())
{
    Console.Write(reader["BlogId"] + "\t");
    Console.Write(reader["BlogTitle"] + "\t");
    Console.Write(reader["BlogAuthor"] + "\t");
    Console.Write(reader["BlogContent"] + "\t");
    Console.Write(reader["DeleteFlag"] + "\t");
    Console.WriteLine();
}

Console.WriteLine("Connection is closing ...");
connection.Close();
Console.WriteLine("Connection is closed.");