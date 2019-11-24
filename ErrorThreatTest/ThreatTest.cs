using Microsoft.VisualStudio.TestTools.UnitTesting;
using Error_Handling;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using System.Net;
using System.Net.Sockets;
using MongoDB.Driver.Core.Servers;


namespace ErrorThreatTest
{
    /// <summary>
    /// Class to Test Functionality of Error Threat Assessment
    /// </summary>
    [TestClass]
    public class ThreatTest
    {
        /// <summary>
        /// Test For Valid answer of generic MongoException, returning error
        /// </summary>
        [TestMethod]
        public void TestMongoException()
        {
            //assign
            ErrorController ErrorThreatController = new ErrorController(new MongoException("yeet"));
            //act
            ErrorThreatController.Handle();
            bool answer = (ErrorThreatController.Lev == Level.Error);
           
            //assert
            Assert.IsTrue(answer);
            
        }
        /// <summary>
        /// Method to test if MongoAuthenticationException is returning fatal
        /// </summary>
        [TestMethod]
        public void TestMongoAuthenticationException()
        {
            //SocketAddress address = new SocketAddress(AddressFamily.Unknown);
            //ClusterId cluster = new ClusterId();
            //ServerId server =(cluster, EndPoint.Create(address))
            //MongoAuthenticationException E = new MongoAuthenticationException();
            //ErrorController ErrorThreatController = new ErrorController(E);

            //ErrorThreatController.Handle();
            //bool answer = (ErrorThreatController.Lev == Level.Fatal);

            //Assert.IsTrue(answer);
        }

        [TestMethod]
        public void TestMongoConnectionException()
        {
            


        }

        [TestMethod]
        public void TestMongoConfigurationException()
        {
            bool answer;
            ErrorController ThreatController = new ErrorController(new MongoConfigurationException("yeet"));

            ThreatController.Handle();
            answer = (ThreatController.Lev == Level.Fatal);

            Assert.IsTrue(answer);


        }

        [TestMethod]
        public void TestMongoCursorNotFoundException()
        {
            bool answer;
            //ErrorController ThreatController = new ErrorController(new MongoCursorNotFoundException());
        }

        [TestMethod]
        public void TestMongoInternalException()
        {
            bool answer;
            ErrorController ThreatController = new ErrorController(new MongoInternalException("yee"));

            ThreatController.Handle();
            answer = (ThreatController.Lev == Level.Warning);

            Assert.IsTrue(answer);
        }

        [TestMethod]
        public void TestGeneralException()
        {
            bool answer;
            ErrorController ThreatController = new ErrorController(new System.Exception("yee"));

            ThreatController.Handle();
            answer = (ThreatController.Lev == Level.Error);

            Assert.IsTrue(answer);
        }

        [TestMethod]
        public void TestSqlExceptionUnderEleven()
        {
            bool answer;
            //ErrorController ThreatController = new ErrorController(new SqlException());
        }

        
        
    }
}
