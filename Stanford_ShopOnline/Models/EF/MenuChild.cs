//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stanford_ShopOnline.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuChild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string Target { get; set; }
        public Nullable<int> Parent_Id { get; set; }
    
        public virtual Menu Menu { get; set; }
    }
}
