using PorwalGeneralStore.DataModel.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataAccessLayer.Interface
{
	public interface ICustomerInfo
	{
		List<CustomerInfo> GetCustomerInfo();
	}
}
