using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PorwalGeneralStore.WebApi.Areas.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet("{userId}")]
        public ActionResult Get(long userId)
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(long userId)
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(long userId)
        {
            return Ok();
        }
    }
}
