using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Contracts.V1;
using WebAPI_Template.Contracts.V1.Responses;
using WebAPI_Template.Domain;
using WebAPI_Template.Services;

namespace WebAPI_Template.Controllers.V1
{
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Policies.Roles.Admin + "," + Policies.Roles.Poster)]
    public class TagsController : Controller
    {
        private readonly IPostService _postService;

        public TagsController(IPostService postService)
        {
            _postService = postService;

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
