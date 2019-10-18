using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class CustomerAddressInfo
    {
        public long Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public long CustomerId { get; set; }
        public bool IsDefault { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public long Pincode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual CustomerInfo Customer { get; set; }
    }
}
