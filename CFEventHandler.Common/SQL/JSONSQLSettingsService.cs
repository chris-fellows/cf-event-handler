//using CFEventHandler.Interfaces;
//using CFEventHandler.JSON;
//using CFEventHandler.Models;
//using CFEventHandler.Process;
//using CFEventHandler.SMS;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFEventHandler.SQL
//{
//    public class JSONSQLSettingsService : JSONItemRepository<SQLEventSettings, string>, ISQLSettingsService
//    {
//        public JSONSQLSettingsService(string folder) :
//                      base(folder,
//                          ((SQLEventSettings settings) => { return settings.Id; }),
//                          ((SQLEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
//        {

//        }
//    }
//}
