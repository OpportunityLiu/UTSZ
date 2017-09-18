using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UTSZ.Client
{
    public static class Client
    {
        private static HttpClient client = new HttpClient
        {

        };

        public static async Task ConnentAsync(string username, string password)
        {
            var ac_test = await client.GetAsync("http://msconnecttest.com");
            if (ac_test.RequestMessage.RequestUri.Host == "msconnecttest.com")
                return;
            var ac_id = string.Concat(ac_test.RequestMessage.RequestUri.PathAndQuery.Where(char.IsDigit));
            if (string.IsNullOrEmpty(ac_id))
                ac_id = "1";
            var r = await client.PostAsync("http://10.0.10.66/include/auth_action.php", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["action"] = "login",
                ["username"] = username,
                ["password"] = password,
                ["ac_id"] = ac_id,
                ["ajax"] = "1"
            }));
            var data = await r.Content.ReadAsStringAsync();
            if (data.StartsWith("login_ok"))
                return;
            throw new InvalidOperationException(data);
        }

        public static async Task DisconnentAsync()
        {
            var r = await client.PostAsync("http://10.0.10.66/include/auth_action.php", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["action"] = "logout",
                ["ajax"] = "1"
            }));
            var data = await r.Content.ReadAsStringAsync();
            if (data == "网络已断开")
                return;
            throw new InvalidOperationException(data);
        }
    }
}
