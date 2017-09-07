using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StarmileFx.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string str = "erewr";
            str = str.Length > 200 ? str.Substring(0, 200) : str;
        }
    }
}
