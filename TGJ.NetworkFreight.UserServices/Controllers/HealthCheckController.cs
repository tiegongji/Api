﻿using Microsoft.AspNetCore.Mvc;

namespace TGJ.NetworkFreight.UserServices.Controllers
{
    [Route("Users/HealthCheck")]
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
