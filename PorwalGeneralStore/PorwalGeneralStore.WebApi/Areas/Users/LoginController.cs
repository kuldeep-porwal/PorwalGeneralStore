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

        /// <summary>
        /// User will give UserName and Password.
        /// UseCase 1-: If a Valid User then, It got Token in Response.
        /// UseCase 2-: If is not a Valid User then, It got Validation Error in Response. 
        /// </summary>
        /// <param name="loginForm"></param>
        /// <returns>LoginFormResponse</returns>
        [HttpPost("ByUserNamePassword")]
        public ActionResult<LoginFormResponse> Post([FromBody]LoginForm loginForm)
        {
            LoginFormResponse loginFormResponse = _userBiz.AuthenticateUser(loginForm);
            return Ok(loginFormResponse);
        }

        /// <summary>
        /// User will give MobileNumber as Parameter, we will 
        /// validate Mobile Number .
        /// UseCase 1-: If Valid Mobile Number then , It got Token in Response.
        /// UseCase 2-: If Mobile Number is Invalid then, It got Validation Error in Response. 
        /// </summary>
        /// <param name="MobileNumber"></param>
        /// <returns></returns>
        [HttpPost("ByMobileNumber")]
        public ActionResult<LoginFormResponse> Post([FromBody]OtpLoginForm otpLoginForm)
        {
            LoginFormResponse loginFormResponse = _userBiz.AuthenticateUserByMobileNumber(otpLoginForm);
            return Ok(loginFormResponse);
        }
    }
}
