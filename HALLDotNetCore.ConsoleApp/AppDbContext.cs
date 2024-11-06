using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HALLDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HALLDotNetCore.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<BlogDapperDataModel> Blogs { get; set; }
    }
}
