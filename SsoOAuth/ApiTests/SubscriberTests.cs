using SsoOAuth.Helpers;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class SubscriberGroupsApiTests
    {
        private SubscriberGroupsApiSteps _steps;
        private Dictionary<string, string> _testData;

        [SetUp]
        public void Setup()
        {
            _steps = new SubscriberGroupsApiSteps();
            _testData = FileManagerHelper.ReadJson("TestData.json");
        }

        [Test]
        public void GetSubscriberGroupsFull_WithValidPhone_ShouldReturnOkStatus()
        {
            var param = new Dictionary<string, string> 
            {
                { "phone", _testData["userPhoneNumber"] }
            };

            _steps.GetSubscriberGroupsFullListByPhone(param);
            _steps.VerifyResponseStatusCode("OK");
        }

        [Test]
        public void GetSubscriberGroupsFull_WithValidPhone_ShouldReturnExpectedGroupsCount()
        {
            var param = new Dictionary<string, string> 
            {
                { "phone", _testData["userPhoneNumber"] }
            };
            _steps.GetSubscriberGroupsFullListByPhone(param);
            _steps.VerifyGroupsCount("20");
        }
    }
}