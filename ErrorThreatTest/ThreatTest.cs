using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ErrorHandling;
using MongoDB.Driver;
using System.Data.SqlClient;
using System;

namespace ErrorThreatTest
{
    /// <summary>
    /// Class to Test Functionality of Warning Threat Assessment
    /// </summary>
    [TestClass]
    public class ThreatTest
    {
        
        /// <summary>
        /// Method to test if MongoAuthenticationException is returning fatal
        /// </summary>
        [TestMethod]
        public void TestMongoAuthenticationException()
        {
            
            bool Answer = false;
            try
            {
                String ConnectionString = "mongodb+srv://<rwUser>:<readwrite>@logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority";
                MongoClient Client = new MongoClient(ConnectionString);
                var Database = Client.GetDatabase("test");
            }   
            catch (MongoAuthenticationException e)
            {
                ErrorController ErrorThreatController = new ErrorController(e);

                ErrorThreatController.Handle();
                Answer = ErrorThreatController.Lev == Level.Warning;
            }
            catch (Exception)
            {
               
            }

            
            Assert.IsTrue(Answer);
        }

        [TestMethod]
        public void TestMongoConnectionException()
        {
            bool Answer = false;




            Assert.IsTrue(Answer);


        }

        [TestMethod]
        public void TestMongoConfigurationException()
        {
            bool Answer;
            ErrorController ThreatController = new ErrorController(new MongoConfigurationException("yeet"));

            ThreatController.Handle();
            Answer = (ThreatController.Lev == Level.Warning);

            Assert.IsTrue(Answer);


        }

        [TestMethod]
        public void TestMongoCursorNotFoundException()
        {
            //bool Answer;
            //ErrorController ThreatController = new ErrorController(new MongoCursorNotFoundException());
        }

        /// <summary>
        /// Test For Valid Answer of generic MongoException, returning error
        /// </summary>
        [TestMethod]
        public void TestMongoException()
        {
            //assign
            ErrorController ErrorThreatController = new ErrorController(new MongoException("yeet"));
            //act
            ErrorThreatController.Handle();
            bool Answer = (ErrorThreatController.Lev == Level.Warning);

            //assert
            Assert.IsTrue(Answer);

        }

        [TestMethod]
        public void TestErrorThreatManager()
        {
            bool Answer = false;

            Answer = (ErrorThreatManager.GetThreatLevel(new Exception()) == Level.Warning);
            Assert.IsTrue(Answer);


        }

        [TestMethod]
        public void TestGeneralException()
        {
            bool Answer;
            ErrorController ThreatController = new ErrorController(new System.Exception("yee"));

            ThreatController.Handle();
            Answer = (ThreatController.Lev == Level.Warning);

            Assert.IsTrue(Answer);
        }

        [TestMethod]
        public void TestSqlExceptionUnderEleven()
        {
            bool Answer = false;
            string connect = "";
            try
            {
                SqlConnection connection = new SqlConnection(connect);
            }
            catch (SqlException e)
            {

                ErrorController ThreatController = new ErrorController(e);
                ThreatController.Handle();
                Answer = (ThreatController.Lev == Level.Warning);
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(Answer);

        }

        [TestMethod]
        public void TestSqlExceptionBetweenElevenAndSixteen()
        {
            bool Answer = false;






            Assert.IsTrue(Answer);
        }

        [TestMethod]
        public void TestSqlExceptionBetweenSeventeenAndNineteen()
        {
            bool Answer = false;



            Assert.IsTrue(Answer);
        }

        [TestMethod]
        public void TestSqlExceptionAboveNineteen()
        {
            bool Answer = false;



            Assert.IsTrue(Answer);
        }

        
        
    }
}
