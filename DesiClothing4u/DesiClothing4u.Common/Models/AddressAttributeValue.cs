using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class AddressAttributeValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressAttributeId { get; set; }
        public bool IsPreSelected { get; set; }
        public int DisplayOrder { get; set; }

        public virtual AddressAttribute AddressAttribute { get; set; }
    }
}
