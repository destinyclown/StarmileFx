using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StarmileFx.Common
{
    public class HttpHelper
    {
        public HttpHelper() { }

        #region 处理网络连接
        /// <summary>
        /// 处理网络连接
        /// </summary>
        /// <param name="Url">Url连接</param>
        /// <param name="PostData">参数</param>
        /// <param name="Token">令牌</param>
        /// <param name="Method">访问类型（GET/POST）</param>
        /// <param name="select">数据访问类型（select/update/insert/delete）</param>
        /// <returns></returns>
        public async Task<string> QueryData(string Url, string PostData, MethodType Method = MethodType.GET, SelectType select = SelectType.Select, object formData = null, string Token = null)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            var begTime = DateTime.Now;
            try
            {
                if (!string.IsNullOrEmpty(PostData))
                {
                    Url = Url + "?" + PostData.Trim();
                }


                switch(Method)
                {
                    case MethodType.GET:
                        return await HttpGetAsync(Url, encoding);
                    case MethodType.POST:
                        if (formData != null)
                            return await HttpPostAsync(Url, formData.ToDictionary(), encoding);
                        else
                            return await HttpPostAsync(Url, null, encoding);
                    case MethodType.PUT:
                        if (formData != null)
                            return await HttpPutAsync(Url, formData.ToDictionary(), encoding);
                        else
                            return await HttpPutAsync(Url, null, encoding);
                }


                //日志
                TimeSpan a = DateTime.Now - begTime;
                if (a.Milliseconds > 500)
                {

                }

                return await Task.Run(() =>
                {
                    return string.Empty;
                });
            }
            catch (Exception ex)
            {
                //if (!string.IsNullOrEmpty(guid))
                //{
                //    CommonLib.SaveErrLogHelper.ErrorMsgLog[guid] = ex.Message;
                //}
                //TryCount++;
                //if (TryCount < 2 && select == SelectType.Select)
                //{
                //    Thread.CurrentThread.Join(500);
                //    goto reTry;
                //}
                //else
                //{
                //    return string.Empty;
                //}
                return await Task.Run(() =>
                {
                    return ex.Message;
                });
            }
        }
        #endregion

        #region 枚举
        public enum SelectType
        {
            Select = 0,
            Update = 1,
            Insert = 2,
            Delete = 3
        }

        public enum MethodType
        {
            GET = 0,
            POST = 1,
            PUT = 2
        }
        #endregion

        #region GET
        /// <summary>
        /// 使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> HttpGetAsync(string url, Encoding encoding = null, string token = null)
        {
            HttpClient httpClient = new HttpClient();
            string userKey = "Sf-Developer " + token + ";" + Guid.NewGuid().ToString().Replace("-", "");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", userKey);
            var data = await httpClient.GetByteArrayAsync(url);
            var ret = encoding.GetString(data);
            return ret;
        }
        /// <summary>
        /// Http Get 同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string HttpGet(string url, Encoding encoding = null)
        {
            HttpClient httpClient = new HttpClient();
            var t = httpClient.GetByteArrayAsync(url);
            t.Wait();
            var ret = encoding.GetString(t.Result);
            return ret;
        }
        #endregion

        #region POST
        /// <summary>
        /// POST 异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> HttpPostAsync(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000, string token = null)
        {

            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            //MemoryStream ms = new MemoryStream();
            //formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new FormUrlEncodedContent(formData);

            string userKey = "Sf-Developer " + token + ";" + Guid.NewGuid().ToString().Replace("-", "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", userKey);
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var r = await client.PostAsync(url, hc);
            r.EnsureSuccessStatusCode();
            byte[] tmp = await r.Content.ReadAsByteArrayAsync();

            return encoding.GetString(tmp);
        }

        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public string HttpPost(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {

            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            //MemoryStream ms = new MemoryStream();
            //formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new FormUrlEncodedContent(formData);


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var t = client.PostAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            return encoding.GetString(t2.Result);
        }
        #endregion

        #region PUT
        /// <summary>
        /// POST 异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<string> HttpPutAsync(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {

            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            //MemoryStream ms = new MemoryStream();
            //formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new FormUrlEncodedContent(formData);


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var r = await client.PutAsync(url, hc);
            r.EnsureSuccessStatusCode();
            byte[] tmp = await r.Content.ReadAsByteArrayAsync();

            return encoding.GetString(tmp);
        }

        /// <summary>
        /// Put 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public string HttpPut(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {

            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            //MemoryStream ms = new MemoryStream();
            //formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new FormUrlEncodedContent(formData);


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var t = client.PutAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            return encoding.GetString(t2.Result);
        }
        #endregion
    }
}
