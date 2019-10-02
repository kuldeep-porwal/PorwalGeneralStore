using Microsoft.EntityFrameworkCore;
using PorwalGeneralStore.EdmxModel;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq;

namespace InMemoryDbContext
{
    public class Startup
    {
        public readonly PorwalGeneralStoreContext _inMemoryContext;
        public Startup()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<PorwalGeneralStoreContext>()
                                .UseInMemoryDatabase(databaseName: "PorwalGeneralStore")
                                .EnableSensitiveDataLogging(true)
                                .Options;
            _inMemoryContext = new PorwalGeneralStoreContext(dbContextOptionsBuilder);
            SeedingTestData();
        }

        public void SeedingTestData()
        {
            if (!_inMemoryContext.StoreItem.Any())
            {
                _inMemoryContext.StoreItem.AddRange(StoreItemData.GetStoreItems());
                _inMemoryContext.SaveChanges();
            }

            if (!_inMemoryContext.StoreItemCategory.Any())
            {
                _inMemoryContext.StoreItemCategory.AddRange(StoreItemCategoryData.GetStoreItemsCategories());
                _inMemoryContext.SaveChanges();
            }

            if (!_inMemoryContext.CustomerInfo.Any())
            {
                _inMemoryContext.CustomerInfo.AddRange(CustomerInfoData.GetCustomerInformation());
                _inMemoryContext.SaveChanges();
            }
        }
    }
}
