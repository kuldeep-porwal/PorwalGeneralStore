using PorwalGeneralStore.DataModel.Response.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface.Products
{
    public interface IProductBiz
    {
        SingleProductResponse ReadSingleProduct(long productId);
        BulkProductResponse ReadMultipleProduct(int page = 1, int pageSize = 50, long? categoryId = null);
    }
}
