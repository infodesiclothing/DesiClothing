using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothing4u.Common.Models
{
    public partial class Picture
    {
        public Picture()
        {//commented by SM on Nov 13, 2020 to stop cycling
            //PictureBinaries = new HashSet<PictureBinary>();
            //ProductPictureMappings = new HashSet<ProductPictureMapping>();
        }

        public int Id { get; set; }
        public string MimeType { get; set; }
        public string SeoFilename { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribute { get; set; }
        public bool IsNew { get; set; }
        public string VirtualPath { get; set; }

        public string FileName
        {
            get
            {
                return VirtualPath + SeoFilename;
            }
        }
        //commented by SM on Nov 13, 2020 to stop cycling
        public int ProductId { get; set; }
        //commented by SM on Nov 13, 2020 to stop cycling
        //public virtual ICollection<PictureBinary> PictureBinaries { get; set; }
        //public virtual ICollection<ProductPictureMapping> ProductPictureMappings { get; set; }
    }
}