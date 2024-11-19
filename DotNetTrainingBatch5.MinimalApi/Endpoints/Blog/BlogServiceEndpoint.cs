

namespace DotNetTrainingBatch5.MinimalApi.Endpoints.Blog
{
    public static class BlogServiceEndpoint
    {
        //public static string Test(this int i)
        //{
        //    return i.ToString();
        //}
        public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", () =>
            {
                BlogService service = new BlogService();
                var lst = service.GetBlogs();
                return Results.Ok(lst);
            }).WithName("GetBlogs").WithOpenApi();

            app.MapGet("/blogs/{id}", (int id) =>
            {
                BlogService service = new BlogService();
                var item = service.GetBlog(id);
                if (item is null) return Results.NotFound($"Blog is not found by provided id {id}.");

                return Results.Ok(item);
            }).WithName("GetBlog").WithOpenApi();

            app.MapPost("/blogs", (TblBlog blog) =>
            {
                BlogService service = new BlogService();
                var item = service.CreateBlog(blog);
                return Results.Ok(item);
            }).WithName("PostBlog").WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                BlogService service = new BlogService();
                var item = service.UpdateBlog(id, blog);
                if(item is null) return Results.NotFound($"Blog is not found by provided id {id}.");

                return Results.Ok(item);
            }).WithName("PutBlog").WithOpenApi();

            app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
            {
                BlogService service = new BlogService();
                var item = service.PatchBlog(id, blog);
                if(item is null) return Results.NotFound($"Blog is not found by provided id {id}.");
                return Results.Ok(item);
            }).WithName("PatchBlog").WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                BlogService service = new BlogService();
                var item = service.DeleteBlog(id);
                if(item is null) return Results.NotFound($"Blog is not found by provided id {id}.");
                return Results.Ok("Successfully deleted.");
            }).WithName("DeleteBlog").WithOpenApi();
        }
    }
}
