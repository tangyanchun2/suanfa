using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string sIn= "kkkdjaadklsk";
            string sTarget = "kdjals";
            string res = SuanFa1.Program.removeDup(sIn.ToCharArray());
            Assert.AreEqual(res, sTarget);

        }
    }
}