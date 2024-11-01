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

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
adoDotNetExample.Create();
//adoDotNetExample.Read();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
adoDotNetExample.Read();
adoDotNetExample.Delete();
adoDotNetExample.Read();

