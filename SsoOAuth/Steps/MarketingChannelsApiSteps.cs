using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.BaseClasses.Entities.API;
using SsoOAuth.DB.Entities;
using SsoOAuth.Managers;

namespace SsoOAuth.Steps
{
    public class MarketingChannelsApiSteps
    {
        private RestResponse _response;
        private string _content;

        public void SendPostRequestForSearchMarketingChannelsWithAuth(Dictionary<string, string> body)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            _response = FireballApiManager.PostRequestWithAuth("/api/fsa/marketing-channels/search", jsonBody);
            _content = _response.Content;
        }

        public void SendPostRequestForSearchMarketingChannelByIdWithAuth(decimal id)
        {
            var body = new JObject { ["ids"] = new JArray { id } };
            _response = FireballApiManager.PostRequestWithAuth("/api/fsa/marketing-channels/search", body.ToString());
            _content = _response.Content;
        }

        public void VerifyResponseStatus(string expected)
        {
            SoftAssert.AreEqual(expected, _response.StatusDescription);
        }

        public (List<MarketingChannelDbEntity> Items, int TotalCount) GetMarketingChannelsFromResponse()
        {
            var response = JsonConvert.DeserializeObject<SearchMarketingChannelsResponse>(_content)!;
            var items = response.Items.Select(i => new MarketingChannelDbEntity
            {
                Id = i.Id,
                Name = i.Name,
                CountryAbbreviations = i.CountryAbbreviations
            }).ToList();
            return (items, response.TotalCount);
        }
    }
}
