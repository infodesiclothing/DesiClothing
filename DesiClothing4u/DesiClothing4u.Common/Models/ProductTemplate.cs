using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class ProductTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ViewPath { get; set; }
        public int DisplayOrder { get; set; }
        public string IgnoredProductTypes { get; set; }
    }
}
