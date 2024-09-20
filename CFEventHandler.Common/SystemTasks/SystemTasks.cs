using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.SystemTasks
{
    public class SystemTasks : ISystemTasks
    {
        private readonly int _maxConcurrentTasks;
        private readonly List<ISystemTask> _systemTasks;

        public SystemTasks(List<ISystemTask> systemTasks, int maxConcurrentTasks)
        {
            _maxConcurrentTasks = maxConcurrentTasks;
            _systemTasks = systemTasks;
        }

        public int MaxConcurrentTasks => _maxConcurrentTasks;

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
