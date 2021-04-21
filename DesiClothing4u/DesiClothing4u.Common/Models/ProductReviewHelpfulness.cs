using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class ProductReviewHelpfulness
    {
        public int Id { get; set; }
        public int ProductReviewId { get; set; }
        public bool WasHelpful { get; set; }
        public int CustomerId { get; set; }

        public virtual ProductReview ProductReview { get; set; }
    }
}
