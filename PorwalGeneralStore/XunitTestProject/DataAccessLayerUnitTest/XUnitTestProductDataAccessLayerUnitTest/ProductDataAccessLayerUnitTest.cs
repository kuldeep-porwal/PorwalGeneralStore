using Moq;
using System;
using Xunit;
using PorwalGeneralStore.DataAccessLayer.Interface.Products;
using PorwalGeneralStore.EdmxModel;
using PorwalGeneralStore.DataAccessLayer.Implementation.Products;
using System.Collections.Generic;
using PorwalGeneralStore.DataModel.Public.Business;
using Microsoft.EntityFrameworkCore;
using InMemoryDbContext;
using System.Linq;

namespace XUnitTestProductDataAccessLayerUnitTest
{
    public class ProductDataAccessLayerUnitTest
    {
        private readonly IProductLayer _productLayer;
        private readonly PorwalGeneralStoreContext _porwalGeneralStoreContext;
        public readonly Startup startup;
        public ProductDataAccessLayerUnitTest()
        {
            startup = new Startup();
            _porwalGeneralStoreContext = startup._inMemoryContext;
            _productLayer = new ProductLayer(_porwalGeneralStoreContext);
        }

        [Fact(DisplayName = "DataAccessLayer -: Get Multiple Product")]
        public void UnitTest1()
        {
            var ActualResult = _productLayer.ReadMultipleProduct(1, 50, null);
            Assert.NotNull(ActualResult);
            Assert.IsType<List<Item>>(ActualResult);
        }

        [Fact(DisplayName = "DataAccessLayer -: Get All Product count When CategoryId=null ")]
        public void UnitTest2()
        {
            int Count = _porwalGeneralStoreContext.StoreItem.Count();
            var ActualResult = _productLayer.ReadMultipleProduct(1, 50, null);
            Assert.NotNull(ActualResult);
            Assert.IsType<List<Item>>(ActualResult);
            Assert.True(ActualResult.Count == Count);
        }

        [Theory(DisplayName = "DataAccessLayer -: Get Product Count With Category ID")]
        [InlineData(0)]
        [InlineData(1)]
        public void UnitTest3(long categoryId)
        {
            int Count = _porwalGeneralStoreContext.StoreItem.Where(x => x.CategoryId == categoryId).Count();
            var ActualResult = _productLayer.ReadMultipleProduct(1, 50, categoryId);
            Assert.NotNull(ActualResult);
            Assert.IsType<List<Item>>(ActualResult);
            Assert.True(ActualResult.Count == Count);
        }

        [Theory(DisplayName = "DataAccessLayer -: Get Product Count Paging Wise With Category ID")]
        [InlineData(0)]
        [InlineData(1)]
        public void UnitTest4(long? categoryId)
        {
            int Count = _porwalGeneralStoreContext.StoreItem.Where(x => x.CategoryId == categoryId).Take(2).Count();
            var ActualResult = _productLayer.ReadMultipleProduct(1, 2, categoryId);
            Assert.NotNull(ActualResult);
            Assert.IsType<List<Item>>(ActualResult);
            Assert.True(ActualResult.Count == Count);
        }

        [Theory(DisplayName = "DataAccessLayer -: Get Product Count Paging Wise With Category ID=null")]
        [InlineData(null)]
        public void UnitTest5(long? categoryId)
        {
            int Count = _porwalGeneralStoreContext.StoreItem.Take(2).Count();
            var ActualResult = _productLayer.ReadMultipleProduct(1, 2, categoryId);
            Assert.NotNull(ActualResult);
            Assert.IsType<List<Item>>(ActualResult);
            Assert.True(ActualResult.Count == Count);
        }

        [Theory(DisplayName = @"DataAccessLayer -: Get Product Paging Wise With Category ID and Order By 'ASC'")]
        [InlineData(0)]
        [InlineData(1)]
        public void UnitTest6(long? categoryId)
        {
            List<StoreItem> items = _porwalGeneralStoreContext.StoreItem.Where(x => x.CategoryId == categoryId).Take(2).OrderBy(x => x.Id).ToList();
            int Count = items.Count;
            var ActualResult = _productLayer.ReadMultipleProduct(1, 2, categoryId);

            Assert.NotNull(ActualResult);
            Assert.IsType<List<Item>>(ActualResult);
            Assert.True(ActualResult.Count == Count);
            Assert.True(ActualResult.Select(x => x.Id).SequenceEqual(items.Select(x => x.Id)));
        }

        [Theory(DisplayName = @"DataAccessLayer -: Get Product Paging Wise With Category ID and Order By 'ASC' and with different Page value.")]
        [InlineData(1, 3, 1)]
        [InlineData(2, 3, 1)]
        [InlineData(3, 3, 1)]
        [InlineData(1, 3, 0)]
        [InlineData(2, 3, 0)]
        [InlineData(3, 3, 0)]
        [InlineData(1, 3, null)]
        [InlineData(2, 3, null)]
        [InlineData(3, 3, null)]
        public void UnitTest7(int page, int pageSize, long? categoryId)
        {
            List<StoreItem> items = null;
            if (categoryId != null)
            {
                items = _porwalGeneralStoreContext.StoreItem.Where(x => x.CategoryId == categoryId).Skip((page - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToList();
            }
            else
            {
                items = _porwalGeneralStoreContext.StoreItem.Skip((page - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToList();
            }
            int Count = items.Count;
            var ActualResult = _productLayer.ReadMultipleProduct(page, pageSize, categoryId);

            Assert.NotNull(ActualResult);
            Assert.IsType<List<Item>>(ActualResult);
            Assert.True(ActualResult.Count == Count);
            Assert.True(ActualResult.Select(x => x.Id).SequenceEqual(items.Select(x => x.Id)));
        }
    }
}
