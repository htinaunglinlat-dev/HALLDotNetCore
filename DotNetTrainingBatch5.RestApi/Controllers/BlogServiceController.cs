using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotNetTrainingBatch5.Domain.features.Blog;
using DotNetTrainingBatch5.Database.Models;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogService _service;
        public BlogServiceController()
        {
            _service = new BlogService();
        }
        [HttpGet]
        public IActionResult GetBlogs() {
            var lst = _service.GetBlogs();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id) {
            var item = _service.GetBlog(id);
            if (item is null) return BadRequest($"No blog is found by provided id {id}.");
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var item = _service.CreateBlog(blog);
            return Ok(blog);
        }
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _service.UpdateBlog(id, blog);
            if (item is null) return BadRequest($"No blog is found by provided id {id}.");
            return Ok(item);
        }
        public IActionResult DeleteBlog(int id) {
            var item = _service.DeleteBlog(id);
            if (item is null) return NotFound($ "Blog with provided id is {id} not found.");
            return Ok("Successfully Deleted.");
        }
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            TblBlog item = _service.PatchBlog(id, blog);
            if (item is null) return BadRequest($"No blog is found by provided id {id}.");
            return Ok(item);
        }
    }
}
