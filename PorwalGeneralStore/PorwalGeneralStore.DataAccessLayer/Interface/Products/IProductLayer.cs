using PorwalGeneralStore.DataModel.Public.Business;

namespace PorwalGeneralStore.DataAccessLayer.Interface.Products
{
    public interface IProductLayer
    {
        Item ReadSingleProduct(long productId);
    }
}
