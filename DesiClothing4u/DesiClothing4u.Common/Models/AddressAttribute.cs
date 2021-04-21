using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class AddressAttribute
    {
        public AddressAttribute()
        {
            AddressAttributeValues = new HashSet<AddressAttributeValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public int AttributeControlTypeId { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<AddressAttributeValue> AddressAttributeValues { get; set; }
    }
}
