using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PorwalGeneralStore.BusinessLayer.Interface.Orders;
using PorwalGeneralStore.DataModel.Request.Orders;
using PorwalGeneralStore.DataModel.Response.Orders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PorwalGeneralStore.WebApi.Areas.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderBiz _orderBiz;
        public OrderController(IOrderBiz orderBiz)
        {
            _orderBiz = orderBiz;
        }

        [HttpPost]
        public ActionResult<OrderFormResponse> Post([FromBody]OrderForm orderForm)
        {
            OrderFormResponse orderResponse = _orderBiz.CreateOrder(orderForm);
            return Ok(orderResponse);
        }

        [HttpPost("Cancel")]
        public ActionResult<CancelOrderFormResponse> CancelOrder([FromBody]CancelOrderForm cancelOrderForm)
        {
            CancelOrderFormResponse cancelOrderFormResponse = _orderBiz.CancelOrder(cancelOrderForm);
            return Ok(cancelOrderFormResponse);
        }

        [HttpPost("Cancel")]
        public ActionResult<UpdateOrderAddressFormResponse> UpdateOrderAddress([FromBody]UpdateOrderAddressForm updateOrderAddressForm)
        {
            UpdateOrderAddressFormResponse updateOrderAddressFormResponse = _orderBiz.UpdateOrderAddress(updateOrderAddressForm);
            return Ok(updateOrderAddressFormResponse);
        }
    }
}
