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
    
    public partial class Document
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> DocumentDateTime { get; set; }
        public string DocumentText { get; set; }
        public string DocumentType { get; set; }
        public string DocumentAuthor { get; set; }
        public Nullable<int> RegattaID { get; set; }
        public Nullable<bool> Public { get; set; }
    }
}