using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return Ok(await _postService.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.GetAllWithClaims)]
        [Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAllWithClaims()
        {
            return Ok(await _postService.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.GetAllWithRoles)]
        [Authorize(Roles = "Tester")]
        public async Task<IActionResult> GetAllWithRoles()
        {
            return Ok(await _postService.GetPostsAsync());
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
            var test = await _postService.GetPostByIdAsync(testId);
            if (test == null)
                return NotFound();
            return Ok(test);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest testRequest)
        {
            var test = new Post
            {
                Name = testRequest.Name,
                UserId = HttpContext.GetUserId()
            };

            await _postService.CreatePostAsync(test);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{testId}", test.Id.ToString());
            var response = new PostResponse { Id = test.Id };

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

            var test = await _postService.GetPostByIdAsync(testId);
            test.Name = request.Name;


            var updated = await _postService.UpdatePostAsync(test);

            if (updated)
                return Ok(test);

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
