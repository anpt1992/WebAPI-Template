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
    public class TestController : ControllerBase
    {

        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;

        }

        [HttpGet(ApiRoutes.Tests.GetAll)]
        [Authorize(Policy ="TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _testService.GetTestsAsync());
        }

        [HttpGet(ApiRoutes.Tests.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid testId)
        {
            var test = await _testService.GetTestByIdAsync(testId);
            if (test == null)
                return NotFound();
            return Ok(test);
        }

        [HttpPost(ApiRoutes.Tests.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTestRequest testRequest)
        {
            var test = new Test
            {
                Name = testRequest.Name,
                UserId = HttpContext.GetUserId()
            };

            await _testService.CreateTestAsync(test);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Tests.Get.Replace("{testId}", test.Id.ToString());
            var response = new TestResponse { Id = test.Id };

            return Created(locationUrl, response);
        }
        [HttpPut(ApiRoutes.Tests.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid testId, [FromBody] UpdateTestRequest request)
        {
            var userOwnsTest = await _testService.UserOwnsTestAsync(testId, HttpContext.GetUserId());
            if(!userOwnsTest)
            {
                return BadRequest(new { error = "You do not own this test" });
            }

            var test = await _testService.GetTestByIdAsync(testId);
            test.Name = request.Name;
            

            var updated = await _testService.UpdateTestAsync(test);

            if (updated)
                return Ok(test);

            return NotFound();
        }
        [HttpDelete(ApiRoutes.Tests.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid testId)
        {
            var userOwnsTest = await _testService.UserOwnsTestAsync(testId, HttpContext.GetUserId());
            if (!userOwnsTest)
            {
                return BadRequest(new { error = "You do not own this test" });
            }

            var deleted = await _testService.DeleteTestAsync(testId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
