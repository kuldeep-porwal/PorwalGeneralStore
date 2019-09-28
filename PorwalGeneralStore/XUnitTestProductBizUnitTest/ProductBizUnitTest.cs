using System;
using Xunit;
using PorwalGeneralStore.BusinessLayer.Implementation.Products;
using PorwalGeneralStore.BusinessLayer.Interface.Products;
using System.Linq;
using PorwalGeneralStore.DataAccessLayer.Implementation.Products;
using PorwalGeneralStore.DataAccessLayer.Interface.Products;
using Moq;
using PorwalGeneralStore.DataModel.Public.Business;

namespace XUnitTestProductBizUnitTest
{
    public class ProductBizUnitTest
    {
        private IProductBiz _productBiz;
        private Mock<IProductLayer> _productLayer;
        public ProductBizUnitTest()
        {
            _productLayer = new Mock<IProductLayer>();
            _productBiz = new ProductBiz(_productLayer.Object);
        }

        [Theory(DisplayName = "Business-Get Single Product")]
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

        [Theory(DisplayName = "Business-Download Single Product With Error Response")]
        [InlineData(null)]
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

        [Theory(DisplayName = "Business-Not Found Single Product")]
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
    }
}
