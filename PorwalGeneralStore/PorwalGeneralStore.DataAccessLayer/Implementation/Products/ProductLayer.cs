using PorwalGeneralStore.DataAccessLayer.Interface.Products;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Response.Products;
using PorwalGeneralStore.EdmxModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PorwalGeneralStore.DataAccessLayer.Implementation.Products
{
    public class ProductLayer : IProductLayer
    {
        private readonly PorwalGeneralStoreContext context;
        public ProductLayer(PorwalGeneralStoreContext _context)
        {
            context = _context;
        }

        public Item ReadSingleProduct(long productId)
        {
            Item item = null;
            StoreItem storeItem = context.StoreItem.FirstOrDefault(x => x.Id == productId);
            if (storeItem != null)
            {
                item = new Item()
                {
                    Id = storeItem.Id,
                    Description = storeItem.Description,
                    IsInStoke = storeItem.IsInStoke,
                    ItemType = storeItem.ItemType,
                    ProductName = storeItem.ProductName,
                    Qty = storeItem.Qty,
                    SellingPrice = storeItem.SellingPrice,
                    Sku = storeItem.Sku,
                    Title = storeItem.Title,
                };
            }
            return item;
        }
    }
}
