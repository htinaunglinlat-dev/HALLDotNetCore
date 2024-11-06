using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HALLDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HALLDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();
            foreach(var item in lst)
            {
                Console.WriteLine($"{item.BlogId} {item.BlogTitle} {item.BlogAuthor} {item.BlogContent}");
            }
        }
        public void Create(string title, string author, string content)
        {
            BlogDapperDataModel blog = new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
                DeleteFlag = false
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            int result = db.SaveChanges();
            Console.WriteLine((result == 0) ? "Created Failed" : "Created Successfully.");
        }
        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            //db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if(item is null)
            {
                Console.WriteLine($"No data is founded by provided id {id}.");
                return;
            }
            Console.WriteLine($"{item.BlogId} {item.BlogTitle} {item.BlogAuthor} {item.BlogContent}");
        }
        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if(item is null)
            {
                Console.WriteLine($"No data is founded by provided id {id}.");
                return;
            }
            Console.WriteLine($"{item.BlogId} {item.BlogTitle} {item.BlogAuthor} {item.BlogContent}.");
            if(string.IsNullOrEmpty(title) == false)
            {
                item.BlogTitle = title;
            }
            if (string.IsNullOrEmpty(author) == false)
            {
                item.BlogAuthor = author;
            }
            if (string.IsNullOrEmpty(content) == false)
            {
                item.BlogContent = content;
            }
            db.Entry(item).State = EntityState.Modified;
            int result = db.SaveChanges();
            Console.WriteLine((result == 0) ? "Saved on changes failed." : "Saved on changes successfully.");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().Where(x => x.BlogId == id).FirstOrDefault();
            if(item is null)
            {
                Console.WriteLine($"No data is founded by provided id {id}.");
                return;
            }
            Console.WriteLine($"{item.BlogId} {item.BlogTitle} {item.BlogAuthor} {item.BlogContent}.");
            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine((result == 0) ? "Deleted failed." : "Deleted Successfully.");
        }
        public void DeleteByFlag(int id)
        {
            AppDbContext db = new AppDbContext();   
            var item = db.Blogs.AsNoTracking().Where(x => x.BlogId == id && x.DeleteFlag == false).FirstOrDefault();
            if(item is null)
            {
                Console.WriteLine($"No data is founded by provided id {id}.");
                return;
            }
            item.DeleteFlag = true;
            db.Entry(item).State = EntityState.Modified;
            int result = db.SaveChanges();
            Console.WriteLine((result == 0) ? "Deleted by Flag failed." : "Deleted Successfully by Flag.");
        }
    }

}
