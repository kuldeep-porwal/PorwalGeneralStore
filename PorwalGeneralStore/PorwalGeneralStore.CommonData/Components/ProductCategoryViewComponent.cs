using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PorwalGeneralStore.CommonData.Components
{

    public class ProductCategoryViewComponent : ViewComponent
    {
        public ProductCategoryViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync(string DropDownId)
        {
            ViewBag.DropDownId = DropDownId;
            return await Task.FromResult((IViewComponentResult)View("Default"));
        }
    }
}
