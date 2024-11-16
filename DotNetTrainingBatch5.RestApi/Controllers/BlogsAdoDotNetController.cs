using System.Data;
using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string searchQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag] 
            FROM [dbo].[Tbl_blog] WHERE [DeleteFlag] = 0;";
            SqlCommand command = new SqlCommand(searchQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                Console.WriteLine($"{reader["BlogId"]} {reader["BlogTitle"]} {reader["BlogContent"]} {reader["BlogContent"]}");
                var item = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };
                lst.Add(item);
            }
            connection.Close();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogViewModel item = new BlogViewModel();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string searchByIdQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag] 
            FROM [dbo].[Tbl_blog] WHERE [DeleteFlag] = 0 AND [BlogId] = @BlogId;";
            SqlCommand cmd = new SqlCommand(searchByIdQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if(dt.Rows.Count == 0)
            {
                return NotFound();
            }
            item.Id = Convert.ToInt32(dt.Rows[0]["BlogId"]);
            item.Title = Convert.ToString(dt.Rows[0]["BlogTitle"]);
            item.Author = Convert.ToString(dt.Rows[0]["BlogAuthor"]);
            item.Content = Convert.ToString(dt.Rows[0]["BlogContent"]);
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string insertQuery = $@"INSERT INTO [dbo].[tbl_blog] ([BlogTitle], [BlogAuthor], [BlogContent] ,[DeleteFlag]) VALUES (
                @BlogTitle, @BlogAuthor, @BlogContent, 0
            )";
            SqlCommand cmd = new SqlCommand(insertQuery, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(result == 0 ? "Created Failed." : "Created Successfully.");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection();
            connection.Open();
            string searchByIdQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag]
            FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(searchByIdQuery, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if(dt.Rows.Count == 0) {
                return NotFound();
            }
            string updateQuery = @"UPDATE [dbo].[tbl_blog] 
            SET [BlogTitle] = @BlogTitle, [BlogAuthor] = @BlogAuthor,[BlogContent] = @BlogContent WHERE 
            [BlogId] = @BlogId;";
            SqlCommand cmdUpdate = new SqlCommand(updateQuery, connection);
            cmdUpdate.Parameters.AddWithValue("@BlogId", blog.Id);
            cmdUpdate.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmdUpdate.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmdUpdate.Parameters.AddWithValue("@BlogContent", blog.Content);
            int result = cmdUpdate.ExecuteNonQuery();
            connection.Close();
            return Ok(result == 0 ? "Updated Failed." : "Updated Succesfully.");
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";
            if (!String.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!String.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!String.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }
            if(conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters!");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string updateQuery = $@"UPDATE [dbo].[Tbl_blog] SET {conditions} WHERE [BlogId] = @BlogId";
            SqlCommand cmd = new SqlCommand(updateQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!String.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!String.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!String.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(result > 0 ? "Updated Successfully." : "Updating Failed.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string searchByIdQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag] FROM [dbo].[Tbl_blog] 
            WHERE [BlogId] = @BlogId AND [DeleteFlag] = 0";
            SqlCommand searchCmd = new SqlCommand(searchByIdQuery, connection);
            searchCmd.Parameters.AddWithValue("@BlogId", id);
            int result = searchCmd.ExecuteNonQuery();
            if(result == 0)
            {
                return NotFound();
            }
            string deleteQuery = $@"UPDATE [dbo].[Tbl_blog] SET [DeleteFlag] = 1 WHERE [BlogId] = @BlogId";
            SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
            deleteCmd.Parameters.AddWithValue("@BlogId", id);
            int result1 = deleteCmd.ExecuteNonQuery();
            connection.Close();
            return Ok(result1 == 0 ? "Deleted Failed." : "Deleted Successfully.");
        }
    }
}
