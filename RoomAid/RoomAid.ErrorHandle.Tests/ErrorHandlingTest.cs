using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ErrorHandling;
using MongoDB.Driver;
using System.Data.SqlClient;
using System;
using MongoDB.Bson;
using RoomAid.ServiceLayer.Logging;
using System.Web;

namespace RoomAid.ErrorHandlingTest
{
    /// <summary>
    /// Class to Test Functionality of Warning Threat Assessment
    /// </summary>
    [TestClass]
    public class ErrorHandlingTest
    {
        readonly MongoClient _client = new MongoClient("mongodb+srv://rwUser:4agLEh9JFz7P5QC4@roomaid-logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");

        [TestMethod]
        public void TestErrorResponseManagerFatal()
        {
            var Err = new AnalyzedError(new Exception());
            Err.Lev = LogLevels.Levels.Fatal;
            Err.Message = "A fatal error has occurred and system admin has been contacted. Please try again at another time";

            var ErrFatal = new AnalyzedError(new Exception())
            {
                Lev = LogLevels.Levels.Fatal
            };

            var manager = new ErrorResponseManager();
            var Response = manager.GetResponse(ErrFatal);
            Assert.AreEqual(Err.Message, Response.Message);
        }

        [TestMethod]
        public void TestErrorResponseManagerError()
        {
            var Err = new AnalyzedError(new Exception());
            Err.Lev = LogLevels.Levels.Error;
            Err.Message = "An Error has occurred. Please try again";

            var ErrError = new AnalyzedError(new Exception())
            {
                Lev = LogLevels.Levels.Error
            };

            var manager = new ErrorResponseManager();
            var Response = manager.GetResponse(ErrError);
            Assert.AreEqual(Err.Message, Response.Message);
        }

        [TestMethod]
        public void TestErrorResponseManagerWarning()
        {
            var Err = new AnalyzedError(new Exception());
            Err.Lev = LogLevels.Levels.Warning;
            Err.Message = "Something unexpected occurred. Please try again.";

            var ErrWarning = new AnalyzedError(new Exception())
            {
                Lev = LogLevels.Levels.Warning
            };

            var manager = new ErrorResponseManager();
            var Response = manager.GetResponse(ErrWarning);
            Assert.AreEqual(Err.Message, Response.Message);
        }

      
       
        /// <summary>
        /// Testing the TimeoutExceptionGetThreatlevel
        /// </summary>
        [TestMethod]
        public void TestTimeoutException()
        {
            var Lev = LogLevels.Levels.Debug;
            try
            {
                throw new TimeoutException();
            }
            catch (TimeoutException e)
            {
                var Service = new ErrorThreatService();
                Lev = Service.GetThreatLevel(e);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.AreEqual(Lev, LogLevels.Levels.Error);
        }

        [TestMethod]
        public void TestUnauthorizedAccessException()
        {
            var Lev = LogLevels.Levels.Debug;
            try
            {
                throw new UnauthorizedAccessException();
            }
            catch (UnauthorizedAccessException e)
            {
                var Service = new ErrorThreatService();
                Lev = Service.GetThreatLevel(e);
            }
            catch
            {
                throw;
            }

            Assert.AreEqual(Lev, LogLevels.Levels.Warning);
        }

        /// <summary>
        /// testing httpunhandledexception getthreatlevel
        /// </summary>
        [TestMethod]
        public void TestHttpUnhandledException()
        {
            var Lev = LogLevels.Levels.Debug;
            try
            {
                throw new HttpUnhandledException();
            }
            catch (HttpUnhandledException e)
            {
                var Service = new ErrorThreatService();
                Lev = Service.GetThreatLevel(e);
              
            }
            catch
            {
                throw;
            }

            Assert.AreEqual(Lev, LogLevels.Levels.Warning);
        }

        /// <summary>
        /// Testing custom exception getthreatlevel
        /// </summary>
        [TestMethod]
        public void TestCustomException()
        {
            var Lev = LogLevels.Levels.Debug;
            try
            {
                throw new CustomException();
            }
            catch (CustomException e)
            {
                var Service = new ErrorThreatService();
                Lev = Service.GetThreatLevel(e);

            }
            catch
            {
                throw;
            }

            Assert.AreEqual(Lev, LogLevels.Levels.Warning);
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
        /// <summary>
        /// Test the full controller with .handle
        /// </summary>
        [TestMethod]
        public void TestErrorController()
        {
            var Control = new ErrorController(new Exception());
            Control.Handle();
            var Analyze = Control.Err;

            Assert.AreEqual(Analyze.Lev, LogLevels.Levels.Warning);
            Assert.IsInstanceOfType(Analyze.E, typeof(Exception));
            Assert.AreEqual(Analyze.Message, "Something unexpected occurred. Please try again.");
            
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

        // TODO: implement sql database integration test
        //[TestMethod]
        //public void TestSqlExceptionUnderEleven()
        //{
        //    bool Answer = false;
        //    string connect = "";
        //    try
        //    {
        //        SqlConnection connection = new SqlConnection(connect);
        //    }
        //    catch (SqlException e)
        //    {

        //        ErrorController ThreatController = new ErrorController(e);
        //        ThreatController.Handle();
        //        Answer = (ThreatController.Err.Lev == LogLevels.Levels.Warning);
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    Assert.IsTrue(Answer);

        //}

        // TODO: implement sql database integration test
        //[TestMethod]
        //public void TestSqlExceptionBetweenElevenAndSixteen()
        //{
        //    bool Answer = false;






        //    Assert.IsTrue(Answer);
        //}

        // TODO: implement sql database integration test
        //[TestMethod]
        //public void TestSqlExceptionBetweenSeventeenAndNineteen()
        //{
        //    bool Answer = false;



        //    Assert.IsTrue(Answer);
        //}

        // TODO: implement sql database integration test
        //[TestMethod]
        //public void TestSqlExceptionAboveNineteen()
        //{
        //    bool Answer = false;



        //    Assert.IsTrue(Answer);
        //}

        
        
    }
}
