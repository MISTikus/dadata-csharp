using Dadata.Model;
using System.Threading.Tasks;

namespace Dadata
{
    public class GeolocateClient : ClientBase
    {
        private const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public GeolocateClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        public SuggestResponse<Address> Geolocate(double lat, double lon)
        {
            var request = new GeolocateRequest(lat, lon);
            return Execute<SuggestResponse<Address>>(method: "geolocate", entity: "address", request: request);
        }

        public async Task<SuggestResponse<Address>> GeolocateAsync(double lat, double lon)
        {
            var request = new GeolocateRequest(lat, lon);
            return await ExecuteAsync<SuggestResponse<Address>>(method: "geolocate", entity: "address", request: request);
        }
    }
}
