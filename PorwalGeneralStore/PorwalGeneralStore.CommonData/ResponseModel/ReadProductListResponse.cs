using PorwalGeneralStore.DataModel.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.CommonData.ResponseModel
{
    public class ReadProductListResponse : BaseResponse
    {
        public List<GetStoreItem> Products { get; set; }
    }
}
