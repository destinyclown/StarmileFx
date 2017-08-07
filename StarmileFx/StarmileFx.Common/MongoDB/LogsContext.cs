using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StarmileFx.Models.MongoDB;
using System.Collections.Generic;

namespace StarmileFx.Common.MongoDB
{
    public class LogsContext
    {
        private readonly IMongoDatabase _db;

        public LogsContext(IOptions<MongoDBSetting> options)

        {
            var permissionSystem =
                MongoCredential.CreateCredential(options.Value.DataBase, options.Value.UserName,
                    options.Value.Password);
            var services = new List<MongoServerAddress>();
            foreach (var item in options.Value.Services)
            {
                services.Add(new MongoServerAddress(item.Host, item.Port));
            }
            var settings = new MongoClientSettings
            {
                Credentials = new[] { permissionSystem },
                Servers = services
            };


            var _mongoClient = new MongoClient(settings);
            _db = _mongoClient.GetDatabase(options.Value.DataBase);
        }

        public IMongoCollection<ErrorLogs> ErrorLog => _db.GetCollection<ErrorLogs>("Error");

        public IMongoCollection<ErrorLogs> WarningLog => _db.GetCollection<ErrorLogs>("Warning");

        public IMongoCollection<CacheProductList> CacheProductList => _db.GetCollection<CacheProductList>("CacheProductList");
    }
}
