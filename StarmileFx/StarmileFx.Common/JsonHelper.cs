using Newtonsoft.Json;

namespace StarmileFx.Common
{
    public class JsonHelper
    {
        public static string Object_To_Json(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {

                return string.Empty;
            }
        }

        public static string T_To_Json<T>(T t)
        {
            try
            {
                return JsonConvert.SerializeObject(t);
            }
            catch(System.Exception ex)
            {
                return string.Empty;
            }
        }

        public static T Json_To_T<T>(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {

                return default(T);
            }
        }
    }
}
