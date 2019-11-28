using Dadata.Model;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Dadata.Test
{
    [TestFixture]
    public partial class SuggestClientTest
    {
        [Test]
        public async Task SuggestAddressAsyncTest()
        {
            var query = "москва турчанинов 6с2";
            var response = await api.SuggestAddressAsync(query);
            var address_data = response.suggestions[0].data;
            Assert.AreEqual("119034", address_data.postal_code);
            Assert.AreEqual("7704", address_data.tax_office);
            Assert.AreEqual("Парк культуры", address_data.metro[0].name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestAddressAsyncLocationsKladrTest()
        {
            var query = new SuggestAddressRequest("ватутина")
            {
                locations = new[] { new Address() { kladr_id = "65" } }
            };
            var response = await api.SuggestAddressAsync(query);
            Assert.AreEqual("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestAddressAsyncLocationsMultipleLocationsTest()
        {
            var query = new SuggestAddressRequest("зеленоград")
            {
                locations = new[]
                {
                    new Address() { kladr_id = "50" },
                    new Address() { kladr_id = "77" }
                }
            };
            var response = await api.SuggestAddressAsync(query);
            Assert.AreEqual("Зеленоград", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestAddressAsyncLocationsFiasCityTest()
        {
            var query = new SuggestAddressRequest("ватутина")
            {
                // Южно-Сахалинск
                locations = new[] { new Address() { city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68" } }
            };
            var response = await api.SuggestAddressAsync(query);
            Assert.AreEqual("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestAddressAsyncBoundsTest()
        {
            var query = new SuggestAddressRequest("ново")
            {
                from_bound = new AddressBound("city"),
                to_bound = new AddressBound("city")
            };
            var response = await api.SuggestAddressAsync(query);
            Assert.AreEqual("Новосибирск", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestAddressAsyncHistoryTest()
        {
            var query = "москва хабар";
            var response = await api.SuggestAddressAsync(query);
            var address_data = response.suggestions[0].data;
            Assert.AreEqual("ул Черненко", address_data.history_values[0]);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task FindAddressAsyncTest()
        {
            var response = await api.FindAddressAsync("95dbf7fb-0dd4-4a04-8100-4f6c847564b5");
            var address = response.suggestions[0].data;
            Assert.AreEqual(address.city, "Москва");
            Assert.AreEqual(address.street, "Сухонская");
        }

        [Test]
        public async Task SuggestBankTypeAsyncTest()
        {
            var query = new SuggestBankRequest("я")
            {
                type = new BankType[] { BankType.NKO }
            };
            var response = await api.SuggestBankAsync(query);
            Assert.AreEqual("044525444", response.suggestions[0].data.bic);
            Assert.AreEqual(new DateTime(2012, 08, 02), response.suggestions[0].data.state.registration_date);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task FindBankAsyncTest()
        {
            var response = await api.FindBankAsync("044525974");
            var bank = response.suggestions[0].data;
            Assert.AreEqual(bank.swift, "TICSRUMMXXX");
        }

        [Test]
        public async Task SuggestEmailAsyncTest()
        {
            var query = "anton@m";
            var response = await api.SuggestEmailAsync(query);
            Assert.AreEqual("anton@mail.ru", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestFioAsyncTest()
        {
            var query = "викт";
            var response = await api.SuggestNameAsync(query);
            Assert.AreEqual("Виктор", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestFioPartsAsyncTest()
        {
            var query = new SuggestNameRequest("викт")
            {
                parts = new FullnamePart[] { FullnamePart.SURNAME }
            };
            var response = await api.SuggestNameAsync(query);
            Assert.AreEqual("Викторова", response.suggestions[0].data.surname);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestPartyAsyncTest()
        {
            var query = "7707083893";
            var response = await api.SuggestPartyAsync(query);
            var party = response.suggestions[0];
            var address = response.suggestions[0].data.address;
            Assert.AreEqual("7707083893", party.data.inn);
            Assert.AreEqual("г Москва, ул Вавилова, д 19", address.value);
            Assert.AreEqual("117997, ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, 19", address.data.source);
            Assert.AreEqual("117312", address.data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestPartyStatusAsyncTest()
        {
            var query = new SuggestPartyRequest("витас")
            {
                status = new PartyStatus[] { PartyStatus.LIQUIDATED }
            };
            var response = await api.SuggestPartyAsync(query);
            Assert.AreEqual("4713008497", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task SuggestPartyTypeAsyncTest()
        {
            var query = new SuggestPartyRequest("лаукаитис витас")
            {
                type = PartyType.INDIVIDUAL
            };
            var response = await api.SuggestPartyAsync(query);
            Assert.AreEqual("773165008890", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public async Task FindPartyAsyncTest()
        {
            var response = await api.FindPartyAsync("7719402047");
            var party = response.suggestions[0].data;
            Assert.AreEqual(party.name.@short, "МОТОРИКА");
        }

        [Test]
        public async Task FindPartyWithKppAsyncTest()
        {
            var request = new FindPartyRequest(query: "7728168971", kpp: "667102002");
            var response = await api.FindPartyAsync(request);
            var party = response.suggestions[0].data;
            Assert.AreEqual(party.name.short_with_opf, "ФИЛИАЛ \"ЕКАТЕРИНБУРГСКИЙ\" АО \"АЛЬФА-БАНК\"");
        }
    }
}
