using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            ShipmentItems = new HashSet<ShipmentItem>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public decimal? TotalWeight { get; set; }
        public DateTime? ShippedDateUtc { get; set; }
        public DateTime? DeliveryDateUtc { get; set; }
        public string AdminComment { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}
