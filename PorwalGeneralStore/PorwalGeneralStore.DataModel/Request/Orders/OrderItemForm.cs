using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Orders
{
    public class OrderItemForm
    {
        public string ItemName { get; set; }
        public long ItemId { get; set; }
        public int Qty { get; set; }
        public decimal ListPrice { get; set; }
    }
}
