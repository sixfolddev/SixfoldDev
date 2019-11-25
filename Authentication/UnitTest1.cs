using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuthenticationSystem;
using System;


namespace UnitTestAuthentication
{
    [TestClass]
    public class UnitTest1
    {
        Authentication authentication = new Authentication("tester01", "password1");

        [TestMethod]
        public void salt()
        {
            Assert.AreNotEqual(authentication.getRetrievedSalt(), -1);
        }
        [TestMethod]
        public void hashing()
        {
            Assert.AreEqual(authentication.generateHash(), "ZgXRBbgVVjx20b8hGJstIC/U97nb6FZrcBC+khDT0Vs=");
        }
    }
}
