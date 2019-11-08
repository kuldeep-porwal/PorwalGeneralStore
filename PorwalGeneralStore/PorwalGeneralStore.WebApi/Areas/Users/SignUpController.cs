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
    public class SignUpController : Controller
    {
        private readonly IUserBiz _userBiz;
        public SignUpController(IUserBiz userBiz)
        {
            _userBiz = userBiz;
        }

        [HttpPost]
        public ActionResult<SignUpFormResponse> Post([FromBody]SignUpForm signUpForm)
        {
            SignUpFormResponse signUpFormResponse = _userBiz.RegistorUser(signUpForm);
            return Ok(signUpFormResponse);
        }

        [HttpGet("VerifyUserAccount/{mobileNumber}")]
        public ActionResult<MobileNumberVerificationResponse> VerifyUserAccount([FromRoute]string mobileNumber)
        {
            MobileNumberVerificationResponse mobileNumberVerificationResponse = _userBiz.VerifyUserAccount(mobileNumber);
            return Ok(mobileNumberVerificationResponse);
        }
    }
}
