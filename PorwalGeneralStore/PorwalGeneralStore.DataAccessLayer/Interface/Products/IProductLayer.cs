using PorwalGeneralStore.DataModel.Public.Business;
using System.Collections.Generic;

namespace PorwalGeneralStore.DataAccessLayer.Interface.Products
{
    public interface IProductLayer
    {
        Item ReadSingleProduct(long productId);
        List<Item> ReadMultipleProduct(int page = 1, int pageSize = 50, long? categoryId = null);
    }
}
