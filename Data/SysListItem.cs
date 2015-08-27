using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class SysListItem
    {
        public int SysListItemId { get; set; }
        public int SysListId { get; set; }
        public string Value { get; set; }
        public string ShortCode { get; set; }

        public List<AuditTrail> History { get; set; }
    }
}
