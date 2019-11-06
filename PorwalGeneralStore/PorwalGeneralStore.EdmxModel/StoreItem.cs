using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class StoreItem
    {
        public StoreItem()
        {
            StoreOrderItem = new HashSet<StoreOrderItem>();
        }

        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public long CategoryId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public long Qty { get; set; }
        public string ItemType { get; set; }
        public bool? IsInStoke { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string ImgUrl { get; set; }
        public string ShortDesc { get; set; }
        public bool IsVariantProduct { get; set; }

        public virtual StoreItemCategory Category { get; set; }
        public virtual ICollection<StoreOrderItem> StoreOrderItem { get; set; }
    }
}
