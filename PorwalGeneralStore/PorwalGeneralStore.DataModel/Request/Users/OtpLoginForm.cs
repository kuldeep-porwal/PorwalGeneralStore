using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Users
{
    public class OtpLoginForm
    {
        public string MobileNumber { get; set; }
        public string Otp { get; set; }
    }
}
