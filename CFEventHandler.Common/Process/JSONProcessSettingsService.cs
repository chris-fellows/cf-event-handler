//using CFEventHandler.Console;
//using CFEventHandler.HTTP;
//using CFEventHandler.JSON;
//using CFEventHandler.Process;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFEventHandler.Common.Process
//{
//    public class JSONProcessSettingsService : JSONItemRepository<ProcessEventSettings, string>, IProcessSettingsService
//    {
//        public JSONProcessSettingsService(string folder) :
//                       base(folder,
//                           ((ProcessEventSettings settings) => { return settings.Id; }),
//                           ((ProcessEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
//        {

//        }
//    }
//}
