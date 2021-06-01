﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Contracts.V1;
using WebAPI_Template.Contracts.V1.Requests;
using WebAPI_Template.Contracts.V1.Responses;
using WebAPI_Template.Domain;
using WebAPI_Template.Extensions;
using WebAPI_Template.Services;

namespace WebAPI_Template.Controllers.V1
{
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostController(IPostService testService)
        {
            _postService = testService;

        }

        [HttpGet(ApiRoutes.Posts.GetAll)]

        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetPostsAsync();

            return Ok(posts.Select(post => new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                Tags = post.Tags.Select(postTag => postTag.TagName)
            }));
        }

        [HttpGet(ApiRoutes.Posts.GetAllWithClaims)]
        [Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAllWithClaims()
        {
            var posts = await _postService.GetPostsAsync();

            return Ok(posts.Select(post => new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                Tags = post.Tags.Select(postTag => postTag.TagName)
            }));
        }

        [HttpGet(ApiRoutes.Posts.GetAllWithRoles)]
        [Authorize(Roles = "Tester")]
        public async Task<IActionResult> GetAllWithRoles()
        {
            var posts = await _postService.GetPostsAsync();

            return Ok(posts.Select(post => new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                Tags = post.Tags.Select(postTag => postTag.TagName)
            }));
        }

        [HttpGet(ApiRoutes.Posts.GetAllWithAuthorizationHandles)]
        [Authorize(Policy = "MustWorkForAnpt1992")]
        public async Task<IActionResult> GetAllWithAuthorizationHandles()
        {
            return Ok(await _postService.GetPostsAsync());
        }


        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid testId)
        {
            var post = await _postService.GetPostByIdAsync(testId);
            if (post == null)
                return NotFound();
            return Ok(new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                Tags = post.Tags.Select(postTag => postTag.TagName)
            });
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var newPostId = Guid.NewGuid();
            var post = new Post
            {
                Id = newPostId,
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId(),
                Tags = postRequest.Tags.Select(tagName => new PostTag { TagName = tagName, PostId = newPostId }).ToList()
            };

            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{testId}", post.Id.ToString());
            var response = new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                Tags = post.Tags.Select(postTag => postTag.TagName)
            };

            return Created(locationUrl, response);
        }
        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid testId, [FromBody] UpdatePostRequest request)
        {
            var userOwnsTest = await _postService.UserOwnsPostAsync(testId, HttpContext.GetUserId());
            if (!userOwnsTest)
            {
                return BadRequest(new { error = "You do not own this test" });
            }

            var post = await _postService.GetPostByIdAsync(testId);
            post.Name = request.Name;
            post.Tags = request.Tags.Select(tagName => new PostTag { TagName = tagName, PostId = post.Id }).ToList();


            var updated = await _postService.UpdatePostAsync(post);

            if (updated)
                return Ok(new PostResponse
                {
                    Id = post.Id,
                    Name = post.Name,
                    Tags = post.Tags.Select(postTag => postTag.TagName)
                });

            return NotFound();
        }
        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid testId)
        {
            var userOwnsTest = await _postService.UserOwnsPostAsync(testId, HttpContext.GetUserId());
            if (!userOwnsTest)
            {
                return BadRequest(new { error = "You do not own this test" });
            }

            var deleted = await _postService.DeletePostAsync(testId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}