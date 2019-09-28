using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PorwalGeneralStore.BusinessLayer.Interface.Products;
using PorwalGeneralStore.DataModel.Response.Products;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PorwalGeneralStore.WebApi.Areas.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBiz _productBiz;
        public ProductController(IProductBiz productBiz)
        {
            _productBiz = productBiz;
        }
        // GET api/<controller>/5
        [HttpGet("{productId}")]
        public ActionResult<SingleProductResponse> Get(long productId)
        {
            SingleProductResponse response = _productBiz.ReadSingleProduct(productId);
            return Ok(response);
        }
    }
}
