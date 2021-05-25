using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Contracts.V1;
using WebAPI_Template.Contracts.V1.Requests;
using WebAPI_Template.Contracts.V1.Responses;
using WebAPI_Template.Domain;
using WebAPI_Template.Services;

namespace WebAPI_Template.Controllers.V1
{
    [ApiController]
    public class TestController : ControllerBase
    {


        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;

        }

        [HttpGet(ApiRoutes.Tests.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_testService.GetTests());
        }

        [HttpGet(ApiRoutes.Tests.Get)]
        public IActionResult Get([FromRoute] Guid testId)
        {
            var test = _testService.GetTestById(testId);
            if (test == null)
                return NotFound();
            return Ok(test);
        }

        [HttpPost(ApiRoutes.Tests.Create)]
        public IActionResult Create([FromBody] CreateTestRequest testRequest)
        {
            var test = new Test { Id = testRequest.Id };
            if (test.Id != Guid.Empty)
                test.Id = Guid.NewGuid();

            _testService.GetTests().Add(test);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Tests.Get.Replace("{testId}", test.Id.ToString());
            var response = new TestResponse { Id = test.Id };

            return Created(locationUrl, response);
        }
        [HttpPut(ApiRoutes.Tests.Update)]
        public IActionResult Update([FromRoute] Guid testId, [FromBody] UpdateTestRequest request)
        {
            var test = new Test
            {
                Id = testId,
                Name = request.Name
            };

            var updated = _testService.UpdateTest(test);

            if (updated)
                return Ok(test);

            return NotFound();
        }
        [HttpDelete(ApiRoutes.Tests.Delete)]
        public IActionResult Delete([FromRoute] Guid testId)
        {
           
            var deleted = _testService.DeleteTest(testId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
