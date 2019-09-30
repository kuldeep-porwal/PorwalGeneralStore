using PorwalGeneralStore.EdmxModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryDbContext
{
    public static class StoreItemCategoryData
    {
        public static List<StoreItemCategory> GetStoreItemsCategories()
        {
            return new List<StoreItemCategory>()
            {
                new StoreItemCategory()
                {
                    Id=1,
                    IsActive=true,
                    Name="Soap",
                    CreatedDate=DateTime.UtcNow
                }
            };
        }

    }
}
