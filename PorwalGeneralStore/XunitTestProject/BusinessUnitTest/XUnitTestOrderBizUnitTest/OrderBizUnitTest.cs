using Moq;
using PorwalGeneralStore.BusinessLayer.Implementation.Orders;
using PorwalGeneralStore.BusinessLayer.Interface.Orders;
using PorwalGeneralStore.DataAccessLayer.Interface.Orders;
using PorwalGeneralStore.DataModel.Request.Orders;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestOrderBizUnitTest
{
    public class OrderBizUnitTest
    {
        private readonly IOrderBiz _orderBiz;
        private readonly Mock<IOrderLayer> _orderLayer;
        public OrderBizUnitTest()
        {
            _orderLayer = new Mock<IOrderLayer>();
            _orderBiz = new OrderBiz(_orderLayer.Object);
        }

        [Theory(DisplayName = "Validation Error Should Come when Cancel Order Request is not Correct")]
        [MemberData(nameof(invalidCancelOrderRequest))]
        public void UnitTest1(CancelOrderForm cancelOrderForm)
        {
            var ActualResult = _orderBiz.CancelOrder(cancelOrderForm);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Validation Error Should Come when Create Order Request is not Correct")]
        [MemberData(nameof(invalidCreateOrderRequest))]
        public void UnitTest2(OrderForm orderForm)
        {
            var ActualResult = _orderBiz.CreateOrder(orderForm);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Validation Error Should Come when Update Order Address Request is not Correct")]
        [MemberData(nameof(invalidUpdateOrderAddressRequest))]
        public void UnitTest3(UpdateOrderAddressForm updateOrderAddress)
        {
            var ActualResult = _orderBiz.UpdateOrderAddress(updateOrderAddress);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Validation Error should come when gettings error while saving")]
        public void UnitTest4()
        {
            CancelOrderForm cancelOrderForm = new CancelOrderForm() { OrderId = 1, OrderCancelReason = "Not good product" };

            _orderLayer.Setup(x => x.IsOrderExist(cancelOrderForm.OrderId)).Returns(true);
            _orderLayer.Setup(x => x.CancelOrder(cancelOrderForm)).Returns(false);

            var ActualResult = _orderBiz.CancelOrder(cancelOrderForm);

            _orderLayer.Verify(x => x.CancelOrder(cancelOrderForm), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Validation Error should come when order is not found")]
        public void UnitTest5()
        {
            CancelOrderForm cancelOrderForm = new CancelOrderForm() { OrderId = 1, OrderCancelReason = "Not good product" };

            _orderLayer.Setup(x => x.IsOrderExist(cancelOrderForm.OrderId)).Returns(false);
            _orderLayer.Setup(x => x.CancelOrder(cancelOrderForm)).Returns(true);

            var ActualResult = _orderBiz.CancelOrder(cancelOrderForm);

            _orderLayer.Verify(x => x.CancelOrder(cancelOrderForm), Times.Never);
            _orderLayer.Verify(x => x.IsOrderExist(cancelOrderForm.OrderId), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Validation Error should come when order is not found")]
        public void UnitTest6()
        {
            UpdateOrderAddressForm updateOrderAddressForm = new UpdateOrderAddressForm()
            {
                OrderId = 1,
                Address1 = "Addre1",
                Address2 = "Addre1",
                City = "City",
                Phone = "987654321",
                Pincode = 987456,
                State = "State"
            };

            _orderLayer.Setup(x => x.IsOrderExist(updateOrderAddressForm.OrderId)).Returns(false);

            var ActualResult = _orderBiz.UpdateOrderAddress(updateOrderAddressForm);

            _orderLayer.Verify(x => x.IsOrderExist(updateOrderAddressForm.OrderId), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Validation Error should come when order is not updated")]
        public void UnitTest7()
        {
            UpdateOrderAddressForm updateOrderAddressForm = new UpdateOrderAddressForm()
            {
                OrderId = 1,
                Address1 = "Addre1",
                Address2 = "Addre1",
                City = "City",
                Phone = "987654321",
                Pincode = 987456,
                State = "State"
            };

            _orderLayer.Setup(x => x.IsOrderExist(updateOrderAddressForm.OrderId)).Returns(true);
            _orderLayer.Setup(x => x.UpdateOrderAddress(updateOrderAddressForm)).Returns(false);

            var ActualResult = _orderBiz.UpdateOrderAddress(updateOrderAddressForm);

            _orderLayer.Verify(x => x.IsOrderExist(updateOrderAddressForm.OrderId), Times.Once);
            _orderLayer.Verify(x => x.UpdateOrderAddress(updateOrderAddressForm), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Validation Error should come when order is not created")]
        public void UnitTest8()
        {
            OrderForm orderForm = new OrderForm()
            {
                CustomerId = 1,
                OrderTotal = 1,
                TotalItem = 1,
                PaymentMode = "Cash",
                CustomerAddress = new OrderAddressForm()
                {
                    Address1 = "Addr1",
                    Address2 = "Addr2",
                    City = "City",
                    Phone = "Phone",
                    Pincode = 457336,
                    State = "State"
                },
                OrderItems = new List<OrderItemForm>() {
                    new OrderItemForm()
                    {
                        ItemId = 1,
                        ItemName = "ItemName", ListPrice = 1, Qty = 1}
                }
            };
            _orderLayer.Setup(x => x.CreateOrder(orderForm)).Returns(false);

            var ActualResult = _orderBiz.CreateOrder(orderForm);

            _orderLayer.Verify(x => x.CreateOrder(orderForm), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }



        public static IEnumerable<object[]> invalidCancelOrderRequest => new List<object[]>
        {
            new object[] { null },
            new object[] { new CancelOrderForm() { } },
            new object[] { new CancelOrderForm() { OrderCancelReason=null } },
            new object[] { new CancelOrderForm() { OrderCancelReason=""} },
            new object[] { new CancelOrderForm() { OrderCancelReason="   "} },
            new object[] { new CancelOrderForm() { OrderId=0,OrderCancelReason="Not Good Item" } },
            new object[] { new CancelOrderForm() { OrderId=-1 ,OrderCancelReason="Mind Changes"} },
            new object[] { new CancelOrderForm() { OrderId=-100 ,OrderCancelReason="Mistake doing Order"} },
        };

        public static IEnumerable<object[]> invalidCreateOrderRequest => new List<object[]>
        {
            new object[] { null },
            new object[] { new OrderForm() { } },
            new object[] { new OrderForm() {CustomerId=0 } },
            new object[] { new OrderForm() {CustomerId=-1 } },
            new object[] { new OrderForm() {CustomerId=1,OrderTotal=0 } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=-1} },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=0} },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=-1} },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode=""} },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=null} },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() { } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() { Address1=""} } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2="" } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City="" } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City= "City",  Phone="" } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone" } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=0 } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="" } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=null } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() { new OrderItemForm() { ItemId=0} } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() { new OrderItemForm() { ItemId=-1} } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() { new OrderItemForm() { ItemId=1,ItemName=""} } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() { new OrderItemForm() { ItemId=1,ItemName="ItemName",ListPrice=0} } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() { new OrderItemForm() { ItemId=1,ItemName="ItemName",ListPrice=-1} } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() { new OrderItemForm() { ItemId=1,ItemName="ItemName",ListPrice=1,Qty=0} } } },
            new object[] { new OrderForm() {CustomerId=1 ,OrderTotal=1,TotalItem=1,PaymentMode="Cash",CustomerAddress=new OrderAddressForm() {Address1="Addr1",Address2= "Addr2", City = "City", Phone = "Phone", Pincode=457336,State="State" },OrderItems=new List<OrderItemForm>() { new OrderItemForm() { ItemId=1,ItemName="ItemName",ListPrice=1,Qty=-1} } } },
            };

        public static IEnumerable<object[]> invalidUpdateOrderAddressRequest => new List<object[]>
        {
            new object[] { null },
            new object[] { new UpdateOrderAddressForm() { } },
            new object[] { new UpdateOrderAddressForm() {OrderId=0 } },
            new object[] { new UpdateOrderAddressForm() {OrderId=-1 } },
            new object[] { new UpdateOrderAddressForm() {OrderId=1,Address1="" } },
            new object[] { new UpdateOrderAddressForm() {OrderId=1,Address1="Addr1",Address2="" } },
            new object[] { new UpdateOrderAddressForm() {OrderId=1,Address1="Addr1",Address2="Addr2",City="" } },
            new object[] { new UpdateOrderAddressForm() {OrderId=1,Address1="Addr1",Address2="Addr2",City="City",Phone="" } },
            new object[] { new UpdateOrderAddressForm() {OrderId=1,Address1="Addr1",Address2="Addr2",City="City" ,Phone="Phone",Pincode=0} },
            new object[] { new UpdateOrderAddressForm() {OrderId=1,Address1="Addr1",Address2="Addr2",City="City" ,Phone="Phone",Pincode=-1} },
            new object[] { new UpdateOrderAddressForm() {OrderId=1,Address1="Addr1",Address2="Addr2",City="City" ,Phone="Phone",Pincode=457336,State=""} },
        };
    }
}
