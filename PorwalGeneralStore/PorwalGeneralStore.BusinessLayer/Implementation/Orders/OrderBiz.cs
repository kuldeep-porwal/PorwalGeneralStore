using PorwalGeneralStore.BusinessLayer.Interface.Orders;
using PorwalGeneralStore.DataAccessLayer.Interface.Orders;
using PorwalGeneralStore.DataModel.Request.Orders;
using PorwalGeneralStore.DataModel.Response.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PorwalGeneralStore.BusinessLayer.Implementation.Orders
{
    public class OrderBiz : IOrderBiz
    {
        private readonly IOrderLayer _orderLayer;

        public OrderBiz(IOrderLayer orderLayer)
        {
            _orderLayer = orderLayer;
        }

        public CancelOrderFormResponse CancelOrder(CancelOrderForm cancelOrderForm)
        {
            CancelOrderFormResponse cancelOrderFormResponse = new CancelOrderFormResponse()
            {
                StatusCode = 200
            };
            try
            {
                if (cancelOrderForm == null)
                {
                    cancelOrderFormResponse.StatusCode = 400;
                    cancelOrderFormResponse.ErrorList = new List<CancelOrderFormValidationResponse>()
                    {
                        new CancelOrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(cancelOrderForm),
                            Message="Invalid Request Data"
                        }
                    };
                    return cancelOrderFormResponse;
                }
                if (cancelOrderForm.OrderId <= 0)
                {
                    cancelOrderFormResponse.StatusCode = 400;
                    cancelOrderFormResponse.ErrorList = new List<CancelOrderFormValidationResponse>()
                    {
                        new CancelOrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(cancelOrderForm.OrderId),
                            Message="Invalid OrderId"
                        }
                    };
                    return cancelOrderFormResponse;
                }
                if (string.IsNullOrWhiteSpace(cancelOrderForm.OrderCancelReason))
                {
                    cancelOrderFormResponse.StatusCode = 400;
                    cancelOrderFormResponse.ErrorList = new List<CancelOrderFormValidationResponse>()
                    {
                        new CancelOrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(cancelOrderForm.OrderCancelReason),
                            Message="OrderCancelReason Required"
                        }
                    };
                    return cancelOrderFormResponse;
                }
                bool isOrderExist = _orderLayer.IsOrderExist(cancelOrderForm.OrderId);
                if (!isOrderExist)
                {
                    cancelOrderFormResponse.StatusCode = 400;
                    cancelOrderFormResponse.ErrorList = new List<CancelOrderFormValidationResponse>()
                    {
                        new CancelOrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(cancelOrderForm.OrderId),
                            Message="Order Not Found."
                        }
                    };
                    return cancelOrderFormResponse;
                }
                bool isOrderCanceled = _orderLayer.CancelOrder(cancelOrderForm);
                if (isOrderCanceled)
                {
                    cancelOrderFormResponse.StatusCode = 200;
                    cancelOrderFormResponse.Message = "Order Canceled Successfully";
                    return cancelOrderFormResponse;
                }
                else
                {
                    cancelOrderFormResponse.StatusCode = 400;
                    cancelOrderFormResponse.ErrorList = new List<CancelOrderFormValidationResponse>()
                    {
                        new CancelOrderFormValidationResponse()
                        {
                            Code=1001,
                            Message="Error While Cancle Order."
                        }
                    };
                    return cancelOrderFormResponse;
                }
            }
            catch (Exception ex)
            {
                cancelOrderFormResponse.StatusCode = 400;
                cancelOrderFormResponse.ErrorList = new List<CancelOrderFormValidationResponse>()
                    {
                        new CancelOrderFormValidationResponse()
                        {
                            Code=1001,
                            Message="Error While Cancel Order."+ex.Message
                        }
                    };
            }
            return cancelOrderFormResponse;
        }

        public OrderFormResponse CreateOrder(OrderForm orderForm)
        {
            OrderFormResponse orderFormResponse = new OrderFormResponse()
            {
                StatusCode = 200
            };

            try
            {

                if (orderForm == null)
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm),
                            Message="Invalid Request Data"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.OrderItems == null || orderForm.OrderItems.Count == 0)
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.OrderItems),
                            Message="Invalid OrderItems -: Should be atleast one"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.CustomerAddress == null)
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerAddress),
                            Message="Invalid CustomerAddress"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.CustomerId <= 0)
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerId),
                            Message="Invalid CustomerId"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.TotalItem <= 0 || orderForm.TotalItem != orderForm.OrderItems.Count)
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.TotalItem),
                            Message="Invalid TotalItem , it is equal to OrderItems."
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.OrderTotal <= 0)
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.OrderTotal),
                            Message="Invalid OrderTotal"
                        }
                    };
                    return orderFormResponse;
                }

                if (string.IsNullOrWhiteSpace(orderForm.PaymentMode))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.PaymentMode),
                            Message="Invalid PaymentMode"
                        }
                    };
                    return orderFormResponse;
                }

                if (string.IsNullOrWhiteSpace(orderForm.CustomerAddress.Address1))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerAddress.Address1),
                            Message="Invalid Address1"
                        }
                    };
                    return orderFormResponse;
                }

                if (string.IsNullOrWhiteSpace(orderForm.CustomerAddress.Address2))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerAddress.Address2),
                            Message="Invalid Address2"
                        }
                    };
                    return orderFormResponse;
                }

                if (string.IsNullOrWhiteSpace(orderForm.CustomerAddress.City))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerAddress.City),
                            Message="Invalid City"
                        }
                    };
                    return orderFormResponse;
                }

                if (string.IsNullOrWhiteSpace(orderForm.CustomerAddress.State))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerAddress.State),
                            Message="Invalid State"
                        }
                    };
                    return orderFormResponse;
                }

                if (string.IsNullOrWhiteSpace(orderForm.CustomerAddress.Phone))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerAddress.Phone),
                            Message="Invalid Phone"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.CustomerAddress.Pincode <= 0)
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(orderForm.CustomerAddress.Pincode),
                            Message="Invalid Pincode"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.OrderItems.Any(x => x.ItemId <= 0))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName="ItemId",
                            Message="OrderItems Contain Invalid ItemId"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.OrderItems.Any(x => x.ListPrice <= 0))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName="ListPrice",
                            Message="OrderItems Contain Invalid ListPrice"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.OrderItems.Any(x => x.Qty <= 0))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName="Qty",
                            Message="OrderItems Contain Invalid Qty"
                        }
                    };
                    return orderFormResponse;
                }

                if (orderForm.OrderItems.Any(x => string.IsNullOrWhiteSpace(x.ItemName)))
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            FieldName="ItemName",
                            Message="OrderItems Contain Invalid ItemName"
                        }
                    };
                    return orderFormResponse;
                }

                bool isOrderCreated = _orderLayer.CreateOrder(orderForm);
                if (isOrderCreated)
                {
                    orderFormResponse.StatusCode = 200;
                    orderFormResponse.Message = "Order Created Successfully";
                }
                else
                {
                    orderFormResponse.StatusCode = 400;
                    orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            Message="Error While Creating Order"
                        }
                    };
                    return orderFormResponse;
                }
            }
            catch (Exception ex)
            {
                orderFormResponse.StatusCode = 400;
                orderFormResponse.ErrorList = new List<OrderFormValidationResponse>()
                    {
                        new OrderFormValidationResponse()
                        {
                            Code=1001,
                            Message="Error While Creating Order"+ex.Message
                        }
                    };
            }

            return orderFormResponse;
        }

        public UpdateOrderAddressFormResponse UpdateOrderAddress(UpdateOrderAddressForm updateOrderAddressForm)
        {
            UpdateOrderAddressFormResponse updateOrderAddressFormResponse = new UpdateOrderAddressFormResponse()
            {
                StatusCode = 200
            };
            try
            {


                if (updateOrderAddressForm == null)
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm),
                            Message="Invalid Request Data"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                if (updateOrderAddressForm.OrderId <= 0)
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.OrderId),
                            Message="Invalid OrderId"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                if (string.IsNullOrWhiteSpace(updateOrderAddressForm.Address1))
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.Address1),
                            Message="Invalid Address1"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                if (string.IsNullOrWhiteSpace(updateOrderAddressForm.Address2))
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.Address2),
                            Message="Invalid Address2"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                if (string.IsNullOrWhiteSpace(updateOrderAddressForm.City))
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.City),
                            Message="Invalid City"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                if (string.IsNullOrWhiteSpace(updateOrderAddressForm.Phone))
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.Phone),
                            Message="Invalid Phone"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                if (string.IsNullOrWhiteSpace(updateOrderAddressForm.State))
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.State),
                            Message="Invalid State"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                if (updateOrderAddressForm.Pincode <= 0)
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.Pincode),
                            Message="Invalid Pincode"
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                bool isOrderExist = _orderLayer.IsOrderExist(updateOrderAddressForm.OrderId);
                if (!isOrderExist)
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(updateOrderAddressForm.OrderId),
                            Message="Order Not Found."
                        }
                    };
                    return updateOrderAddressFormResponse;
                }

                bool isOrderCanceled = _orderLayer.UpdateOrderAddress(updateOrderAddressForm);
                if (isOrderCanceled)
                {
                    updateOrderAddressFormResponse.StatusCode = 200;
                    updateOrderAddressFormResponse.Message = "Order Address Updated Successfully";
                    return updateOrderAddressFormResponse;
                }
                else
                {
                    updateOrderAddressFormResponse.StatusCode = 400;
                    updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            Message="Error While Address Updating Order."
                        }
                    };
                    return updateOrderAddressFormResponse;
                }
            }
            catch (Exception ex)
            {
                updateOrderAddressFormResponse.StatusCode = 400;
                updateOrderAddressFormResponse.ErrorList = new List<UpdateOrderAddressFormValidationResponse>()
                    {
                        new UpdateOrderAddressFormValidationResponse()
                        {
                            Code=1001,
                            Message="Error While Address Updating Order."+ex.Message
                        }
                    };
            }

            return updateOrderAddressFormResponse;
        }
    }
}
