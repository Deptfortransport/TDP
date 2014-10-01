// *********************************************** 
// NAME             : ThemeProviderTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 30 Aug 2013
// DESCRIPTION  	: ThemeProviderTest
// ************************************************
// 

using TDP.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.Common
{
    
    
    /// <summary>
    ///This is a test class for ThemeProviderTest and is intended
    ///to contain all ThemeProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ThemeProviderTest
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
        ///A test for ThemeProvider Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.dll")]
        public void ThemeProviderConstructorTest()
        {
            ThemeProvider_Accessor target = new ThemeProvider_Accessor();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetDefaultThemeId
        ///</summary>
        [TestMethod()]
        public void ThemeProviderGetDefaultThemeIdTest()
        {
            ThemeProvider_Accessor target = new ThemeProvider_Accessor();
            int expected = 1;
            int actual;
            actual = target.GetDefaultThemeId();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDefaultThemeName
        ///</summary>
        [TestMethod()]
        public void ThemeProviderGetDefaultThemeNameTest()
        {
            ThemeProvider_Accessor target = new ThemeProvider_Accessor();
            string expected = "TransportDirect";
            string actual;
            actual = target.GetDefaultThemeName();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Instance
        ///</summary>
        [TestMethod()]
        public void ThemeProviderInstanceTest()
        {
            ThemeProvider actual;
            actual = ThemeProvider.Instance;
            Assert.IsNotNull(actual);
        }
    }
}
