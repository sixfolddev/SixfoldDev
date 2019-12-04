using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ErrorHandling;
using MongoDB.Driver;
using System.Data.SqlClient;
using System;
using MongoDB.Bson;
using RoomAid.ServiceLayer.Logging;

namespace RoomAid.ErrorHandlingTest
{
    /// <summary>
    /// Class to Test Functionality of Warning Threat Assessment
    /// </summary>
    [TestClass]
    public class ThreatTest
    {
        //MongoClient _client = new MongoClient("mongodb+srv://rwUser:4agLEh9JFz7P5QC4@roomaid-logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");
        public IMongoCollection<BsonDocument> collection;

        /// <summary>
        /// Method to test if MongoAuthenticationException is returning fatal
        /// </summary>
        [TestMethod]
        public void TestMongoAuthenticationException()
        {
            
            bool Answer = false;
            try
            {
                MongoClient Client = new MongoClient("mongodb+srv://rwUser:4agLEh9JFz7P5QC4@roomaid-logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");
                IMongoDatabase db = Client.GetDatabase("test");
            }   
            catch (MongoAuthenticationException e)
            {
                ErrorController ErrorThreatController = new ErrorController(e);

                ErrorThreatController.Handle();
                Answer = ErrorThreatController.Err.Lev == LogLevels.Levels.Warning;
            }
            catch (Exception)
            {
                Assert.Fail();
                throw;
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
            try
            {
                throw new MongoConfigurationException("f");
            }
            catch(Exception e)
            {

                ErrorThreatService Service = new ErrorThreatService();
                Answer = Service.GetThreatLevel(e) == LogLevels.Levels.Warning;
            }
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
            bool Answer;
            //act
            var Service = new ErrorThreatService();
            Answer = Service.GetThreatLevel(new MongoException("yeet")) == LogLevels.Levels.Warning;
            //assert
            Assert.IsTrue(Answer);

        }

        [TestMethod]
        public void TestErrorThreatManager()
        {
            bool Answer = false;
            var Err = new AnalyzedError(new Exception())
            {
                Lev = LogLevels.Levels.Warning
            };
            var Manager = new ErrorThreatManager(new Exception());
            var Compare = Manager.GetThreatLevel();
            Answer = (Compare.Lev == Err.Lev);
            Assert.IsTrue(Answer);


        }

        [TestMethod]
        public void TestGeneralException()
        {
            bool Answer;
            var Service = new ErrorThreatService();

            try
            {
                Answer = Service.GetThreatLevel(new Exception()) == LogLevels.Levels.Warning;
            }
            catch
            {
                throw;
            }
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
                Answer = (ThreatController.Err.Lev == LogLevels.Levels.Warning);
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
