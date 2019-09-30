using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Public.Business
{
    public class UserInformation
    {
        public long UserId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
