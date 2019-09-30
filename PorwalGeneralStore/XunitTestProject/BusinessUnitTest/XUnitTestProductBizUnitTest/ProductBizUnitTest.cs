using System;
using Xunit;
using PorwalGeneralStore.BusinessLayer.Implementation.Products;
using PorwalGeneralStore.BusinessLayer.Interface.Products;
using System.Linq;
using PorwalGeneralStore.DataAccessLayer.Implementation.Products;
using PorwalGeneralStore.DataAccessLayer.Interface.Products;
using Moq;
using PorwalGeneralStore.DataModel.Public.Business;
using System.Collections.Generic;

namespace XUnitTestProductBizUnitTest
{
    public class ProductBizUnitTest
    {
        private readonly IProductBiz _productBiz;
        private readonly Mock<IProductLayer> _productLayer;
        public ProductBizUnitTest()
        {
            _productLayer = new Mock<IProductLayer>();
            _productBiz = new ProductBiz(_productLayer.Object);
        }

        [Theory(DisplayName = "Business -: Get Single Product")]
        [InlineData(100)]
        public void UnitTest1(long ProductId)
        {

            _productLayer
                .Setup(x => x.ReadSingleProduct(ProductId))
                .Returns(ProductId == 100 ? new Item() : null);

            var ActualResult = _productBiz.ReadSingleProduct(ProductId);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.NotNull(ActualResult.Product);
            Assert.Null(ActualResult.ErrorList);
        }

        [Theory(DisplayName = "Business -: Download Single Product With Error Response")]
        [InlineData(0)]
        public void UnitTest2(long ProductId)
        {
            var ActualResult = _productBiz.ReadSingleProduct(ProductId);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Product);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(ActualResult.ErrorList.First(x => x.Code == 101).Message == "Invalid Product Id");
        }

        [Theory(DisplayName = "Business -: Not Found Single Product")]
        [InlineData(1)]
        public void UnitTest3(long ProductId)
        {

            _productLayer
                .Setup(x => x.ReadSingleProduct(ProductId))
                .Returns(ProductId != 1 ? new Item() : null);

            var ActualResult = _productBiz.ReadSingleProduct(ProductId);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Product);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid Page Value Error ")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void UnitTest4(int page)
        {
            var ActualResult = _productBiz.ReadMultipleProduct(page, 50, null);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.Products);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid pageSize Value Error ")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(49)]
        public void UnitTest5(int pageSize)
        {
            var ActualResult = _productBiz.ReadMultipleProduct(1, pageSize, null);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.Products);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid CategoryId Value Error ")]
        [InlineData(0)]
        public void UnitTest6(long? categoryId)
        {
            var ActualResult = _productBiz.ReadMultipleProduct(1, 50, categoryId);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.Products);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Business -: Handling Exception in ReadMultipleProduct")]
        public void UnitTest7()
        {
            _productLayer.Setup(x => x.ReadMultipleProduct(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<long?>())).Throws(new Exception());

            var exception = Record.Exception(() =>
            {
                _productBiz.ReadMultipleProduct(1, 50, null);
            });

            Assert.Null(exception);
        }

        [Fact(DisplayName = "Business -: Get Multiple Product Response")]
        public void UnitTest8()
        {
            _productLayer.Setup(x => x.ReadMultipleProduct(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<long?>())).Returns(new List<Item>());

            var ActualResult = _productBiz.ReadMultipleProduct(1, 50, null);

            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.Products);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(ActualResult.Products.Count == 0);
        }
    }
}
