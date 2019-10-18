using PorwalGeneralStore.DataAccessLayer.Interface.Orders;
using PorwalGeneralStore.DataModel.Request.Orders;
using PorwalGeneralStore.EdmxModel;
using System;

namespace PorwalGeneralStore.DataAccessLayer.Implementation.Orders
{
    public class OrderLayer : IOrderLayer
    {
        private readonly PorwalGeneralStoreContext context;
        public OrderLayer(PorwalGeneralStoreContext _context)
        {
            context = _context;
        }

        public bool CancelOrder(CancelOrderForm cancelOrderForm)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrder(OrderForm orderForm)
        {
            throw new NotImplementedException();
        }

        public bool IsOrderExist(long orderId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderAddress(UpdateOrderAddressForm updateOrderAddressForm)
        {
            throw new NotImplementedException();
        }
    }
}
