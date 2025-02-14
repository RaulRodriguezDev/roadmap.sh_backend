using BloggingPlatform.Models;
using BloggingPlatform.Models.Response;
using BloggingPlatform.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [Route("api")]
    public class BlogginPlatformController(IRepository repository) : ControllerBase
    {
        private readonly IRepository _repository = repository;
        private BadRequestResponse? _badRequestResponse = null;

        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> CreatePost(Post post)
        {
            var isValidRequest = await CheckPostModelIsValid(post);

            if (isValidRequest) {
                return BadRequest(_badRequestResponse);
            }

            else
            {
                var postCreated = await _repository.CreatPost(post);

                if (postCreated == null)
                {
                    return StatusCode(500,"Something went wrong");
                }

                return StatusCode(201, postCreated);
            }
            
        }

        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok();
        }

        [HttpGet]
        [Route("posts/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _repository.GetPostById(id);

            if (post == null)
            {
                return NotFound("There isn't a blog with the given id");
            }

            return Ok(post);
        }

        [HttpPut]
        [Route("posts/{id}")]
        public async Task<IActionResult> UpdatePost(int id,[FromBody] Post post)
        {
            var isValidRequest = await CheckPostModelIsValid(post);

            if (!isValidRequest)
            {
                return BadRequest(_badRequestResponse);
            }

            var postUpdated = await _repository.UpdatePost(id, post);

            return Ok(postUpdated);

        }

        [HttpDelete]
        [Route("posts/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var isSuccessfull = await _repository.DeletePost(id);

            return isSuccessfull ? NoContent() : NotFound("The post id isn't exist");
        }

        private async Task<bool> CheckPostModelIsValid(Post post)
        {
            if (post.Title == null || post.Content == null || post.Category == null)
            {
                _badRequestResponse = new BadRequestResponse
                {
                    Status = "Bad Request",
                    Message = "Some fields in the post are worng or missing"
                };

                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);

        }

    }
}
