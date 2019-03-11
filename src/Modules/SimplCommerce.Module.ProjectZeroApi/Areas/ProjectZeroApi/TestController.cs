using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimplCommerce.Module.ProjectZeroApi.Areas.ProjectZeroApi
{
    [Area("ProjectZeroApi")]
    [Route("api/projectzero")]
    public class TestApiController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new { TestResult = "Test successful!" });
        }
    }
}
