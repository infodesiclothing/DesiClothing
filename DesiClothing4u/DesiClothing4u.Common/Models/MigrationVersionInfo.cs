using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class MigrationVersionInfo
    {
        public long Version { get; set; }
        public DateTime? AppliedOn { get; set; }
        public string Description { get; set; }
    }
}
