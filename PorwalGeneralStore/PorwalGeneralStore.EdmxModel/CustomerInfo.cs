using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class CustomerInfo
    {
        public CustomerInfo()
        {
            StoreOrder = new HashSet<StoreOrder>();
        }

        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<StoreOrder> StoreOrder { get; set; }
    }
}
