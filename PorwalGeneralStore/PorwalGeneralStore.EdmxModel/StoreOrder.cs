using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class StoreOrder
    {
        public StoreOrder()
        {
            StoreOrderItem = new HashSet<StoreOrderItem>();
        }

        public long Id { get; set; }
        public string CustomerName { get; set; }
        public decimal OrderTotal { get; set; }
        public int TotalItem { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<StoreOrderItem> StoreOrderItem { get; set; }
    }
}
