using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetTrainingBatch5.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.Domain.features.Blog
{
    // Business Logic + Data Access
    public class BlogService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public List<TblBlog> GetBlogs()
        {
            var model = _db.TblBlogs.AsNoTracking().ToList();
            return model;
        } 
        public TblBlog CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return blog;
        }
        public TblBlog GetBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            return item;
        }
        public TblBlog UpdateBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return null;
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }
        public TblBlog PatchBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return null;
            if(!String.IsNullOrEmpty(blog.BlogTitle)) item.BlogTitle = blog.BlogTitle;
            if(!String.IsNullOrEmpty(blog.BlogAuthor)) item.BlogAuthor = blog.BlogAuthor;
            if (!String.IsNullOrEmpty(blog.BlogContent)) item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return blog;
        }
        public bool? DeleteBlog(int id) { // bool? - ? is opional
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if(item is null) return null;
            item.DeleteFlag = false;

            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            return result > 0;
        }

    }
}
