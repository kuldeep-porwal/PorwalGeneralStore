using PorwalGeneralStore.DataModel.Response.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface.Products
{
    public interface IProductBiz
    {
        SingleProductResponse ReadSingleProduct(long productId);
    }
}
