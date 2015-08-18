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
    
    public partial class AppEvent
    {
        public long AppEventId { get; set; }
        public int UserId { get; set; }
        public System.DateTime EventDate { get; set; }
        public bool IsError { get; set; }
        public string ActionType { get; set; }
        public string TableName { get; set; }
        public long RecordId { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public bool IsUserVerifiedEvent { get; set; }
        public string OldRecordVersion { get; set; }
        public string NewRecordVersion { get; set; }
    
        public virtual Account Account { get; set; }
    }
}