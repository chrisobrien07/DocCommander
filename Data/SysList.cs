using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable]
    class SysList
    {
        public int SysListId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ParentListId { get; set; }

        public List<SysListItem> Items { get; set; }
        public List<AuditTrail> History { get; set; }
        
        public bool Save()
        {
            return true;
        }

        public bool Load()
        {
            return true;
        }






    }
}
