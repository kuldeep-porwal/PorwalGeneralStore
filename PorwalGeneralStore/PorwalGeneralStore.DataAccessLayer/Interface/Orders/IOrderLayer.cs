using PorwalGeneralStore.DataAccessLayer.Implementation.Orders;
using PorwalGeneralStore.DataModel.Request.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataAccessLayer.Interface.Orders
{
    public interface IOrderLayer
    {
        bool IsOrderExist(long orderId);
        bool CreateOrder(OrderForm orderForm);
        bool CancelOrder(CancelOrderForm cancelOrderForm);
        bool UpdateOrderAddress(UpdateOrderAddressForm updateOrderAddressForm);
    }
}
