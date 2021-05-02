using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesiClothing4u.Common.Models
{
    public class productExt
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string SeoFilename { get; set; }

        public string VirtualPath { get; set; }

    }
}
