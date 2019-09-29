using PorwalGeneralStore.EdmxModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryDbContext
{
    public static class StoreItemData
    {
        public static List<StoreItem> GetStoreItems()
        {
            return new List<StoreItem>()
            {
                new StoreItem()
                {
                    Id=1,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                },new StoreItem()
                {
                    Id=2,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                },new StoreItem()
                {
                    Id=3,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                },new StoreItem()
                {
                    Id=4,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                },new StoreItem()
                {
                    Id=5,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                },new StoreItem()
                {
                    Id=6,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                },new StoreItem()
                {
                    Id=7,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                },
                new StoreItem()
                {
                    Id=8,
                    CategoryId=1,
                    CostPrice=10,
                    CreateDate=DateTime.UtcNow,
                    Description="Test Descritpion",
                    IsInStoke=true,
                    ItemType="Inventory",
                    ProductName="Dettol Bath Soap",
                    Qty=10,
                    SellingPrice=15,
                    Sku="Dettol101",
                    Title="Dettole Soap"
                }
            };
        }

    }
}
