using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFEventHandler.Services
{
    public class MongoDBTenantService : MongoDBBaseService<Tenant>, ITenantService
    {      
        public MongoDBTenantService(IDatabaseConfig databaseConfig) : base(databaseConfig, "tenants")
        {
            
        }      

        public Task<Tenant?> GetByIdAsync(string id)
        {
            return _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<Tenant?> GetByNameAsync(string name)
        {
            return _entities.Find(x => x.Name == name).FirstOrDefaultAsync();
        }
        
        public Task DeleteByIdAsync(string id)
        {
            return _entities.DeleteOneAsync(id);
        }
    }
}
