using PorwalGeneralStore.BusinessLayer.Interface.Products;
using PorwalGeneralStore.DataAccessLayer.Interface.Products;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Response.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Implementation.Products
{
    public class ProductBiz : IProductBiz
    {
        private readonly IProductLayer _productLayer;

        public ProductBiz(IProductLayer productLayer)
        {
            _productLayer = productLayer;
        }
        public SingleProductResponse ReadSingleProduct(long productId)
        {
            SingleProductResponse singleProductResponse = new SingleProductResponse()
            {
                StatusCode = 200
            };
            try
            {
                if (productId == 0)
                {
                    singleProductResponse.StatusCode = 400;
                    singleProductResponse.ErrorList = new List<SingleProductValidationResponse>()
                    {
                        new SingleProductValidationResponse()
                        {
                            Code=101,
                            Message="Invalid Product Id"
                        }
                    };
                    return singleProductResponse;
                }
                Item item = _productLayer.ReadSingleProduct(productId);

                if (item != null)
                {
                    singleProductResponse.Product = item;
                }
                else
                {
                    singleProductResponse.StatusCode = 400;
                    singleProductResponse.ErrorList = new List<SingleProductValidationResponse>()
                {
                    new SingleProductValidationResponse()
                    {
                        Code=101,
                        Message="Product Not Found"
                    }
                };

                }
            }
            catch (Exception ex)
            {
                singleProductResponse.StatusCode = 400;
                singleProductResponse.ErrorList = new List<SingleProductValidationResponse>()
                {
                    new SingleProductValidationResponse()
                    {
                        Code=101,
                        Message="Error While Retriving Product"
                    }
                };
                // TODO
            }
            return singleProductResponse;
        }
    }
}
