//using CFEventHandler.Interfaces;
//using CFEventHandler.Utilities;

//namespace CFEventHandler.JSON
//{
//    /// <summary>
//    /// Repository for JSON serialized items.
//    /// 
//    /// Not very efficient with large numbers of items.
//    /// </summary>
//    /// <typeparam name="TEntity"></typeparam>
//    /// <typeparam name="TIDType"></typeparam>
//    public class JSONItemRepository<TEntity, TIDType>
//    {
//        private readonly string _folder = "";
//        private readonly Func<TEntity, TIDType> _getIdFunction;    // Returns Id from entity
//        private readonly Action<TEntity> _setIdFunction;           // Sets entity Id

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="folder">Folder where data stored</param>
//        /// <param name="getIdFunction">Function to return entity Id</param>
//        /// <param name="setIdFunction">Action to set entity Id</param>
//        public JSONItemRepository(string folder, 
//                                Func<TEntity, TIDType> getIdFunction,
//                                Action<TEntity> setIdFunction)
//        {
//            _folder = folder;
//            _getIdFunction = getIdFunction;
//            _setIdFunction = setIdFunction;
//            Directory.CreateDirectory(_folder);
//        }

//        public Task ImportAsync(IEntityList<TEntity> entityList)
//        {
//            return Task.Factory.StartNew(() =>
//            {
//                var entities = entityList.ReadAllAsync().Result;
//                foreach(var entity in entities)
//                {
//                    AddAsync(entity).Wait();
//                }
//            });
//        }

//        public Task ExportAsync(IEntityList<TEntity> entityList)
//        {
//            return Task.Factory.StartNew(() =>
//            {
//                var entities = GetAll().ToList();
//                entityList.WriteAllAsync(entities).Wait();
//            });
//        }

//        private string GetItemFile(TIDType id)
//        {
//            return Path.Combine(_folder, string.Format("{0}.json", id));
//        }

//        public Task<TEntity?> GetByIdAsync(TIDType id)
//        {
//            return Task.Factory.StartNew(() =>
//            {
//                var itemFile = GetItemFile(id);
//                if (File.Exists(itemFile))
//                {
//                    var item = JSONUtilities.DeserializeFromString<TEntity>(File.ReadAllText(itemFile), JSONUtilities.DefaultJsonSerializerOptions);
//                    return item;
//                }
//                return default(TEntity);
//            });
//        }

//        public IEnumerable<TEntity> GetByIds(List<TIDType> ids)
//        {                            
//            foreach (var id in ids)
//            {
//                var itemFile = GetItemFile(id);
//                if (!File.Exists(itemFile))
//                {
//                    throw new ArgumentException($"Item {id} does not exist");
//                }
//                var item = JSONUtilities.DeserializeFromString<TEntity>(File.ReadAllText(itemFile), JSONUtilities.DefaultJsonSerializerOptions);             
//                yield return item;
//            }                
//        }

//        public IEnumerable<TEntity> GetAll()
//        {            
//            foreach (var item in GetAll(null))
//            {
//                yield return item;
//            }

//            //foreach (string itemFile in Directory.GetFiles(_folder, "*.json", SearchOption.TopDirectoryOnly))
//            //{
//            //    var item = JSONUtilities.DeserializeFromString<TEntity>(File.ReadAllText(itemFile), JSONUtilities.DefaultJsonSerializerOptions);
//            //    yield return item;
//            //}        
//        }

//        /// <summary>
//        /// Returns all items that meet filter
//        /// </summary>
//        /// <param name="filter"></param>
//        /// <returns></returns>
//        private IEnumerable<TEntity> GetAll(Func<TEntity, bool> filter = null)
//        {
//            foreach (string itemFile in Directory.GetFiles(_folder, "*.json", SearchOption.TopDirectoryOnly))
//            {
//                var item = JSONUtilities.DeserializeFromString<TEntity>(File.ReadAllText(itemFile), JSONUtilities.DefaultJsonSerializerOptions);                
//                if (filter == null || filter(item))
//                {
//                    yield return item;
//                }
//            }
//        }

//        /// <summary>
//        /// Deletes all items that meet filter
//        /// </summary>
//        /// <param name="filter"></param>
//        private void DeleteAll(Func<TEntity, bool> filter = null)
//        {
//            foreach (string itemFile in Directory.GetFiles(_folder, "*.json", SearchOption.TopDirectoryOnly))
//            {
//                if (filter == null)
//                {
//                    File.Delete(itemFile);
//                }
//                else
//                {
//                    var item = JSONUtilities.DeserializeFromString<TEntity>(File.ReadAllText(itemFile), JSONUtilities.DefaultJsonSerializerOptions);
//                    if (filter(item))
//                    {
//                        File.Delete(itemFile);
//                    }
//                }
//            }
//        }

//        public IEnumerable<TEntity> GetByFilter(Func<TEntity, bool> filter)
//        {
//            return GetAll(filter);            
//        }

//        public Task DeleteByIdAsync(TIDType id)
//        {
//            return Task.Factory.StartNew(() =>
//            {
//                var itemFile = GetItemFile(id);
//                if (File.Exists(itemFile))
//                {
//                    File.Delete(itemFile);
//                }
//            });
//        }

//        public Task DeleteAllAsync()
//        {
//            return Task.Factory.StartNew(() =>
//            {
//                DeleteAll();
//            });
//        }

//        public Task<TEntity> AddAsync(TEntity item)
//        {
//            return Task.Factory.StartNew(() =>
//            {
//                // Set Id if not set
//                var id = _getIdFunction(item);
//                if (id == null || id.Equals(default(TIDType)))
//                {
//                    _setIdFunction(item);
//                }

//                var itemFile = GetItemFile(_getIdFunction(item));
//                if (File.Exists(itemFile))
//                {
//                    File.Delete(itemFile);
//                }
//                File.WriteAllTextAsync(itemFile, JSONUtilities.SerializeToString<TEntity>(item, JSONUtilities.DefaultJsonSerializerOptions)).Wait();
//                return item;
//            });
//        }

//        public Task UpdateAsync(TEntity item)
//        {            
//            return AddAsync(item);
//        }
//    }
//}
