using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class VendorBankDetail
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string AccHolderName { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string SwiftCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }

       // public virtual Vendor Vendor { get; set; }
    }
}
