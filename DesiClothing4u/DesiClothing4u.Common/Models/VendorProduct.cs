using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesiClothing4u.Common.Models
{
    public class VendorProduct
    { 
        [Key]
        public int ProductId { get; set; }
        public Vendor Vendor { get; set; }
        public IEnumerable<Product> Product { get; set; }
        //public IEnumerable<ProductPictureMapping> ProductPictureMapping { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<ProductCategoryMapping> ProductCategoryMapping { get; set; }
        public IEnumerable<Picture> Picture { get; set; }
        public IEnumerable<ProductByVendor> ProductByVendor { get; set; }
        public VendorBankDetail VendorBankDetail { get; set; }
        public IEnumerable<ProductCategory> ProductCategory { get; set; }
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<OrderItem> OrderItem { get; set; }
        public IEnumerable<CustOrderItems> CustOrderItems { get; set; }



    }
}
