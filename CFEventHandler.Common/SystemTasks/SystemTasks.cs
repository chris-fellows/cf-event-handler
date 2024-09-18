using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.SystemTasks
{
    public class SystemTasks : ISystemTasks
    {
        private readonly List<ISystemTask> _systemTasks;

        public SystemTasks(List<ISystemTask> systemTasks)
        {
            _systemTasks = systemTasks;
        }

        public List<ISystemTask> AllTasks => _systemTasks;

        public List<ISystemTask> ActiveTasks
        {
            get { return _systemTasks.Where(st => st.Schedule.Executing).ToList(); }
        }

        public List<ISystemTask> OverdueTasks  
        {
            get
            {                
                return _systemTasks.Where(st => st.Schedule.ExecuteFrequency != TimeSpan.Zero &&
                                    st.Schedule.NextExecuteTime <= DateTimeOffset.UtcNow).ToList();
            }
        }
    }
}
