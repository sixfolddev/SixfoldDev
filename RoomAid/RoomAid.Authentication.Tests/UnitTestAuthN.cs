﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.Authentication;

namespace UnitTestAuthentication
{
    [TestClass]
    public class UnitTestAuthN
    {
        Authentication authentication = new Authentication("tester01", "password1");

        [TestMethod]
        public void hashing()
        {
            //check if hashed pw is the same is the one stored to user ID in pw file
            //If this passes, user is authenticated
            Assert.IsTrue(authentication.GetAuthenticated());
        }


    }
}

