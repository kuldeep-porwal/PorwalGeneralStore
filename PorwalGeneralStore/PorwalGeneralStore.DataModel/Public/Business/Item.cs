using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Public.Business
{
    public class Item
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal SellingPrice { get; set; }
        public long Qty { get; set; }
        public string ItemType { get; set; }
        public bool? IsInStoke { get; set; }
    }
}
