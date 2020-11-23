using Microsoft.AspNetCore.Mvc;

namespace TGJ.NetworkFreight.OrderServices.Controllers
{
    [Route("HealthCheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetHealthCheck()
        {
            return Ok("连接正常");
        }
    }
}
