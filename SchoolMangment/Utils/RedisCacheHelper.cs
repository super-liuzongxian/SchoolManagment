using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SchoolMangment.Dtos;
using StackExchange.Redis;

namespace SchoolMangment.Utils
{
    public class RedisCacheHelper : ICache
    {
        private StackExchange.Redis.IDatabase cache;

        private ConnectionMultiplexer connection;
        private readonly IOptions<RedisConnection> options;
        public RedisCacheHelper(IOptions<RedisConnection> options)
        {
            //把appsetting.json中配置的Redis连接配置注入进来，连接Redis
            string redisHost = options.Value.Address;
            int redisPort = int.Parse(options.Value.Port);
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints =
                    {
                        { redisHost, redisPort }
                    },
                KeepAlive = 180,
                Password = options.Value.Password,
                AllowAdmin = true
            };
            connection = ConnectionMultiplexer.Connect(configurationOptions);
            cache = connection.GetDatabase();
            this.options = options;
        }

        public bool SetCache<T>(string key, T value, DateTime? expireTime = null)
        {
            try
            {
                var jsonOption = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                string strValue = JsonConvert.SerializeObject(value, jsonOption);
                if (string.IsNullOrEmpty(strValue))
                {
                    return false;
                }
                if (expireTime == null)
                {
                    return cache.StringSet(InitKey(key), strValue);
                }
                else
                {
                    return cache.StringSet(InitKey(key), strValue, (expireTime.Value - DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
            }
            return false;
        }

        public bool RemoveCache(string key)
        {
            return cache.KeyDelete(InitKey(key));
        }

        public T GetCache<T>(string key)
        {
            var t = default(T);
            try
            {
                var value = cache.StringGet(InitKey(key));
                if (string.IsNullOrEmpty(value))
                {
                    return t;
                }
                t = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
            }
            return t;
        }

        public long GetIncr(string key)
        {
            try
            {
                return cache.StringIncrement(InitKey(key));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public long GetIncr(string key, TimeSpan expiresTime)
        {
            try
            {
                var qty = cache.StringIncrement(InitKey(key));
                if (qty == 1)
                {
                    //设置过期时间
                    cache.KeyExpire(key, expiresTime);
                }
                return qty;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #region Hash
        public int SetHashFieldCache<T>(string key, string fieldKey, T fieldValue)
        {
            return SetHashFieldCache<T>(InitKey(key), new Dictionary<string, T> { { fieldKey, fieldValue } });
        }

        public int SetHashFieldCache<T>(string key, Dictionary<string, T> dict)
        {
            int count = 0;
            var jsonOption = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            foreach (string fieldKey in dict.Keys)
            {
                string fieldValue = JsonConvert.SerializeObject(dict[fieldKey], jsonOption);
                count += cache.HashSet(InitKey(key), fieldKey, fieldValue) ? 1 : 0;
            }
            return count;
        }

        public T GetHashFieldCache<T>(string key, string fieldKey)
        {
            var dict = GetHashFieldCache<T>(InitKey(key), new Dictionary<string, T> { { fieldKey, default(T) } });
            return dict[fieldKey];
        }

        public Dictionary<string, T> GetHashFieldCache<T>(string key, Dictionary<string, T> dict)
        {
            foreach (string fieldKey in dict.Keys)
            {
                string fieldValue = cache.HashGet(InitKey(key), fieldKey);
                dict[fieldKey] = JsonConvert.DeserializeObject<T>(fieldValue);
            }
            return dict;
        }

        public Dictionary<string, T> GetHashCache<T>(string key)
        {
            Dictionary<string, T> dict = new Dictionary<string, T>();
            var hashFields = cache.HashGetAll(InitKey(key));
            foreach (HashEntry field in hashFields)
            {
                dict[field.Name] = JsonConvert.DeserializeObject<T>(field.Value);
            }
            return dict;
        }

        public List<T> GetHashToListCache<T>(string key)
        {
            List<T> list = new List<T>();
            var hashFields = cache.HashGetAll(InitKey(key));
            foreach (HashEntry field in hashFields)
            {
                list.Add(JsonConvert.DeserializeObject<T>(field.Value));
            }
            return list;
        }

        public bool RemoveHashFieldCache(string key, string fieldKey)
        {
            Dictionary<string, bool> dict = new Dictionary<string, bool> { { fieldKey, false } };
            dict = RemoveHashFieldCache(InitKey(key), dict);
            return dict[fieldKey];
        }

        public Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict)
        {
            foreach (string fieldKey in dict.Keys)
            {
                dict[fieldKey] = cache.HashDelete(InitKey(key), fieldKey);
            }
            return dict;
        }
        private string InitKey(string key)
        {
            return $"{key}";
        }
        #endregion

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
            }
            GC.SuppressFinalize(this);
        }

        public void tet()
        {
            var pattern = "BUIK_201710*";
            var redisResult = cache.ScriptEvaluateAsync(LuaScript.Prepare(
                            //Redis的keys模糊查询：
                            " local res = redis.call('KEYS', @keypattern) " +
                            " return res "), new { @keypattern = pattern }).Result;

            if (!redisResult.IsNull)
            {
                cache.KeyDelete((RedisKey[])redisResult);  //删除一组key
            }
        }

        public async Task RemoveKeysLeftLike(string keywords)
        {
            var redisResult = await cache.ScriptEvaluateAsync(LuaScript.Prepare(
                           //Redis的keys模糊查询：
                           " local res = redis.call('KEYS', @keywords) " +
                           " return res "), new { @keywords = $"{InitKey(keywords)}*" });

            if (!redisResult.IsNull)
            {
                cache.KeyDelete((RedisKey[])redisResult);  //删除一组key
            }
        }

    }
}
