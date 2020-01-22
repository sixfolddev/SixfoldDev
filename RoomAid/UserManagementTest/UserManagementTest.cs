using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.UserManagement;

namespace UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {
        [TestMethod]
        public void TestUserConstructor()
        {
            User person = new User();
            Assert.IsInstanceOfType(person, typeof(User));
        }

        [TestMethod]
        public void TestGetterSetterUserFirstName()
        {
            User person = new User();
            person.FirstName = "jeff";
            Assert.AreEqual("jeff", person.FirstName);
        }
    }
}
