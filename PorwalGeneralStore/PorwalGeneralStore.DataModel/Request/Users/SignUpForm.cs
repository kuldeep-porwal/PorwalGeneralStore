using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Users
{
    public class SignUpForm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
    }
}
