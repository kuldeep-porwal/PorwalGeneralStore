using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class CustomerInfo
    {
        public CustomerInfo()
        {
            CustomerAddressInfo = new HashSet<CustomerAddressInfo>();
            StoreOrder = new HashSet<StoreOrder>();
        }

        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }

        public virtual ICollection<CustomerAddressInfo> CustomerAddressInfo { get; set; }
        public virtual ICollection<StoreOrder> StoreOrder { get; set; }
    }
}
