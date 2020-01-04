using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.Authorization;

namespace RoomAid.AuthorizationTests
{
    [TestClass]
    public class AuthZHandlerTests
    {
        AuthZInvoker invoker = new AuthZInvoker();
        AuthZAttribute attributes;
        AuthZHandler handler = new AuthZHandler();

        [TestMethod]
        [DataRow("Billy",1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon Creation Isenabled")]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon Creation Isenabled")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon Creation Isenabled")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon Creation Isenabled")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon Creation Isenabled")]
        public void AuthIsEnabled_Test(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthIsEnabled(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthCreateAdmin")]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthCreateAdmin")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthCreateAdmin")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthCreateAdmin")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthCreateAdmin")]
        public void AuthCreateAdmin_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthCreateAdmin(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing User Creation AuthCreateAdmin")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing Host Creation AuthCreateAdmin")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing CoHost Creation AuthCreateAdmin")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing Tenant Creation AuthCreateAdmin")]
        public void AuthCreateAdmin_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act

            var actual = handler.AuthCreateAdmin(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.admin, DisplayName = "Testing User Creation AuthEnableAccount")]
        [DataRow("Jake", 5432, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.admin, DisplayName = "Testing Host Creation AuthEnableAccount")]
        [DataRow("Drake", 34, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.admin, DisplayName = "Testing CoHost Creation AuthEnableAccount")]
        [DataRow("Hannah", 123, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.admin, DisplayName = "Testing Tenant Creation AuthEnableAccount")]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing User Creation AuthEnableAccount")]
        [DataRow("Jake", 5432, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing Host Creation AuthEnableAccount")]
        [DataRow("Drake", 34, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing CoHost Creation AuthEnableAccount")]
        [DataRow("Hannah", 123, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.sysadmin, DisplayName = "Testing Tenant Creation AuthEnableAccount")]
        public void AuthEnableAccount_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthEnableAccount(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthEnableAccount")]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthEnableAccount")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthEnableAccount")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthEnableAccount")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthEnableAccount")]
        public void AuthEnableAccount_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthEnableAccount(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthEditProfile")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthEditProfile")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthEditProfile")]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthEditProfile")]
        public void AuthEditProfile_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthEditProfile(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Billy", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthEditProfile")]
        public void AuthEditProfile_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthEditProfile(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthViewProfile")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthViewProfile")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthViewProfile")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthViewProfile")]
        public void AuthViewProfile_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthViewProfile(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthViewProfile")]
        public void AuthViewProfile_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthViewProfile(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthDeleteAccount")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthDeleteAccount")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthDeleteAccount")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthDeleteAccount")]
        public void AuthDeleteAccount_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthDeleteAccount(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthDeleteAccount")]
        public void AuthDeleteAccount_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthDeleteAccount(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthSearchHousehold")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthSearchHousehold")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthSearchHousehold")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthSearchHousehold")]
        public void AuthSearchHousehold_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthSearchHousehold(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthSearchHousehold")]
        public void AuthSearchHousehold_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthSearchHousehold(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthSendMessage")]
        public void AuthSendMessage_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthSendMessage(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthSendMessage")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthSendMessage")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthSendMessage")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthSendMessage")]
        public void AuthSendMessage_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthSendMessage(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthReplyMessage")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthReplyMessage")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthReplyMessage")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthReplyMessage")]
        public void AuthReplyMessage_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthReplyMessage(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthReplyMessage")]
        
        public void AuthReplyMessage_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthReplyMessage(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthViewMessage")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthViewMessage")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthViewMessage")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthViewMessage")]
        public void AuthViewMessage_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthViewMessage(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthViewMessage")]
        public void AuthViewMessage_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthViewMessage(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthMarkMessage")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthMarkMessage")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthMarkMessage")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthMarkMessage")]
        public void AuthMarkMessage_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthMarkMessage(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthMarkMessage")]
        public void AuthMarkMessage_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthMarkMessage(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthDeleteMessage")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthDeleteMessage")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthDeleteMessage")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthDeleteMessage")]
        public void AuthDeleteMessage_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthDeleteMessage(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthDeleteMessage")]
        public void AuthDeleteMessage_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthDeleteMessage(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Jill", 1234, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing User Creation AuthViewInvite")]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthViewInvite")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthViewInvite")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthViewInvite")]
        public void AuthViewInvite_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthViewInvite(attributes);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthViewInvite")]
        public void AuthViewInvite_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);


            //Act
            var actual = handler.AuthDeleteMessage(attributes);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthEditSRequest")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthEditSRequest")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthEditSRequest")]
        public void AuthEditSRequest_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);
            string creatorName = "Hannah";

            //Act
            var actual = handler.AuthEditSRequest(attributes, creatorName);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthEditSRequest")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthEditSRequest")]
        public void AuthEditSRequest_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);
            string creatorName = "John";

            //Act
            var actual = handler.AuthEditSRequest(attributes, creatorName);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [DataRow("Billy", 234, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Host Creation AuthEditSRequest")]
        [DataRow("Billy", 123, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing CoHost Creation AuthEditSRequest")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthEditSRequest")]
        public void AuthDeleteSRequest_Test_True(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);
            string creatorName = "Hannah";

            //Act
            var actual = handler.AuthDeleteSRequest(attributes, creatorName);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow("Jake", 1234, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Anon AuthDeleteSRequest")]
        [DataRow("Hannah", 134, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin, DisplayName = "Testing Tenant Creation AuthDeleteSRequest")]
        public void AuthDeleteSRequest_Test_False(string displayName, int householdID, AuthZInvoker.AuthRole role, AuthZInvoker.AuthAdmin admin)
        {
            //Arrange
            attributes = invoker.CreateAuthZ(displayName, householdID, role, admin);
            string creatorName = "John";

            //Act
            var actual = handler.AuthDeleteSRequest(attributes, creatorName);

            //Assert
            Assert.AreEqual(false, actual);
        }






    }
}
