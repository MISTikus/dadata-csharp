using Dadata.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Dadata
{
    public abstract class ClientBase
    {
        protected string token;
        protected string baseUrl;
        protected JsonSerializer serializer;

        static ClientBase() => ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

        public ClientBase(string token, string baseUrl)
        {
            this.token = token;
            this.baseUrl = baseUrl;
            this.serializer = new JsonSerializer();
        }

        #region GET
        protected T ExecuteGet<T>(string method, string entity, NameValueCollection parameters, Dictionary<string, string> customHeaders = null)
        {
            var queryString = SerializeParameters(parameters);
            var httpRequest = CreateHttpRequest(verb: "GET", method: method, entity: entity, queryString: queryString, customHeaders: customHeaders);
            using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                return Deserialize<T>(httpResponse);
        }

        protected async Task<T> ExecuteGetAsync<T>(string method, string entity, NameValueCollection parameters, Dictionary<string, string> customHeaders = null)
        {
            var queryString = SerializeParameters(parameters);
            var httpRequest = CreateHttpRequest(verb: "GET", method: method, entity: entity, queryString: queryString, customHeaders: customHeaders);
            using (var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync())
                return await DeserializeAsync<T>(httpResponse);
        }
        #endregion GET

        #region POST
        protected T Execute<T>(string method, string entity, IDadataRequest request, Dictionary<string, string> customHeaders = null)
        {
            var httpRequest = CreateHttpRequest(verb: "POST", method: method, entity: entity, customHeaders: customHeaders);
            httpRequest = Serialize(httpRequest, request);
            using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                return Deserialize<T>(httpResponse);
        }

        protected async Task<T> ExecuteAsync<T>(string method, string entity, IDadataRequest request, Dictionary<string, string> customHeaders = null)
        {
            var httpRequest = CreateHttpRequest(verb: "POST", method: method, entity: entity, customHeaders: customHeaders);
            httpRequest = Serialize(httpRequest, request);
            using (var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync())
                return await DeserializeAsync<T>(httpResponse);
        }

        protected T Execute<T>(IDadataRequest request, Dictionary<string, string> customHeaders = null)
        {
            var httpRequest = CreateHttpRequest(verb: "POST", url: this.baseUrl, customHeaders);
            httpRequest = Serialize(httpRequest, request);
            using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                return Deserialize<T>(httpResponse);
        }

        protected async Task<T> ExecuteAsync<T>(IDadataRequest request, Dictionary<string, string> customHeaders = null)
        {
            var httpRequest = CreateHttpRequest(verb: "POST", url: this.baseUrl, customHeaders);
            httpRequest = Serialize(httpRequest, request);
            using (var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync())
                return await DeserializeAsync<T>(httpResponse);
        }
        #endregion POST

        protected HttpWebRequest CreateHttpRequest(string verb, string method, string entity, string queryString = null, Dictionary<string, string> customHeaders = null)
        {
            var url = string.Format("{0}/{1}/{2}", this.baseUrl, method, entity);
            if (queryString != null)
            {
                url += "?" + queryString;
            }
            return CreateHttpRequest(verb, url, customHeaders);
        }

        protected HttpWebRequest CreateHttpRequest(string verb, string url, Dictionary<string, string> customHeaders = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = verb;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Token " + this.token);
            if (customHeaders != null)
            {
                foreach (var kv in customHeaders)
                {
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }

            return request;
        }

        protected string SerializeParameters(NameValueCollection parameters)
        {
            var parts = new List<string>();
            foreach (var key in parameters.AllKeys)
                parts.Add(string.Format("{0}={1}", key, parameters[key]));
            return string.Join("&", parts);
        }

        protected HttpWebRequest Serialize(HttpWebRequest httpRequest, IDadataRequest request)
        {
            using (var w = new StreamWriter(httpRequest.GetRequestStream()))
            {
                using (JsonWriter writer = new JsonTextWriter(w))
                {
                    this.serializer.Serialize(writer, request);
                }
            }
            return httpRequest;
        }

        protected virtual T Deserialize<T>(HttpWebResponse httpResponse, params JsonConverter[] converters)
        {
            using (var r = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(responseText, converters);
            }
        }
        protected virtual async Task<T> DeserializeAsync<T>(HttpWebResponse httpResponse, params JsonConverter[] converters)
        {
            using (var r = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = await r.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(responseText, converters);
            }
        }
    }
}
