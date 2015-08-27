using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class AuditTrail
    {
        public int AuditTrailId { get; set; }
        public string ObjectType { get; set; }
        public int RecordId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }

    }
}
