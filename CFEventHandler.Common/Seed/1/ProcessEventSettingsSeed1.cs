using CFEventHandler.Interfaces;
using CFEventHandler.Process;

namespace CFEventHandler.Seed
{
    public class ProcessEventSettingsSeed1 : IEntityList<ProcessEventSettings>
    {
        public async Task<List<ProcessEventSettings>> ReadAllAsync()
        {
            var settings = new List<ProcessEventSettings>();

            settings.Add(new ProcessEventSettings()
            {
                Id = "Process1",
                Name = "Process (Default)",
                PathToProcess = "D:\\Temp\\SomeProcess.exe"
            });

            return settings;
        }

        public async Task WriteAllAsync(List<ProcessEventSettings> settingsList)
        {
            // No action
        }
    }
}
