using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Orders
{
    public class OrderForm
    {
        public long CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
        public int TotalItem { get; set; }
        public string PaymentMode { get; set; }

        public List<OrderItemForm> OrderItems { get; set; }
        public OrderAddressForm CustomerAddress { get; set; }
    }
}
