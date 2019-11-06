using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class StoreOrder
    {
        public StoreOrder()
        {
            StoreOrderCustomerInfo = new HashSet<StoreOrderCustomerInfo>();
            StoreOrderItem = new HashSet<StoreOrderItem>();
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
        public int TotalItem { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMode { get; set; }
        public int OrderStatus { get; set; }
        public string OrderCancelReason { get; set; }
        public bool IsCanceledOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long OrderNumber { get; set; }

        public virtual CustomerInfo Customer { get; set; }
        public virtual ICollection<StoreOrderCustomerInfo> StoreOrderCustomerInfo { get; set; }
        public virtual ICollection<StoreOrderItem> StoreOrderItem { get; set; }
    }
}
