using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Policies.Roles.Admin + "," + Policies.Roles.Poster)]
    public class TagsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        public TagsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var tags = await this._postService.GetAllTagsAsync();

            return Ok(tags.Select(tag => new TagResponse
            {
                Name = tag.Name,
                CreatorId = tag.CreatorId,
                CreatedOn = tag.CreatedOn
            }));
        }

        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            if (!ModelState.IsValid) 
            {
                
            }

            var newTag = new Tag
            {
                Name = request.TagName,
                CreatorId = HttpContext.GetUserId(),
                CreatedOn = DateTime.UtcNow
            };

            var created = await _postService.CreateTagAsync(newTag);

            if (!created)
            {
                return BadRequest(new { Error = "Unable to create tag" });
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", newTag.Name);

            return Created(locationUrl, _mapper.Map<TagResponse>(newTag));
        }


        // 6a. However, only authorize Admin users to delete tags
        [HttpDelete(ApiRoutes.Tags.Delete)]
        //[Authorize(Roles = Policies.Roles.Admin)]
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            if (!await _postService.DeleteTagAsync(tagName))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
