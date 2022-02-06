using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Cache;
using WebAPI_Template.Contracts;
using WebAPI_Template.Contracts.V1.Requests;
using WebAPI_Template.Contracts.V1.Requests.Queries;
using WebAPI_Template.Contracts.V1.Responses;
using WebAPI_Template.Domain;
using WebAPI_Template.Extensions;
using WebAPI_Template.Helpers;
using WebAPI_Template.Services;
using WebAPI_Template.Services.V1;

namespace WebAPI_Template.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostsController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Posts.GetAll)]
        // enable cache redis and redis health
       // [Cached(600)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var posts = await _postService.GetPostsAsync(pagination);
            var postsResponse = _mapper.Map<List<PostResponse>>(posts);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageNumber < 1)
            {
                return Ok(new PagedResponse<PostResponse>(postsResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, postsResponse);

            return Ok(paginationResponse);
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
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null)
                return NotFound();
            return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var newPostId = Guid.NewGuid();
            var post = new Post
            {
                Id = newPostId,
                Name = postRequest.Name,
                CreatorId = HttpContext.GetUserId(),
                Tags = postRequest.Tags.Select(tagName => new PostTag { TagName = tagName, PostId = newPostId }).ToList()
            };

            await _postService.CreatePostAsync(post);
            var locationUrl = _uriService.GetPostUri(post.Id.ToString());


            return Created(locationUrl, new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
        }
        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var userOwnsTest = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnsTest)
            {
                return BadRequest(new { error = "You do not own this test" });
            }

            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = request.Name;
            post.Tags = request.Tags.Select(tagName => new PostTag { TagName = tagName, PostId = post.Id }).ToList();


            var updated = await _postService.UpdatePostAsync(post);

            if (updated)
                return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(post)));

            return NotFound();
        }
        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnsTest = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnsTest)
            {
                return BadRequest(new { error = "You do not own this test" });
            }

            var deleted = await _postService.DeletePostAsync(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
