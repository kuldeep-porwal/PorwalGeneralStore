using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Orders
{
    public class CancelOrderForm
    {
        public long OrderId { get; set; }
        public string OrderCancelReason { get; set; }
    }
}
