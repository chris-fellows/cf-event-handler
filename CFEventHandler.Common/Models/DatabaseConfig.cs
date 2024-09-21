using CFEventHandler.Interfaces;

namespace CFEventHandler.Models
{
    public class DatabaseConfig : IDatabaseConfig
    {
        //private string _connectionString = String.Empty;
        //public string ConnectionString
        //{
        //    set { _connectionString = value; }
        //    get { return ConfigUtilities.DecryptSetting(_connectionString); }
        //}

        public string ConnectionString { get; set; } = String.Empty;

        public string DatabaseName { get; set; } = String.Empty;
    }
}
