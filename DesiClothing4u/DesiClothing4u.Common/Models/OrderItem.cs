using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class OrderItem
    {
        public OrderItem()
        {
            GiftCards = new HashSet<GiftCard>();
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Guid OrderItemGuid { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceInclTax { get; set; }
        public decimal UnitPriceExclTax { get; set; }
        public decimal PriceInclTax { get; set; }
        public decimal PriceExclTax { get; set; }
        public decimal DiscountAmountInclTax { get; set; }
        public decimal DiscountAmountExclTax { get; set; }
        public decimal OriginalProductCost { get; set; }
        public string AttributeDescription { get; set; }
        public string AttributesXml { get; set; }
        public int DownloadCount { get; set; }
        public bool IsDownloadActivated { get; set; }
        public int? LicenseDownloadId { get; set; }
        public decimal? ItemWeight { get; set; }
        public DateTime? RentalStartDateUtc { get; set; }
        public DateTime? RentalEndDateUtc { get; set; }

        //Added by Mars on 29 Dec 2020
        public bool IsAccepted { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string AirWaybilNo { get; set; }
        public string InvoiceUrl { get; set; }
        public string RejectedReason { get; set; }
        
        public int? ETA { get; set; } 

        /*public virtual Order Order { get; set; }*/
        public virtual Product Product { get; set; }
        public virtual ICollection<GiftCard> GiftCards { get; set; }
    }
}
