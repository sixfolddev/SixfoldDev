using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErrorHandling;
using MongoDB.Driver;
using System.Data.SqlClient;
using System;

namespace ErrorThreatTest
{
    /// <summary>
    /// Class to Test Functionality of Error Threat Assessment
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
            
            bool answer = false;
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
                answer = (ErrorThreatController.Lev == Level.Fatal);
            }
            catch (Exception)
            {
                answer = true;
            }

            
            Assert.IsTrue(answer);
        }

        [TestMethod]
        public void TestMongoConnectionException()
        {
            bool answer = false;




            Assert.IsTrue(answer);


        }

        [TestMethod]
        public void TestMongoConfigurationException()
        {
            bool answer;
            ErrorController ThreatController = new ErrorController(new MongoConfigurationException("yeet"));

            ThreatController.Handle();
            answer = (ThreatController.Lev == Level.Error);

            Assert.IsTrue(answer);


        }

        [TestMethod]
        public void TestMongoCursorNotFoundException()
        {
            //bool answer;
            //ErrorController ThreatController = new ErrorController(new MongoCursorNotFoundException());
        }

        [TestMethod]
        public void TestMongoInternalException()
        {
            bool answer = false;
            ErrorController ThreatController = new ErrorController(new MongoInternalException("ye"));

            ThreatController.Handle();
            answer = (ThreatController.Lev == Level.Error);

            Assert.IsTrue(answer);
        }

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
            bool answer = false;
            string connect = "";
            try
            {
                SqlConnection connection = new SqlConnection(connect);
            }
            catch (SqlException e)
            {

                ErrorController ThreatController = new ErrorController(e);
                ThreatController.Handle();
                answer = (ThreatController.Lev == Level.Error);
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(answer);

        }

        [TestMethod]
        public void TestSqlExceptionBetweenElevenAndSixteen()
        {
            bool answer = false;






            Assert.IsTrue(answer);
        }

        [TestMethod]
        public void TestSqlExceptionBetweenSeventeenAndNineteen()
        {
            bool answer = false;



            Assert.IsTrue(answer);
        }

        [TestMethod]
        public void TestSqlExceptionAboveNineteen()
        {
            bool answer = false;



            Assert.IsTrue(answer);
        }

        
        
    }
}
