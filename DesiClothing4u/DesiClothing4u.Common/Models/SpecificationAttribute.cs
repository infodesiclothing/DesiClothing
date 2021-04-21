using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class SpecificationAttribute
    {
        public SpecificationAttribute()
        {
            SpecificationAttributeOptions = new HashSet<SpecificationAttributeOption>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<SpecificationAttributeOption> SpecificationAttributeOptions { get; set; }
    }
}
