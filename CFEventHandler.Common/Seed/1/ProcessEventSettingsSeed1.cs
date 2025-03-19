using CFEventHandler.Interfaces;
using CFEventHandler.Process;

namespace CFEventHandler.Seed
{
    public class ProcessEventSettingsSeed1 : IEntityReader<ProcessEventSettings>
    {
        public async Task<List<ProcessEventSettings>> ReadAllAsync()
        {
            var settings = new List<ProcessEventSettings>();

            settings.Add(new ProcessEventSettings()
            {
                //Id = "Process1",
                Name = "Process (Default)",
                PathToProcess = "D:\\Temp\\SomeProcess.exe"
            });

            return settings;
        }
    }
}
