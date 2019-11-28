using Dadata.Model;
using System.Threading.Tasks;

namespace Dadata
{
    /// <summary>
    /// DaData Suggestions API (https://dadata.ru/api/suggest/)
    /// </summary>
    public class SuggestClient : ClientBase
    {
        private const string BASE_URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";

        public SuggestClient(string token, string baseUrl = BASE_URL) : base(token, baseUrl) { }

        #region Find
        public SuggestResponse<Address> FindAddress(string query) =>
            Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Address, request: new SuggestRequest(query));
        public async Task<SuggestResponse<Address>> FindAddressAsync(string query) => await
            ExecuteAsync<SuggestResponse<Address>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Address, request: new SuggestRequest(query));

        public SuggestResponse<Bank> FindBank(string query) =>
            Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Bank, request: new SuggestRequest(query));
        public async Task<SuggestResponse<Bank>> FindBankAsync(string query) => await
            ExecuteAsync<SuggestResponse<Bank>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Bank, request: new SuggestRequest(query));

        public SuggestResponse<Party> FindParty(string query) =>
            FindParty(new FindPartyRequest(query));
        public async Task<SuggestResponse<Party>> FindPartyAsync(string query) => await
            FindPartyAsync(new FindPartyRequest(query));

        public SuggestResponse<Party> FindParty(FindPartyRequest request) =>
            Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Party, request: request);
        public async Task<SuggestResponse<Party>> FindPartyAsync(FindPartyRequest request) => await
            ExecuteAsync<SuggestResponse<Party>>(method: SuggestionsMethod.Find, entity: SuggestionsEntity.Party, request: request);
        #endregion Find

        #region Suggest
        public SuggestResponse<Address> SuggestAddress(string query, int count = 5) =>
            SuggestAddress(new SuggestAddressRequest(query, count));
        public async Task<SuggestResponse<Address>> SuggestAddressAsync(string query, int count = 5) => await
            SuggestAddressAsync(new SuggestAddressRequest(query, count));

        public SuggestResponse<Address> SuggestAddress(SuggestAddressRequest request) =>
            Execute<SuggestResponse<Address>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Address, request: request);
        public async Task<SuggestResponse<Address>> SuggestAddressAsync(SuggestAddressRequest request) => await
            ExecuteAsync<SuggestResponse<Address>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Address, request: request);

        public SuggestResponse<Bank> SuggestBank(string query, int count = 5) =>
            SuggestBank(new SuggestBankRequest(query, count));
        public async Task<SuggestResponse<Bank>> SuggestBankAsync(string query, int count = 5) => await
            SuggestBankAsync(new SuggestBankRequest(query, count));

        public SuggestResponse<Bank> SuggestBank(SuggestBankRequest request) =>
            Execute<SuggestResponse<Bank>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Bank, request: request);
        public async Task<SuggestResponse<Bank>> SuggestBankAsync(SuggestBankRequest request) => await
            ExecuteAsync<SuggestResponse<Bank>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Bank, request: request);

        public SuggestResponse<Email> SuggestEmail(string query, int count = 5) =>
            SuggestEmail(new SuggestRequest(query, count));
        public async Task<SuggestResponse<Email>> SuggestEmailAsync(string query, int count = 5) => await
            SuggestEmailAsync(new SuggestRequest(query, count));

        public SuggestResponse<Email> SuggestEmail(SuggestRequest request) =>
            Execute<SuggestResponse<Email>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Email, request: request);
        public async Task<SuggestResponse<Email>> SuggestEmailAsync(SuggestRequest request) => await
            ExecuteAsync<SuggestResponse<Email>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Email, request: request);

        public SuggestResponse<Fullname> SuggestName(string query, int count = 5) =>
            SuggestName(new SuggestNameRequest(query, count));
        public async Task<SuggestResponse<Fullname>> SuggestNameAsync(string query, int count = 5) => await
            SuggestNameAsync(new SuggestNameRequest(query, count));

        public SuggestResponse<Fullname> SuggestName(SuggestNameRequest request) =>
            Execute<SuggestResponse<Fullname>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Name, request: request);
        public async Task<SuggestResponse<Fullname>> SuggestNameAsync(SuggestNameRequest request) => await
            ExecuteAsync<SuggestResponse<Fullname>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Name, request: request);

        public SuggestResponse<Party> SuggestParty(string query, int count = 5) =>
            SuggestParty(new SuggestPartyRequest(query, count));
        public async Task<SuggestResponse<Party>> SuggestPartyAsync(string query, int count = 5) => await
            SuggestPartyAsync(new SuggestPartyRequest(query, count));

        public SuggestResponse<Party> SuggestParty(SuggestPartyRequest request) =>
            Execute<SuggestResponse<Party>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Party, request: request);
        public async Task<SuggestResponse<Party>> SuggestPartyAsync(SuggestPartyRequest request) => await
            ExecuteAsync<SuggestResponse<Party>>(method: SuggestionsMethod.Suggest, entity: SuggestionsEntity.Party, request: request);
        #endregion Suggest
    }
}
