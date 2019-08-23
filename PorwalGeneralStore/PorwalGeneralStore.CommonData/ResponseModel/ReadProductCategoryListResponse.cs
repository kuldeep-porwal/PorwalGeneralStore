using PorwalGeneralStore.DataModel.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.CommonData.ResponseModel
{
    public class ReadProductCategoryListResponse : BaseResponse
    {
        public List<GetProductCategory> ProductCategories { get; set; }
    }
}
