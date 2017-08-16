using StarmileFx.Models.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmileFx.Common.MongoDB
{
    class IMongoDBService
    {
    }

    public interface IErrorLogService : IRepository<ErrorLogs>
    {
    }
    public class ErrorLogService : MongoRepository<ErrorLogs>, IErrorLogService
    {
        public ErrorLogService(LogsContext dbContext) : base(dbContext.ErrorLog)
        {
        }
    }

    public interface ICacheProductListService : IRepository<CacheProductList>
    {
    }
    public class CacheProductListService : MongoRepository<CacheProductList>, ICacheProductListService
    {
        public CacheProductListService(LogsContext dbContext) : base(dbContext.CacheProductList)
        {
        }
    }
}
