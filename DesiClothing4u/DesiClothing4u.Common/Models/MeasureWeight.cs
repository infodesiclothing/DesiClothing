using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class MeasureWeight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SystemKeyword { get; set; }
        public decimal Ratio { get; set; }
        public int DisplayOrder { get; set; }
    }
}
