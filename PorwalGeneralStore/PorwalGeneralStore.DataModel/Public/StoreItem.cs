using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorwalGeneralStore.DataModel.Public
{
    public class StoreItem
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Sku { get; set; }

        [Required]
        public double CostPrice { get; set; }

        [Required]
        public double SellingPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Product Category")]
        public long? CategoryId { get; set; }

        [Required]
        [StringLength(Int32.MaxValue, MinimumLength = 1)]
        public string MainProductImage { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string ProductHeading { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string ProductShortDesc { get; set; }
        public string ProductFullDesc { get; set; }
        public bool IsInStock { get; set; }
        public bool IsInventoryProduct { get; set; }
        public string Upccode { get; set; }
        public bool HasVariantProduct { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Vendor { get; set; }
        public bool IsActiveProduct { get; set; }
    }
}
