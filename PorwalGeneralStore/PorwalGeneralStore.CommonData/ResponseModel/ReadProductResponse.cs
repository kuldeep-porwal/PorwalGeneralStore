using PorwalGeneralStore.DataModel.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.CommonData.ResponseModel
{
    public class ReadProductResponse : BaseResponse
    {
        public GetStoreItem Product { get; set; }
    }
}
