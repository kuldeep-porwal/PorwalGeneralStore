using PorwalGeneralStore.DataAccessLayer.Interface;
using PorwalGeneralStore.DataModel.Public;
using PorwalGeneralStore.EdmxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicDataModel = PorwalGeneralStore.DataModel.Public;
namespace PorwalGeneralStore.DataAccessLayer.Implementation
{
	public class CustomerInfo : ICustomerInfo
	{
		private readonly PorwalGeneralStoreContext context;
		public CustomerInfo(PorwalGeneralStoreContext _context)
		{
			context = _context;
		}

		public List<PublicDataModel.CustomerInfo> GetCustomerInfo()
		{
			return context.CustomerInfo.Select(x => new PublicDataModel.CustomerInfo()
			{
				CustomerName = x.CustomerName,
				Id = x.Id
			}).ToList();
		}
	}
}
