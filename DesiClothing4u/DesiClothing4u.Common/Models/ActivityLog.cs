using DesiClothing4u.Common.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class ActivityLog
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string IpAddress { get; set; }
        public string EntityName { get; set; }
        public int ActivityLogTypeId { get; set; }
        public int CustomerId { get; set; }
        public int? EntityId { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual ActivityLogType ActivityLogType { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
