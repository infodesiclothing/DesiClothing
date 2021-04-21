using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class ProductPictureMapping
    {
        public int Id { get; set; }
        public int PictureId { get; set; }
        public int ProductId { get; set; }
        public int DisplayOrder { get; set; }

        public virtual Picture Picture { get; set; }
        public string FileName
        {
            get
            {
                return Picture.FileName;
            }
        }
        //commented by SM on Nov 13, 2020 to stop cycling
        //public virtual Product Product { get; set; }
    }
}