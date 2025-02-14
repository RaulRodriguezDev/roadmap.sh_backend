﻿using BloggingPlatform.Models;
using BloggingPlatform.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [Route("api")]
    public class BlogginPlatformController : ControllerBase
    {
        private readonly IRepository _repository;

        public BlogginPlatformController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> CreatePost(Post post)
        {
            var postCreated = await _repository.CreatPost(post);

            if(postCreated == null)
            {
                return BadRequest("Some fields in the post are worng or missing");
            }

            return StatusCode(201, postCreated);
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
