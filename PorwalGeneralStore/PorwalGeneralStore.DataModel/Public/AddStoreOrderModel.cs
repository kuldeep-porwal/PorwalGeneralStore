using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Public
{
	public class AddStoreOrderModel
	{
		public string CustomerName { get; set; }
		public decimal OrderTotal { get; set; }
		public List<AddStoreOrderItemModel> StoreOrderItems { get; set; }
	}
}