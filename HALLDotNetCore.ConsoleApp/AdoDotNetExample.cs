using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HALLDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";
        public void Read() {
            
            Console.WriteLine("connection string = " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

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
            //foreach(DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine($"{dr["BlogId"]} {dr["BlogTitle"]} {dr["BlogContent"]}");
            //}

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
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
        }

        public void Create()
        {
            Console.Write("Blog Title : ");
            string title = Console.ReadLine();

            Console.Write("Blog Author : ");
            string author = Console.ReadLine();

            Console.Write("Blog Content: ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            //string insertQuery1 = $@"INSERT INTO [dbo].[tbl_blog]
            //      ([BlogTitle]
            //      ,[BlogAuthor]
            //      ,[BlogContent]
            //      ,[DeleteFlag]) VALUES (
            //    '{title}', '{author}', '{content}', 0
            //)";

            string insertQuery = $@"INSERT INTO [dbo].[tbl_blog] (
                [BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent]
                ,[DeleteFlag]) VALUES (
                @BlogTitle, @BlogAuthor, @BlogContent, 0
            )";

            SqlCommand cmd = new SqlCommand(insertQuery, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            if (result == 1)
            {
                Console.WriteLine("Saving Successfully.");
            }
            else
            {
                Console.WriteLine("Saving Failed.");
            }
        }

        public void Edit()
        {
            Console.Write("Enter Blog ID = ");
            string blogId = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string searchQuery = @"SELECT [BlogId] 
                ,[BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent]
                ,[DeleteFlag]
            FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(searchQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine($"{dr["BlogId"]} {dr["BlogTitle"]} {dr["BlogAuthor"]} {dr["BlogContent"]}");

        }

        public void Update()
        {
            Console.Write("Blog Blog Id : ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string searchByIdQuery = @"SELECT [BlogId] 
                ,[BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent]
                ,[DeleteFlag]
            FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(searchByIdQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine($"No date is found by that id {id}.");
                return;
            }
            Console.WriteLine($"Old record {dt.Rows[0]["BlogId"]} {dt.Rows[0]["BlogTitle"]} {dt.Rows[0]["BlogAuthor"]} {dt.Rows[0]["BlogContent"]}");

            Console.WriteLine("Please fill for the update new one. ");
            Console.Write("Blog Title : ");
            string title = Console.ReadLine();

            Console.Write("Blog Author : ");
            string author = Console.ReadLine();

            Console.Write("Blog Content: ");
            string content = Console.ReadLine();

            string updateQuery = @"UPDATE [dbo].[tbl_blog] 
                SET [BlogTitle] = @BlogTitle
	                ,[BlogAuthor] = @BlogAuthor
                    ,[BlogContent] = @BlogContent WHERE [BlogId] = @BlogId;";

            SqlCommand cmdUpdate = new SqlCommand(updateQuery, connection);
            cmdUpdate.Parameters.AddWithValue("@BlogId", id);
            cmdUpdate.Parameters.AddWithValue("@BlogTitle", title);
            cmdUpdate.Parameters.AddWithValue("@BlogAuthor", author);
            cmdUpdate.Parameters.AddWithValue("@BlogContent", content);

            int result =  cmdUpdate.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result);
            Console.WriteLine((result == 1) ? "Update Successfully." : "Update Failed.");
        }
        public void Delete()
        {
            Console.Write("Enter the deleted item ID number = ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string searchByIdQuery = @"SELECT [BlogId] 
                ,[BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent]
                ,[DeleteFlag]
            FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
                
            SqlCommand searchCmd = new SqlCommand(searchByIdQuery, connection);
            searchCmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(searchCmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if(dt.Rows.Count == 0 )
            {
                Console.WriteLine($"No data is found by provided id {id}");
                return;
            }
            Console.WriteLine($"{dt.Rows[0]["BlogId"]} {dt.Rows[0]["BlogTitle"]} {dt.Rows[0]["BlogAuthor"]} {dt.Rows[0]["BlogContent"]}");

            string deleteQuery = @"DELETE FROM [dbo].[tbl_blog] 
                WHERE [BlogId] = @BlogId";
            SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
            deleteCmd.Parameters.AddWithValue("@BlogId", id);
            int result = deleteCmd.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine((result == 1) ? "Deleted Successfully" : "Deletion process Failed.");
        }
        public void DeleteWithFlag()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            Console.Write("Enter the delete blog Id = ");
            string id = Console.ReadLine();

            string searchByIdQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent] FROM [tbl_blog] WHERE Blogid = @BlogId AND DeleteFlag = 0";
            SqlCommand searchCmd = new SqlCommand(searchByIdQuery, connection);
            searchCmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(searchCmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine($"No data was found by provided id {id}.");
                return; 
            }

            // Console.WriteLine($"{dt.Rows[0]["BlogId"]} {dt.Rows[0]["BlogTitle"]} {dt.Rows[0]["BlogAuthor"]} {dt.Rows[0]["BlogContent"]}");
            string deleteQuery = @"UPDATE [dbo].[tbl_blog] SET [DeleteFlag] = 1 WHERE [BlogId] = @BlogId;";
            SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
            deleteCmd.Parameters.AddWithValue("@BlogId", id);
            int result = deleteCmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine((result == 0) ? "Deleted by Flag Failed." : "Deleted by Flag Successfully.");
        }
    }
}
