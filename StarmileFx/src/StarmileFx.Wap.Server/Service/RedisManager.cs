using System;
using System.Collections.Generic;
using StarmileFx.Wap.Server.IServices;
using StarmileFx.Common.Redis;
using StackExchange.Redis;

namespace StarmileFx.Wap.Server.Services
{
    public class RedisManager : IRedisServer
    {
        private string Conn;
        RedisHelper RedisHelper = RedisHelper.Instance;
        public string conn
        {
            get { return Conn; }
            set
            {
                Conn = value;
                RedisHelper.connstr = value;
            }
        }
        public bool DeleteHase(RedisKey key, RedisValue hashField)
        {
            return RedisHelper.DeleteHase(key, hashField);
        }

        public List<T> GetHashAll<T>(string key)
        {
            return RedisHelper.GetHashAll<T>(key);
        }

        public List<T> GetHashKey<T>(string key, List<RedisValue> listhashFields)
        {
            return RedisHelper.GetHashKey<T>(key, listhashFields);
        }

        public T GetHashKey<T>(string key, string hasFildValue)
        {
            return RedisHelper.GetHashKey<T>(key, hasFildValue);
        }

        public string GetMyKey(string resourceid = "")
        {
            return RedisHelper.GetMyKey(resourceid);
        }

        public RedisValue[] GetStringKey(List<RedisKey> listKey)
        {
            return RedisHelper.GetStringKey(listKey);
        }

        public RedisValue GetStringKey(string key)
        {
            return RedisHelper.GetStringKey(key);
        }

        public T GetStringKey<T>(string key)
        {
            return RedisHelper.GetStringKey<T>(key);
        }

        public List<T> HashGetAll<T>(string key)
        {
            return RedisHelper.HashGetAll<T>(key);
        }

        public void HashSet<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            RedisHelper.HashSet<T>(key, list, getModelId);
        }

        public long keyDelete(RedisKey[] keys)
        {
            return RedisHelper.keyDelete(keys);
        }

        public bool KeyDelete(string key)
        {
            return RedisHelper.KeyDelete(key);
        }

        public bool KeyExists(string key)
        {
            return RedisHelper.KeyExists(key);
        }

        public bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return RedisHelper.KeyExpire(key, expiry);
        }

        public bool KeyRename(string key, string newKey)
        {
            return RedisHelper.KeyRename(key, newKey);
        }

        public bool SetStringKey(KeyValuePair<RedisKey, RedisValue>[] arr)
        {
            return RedisHelper.SetStringKey(arr);
        }

        public bool SetStringKey(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return RedisHelper.SetStringKey(key, value, expiry);
        }

        public bool SetStringKey<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            return RedisHelper.SetStringKey<T>(key, obj, expiry);
        }

        public void StringAppend(string key, string value)
        {
            RedisHelper.StringAppend(key, value);
        }
    }
}
