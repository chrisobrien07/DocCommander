using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
        public int ParentId { get; set; }

        public string Text01 { get; set; }
        public string Text02 { get; set; }
        public string Text03 { get; set; }
        public string Text04 { get; set; }
        public string Text05 { get; set; }
        public string Text06 { get; set; }
        public string Text07 { get; set; }
        public string Text08 { get; set; }
        public string Text09 { get; set; }
        public string Text10 { get; set; }

        public List<AuditTrail> History { get; set; }
    }
}
