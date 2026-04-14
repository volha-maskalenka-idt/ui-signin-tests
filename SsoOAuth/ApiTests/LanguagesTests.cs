using NUnit.Framework;
using SsoOAuth.DB.Steps;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class LanguagesTests
    {
        private LanguagesApiSteps _steps;
        private DbSteps _dbSteps;

        private const decimal LanguageId = 62; 

        [SetUp]
        public void SetUp()
        {
            _steps = new LanguagesApiSteps();
            _dbSteps = new DbSteps();
        }

        [Test]
        public void CheckGetLanguages()
        {
            var dbLanguages = _dbSteps.GetAllLanguages();

            _steps.SendPostRequestForSearchLanguagesWithAuth(new Dictionary<string, string>());
            _steps.VerifyResponseStatus("OK");

            var (languages, totalCount) = _steps.GetLanguagesFromResponse();
            _dbSteps.VerifyLanguagesResponseMatchesDb(languages, dbLanguages, totalCount);
        }

        [Test]
        public void CheckGetLanguageById()
        {
            var dbExpected = _dbSteps.GetLanguageById(LanguageId);

            _steps.SendPostRequestForSearchLanguageByIdWithAuth(LanguageId);
            _steps.VerifyResponseStatus("OK");

            var (languages, totalCount) = _steps.GetLanguagesFromResponse();
            _dbSteps.VerifyLanguagesResponseMatchesDb(languages, dbExpected, totalCount);
        }
    }
}
