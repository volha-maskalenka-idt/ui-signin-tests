using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.BaseClasses.Entities.API;
using SsoOAuth.Managers;

namespace SsoOAuth.Steps
{
    public class LanguagesApiSteps
    {
        private RestResponse _response;
        private string _content;

        public void SendPostRequestForSearchLanguagesWithAuth(Dictionary<string, string> body)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            _response = FireballApiManager.PostRequestWithAuth("/api/fsa/languages/search", jsonBody);
            _content = _response.Content;
        }

        public void SendPostRequestForSearchLanguageByIdWithAuth(decimal id)
        {
            var body = new JObject { ["ids"] = new JArray { id } };
            _response = FireballApiManager.PostRequestWithAuth("/api/fsa/languages/search", body.ToString());
            _content = _response.Content;
        }

        public void VerifyResponseStatus(string expected)
        {
            SoftAssert.AreEqual(expected, _response.StatusDescription);
        }

        public (List<LanguageEntity> Languages, int TotalCount) GetLanguagesFromResponse()
        {
            var response = JsonConvert.DeserializeObject<SearchLanguagesResponse>(_content);
            return (response!.Languages, response.TotalCount);
        }
    }
}
