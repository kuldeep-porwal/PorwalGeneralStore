using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PorwalGeneralStore.WebApi.Areas.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserBiz _userBiz;
        public LoginController(IUserBiz userBiz)
        {
            _userBiz = userBiz;
        }

        [HttpPost]
        public ActionResult<LoginFormResponse> Post([FromBody]LoginForm loginForm)
        {
            LoginFormResponse loginFormResponse = _userBiz.AuthenticateUser(loginForm);
            return Ok(loginFormResponse);
        }
    }
}
