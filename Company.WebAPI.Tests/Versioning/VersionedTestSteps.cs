using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using Company.WebAPI.Versioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using TechTalk.SpecFlow;
using System.Linq;

namespace Company.WebAPI.Tests.Versioning
{
    [Binding]
    public class VersionedTestSteps
    {
        private string[] _allowedVersions;
        private Mock<VersionConstraint> _versionConstraint;

        [BeforeScenario]
        public void BeforeScenario()
        {
            
        }

        [Given(@"指定可使用的版本號為 '(.*)'")]
        public void Given指定可使用的版本號為(string allowedVersions)
        {
            _allowedVersions = allowedVersions.Split(',').Select(v => v.Trim()).ToArray();
        }

        [Given(@"指定為預設版本控管路徑")]
        public void Given指定為預設版本控管路徑()
        {
            _versionConstraint = new Mock<VersionConstraint>(string.Join(";", _allowedVersions));
            _versionConstraint.Protected().Setup<string[]>("SupportedVersions").Returns(_allowedVersions);
        }

        [Given(@"指定版本控管區間為 (.*) 到 (.*)")]
        public void Given指定版本控管區間為到(string minVersion, string maxVersion)
        {
            _versionConstraint = new Mock<VersionConstraint>(new object[] { new [] { minVersion, maxVersion } });
            _versionConstraint.Protected().Setup<string[]>("SupportedVersions").Returns(_allowedVersions);
        }

        [Given(@"指定版本控管區間最小版本為 (.*) 到最新")]
        public void Given指定版本控管區間最小版本為到最新(string minVersion)
        {
            _versionConstraint = new Mock<VersionConstraint>(new object[] { new[] { minVersion } });
            _versionConstraint.Protected().Setup<string[]>("SupportedVersions").Returns(_allowedVersions);
        }


        [When(@"輸入的版本號為 (.*)")]
        public void When輸入的版本號為(string version)
        {
            bool actual = _versionConstraint.Object.Match(new HttpRequestMessage(), null
                , "version", new Dictionary<string, object>() { { "version", version } }
                , HttpRouteDirection.UriResolution);

            ScenarioContext.Current.Set(actual, "result");
        }
        
        [Then(@"版本路徑正常運作為 (.*)")]
        public void Then版本路徑正常運作(bool expected)
        {
            var actual = ScenarioContext.Current.Get<bool>("result");
            Assert.AreEqual(expected, actual);
        }

        
    }
}
