using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Dadata.Test
{
    [TestFixture]
    public class GeolocateClientTest
    {
        public GeolocateClient api { get; set; }

        [SetUp]
        public void SetUp()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            api = new GeolocateClient(token);
        }

        [Test]
        public void GeolocateTest()
        {
            var response = api.Geolocate(lat: 55.7366021, lon: 37.597643);
            var address = response.suggestions[0].data;
            Assert.AreEqual(address.city, "Москва");
            Assert.AreEqual(address.street, "Турчанинов");
        }

        [Test]
        public async Task GeolocateAsyncTest()
        {
            var response = await api.GeolocateAsync(lat: 55.7366021, lon: 37.597643);
            var address = response.suggestions[0].data;
            Assert.AreEqual(address.city, "Москва");
            Assert.AreEqual(address.street, "Турчанинов");
        }

        [Test]
        public void NotFoundTest()
        {
            var response = api.Geolocate(lat: 0, lon: 0);
            Assert.AreEqual(response.suggestions.Count, 0);
        }

        [Test]
        public async Task AsyncNotFoundTest()
        {
            var response = await api.GeolocateAsync(lat: 0, lon: 0);
            Assert.AreEqual(response.suggestions.Count, 0);
        }
    }
}

