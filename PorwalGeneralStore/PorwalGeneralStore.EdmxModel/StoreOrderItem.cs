using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class StoreOrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string ItemName { get; set; }
        public long ItemId { get; set; }
        public int Qty { get; set; }
        public decimal ListPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual StoreItem Item { get; set; }
        public virtual StoreOrder Order { get; set; }
    }
}
