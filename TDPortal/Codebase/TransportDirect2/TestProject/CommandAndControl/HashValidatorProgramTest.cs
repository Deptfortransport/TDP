﻿using TDP.Common.HashValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TDP.TestProject.CommandAndControl
{
    
    
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HashValidatorProgramTest
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
        ///A test for Main
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HashValidator.exe")]
        public void HashGeneratorAndValidatorMainTest()
        {
            // In order for there to be a hash to validate the HashGeneratorProgramTest has to run first
            HashGeneratorProgramTest setup = new HashGeneratorProgramTest();
            setup.HashGeneratorMainTest();
            
            int expected; 
            int actual;

            // Test no args supplied (receive help back)
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                expected = 2;

                string[] args = new string[0];
                actual = Program_Accessor.Main(args);
                Assert.AreEqual(expected, actual, "Unexpected response code");
                Assert.IsTrue(sw.ToString().Contains("Error. Invalid number of parameters provided:"), "Didn't receive expected error when supplying no paramteres");
            }

            // Test /help args supplied (receive help back)
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                expected = 2;

                string[] args = new string[1] { "/help" };
                Program_Accessor.Main(args);
                actual = Program_Accessor.Main(args);
                Assert.AreEqual(expected, actual, "Unexpected response code");
                Assert.IsTrue(sw.ToString().Contains("Usage:"), "Didn't receive help back when using /help parameter");
            }

            // Test bad algorithm arg supplied (receive help back)
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                expected = 2;

                string[] args = new string[1] { "/algorithm" };
                actual = Program_Accessor.Main(args);
                Assert.AreEqual(expected, actual, "Unexpected response code");
                Assert.IsTrue(sw.ToString().Contains("Usage:"), "Didn't receive help back when using bad /algorithm parameter");
            }

            // Test unexpected arg supplied (receive help back)
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                expected = 2;

                string[] args = new string[1] { "/doh:test" };
                actual = Program_Accessor.Main(args);
                Assert.AreEqual(expected, actual, "Unexpected response code");
                Assert.IsTrue(sw.ToString().Contains("Usage:"), "Didn't receive help back when using unexpected parameter");
            }

            // Test good algorithm arg supplied followed by text arg (receive help back)
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                expected = 2;

                string[] args = new string[2] { "/algorithm:rsa", "text" };
                actual = Program_Accessor.Main(args);
                Assert.AreEqual(expected, actual, "Unexpected response code");
                Assert.IsTrue(sw.ToString().Contains("Usage:"), "Didn't receive help back when using random text parameter");
            }

            // Test root directory arg and good algorithm arg supplied - unfortunately this will
            // only return the error code (as the hash file is missing) 
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string[] args = new string[2] { @"D:\TDPortal\CodeBase\TDP\Initialisation", "/algorithm|rsa" };
                actual = Program_Accessor.Main(args);

                //// Expected return if HashValidator test has not run first
                //expected = 2; // if running in debug you may get a different result
                //Assert.AreEqual(expected, actual, "Unexpected response code - failure expected.");
                //Assert.IsTrue(sw.ToString().Contains("Error - unable to verify files"), "Didn't receive message saying unable to verify files - full output from program - {0}", new string[1] { sw.ToString() });

                // Expected return if HashValidator test has run first
                expected = 0; // if running in debug you may get a different result
                Assert.AreEqual(expected, actual, "Unexpected response code - success expected.");
                Assert.IsTrue(sw.ToString().Contains("Hashes Match"), "Didn't receive message saying hashes match - full output from program - {0}", new string[1] { sw.ToString() });
            }
        }

        /// <summary>
        ///A test for displayHelp
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HashValidator.exe")]
        public void HashValidatorDisplayHelpTest()
        {
            Program_Accessor target = new Program_Accessor();
            try
            {
                // Test root directory arg and good algorithm arg supplied 
                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    target.displayHelp();

                    Assert.IsTrue(sw.ToString().Contains("Usage:"), "Didn't output help content");
                }
                Assert.IsTrue(true, "Help displayed without error");
            }
            catch (Exception ex)
            {
                Assert.Fail("Display help errored: {0}", new string[] { ex.ToString() });
            }
        }
    }
}
