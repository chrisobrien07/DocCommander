//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocCommander.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Num
    {
        public string NumId { get; set; }
        public int ConfigId { get; set; }
        public string List1ShortValue { get; set; }
        public string Lis2ShortValue { get; set; }
        public string List3ShortValue { get; set; }
        public string List4ShortValue { get; set; }
        public string List5ShortValue { get; set; }
    
        public virtual NumConfig NumConfig { get; set; }
    }
}
