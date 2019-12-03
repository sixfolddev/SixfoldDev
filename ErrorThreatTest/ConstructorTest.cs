using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ErrorHandling;
using System;

namespace ErrorHandlingTest
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
            var Err = new ErrorResponseManager(new Exception());
            Assert.IsInstanceOfType(Err, typeof(ErrorResponseManager));
        }

        [TestMethod]
        public void FatalResponseServiceConstructorTest()
        {
            var Err = new FatalResponseService(new Exception());
            Assert.IsInstanceOfType(Err, typeof(FatalResponseService));
        }

    }
}
