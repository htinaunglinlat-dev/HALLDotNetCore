using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HALLDotNetCore.ConsoleApp.Models;

namespace HALLDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";

        public void Read()
        {
            // DTO -> Data Transfer Object
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [tbl_blog] WHERE [DeleteFlag] = 0";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine($"{item.BlogId} {item.BlogTitle} {item.BlogAuthor} {item.BlogContent}");
                }

            }
        }
        public void Create(string title, string author, string content)
        {
            string insertQuery = @"INSERT INTO [dbo].[tbl_blog] (
                    [BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag]
                ) VALUES (
                    @BlogTitle, @BlogAuthor, @BlogContent, 0
                )";

            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(insertQuery, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine((result == 0) ? "Saving Failed." : "Saving Successfully.");
            }
        }

        public void Update(int id, string title, string author, string content)
        {
            string searchByIdQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent] FROM [dbo].[tbl_blog] WHERE [BlogId] = @BlogId AND [DeleteFlag] = 0";
            string updateQuery = @"UPDATE [dbo].[tbl_blog] SET 
                [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var searchItem = db.Query<BlogDataModel>(searchByIdQuery, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();
                if(searchItem is null)
                {
                    Console.WriteLine($"No data was found by provided id {id}");
                    return;
                }
                Console.WriteLine($"Old record = {searchItem.BlogId} {searchItem.BlogTitle} {searchItem.BlogAuthor} {searchItem.BlogContent}");
                int updatedResult = db.Execute(updateQuery, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine((updatedResult == 0) ? "Failed to change." : "Successfully changed.");
            }
        }

        public void deleteById(int id)
        {
            string searchQuery = "SELECT * FROM [dbo].[tbl_blog] WHERE [BlogId] = @BlogId AND [DeleteFlag] = 0";
            string updateQuery = @"UPDATE [dbo].[tbl_blog] SET [DeleteFlag] = 1 WHERE [BlogId] = @BlogId";
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                var searchItem = db.Query<BlogDataModel>(searchQuery, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();
                if(searchItem is null)
                {
                    Console.WriteLine($"No data was found by provided id {id}.");
                    return;
                }
                int result = db.Execute(updateQuery, new BlogDataModel
                {
                    BlogId = id
                });
                Console.WriteLine((result == 0) ? "Deleted Failed." : "Deleted Successfully.");
            }
        }
    }
}
