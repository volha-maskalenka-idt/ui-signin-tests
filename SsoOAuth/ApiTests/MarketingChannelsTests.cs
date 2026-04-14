using NUnit.Framework;
using SsoOAuth.DB.Steps;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class MarketingChannelsTests
    {
        private MarketingChannelsApiSteps _apiSteps;
        private DbSteps _dbSteps;

        private const decimal MarketingChannelId = 4149;

        [SetUp]
        public void SetUp()
        {
            _apiSteps = new MarketingChannelsApiSteps();
            _dbSteps = new DbSteps();
        }

        [Test]
        public void CheckGetMarketingChannels()
        {
            var dbChannels = _dbSteps.GetAllMarketingChannels();

            _apiSteps.SendPostRequestForSearchMarketingChannelsWithAuth(new Dictionary<string, string>());
            _apiSteps.VerifyResponseStatus("OK");

            var (items, totalCount) = _apiSteps.GetMarketingChannelsFromResponse();
            _dbSteps.VerifySearchMarketingChannelsResponseMatchesDb(dbChannels, items, totalCount);
        }

        [Test]
        public void CheckGetMarketingChannelById()
        {
            var dbExpected = _dbSteps.GetMarketingChannelById(MarketingChannelId);

            _apiSteps.SendPostRequestForSearchMarketingChannelByIdWithAuth(MarketingChannelId);
            _apiSteps.VerifyResponseStatus("OK");

            var (items, totalCount) = _apiSteps.GetMarketingChannelsFromResponse();
            _dbSteps.VerifySearchMarketingChannelsResponseMatchesDb(dbExpected, items, totalCount);
        }
    }
}
