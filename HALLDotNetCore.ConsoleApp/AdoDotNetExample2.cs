using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetTrainingBatch5.Shared;

namespace HALLDotNetCore.ConsoleApp
{
    class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";
        private readonly AdoDotNetService _adoDotNetService;
        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }
        public void Read()
        {
            string query = @"SELECT [BlogId], [BlogAuthor], [BlogTitle], [BlogContent], [DeleteFlag] FROM [dbo].[Tbl_Blog] WHERE [DeleteFlag] = 0;";
            DataTable dt = _adoDotNetService.Query(query);
            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("Empty Data Set.");
                return;
            }
            foreach(DataRow dr in dt.Rows)
            {
                Console.WriteLine($"{dr["BlogId"]} {dr["BlogTitle"]} {dr["BlogAuthor"]} {dr["BlogContent"]}");
            }

        }
        public void Edit()
        {
            Console.Write("Enter Blog Id = ");
            string id = Console.ReadLine();
            string query = @"SELECT [BlogId], [BlogAuthor], [BlogTitle], [BlogContent], [DeleteFlag] FROM [dbo].[Tbl_Blog] WHERE [DeleteFlag] = 0 AND [BlogId] = @BlogId;";
            DataTable dt = _adoDotNetService.Query(query, new SqlParameterModel ("@BlogId", id));
            if (dt.Rows.Count == 0) {
                Console.WriteLine("No record is founded. ");return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine($"{dr["BlogId"]} {dr["BlogTitle"]} {dr["BlogAuthor"]} {dr["BlogContent"]}");
        }
        public void Create()
        {
            Console.WriteLine("Enter Blog Title = ");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Blog Author = ");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Blog Content = ");
            string content = Console.ReadLine();

            string insertQuery = $@"INSERT INTO [dbo].[tbl_blog] (
                [BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent]
                ,[DeleteFlag]) VALUES (
                @BlogTitle, @BlogAuthor, @BlogContent, 0
            )";
            //int result = _adoDotNetService.Execute(insertQuery, new SqlParameterModel
            //{
            //    Name = "@BlogTitle", Value = title
            //}, new SqlParameterModel
            //{
            //    Name = "@BlogAuthor", Value = author
            //}, new SqlParameterModel
            //{
            //    Name = "@BlogContent", Value = content
            //});
            int result = _adoDotNetService.Execute(insertQuery,
                new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthor", author),
                new SqlParameterModel("@BlogContent", content));
            Console.WriteLine(result == 0 ? "Created Failed." : "Created Successfully.");
        }
    }
}
