using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ErrorHandling;
using System;

namespace RoomAid.ErrorHandlingTest
{
    [TestClass]
    public class ConstructorTest
    {
        [TestMethod]
        public void AnalyzedErrorConstructorTest()
        {
            var Err = new AnalyzedError(new Exception());
            Assert.IsInstanceOfType(Err, typeof(AnalyzedError));
        }

        [TestMethod]
        public void ErrorControllerConstructorTest()
        {
            var Err = new ErrorController(new Exception());
            Assert.IsInstanceOfType(Err, typeof(ErrorController));
        }

        [TestMethod]
        public void ErrorResponseManagerConstructorTest()
        {
            var Err = new ErrorResponseManager();
            Assert.IsInstanceOfType(Err, typeof(ErrorResponseManager));
        }

        [TestMethod]
        public void FatalResponseServiceConstructorTest()
        {
            var Err = new FatalResponseService(new AnalyzedError(new Exception()));
            Assert.IsInstanceOfType(Err, typeof(FatalResponseService));
        }
        [TestMethod]
        public void ErrorResponseServiceConstructorTest()
        {
            var Err = new ErrorResponseService(new AnalyzedError(new Exception()));
            Assert.IsInstanceOfType(Err, typeof(ErrorResponseService));
        }
        [TestMethod]
        public void WarningResponseServiceConstructorTest()
        {
            var Err = new WarningResponseService(new AnalyzedError(new Exception()));
            Assert.IsInstanceOfType(Err, typeof(WarningResponseService));
        }

        [TestMethod]
        public void ErrorThreatManagerConstructorTest()
        {
            var Err = new ErrorThreatManager(new Exception());
            Assert.IsInstanceOfType(Err, typeof(ErrorThreatManager));
        }

        [TestMethod]
        public void ErrorThreatServiceConstructorTest()
        {
            var Err = new ErrorThreatService();
            Assert.IsInstanceOfType(Err, typeof(ErrorThreatService));
        }

    }
}
