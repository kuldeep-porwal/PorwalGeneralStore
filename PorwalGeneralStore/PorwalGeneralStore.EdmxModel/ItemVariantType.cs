using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class ItemVariantType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
