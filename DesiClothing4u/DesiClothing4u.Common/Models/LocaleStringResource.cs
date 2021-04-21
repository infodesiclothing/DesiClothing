using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class LocaleStringResource
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}
