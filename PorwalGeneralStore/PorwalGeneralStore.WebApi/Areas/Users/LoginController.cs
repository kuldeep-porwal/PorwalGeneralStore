using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PorwalGeneralStore.WebApi.Areas.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult<LoginFormResponse> Post([FromBody]LoginForm loginForm)
        {
            LoginFormResponse loginFormResponse = null;
            return Ok(loginFormResponse);
        }
    }
}
