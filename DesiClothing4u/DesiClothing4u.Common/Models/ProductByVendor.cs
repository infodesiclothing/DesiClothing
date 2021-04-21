using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesiClothing4u.Common.Models
{
    //class added by SM on Nov 20, 2020
    public class ProductByVendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string FileName { get; set; }
       
    }
}
