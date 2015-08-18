using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocCommander.Data;
using WebMatrix.WebData;

namespace DocCommander.Controllers
{
    public class AppEventController : Controller
    {
        public ActionResult GetEvent(int eventId)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                AppEvent ae = db.AppEvents.Find(eventId);
                return View(ae);
            }
        }

        public ActionResult GetEventsForRecord(int recordId, string tableName)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                List<AppEvent> result = db.AppEvents.Where(x => x.TableName == tableName && x.RecordId == recordId)
                    .OrderByDescending(x => x.NewRecordVersion)
                    .OrderBy(x => x.EventDate)
                    .ToList();
                return View(result);
                    
            }
        }


        public ActionResult NewEvent()
        {            
            AppEvent event = new AppEvent(){
                UserId = WebSecurity.CurrentUserId,
                EventDate = DateTime.Now,
                IsError
                ActionType
                TableName
                RecordId
                OldRecordVersion
                NewRecordVersion
                ColumnName
                OldValue
                NewValue
                IsUserVerifiedEvent

            }

        }


	}
}