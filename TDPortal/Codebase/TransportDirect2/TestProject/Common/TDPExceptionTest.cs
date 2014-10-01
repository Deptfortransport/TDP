using TDP.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TDP.TestProject.Common
{
    
    
    /// <summary>
    ///This is a test class for TDPExceptionTest and is intended
    ///to contain all TDPExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPExceptionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for TDPException Constructor
        ///</summary>
        [TestMethod()]
        public void TDPExceptionConstructorTest()
        {
            string message = "Test Exception";
            Exception innerException = new Exception("Inner Exception");
            bool logged = true; 
            TDPExceptionIdentifier id = TDPExceptionIdentifier.CCAgentChecksumRecheckFailed;

            TDPException target = new TDPException(message, innerException, logged, id);

            Assert.AreEqual(message, target.Message);
            Assert.AreEqual(innerException.Message, target.InnerException.Message);
            Assert.AreEqual(logged, target.Logged);
            Assert.AreEqual(id, target.Identifier);
            Assert.AreEqual(null, target.AdditionalInformation);

            Assert.IsTrue(target.ToString().Length > 0);
        }

        /// <summary>
        ///A test for TDPException Constructor
        ///</summary>
        [TestMethod()]
        public void TDPExceptionConstructorTest1()
        {
            string message = "Test Exception";
            bool logged = false; 
            TDPExceptionIdentifier id = TDPExceptionIdentifier.LJSGenLocatonDataLoadFailed; 
            int i = 45;
            object additionalInformation = i; 
            TDPException target = new TDPException(message, logged, id, additionalInformation);
            Assert.AreEqual(id, target.Identifier);
            Assert.AreEqual(logged, target.Logged);
            Assert.IsInstanceOfType(target.AdditionalInformation, typeof(int));
            Assert.AreEqual(45, (int)target.AdditionalInformation);
            Assert.AreEqual(message, target.Message);
            Assert.AreEqual(null, target.InnerException);
        }

        /// <summary>
        ///A test for TDPException Constructor
        ///</summary>
        [TestMethod()]
        public void TDPExceptionConstructorTest2()
        {
            string message = "Test Exception";
            bool logged = false; 
            TDPExceptionIdentifier id = TDPExceptionIdentifier.LSErrorGettingNaPTANLocation;
            TDPException target = new TDPException(message, logged, id);

            Assert.AreEqual(id, target.Identifier);
            Assert.AreEqual(message, target.Message);
            Assert.AreEqual(logged, target.Logged);
            Assert.AreEqual(null, target.AdditionalInformation);
            Assert.AreEqual(null, target.InnerException);
        }

        /// <summary>
        ///A test for TDPException Constructor
        ///</summary>
        [TestMethod()]
        public void TDPExceptionConstructorTest3()
        {
            // All params except additional info
            string message = "Test Exception 1";
            Exception innerException = new Exception("Inner Exception");
            bool logged = true;
            string addInfo = "AdditionalInformation";
            TDPExceptionIdentifier id = TDPExceptionIdentifier.TNSQLHelperStoredProcedureFailure;

            TDPException target = new TDPException(message, innerException, logged, id);
            TDPException actual;
            BinaryFormatter bFormatter = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                bFormatter.Serialize(ms, target);
                ms.Position = 0;

                actual = (TDPException)bFormatter.Deserialize(ms);
            }

            Assert.AreEqual(message, actual.Message);
            Assert.AreEqual(innerException.Message, actual.InnerException.Message);
            Assert.AreEqual(logged, actual.Logged);
            Assert.AreEqual(id, actual.Identifier);
            Assert.AreEqual(null, actual.AdditionalInformation);
            Assert.IsTrue(actual.ToString().Length > 0);

            // Include additional info parameter
            target = new TDPException(message, logged, id, addInfo);

            using (MemoryStream ms = new MemoryStream())
            {
                bFormatter.Serialize(ms, target);
                ms.Position = 0;

                actual = (TDPException)bFormatter.Deserialize(ms);
            }

            Assert.AreEqual(message, actual.Message);
            Assert.AreEqual(null, actual.InnerException);
            Assert.AreEqual(logged, actual.Logged);
            Assert.AreEqual(id, actual.Identifier);
            Assert.AreEqual(addInfo, actual.AdditionalInformation);
            Assert.IsTrue(actual.ToString().Length > 0);

            // Include no parameters
            target = new TDPException();

            using (MemoryStream ms = new MemoryStream())
            {
                bFormatter.Serialize(ms, target);
                ms.Position = 0;

                actual = (TDPException)bFormatter.Deserialize(ms);
            }

            Assert.AreEqual(string.Empty, actual.Message);
            Assert.AreEqual(null, actual.InnerException);
            Assert.AreEqual(false, actual.Logged);
            Assert.AreEqual(TDPExceptionIdentifier.Undefined, actual.Identifier);
            Assert.AreEqual(null, actual.AdditionalInformation);
            Assert.IsTrue(actual.ToString().Length > 0);
        }

        /// <summary>
        ///A test for TDPException Constructor
        ///</summary>
        [TestMethod()]
        public void TDPExceptionConstructorTest4()
        {
            TDPException target = new TDPException();

            Assert.AreEqual(string.Empty, target.Message);
            Assert.AreEqual(false, target.Logged);
            Assert.AreEqual(null, target.InnerException);
            Assert.AreEqual(null, target.AdditionalInformation);
            Assert.AreEqual(TDPExceptionIdentifier.Undefined, target.Identifier);
            Assert.IsTrue(target.ToString().Length > 0);
        }
    }
}
