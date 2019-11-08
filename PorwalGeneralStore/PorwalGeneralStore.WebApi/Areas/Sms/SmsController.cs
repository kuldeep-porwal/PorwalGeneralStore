using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PorwalGeneralStore.BusinessLayer.Interface.Sms;
using PorwalGeneralStore.DataModel.Request.Sms;
using PorwalGeneralStore.DataModel.Response.Sms;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PorwalGeneralStore.WebApi.Areas.Sms
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : Controller
    {
        private readonly ISmsBiz _smsBiz;
        public SmsController(ISmsBiz smsBiz)
        {
            _smsBiz = smsBiz;
        }


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public SmsApiResponse SendOtp([FromRoute]string mobileNumber, [FromQuery]string countryCode)
        {
            return _smsBiz.SendOtpSms(new SmsOtpRequest() { CountryCode = countryCode, Mobile = mobileNumber });
        }
    }
}
