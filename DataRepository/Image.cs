//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        public int Id { get; set; }
        public byte[] ThumbNailSmall { get; set; }
        public byte[] ThumbNailLarge { get; set; }
        public string ImageURL { get; set; }
        public Nullable<int> RegattaID { get; set; }
        public string Caption { get; set; }
        public Nullable<System.DateTime> DateTaken { get; set; }
    }
}
