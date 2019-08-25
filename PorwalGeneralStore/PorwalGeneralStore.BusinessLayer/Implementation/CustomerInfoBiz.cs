using PorwalGeneralStore.BusinessLayer.Interface;
using PorwalGeneralStore.DataAccessLayer.Interface;
using PorwalGeneralStore.DataModel.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Implementation
{
	public class CustomerInfoBiz : ICustomerInfoBiz
	{
		public readonly ICustomerInfo customerInfoDal;
		public CustomerInfoBiz(ICustomerInfo _customerInfoDal)
		{
			customerInfoDal = _customerInfoDal;
		}

		public List<CustomerInfo> GetCustomerInfo()
		{
			return customerInfoDal.GetCustomerInfo();
		}
	}
}
