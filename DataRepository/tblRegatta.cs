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
    
    public partial class tblRegatta
    {
        public int Id { get; set; }
        public string RegattaName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> RegattaYear { get; set; }
        public string Result { get; set; }
    }
}
