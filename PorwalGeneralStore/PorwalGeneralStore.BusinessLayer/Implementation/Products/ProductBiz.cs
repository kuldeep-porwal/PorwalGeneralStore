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

        public BulkProductResponse ReadMultipleProduct(int page = 1, int pageSize = 50, long? categoryId = null)
        {
            BulkProductResponse bulkProductResponse = new BulkProductResponse()
            {
                StatusCode = 200
            };

            try
            {
                if (page <= 0)
                {
                    bulkProductResponse.StatusCode = 400;
                    bulkProductResponse.ErrorList = new List<BulkProductValidationResponse>()
                    {
                        new BulkProductValidationResponse()
                        {
                            Code=1001,
                            Message="Invalid page value, must be greater then 0."
                        }
                    };
                    return bulkProductResponse;
                }

                if (pageSize < 50)
                {
                    bulkProductResponse.StatusCode = 400;
                    bulkProductResponse.ErrorList = new List<BulkProductValidationResponse>()
                    {
                        new BulkProductValidationResponse()
                        {
                            Code=1001,
                            Message="Invalid pageSize value, must be greater then or equal to 50."
                        }
                    };
                    return bulkProductResponse;
                }

                if (categoryId != null &&
                    categoryId.Value == 0)
                {
                    bulkProductResponse.StatusCode = 400;
                    bulkProductResponse.ErrorList = new List<BulkProductValidationResponse>()
                    {
                        new BulkProductValidationResponse()
                        {
                            Code=1001,
                            Message="Invalid CategoryId value, must be null or valid category id."
                        }
                    };
                    return bulkProductResponse;
                }

                List<Item> itemList = _productLayer.ReadMultipleProduct(page, pageSize, categoryId);
                bulkProductResponse.StatusCode = 200;
                bulkProductResponse.Products = itemList;
            }
            catch (Exception ex)
            {
                bulkProductResponse.StatusCode = 400;
                bulkProductResponse.ErrorList = new List<BulkProductValidationResponse>()
                    {
                        new BulkProductValidationResponse()
                        {
                            Code=1001,
                            Message=ex.Message
                        }
                    };
            }

            return bulkProductResponse;
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
                        Message="Error While Retriving Product" + ex.Message
                    }
                };
            }
            return singleProductResponse;
        }
    }
}
