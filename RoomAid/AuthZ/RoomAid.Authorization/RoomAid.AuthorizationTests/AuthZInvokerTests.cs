using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.Authorization;

namespace RoomAid.AuthorizationTests
{
    [TestClass]
    public class AuthZInvokerTests
    {
        AuthZInvoker invoker;
        AuthZFactory factory;

        [TestInitialize]
        public void Setup()
        {
            invoker = new AuthZInvoker();
            factory = new AuthZFactory();
        }

        [TestMethod]
        public void GetAdminAuthZ_NonAdmin_Test()
        {
            //Arrange
            bool[] expected = new bool[2] { false, false };
            var choice = AuthZInvoker.AuthAdmin.nonadmin;
            //Act
            var actual = invoker.GetAdminAuthZ(choice);

            //Assert
            Assert.AreEqual(expected.Length, actual.Length);
            for ( int i=0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void GetAdminAuthZ_Admin_Test()
        {
            //Arrange
            bool[] expected = new bool[2] { false, true };
            var choice = AuthZInvoker.AuthAdmin.admin;
            //Act
            var actual = invoker.GetAdminAuthZ(choice);

            //Assert
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void GetAdminAuthZ_SysAdmin_Test()
        {
            //Arrange
            bool[] expected = new bool[2] { true, true };
            var choice = AuthZInvoker.AuthAdmin.sysadmin;
            //Act
            var actual = invoker.GetAdminAuthZ(choice);

            //Assert
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        /// <summary>
        /// Test to see if AuthRole.anon called the createAnonAuthZ method.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Anon_Test1()
        {
            //Arrange
            var actual = invoker.CreateAuthZ(null, 0, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ , actual.MessageAuthZ );
            AssertAuthorizations(expected.InviteAuthZ , actual.InviteAuthZ );
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Tests invoker to see if displayName or householdID parameter have an effect.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Anon_Test2()
        {
            //Arrange
            var actual = invoker.CreateAuthZ("Substitute Name", 3240, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Tests invoker to see if displayName or householdID parameters have an effect, additionally attempted to give an anon account admin permissions.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Anon_Test3()
        {
            //Arrange
            var actual = invoker.CreateAuthZ("Substitute Name", 3240, AuthZInvoker.AuthRole.anon, AuthZInvoker.AuthAdmin.admin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Tests to see if a user with a householdID parameter will still not be assigned a household.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_User_Test1()
        {
            //Arrange
            var displayName = "Example Name";
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin);
            var authAdmin = invoker.GetAdminAuthZ(AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateUserAuthZ(displayName,authAdmin);

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(0, actual.HouseholdID); //users don't have households.
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Tests to see if a user with a null displayName creates an anon authZ
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_User_Test2()
        {
            //Arrange
            string displayName = null;
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.user, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID); 
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if AuthRole.host creates a host AuthZAttribute object.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Host_Test1()
        {
            //Arrange
            var displayName = "Example Name";
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin);
            var authAdmin = invoker.GetAdminAuthZ(AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateHostAuthZ(displayName, householdID, authAdmin);

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if invalid displayName results in an anonAuthZ attribute object being made.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Host_Test2()
        {
            //Arrange
            string displayName = null;
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if invalid householdID results in an anonAuthZ attribute object being made.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Host_Test3()
        {
            //Arrange
            var displayName = "Example Name";
            var householdID = 0;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.host, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if AuthRole.cohost creates a cohost AuthZAttribute object.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_CoHost_Test1()
        {
            //Arrange
            var displayName = "Example Name";
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin);
            var authAdmin = invoker.GetAdminAuthZ(AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateCoHostAuthZ(displayName, householdID, authAdmin);

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if invalid displayName results in an anon AuthZAttribute object being made.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_CoHost_Test2()
        {
            //Arrange
            string displayName = null;
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if invalid displayName results in an anon AuthZAttribute object being made.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_CoHost_Test3()
        {
            //Arrange
            var displayName = "Example Name";
            var householdID = 0;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.cohost, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }



        [TestMethod]
        public void CreateAuthZ_Tenant_Test1()
        {
            //Arrange
            var displayName = "Example Name";
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin);
            var authAdmin = invoker.GetAdminAuthZ(AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateTenantAuthZ(displayName, householdID, authAdmin);

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if invalid displayName results in an anon AuthZAttribute object being made.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Tenant_Test2()
        {
            //Arrange
            string displayName = null;
            var householdID = 1234;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        /// <summary>
        /// Test to see if invalid displayName results in an anon AuthZAttribute object being made.
        /// </summary>
        [TestMethod]
        public void CreateAuthZ_Tenant_Test3()
        {
            //Arrange
            var displayName = "Example Name";
            var householdID = 0;
            var actual = invoker.CreateAuthZ(displayName, householdID, AuthZInvoker.AuthRole.tenant, AuthZInvoker.AuthAdmin.nonadmin);
            var expected = factory.CreateAnonAuthZ();

            //Assert
            Assert.AreEqual(expected.DisplayName, actual.DisplayName);
            Assert.AreEqual(expected.HouseholdID, actual.HouseholdID);
            AssertAuthorizations(expected.EnabledAuthZ, actual.EnabledAuthZ);
            AssertAuthorizations(expected.AdminAuthZ, actual.AdminAuthZ);
            AssertAuthorizations(expected.AccountAuthZ, actual.AccountAuthZ);
            AssertAuthorizations(expected.SearchHouseholdAuthZ, actual.SearchHouseholdAuthZ);
            AssertAuthorizations(expected.MessageAuthZ, actual.MessageAuthZ);
            AssertAuthorizations(expected.InviteAuthZ, actual.InviteAuthZ);
            AssertAuthorizations(expected.HouseholdAuthZ, actual.HouseholdAuthZ);
            AssertAuthorizations(expected.TenantAuthZ, actual.TenantAuthZ);
            AssertAuthorizations(expected.ExpenseAuthZ, actual.ExpenseAuthZ);
            AssertAuthorizations(expected.TaskAuthZ, actual.TaskAuthZ);
            AssertAuthorizations(expected.SRequestAuthZ, actual.SRequestAuthZ);

        }

        public void AssertAuthorizations(bool[] actual, bool[] expected)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        public void AssertAuthorizations(bool actual, bool expected)
        {
            Assert.AreEqual(expected, actual);
        }
    }
}
