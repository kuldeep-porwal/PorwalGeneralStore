using PorwalGeneralStore.DataModel.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface
{
	public interface ICustomerInfoBiz
	{
		List<CustomerInfo> GetCustomerInfo();
	}
}
