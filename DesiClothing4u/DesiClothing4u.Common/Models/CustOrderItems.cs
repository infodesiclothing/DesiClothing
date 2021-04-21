using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesiClothing4u.Common.Models
{
    public class CustOrderItems
    {
        [Key]
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public bool IsAccepted { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string AirWaybilNo { get; set; }
        public string RejectedReason { get; set; }

        public int? ETA { get; set; }

        //o.id OrderId, p.name Name, o.UnitPriceInclTax Price, o.quantity Quantity,
        //o.isaccepted,o.shipmentdate, o.AirWaybilNo,o.RejectedReason,
    }
}
