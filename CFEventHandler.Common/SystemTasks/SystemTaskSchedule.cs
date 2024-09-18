namespace CFEventHandler.SystemTasks
{
    public class SystemTaskSchedule
    {
        /// <summary>
        /// Execute frequency (Zero if only executed on demand)
        /// </summary>
        public TimeSpan ExecuteFrequency { get; set; }

        /// <summary>
        /// Last execute time
        /// </summary>
        public DateTimeOffset LastExecuteTime { get; set; }

        /// <summary>
        /// Next execute time
        /// </summary>
        public DateTimeOffset NextExecuteTime
        {
            get { return ExecuteFrequency == TimeSpan.Zero ?
                    DateTimeOffset.MaxValue :
                    LastExecuteTime.Add(ExecuteFrequency); }
        }

        /// <summary>
        /// Whether task is executing
        /// </summary>
        public bool Executing { get; set; }
    }
}
