
namespace DotNetTrainingBatch5.MinimalApi.Endpoints.Blog
{
    public static class BlogEndpoint
    {
        //public static string Test(this int i)
        //{
        //    return i.ToString();
        //}
        public static void UseBlogEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", () =>
            {
                AppDbContext db = new AppDbContext();
                var model = db.TblBlogs.AsNoTracking().ToList();
                return Results.Ok(model);
            }).WithName("GetBlogs").WithOpenApi();

            app.MapGet("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("Not data is founded.");
                }
                return Results.Ok(item);
            }).WithName("GetBlog").WithOpenApi();

            app.MapPost("/blogs", (TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                db.TblBlogs.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            }).WithName("PostBlog").WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found.");
                }
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                //return Results.Ok(blog);
                return Results.Ok(item);
            }).WithName("PutBlog").WithOpenApi();

            app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found.");
                }
                if (!String.IsNullOrEmpty(blog.BlogTitle))
                {
                    item.BlogTitle = blog.BlogTitle;
                }
                if (!String.IsNullOrEmpty(blog.BlogAuthor))
                {
                    item.BlogAuthor = blog.BlogAuthor;
                }
                if (!String.IsNullOrEmpty(blog.BlogContent))
                {
                    item.BlogContent = blog.BlogContent;
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(item);
            }).WithName("PatchBlog").WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data is found.");
                }
                item.DeleteFlag = true;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok("Successfully deleted.");
            }).WithName("DeleteBlog").WithOpenApi();
        }
    }
}
