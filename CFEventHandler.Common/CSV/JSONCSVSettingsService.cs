//using CFEventHandler.Console;
//using CFEventHandler.Interfaces;
//using CFEventHandler.JSON;
//using CFEventHandler.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFEventHandler.CSV
//{
//    public class JSONCSVSettingsService : JSONItemRepository<CSVEventSettings, string>, ICSVSettingsService
//    {
//        public JSONCSVSettingsService(string folder) :
//                      base(folder,
//                          ((CSVEventSettings settings) => { return settings.Id; }),
//                          ((CSVEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
//        {

//        }
//    }
//}
