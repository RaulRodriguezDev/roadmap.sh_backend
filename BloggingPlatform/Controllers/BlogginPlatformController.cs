using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [Route("api")]
    public class BlogginPlatformController : Controller
    {
        [HttpPost]
        [Route("posts")]
        public IActionResult CreatePost(Post post)
        {
            // Create a new post
            return Ok(post);
        }

        [HttpGet]
        [Route("posts")]
        public IActionResult GetPosts()
        {
            // Get all posts
            return Ok();
        }

        [HttpGet]
        [Route("posts/{id}")]
        public IActionResult GetPost(int id)
        {
            // Get a post by id
            return Ok();
        }

        [HttpPut]
        [Route("posts/{id}")]
        public IActionResult UpdatePost(int id, Post post)
        {
            // Update a post by id
            return Ok(post);
        }

        [HttpDelete]
        [Route("posts/{id}")]
        public IActionResult DeletePost(int id)
        {
            // Delete a post by id
            return Ok();
        }

    }
}
