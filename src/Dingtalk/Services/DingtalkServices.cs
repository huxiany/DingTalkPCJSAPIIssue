namespace EaseSource.Dingtalk.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using EaseSource.Dingtalk.Entity;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class DingtalkServices : Interfaces.IDingtalkServices
    {
#pragma warning disable SA1310 // Field names must not contain underscore
        private const string DD_ACCESS_TOKEN_CACHE_KEY = "DD_Access_Token";
        private const string DD_JSAPI_TICKET_CACHE_KEY = "DD_JSAPI_Ticket";
#pragma warning restore SA1310 // Field names must not contain underscore

        private readonly ILogger<DingtalkServices> logger;
        private DingtalkConfig dingtalkConfig;
        private IMemoryCache cache;

        private TimeSpan cacheLifeTime = new TimeSpan(1, 50, 0);

        public DingtalkServices(IOptions<DingtalkConfig> dingtalkConfig, IMemoryCache cache, ILogger<DingtalkServices> logger)
        {
            this.dingtalkConfig = dingtalkConfig.Value;
            this.cache = cache;
            this.logger = logger;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            string accessToken = null;
            if (!cache.TryGetValue<string>(DD_ACCESS_TOKEN_CACHE_KEY, out accessToken))
            {
                var uri = string.Format(CultureInfo.InvariantCulture, "{0}/gettoken?corpid={1}&corpsecret={2}", dingtalkConfig.DingtalkAPIBaseUrl, dingtalkConfig.CorpID, dingtalkConfig.Secret);

                JObject jsonObj = await GetDTContentAsync(uri);
                if (jsonObj != null)
                {
                    accessToken = jsonObj["access_token"].ToString();
                }

                if (!string.IsNullOrEmpty(accessToken))
                {
                    cache.Set<string>(DD_ACCESS_TOKEN_CACHE_KEY, accessToken, cacheLifeTime);
                }
            }

            return accessToken;
        }

        public async Task<string> GetTicketAsync(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            string ticket = null;

            if (!cache.TryGetValue<string>(DD_JSAPI_TICKET_CACHE_KEY, out ticket))
            {
                var uri = string.Format(CultureInfo.InvariantCulture, "{0}/get_jsapi_ticket?type=jsapi&access_token={1}", dingtalkConfig.DingtalkAPIBaseUrl, accessToken);

                JObject jsonObj = await GetDTContentAsync(uri);
                if (jsonObj != null)
                {
                    ticket = jsonObj["ticket"].ToString();
                }

                if (!string.IsNullOrEmpty(ticket))
                {
                    cache.Set<string>(DD_JSAPI_TICKET_CACHE_KEY, ticket, cacheLifeTime);
                }
            }

            return ticket;
        }

        public async Task<Dictionary<string, string>> GetPCConfigAsync(string clientUrl)
        {
            return await GetConfigAsync(clientUrl);
        }

        private async Task<Dictionary<string, string>> GetConfigAsync(string url)
        {
            string signStrPattern = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
            var corpId = dingtalkConfig.CorpID;
            var agentId = dingtalkConfig.AgentID;
            var nonceStr = dingtalkConfig.NonceString;
            int timeStamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            string corpAccessToken = await GetAccessTokenAsync();

            string ticket = await GetTicketAsync(corpAccessToken);
            var strToBeSigned = string.Format(CultureInfo.InvariantCulture, signStrPattern, ticket, nonceStr, timeStamp.ToString(CultureInfo.InvariantCulture), url);
            var signature = Sha1Hash(strToBeSigned);

            Dictionary<string, string> config = new Dictionary<string, string>
            {
                { "url", url },
                { "nonceStr", nonceStr },
                { "agentId", agentId },
                { "timeStamp", timeStamp.ToString(CultureInfo.InvariantCulture) },
                { "corpId", corpId },
                { "signature", signature }
            };

            return config;
        }

        private async Task<JObject> GetDTContentAsync(string uri)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    JObject o = JObject.Parse(responseJson);
                    var errCode = o["errcode"].ToString();
                    if (errCode == "0")
                    {
                        return o;
                    }
                }

                return null;
            }
        }

        private string Sha1Hash(string input)
        {
#pragma warning disable CA5350 // Do not use insecure cryptographic algorithm SHA1.
            SHA1 s = SHA1.Create();
#pragma warning restore CA5350 // Do not use insecure cryptographic algorithm SHA1.
            byte[] b = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] c = s.ComputeHash(b);
#pragma warning disable CA1308 // Normalize strings to uppercase
            return BitConverter.ToString(c).Replace("-", string.Empty).ToLower(CultureInfo.InvariantCulture);
#pragma warning restore CA1308 // Normalize strings to uppercase
        }
    }
}