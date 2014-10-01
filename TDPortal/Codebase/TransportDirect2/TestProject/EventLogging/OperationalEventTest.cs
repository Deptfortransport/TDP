using TDP.Common.EventLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.TestProject.EventLogging.MockObjects;
using TDP.Common.PropertyManager;
using System.Diagnostics;
using TDP.Common;
using System.Collections;

namespace TDP.TestProject.EventLogging
{
    
    
    /// <summary>
    ///This is a test class for OperationalEventTest and is intended
    ///to contain all OperationalEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OperationalEventTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            IPropertyProvider goodProperties = new MockPropertiesGood();
            IEventPublisher[] customPublishers = new IEventPublisher[2];
            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");
            List<string> errors = new  List<string>();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, customPublishers, errors));
            }
            catch (TDPException e)
            {
                Assert.Fail("Initialisation of TDTraceListener failed - " + e.Message);
            }
        }
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

        

        [TestMethod()]
        public void ConstructorAllParams()
        {
            string sessionId = "11";
            string message = "Message";
            ArrayList target = new ArrayList();
            string targetString = "TargetString";
            target.Add("TargetString");

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, message, target, sessionId);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.OverrideLevel == TDPTraceLevelOverride.None);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorAllParams");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == sessionId);
            ArrayList storedTarget = (ArrayList)oe.Target;
            foreach (string targetElement in storedTarget)
                Assert.IsTrue(targetElement == targetString);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

            // Now reinitialise using the User override level. The only difference is that the 
            // Filter.ShouldLog method should return True
            oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, message, target, sessionId, TDPTraceLevelOverride.User);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.OverrideLevel == TDPTraceLevelOverride.User);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorAllParams");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == sessionId);
            storedTarget = (ArrayList)oe.Target;
            foreach (string targetElement in storedTarget)
                Assert.IsTrue(targetElement == targetString);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");

            // Force time update for coverage
            oe.Time = DateTime.MinValue;
            Assert.IsTrue(oe.Time == DateTime.MinValue);
        }

        [TestMethod()]
        public void ConstructorWithoutSessionId()
        {
            string message = "Message";
            ArrayList target = new ArrayList();
            string targetString = "TargetString";
            target.Add("TargetString");

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, message, target);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorWithoutSessionId");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
            ArrayList storedTarget = (ArrayList)oe.Target;
            foreach (string targetElement in storedTarget)
                Assert.IsTrue(targetElement == targetString);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

            // Now reinitialise using the User override level. The only difference is that the 
            // Filter.ShouldLog method should return True
            oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, message, target, TDPTraceLevelOverride.User);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorWithoutSessionId");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
            storedTarget = (ArrayList)oe.Target;
            foreach (string targetElement in storedTarget)
                Assert.IsTrue(targetElement == targetString);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");
        }

        [TestMethod()]
        public void ConstructorWithoutTarget()
        {
            string sessionId = "11";
            string message = "Message";

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionId, TDPTraceLevel.Verbose, message);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorWithoutTarget");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == sessionId);
            Assert.IsTrue(oe.Target == null);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

            // Now reinitialise using the User override level. The only difference is that the 
            // Filter.ShouldLog method should return True
            oe = new OperationalEvent(TDPEventCategory.Business, sessionId, TDPTraceLevel.Verbose, message, TDPTraceLevelOverride.User);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorWithoutTarget");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == sessionId);
            Assert.IsTrue(oe.Target == null);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");
        }


        [TestMethod()]
        public void ConstructorMinParams()
        {
            string message = "Message";

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, message);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorMinParams");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
            Assert.IsTrue(oe.Target == null);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

            // Now reinitialise using the User override level. The only difference is that the 
            // Filter.ShouldLog method should return True
            oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, message, TDPTraceLevelOverride.User);

            Assert.IsTrue(oe.AssemblyName == "tdp.testproject");
            Assert.IsTrue(oe.AuditPublishersOff == true);
            Assert.IsTrue(oe.Category == TDPEventCategory.Business);
            Assert.IsTrue(oe.ConsoleFormatter != null);
            Assert.IsTrue(oe.EmailFormatter != null);
            Assert.IsTrue(oe.EventLogFormatter != null);
            Assert.IsTrue(oe.FileFormatter != null);
            Assert.IsTrue(oe.Filter != null);
            Assert.IsTrue(oe.Level == TDPTraceLevel.Verbose);
            Assert.IsTrue(oe.MachineName == Environment.MachineName);
            Assert.IsTrue(oe.Message == message);
            Assert.IsTrue(oe.MethodName == "ConstructorMinParams");
            Assert.IsTrue(oe.PublishedBy == String.Empty);
            Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
            Assert.IsTrue(oe.Target == null);
            Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
            Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");

        }
    }
}
