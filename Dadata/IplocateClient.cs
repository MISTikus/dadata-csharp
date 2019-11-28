using Dadata.Model;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Dadata
{
    public class IplocateClient : ClientBase
    {
        private const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public IplocateClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        public IplocateResponse Iplocate(string ip)
        {
            var parameters = new NameValueCollection { ["ip"] = ip };
            return ExecuteGet<IplocateResponse>(method: "iplocate", entity: "address", parameters: parameters);
        }

        public async Task<IplocateResponse> IplocateAsync(string ip)
        {
            var parameters = new NameValueCollection { ["ip"] = ip };
            return await ExecuteGetAsync<IplocateResponse>(method: "iplocate", entity: "address", parameters: parameters);
        }
    }
}
