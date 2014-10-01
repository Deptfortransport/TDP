// *********************************************** 
// NAME             : TDPModeTypeHelperTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 30 Aug 2013
// DESCRIPTION  	: TDPModeTypeHelperTest
// ************************************************
// 
                
using TDP.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.Common
{
    
    
    /// <summary>
    ///This is a test class for TDPModeTypeHelperTest and is intended
    ///to contain all TDPModeTypeHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPModeTypeHelperTest
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
        ///A test for GetTDPModeTypeQS
        ///</summary>
        [TestMethod()]
        public void TDPModeTypeGetTDPModeTypeQSTest()
        {
            TDPModeType expected;
            TDPModeType actual;

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("P");
            expected = TDPModeType.Air;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("B");
            expected = TDPModeType.Bus;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("O");
            expected = TDPModeType.Coach;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("C");
            expected = TDPModeType.Telecabine;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("D");
            expected = TDPModeType.Drt;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("F");
            expected = TDPModeType.Ferry;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("M");
            expected = TDPModeType.Metro;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("R");
            expected = TDPModeType.Rail;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("T");
            expected = TDPModeType.Tram;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("U");
            expected = TDPModeType.Underground;
            Assert.AreEqual(expected, actual);

            actual = TDPModeTypeHelper.GetTDPModeTypeQS("zzz");
            expected = TDPModeType.Unknown;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTDPModeTypeQS
        ///</summary>
        [TestMethod()]
        public void TDPModeTypeGetTDPModeTypeQSTest1()
        {
            string expected;
            string actual;

            expected = "P";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Air);
            Assert.AreEqual(expected, actual);

            expected = "B";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Bus);
            Assert.AreEqual(expected, actual);

            expected = "O";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Coach);
            Assert.AreEqual(expected, actual);

            expected = "C";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Telecabine);
            Assert.AreEqual(expected, actual);

            expected = "D";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Drt);
            Assert.AreEqual(expected, actual);

            expected = "F";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Ferry);
            Assert.AreEqual(expected, actual);

            expected = "M";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Metro);
            Assert.AreEqual(expected, actual);

            expected = "R";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Rail);
            Assert.AreEqual(expected, actual);

            expected = "T";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Tram);
            Assert.AreEqual(expected, actual);

            expected = "U";
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Underground);
            Assert.AreEqual(expected, actual);

            expected = string.Empty;
            actual = TDPModeTypeHelper.GetTDPModeTypeQS(TDPModeType.Unknown);
            Assert.AreEqual(expected, actual);
        }
    }
}
