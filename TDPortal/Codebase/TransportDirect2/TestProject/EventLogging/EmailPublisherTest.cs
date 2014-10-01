// *********************************************** 
// NAME             : EmailPublisherTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 19 Sep 2013
// DESCRIPTION  	: EmailPublisher test
// ************************************************
// 

using TDP.Common.EventLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Mail;

namespace TDP.TestProject.EventLogging
{
    
    
    /// <summary>
    ///This is a test class for EmailPublisherTest and is intended
    ///to contain all EmailPublisherTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EmailPublisherTest
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
        ///A test for EmailPublisher Constructor
        ///</summary>
        [TestMethod()]
        public void EmailPublisherConstructorTest()
        {
            // Valid email publisher
            string identifier = "EMAIL";
            string from = "from@transportdirect.info";
            string to = "to@transportdirect.info";
            string subject = "subject";
            MailPriority priority = MailPriority.Normal;
            string smtpServer = "localhost";
            
            EmailPublisher target = new EmailPublisher(identifier, to, from, subject, priority, smtpServer);

            Assert.IsTrue(target.Identifier == identifier);
        }

        /// <summary>
        ///A test for WriteEvent
        ///</summary>
        [TestMethod()]
        public void EmailPublisherWriteEventTest()
        {
            // Valid email publisher
            string identifier = "EMAIL";
            string from = "from@transportdirect.info";
            string to = "to@transportdirect.info";
            string subject = "subject";
            MailPriority priority = MailPriority.Normal;
            string smtpServer = "localhost";

            EmailPublisher target = new EmailPublisher(identifier, to, from, subject, priority, smtpServer);
            
            // Valid email
            OperationalEvent emailEvent = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "Event to email");
            
            try
            {
                target.WriteEvent(emailEvent);
            }
            catch
            {
                Assert.Inconclusive("Email smtp server may not be available on machine.");
            }

            // Invalid email publisher
            target = new EmailPublisher(identifier, to, from, subject, priority, "DOESNOTEXIST");

            try
            {
                target.WriteEvent(emailEvent);

                Assert.Fail("Expected exception");
            }
            catch
            {
                // Exception expected
            }
        }
    }
}
