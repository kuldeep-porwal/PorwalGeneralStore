using Newtonsoft.Json;
using PorwalGeneralStore.DataModel.Public.Business;
using System;
using System.Collections.Generic;
using System.Text;
namespace PorwalGeneralStore.DataModel.Response.Products
{
    public class SingleProductResponse : BaseResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<SingleProductValidationResponse> ErrorList { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Item Product { get; set; }
    }

    public class SingleProductValidationResponse : BaseValidationResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FieldName { get; set; }
    }
}
