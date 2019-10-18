using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class OrderActivityInformation
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public string OrderActivityDescription { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
