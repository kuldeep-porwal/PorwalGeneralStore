using Microsoft.AspNetCore.Mvc;
using PorwalGeneralStore.BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorwalGeneralStore.WebApp.Components
{
	public class GetCustomerDropDownList : ViewComponent
	{
		public readonly ICustomerInfoBiz customerInfoBiz;
		public GetCustomerDropDownList(ICustomerInfoBiz _customerInfoBiz)
		{
			customerInfoBiz = _customerInfoBiz;
		}

		public IViewComponentResult Invoke(string DropDownId)
		{
			ViewBag.DropDownId = DropDownId;
			var items = customerInfoBiz.GetCustomerInfo();
			return View(nameof(GetCustomerDropDownList), items);
		}
	}
}
