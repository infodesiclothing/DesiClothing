using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesiClothing4u.Common.Models
{
    public class ProductCategory
    {
        [Key]
        public int RecId { get; set; }
        public string CatName { get; set; }
    }
}
