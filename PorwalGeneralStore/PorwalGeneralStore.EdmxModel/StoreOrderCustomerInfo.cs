using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class StoreOrderCustomerInfo
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long Pincode { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual StoreOrder Order { get; set; }
    }
}
