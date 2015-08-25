using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DocCommander.Data;

namespace DocCommander.Extensions
{
    public static class Extensions
    {
        public static List<SelectListItem> GetSelectList(this HtmlHelper html, string listName)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                var list = db.SysLists.SingleOrDefault(x => x.Name == listName);
                var res = db.SysListItems.Where(x => x.SysListId == list.SysListId).Select(x => new SelectListItem() { Text = x.Value, Value = x.Value }).ToList();
                return res;
            }
        }
    }
}