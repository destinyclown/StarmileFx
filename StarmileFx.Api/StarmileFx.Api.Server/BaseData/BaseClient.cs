using SqlSugar;
using System;

namespace StarmileFx.Api.Server.BaseData
{
    /// <summary>
    /// 基本链接类
    /// </summary>
    public class BaseClient
    {
        /// <summary>
        /// 获取db实例
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static SqlSugarClient GetInstance(string ConnectionString)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = ConnectionString, DbType = DbType.MySql, IsAutoCloseConnection = true });
            db.Ado.IsEnableLogEvent = true;
            db.Ado.LogEventStarting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + db.RewritableMethods.SerializeObject(pars));
                Console.WriteLine();
            };
            return db;
        }
    }
}
