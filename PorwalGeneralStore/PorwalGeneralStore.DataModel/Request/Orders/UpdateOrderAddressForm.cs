using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Orders
{
    public class UpdateOrderAddressForm
    {
        public long OrderId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long Pincode { get; set; }
        public string Phone { get; set; }
    }
}
