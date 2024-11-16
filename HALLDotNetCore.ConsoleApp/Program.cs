// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using HALLDotNetCore.ConsoleApp;

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

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();
//adoDotNetExample.Read();
//adoDotNetExample.DeleteWithFlag();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("asd", "asd", "asd");
//dapperExample.Read();
//dapperExample.Update(100, "hello", "hello", "hello");
//dapperExample.Read();
//Console.WriteLine(".................");
//dapperExample.Update(19, "hello world", "hello world", "hello world");
//dapperExample.Read();
//dapperExample.deleteById(5);
//dapperExample.Read();

//EFCoreExample efCoreExample = new EFCoreExample();
//efCoreExample.Read();
//efCoreExample.Create("new one", "lama dev", "new one is the best");
//efCoreExample.Edit(9);
//efCoreExample.Edit(4);
//efCoreExample.Edit(100);
//efCoreExample.Update(17, "asd123!@#", null, "");
//efCoreExample.DeleteByFlag(20);
//efCoreExample.DeleteByFlag(6);
//efCoreExample.Read();


string _connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";

    Console.WriteLine("connection string = " + _connectionString);
    SqlConnection connection = new SqlConnection(_connectionString);

    Console.WriteLine("Connection is opening ....");
    connection.Open();
    Console.WriteLine("Connection is opened.");

connection.Close();
Console.WriteLine("Connection is closed");

Console.ReadKey();