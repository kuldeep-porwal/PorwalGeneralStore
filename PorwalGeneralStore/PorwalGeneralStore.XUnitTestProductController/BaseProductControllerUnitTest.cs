using System;
using Xunit;
using PorwalGeneralStore.WebApi.Areas.Products;
using PorwalGeneralStore.BusinessLayer.Implementation.Products;
using PorwalGeneralStore.DataAccessLayer.Implementation.Products;

namespace PorwalGeneralStore.XUnitTestProductController
{
    public class BaseProductControllerUnitTest
    {
        private ProductController productController;

        public BaseProductControllerUnitTest()
        {
            productController = new ProductController(new ProductBiz(new ProductLayer(null)));
        }

        [Theory(DisplayName = "Controller - Download Single Product")]
        [InlineData(100)]
        public void ReadSingleProduct(long ProductId)
        {
            var ActualResult = productController.Get(ProductId);

            Assert.NotNull(ActualResult);
        }
    }
}
