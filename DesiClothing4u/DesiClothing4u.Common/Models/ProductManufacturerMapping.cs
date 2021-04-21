using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class ProductManufacturerMapping
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int ProductId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Product Product { get; set; }
    }
}
