using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetTrainingBatch5.Shared;
using HALLDotNetCore.ConsoleApp.Models;

namespace HALLDotNetCore.ConsoleApp
{
    class DapperExample2
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";
        private readonly DapperService _dapperService;
        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }
        public void Read()
        {
            string query = "SELECT * FROM [tbl_blog] WHERE [DeleteFlag] = 0;";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach(var item in lst)
            {
                Console.WriteLine($"{item.BlogId} {item.BlogTitle} {item.BlogAuthor} {item.BlogContent}");
            }
        }
        public void Edit(int id)
        {
            //Console.Write("Enter Blog id = ");
            //string id = Console.ReadLine();
            string query = "SELECT * FROM [tbl_blog] WHERE [BlogId] = @BlogId";
            var item = _dapperService.QueryFirstOrDefaullt<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = id
            });
            if(item is null)
            {
                Console.WriteLine("No data is founded."); return;
            }
            Console.WriteLine($"{item.BlogId} {item.BlogTitle} {item.BlogAuthor} {item.BlogContent}");
        }
        public void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[tbl_blog] ([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag]) VALUES (
            @BlogTitle, @BlogAuthor, @BlogContent, 0);";
            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 0 ? "Created Failed." : "Created Successfully.");
        }
    }
}
