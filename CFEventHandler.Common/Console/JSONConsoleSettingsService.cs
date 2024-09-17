//using CFEventHandler.Interfaces;
//using CFEventHandler.JSON;
//using CFEventHandler.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFEventHandler.Console
//{    
//    public class JSONConsoleSettingsService : JSONItemRepository<ConsoleEventSettings, string>, IConsoleSettingsService
//    {
//        public JSONConsoleSettingsService(string folder) :
//                    base(folder,
//                        ((ConsoleEventSettings settings) => { return settings.Id; }),
//                        ((ConsoleEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
//        {

//        }       
//    }
//}
