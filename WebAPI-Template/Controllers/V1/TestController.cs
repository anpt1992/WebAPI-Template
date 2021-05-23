using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Contracts.V1;

namespace WebAPI_Template.Controllers.V1
{
    [ApiController]   
    public class TestController : ControllerBase
    {
       
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Tests.GetAll)]
        public IActionResult Get()
        {
            return Ok(new { name = "An" });
        }
    }
}
