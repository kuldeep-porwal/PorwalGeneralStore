using PorwalGeneralStore.DataModel.Request.Orders;
using PorwalGeneralStore.DataModel.Response.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface.Orders
{
    public interface IOrderBiz
    {
        OrderFormResponse CreateOrder(OrderForm orderForm);
        CancelOrderFormResponse CancelOrder(CancelOrderForm cancelOrderForm);
        UpdateOrderAddressFormResponse UpdateOrderAddress(UpdateOrderAddressForm updateOrderAddressForm);
    }
}
