using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class StoreItemCategory
    {
        public StoreItemCategory()
        {
            StoreItem = new HashSet<StoreItem>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<StoreItem> StoreItem { get; set; }
    }
}
