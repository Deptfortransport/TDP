using TDP.Common.EventLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.PropertyManager;
using System.Collections.Generic;
using System.Diagnostics;
using TDP.TestProject.EventLogging.MockObjects;
using System.IO;
using System.Collections;
using TDP.Common;

namespace TDP.TestProject.EventLogging
{
    
    
    /// <summary>
    ///This is a test class for TDPTraceListenerTest and is intended
    ///to contain all TDPTraceListenerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPTraceListenerTest
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
        [ClassInitialize()]
        public static void SetUp(TestContext testContext)
        {
            Trace.Listeners.Remove("TDPTraceListener");

            // delete file publisher dirs
            IPropertyProvider MockPropertiesGood = new MockPropertiesGood();

            DirectoryInfo di1 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File1")]);
            di1.Delete(true);

            DirectoryInfo di2 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File2")]);
            di2.Delete(true);
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void Cleanup()
        {
            Trace.Listeners.Remove("TDPTraceListener");

            // delete file publisher dirs
            IPropertyProvider MockPropertiesGood = new MockPropertiesGood();

            DirectoryInfo di1 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File1")]);
            di1.Delete(true);

            DirectoryInfo di2 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File2")]);
            di2.Delete(true);

        }
        
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Trace.Listeners.Remove("TDPTraceListener");
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void TDPTraceListenerUnsupportedPrototypes()
        {
            Cleanup();

            IPropertyProvider MockPropertiesGoodMinimumProperties = new MockPropertiesGoodMinimumProperties();
            IEventPublisher[] customPublishers = new IEventPublisher[0];
            List<string> errors = new List<string>();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(MockPropertiesGoodMinimumProperties, customPublishers, errors));
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            Assert.IsTrue(errors.Count == 0);

            // call each unsupported prototype - no exceptions should result
            try
            {
                Exception testObject = new Exception("test object");
                Trace.Write("My call to Write(string)");
                Trace.Write((object)testObject, "My call to Write(object,string)");
                Trace.Write("My message for Write(string,string)", "My category for Write(string, string)");
                Trace.WriteLine("My call to WriteLine(string)");
                Trace.WriteLine((object)testObject, "My call to WriteLine(object,string)");
                Trace.WriteLine("My message for WriteLine(string,string)", "My category for WriteLine(string, string)");
                Trace.WriteLine((object)testObject);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }


            DirectoryInfo publisherDir = new DirectoryInfo(MockPropertiesGoodMinimumProperties[String.Format(Keys.FilePublisherDirectory, "File1")] + "\\");
            FileInfo[] fileInfoArray = publisherDir.GetFiles("*.txt");

            Assert.IsTrue(fileInfoArray.Length > 0);

            FileInfo tempFile = fileInfoArray[0];
            using (FileStream fileStream = tempFile.OpenRead())
            {
                StreamReader streamReader = new StreamReader(fileStream);

                int count = 0;
                while (streamReader.ReadLine() != null)
                    count++;

                
                Assert.IsTrue(count == 7); // one per prototype call
            }

        }


        [TestMethod()]
        public void TDPTraceListenerEmptyProperties()
        {
            bool exceptionThrown = false;
            IEventPublisher[] customPublishers = new IEventPublisher[0];
            List<string> errors = new List<string>();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(new MockPropertiesEmpty(), customPublishers, errors));
            }
            catch (TDPException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                Assert.IsTrue(false);

            // display the errors for visual comfort factor
            foreach (string error in errors)
            {
                string message = "NUNIT:TestTDPTraceListener. " + error;
                Console.WriteLine(message);
            }

            Assert.IsTrue(errors.Count == 6);
        }

        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void TDPTraceListenerEmptyValueProperties()
        {
            IEventPublisher[] customPublishers = new IEventPublisher[0];
            List<string> errors = new List<string>();

            
            Trace.Listeners.Add(new TDPTraceListener(new MockPropertiesEmptyValues(), customPublishers, errors));
           
            Assert.IsTrue(errors.Count > 0);

        }

        [TestMethod()]
        public void TDPTraceListenerMinimumProperties()
        {
            IPropertyProvider MockPropertiesGoodMinimumProperties = new MockPropertiesGoodMinimumProperties();
            IEventPublisher[] customPublishers = new IEventPublisher[0];
            List<string> errors = new List<string>();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(MockPropertiesGoodMinimumProperties,
                                                        customPublishers, errors));
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod()]
        public void TDPTraceListenerAllGood()
        {
            // NB. In proper use, an instance of Properties would be
            // retrieved from the Service Discovery (? or other means)
            // and then IPropertyProvider goodProperties = Properties.Current;

            IPropertyProvider goodProperties = new MockPropertiesGood();

            IEventPublisher[] customPublishers = new IEventPublisher[2];

            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");

            List<string> errors = new List<string>();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties,
                                                        customPublishers,
                                                        errors));
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod()]
        public void TDPTraceListenerPublisherNotPassed()
        {
            bool exceptionThrown = false;

            IPropertyProvider goodProperties = new MockPropertiesGood();
            List<string> errors = new List<string>();
            IEventPublisher[] customPublishers = new IEventPublisher[1];

            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            // do not pass a TDPublisher2 even though it is defined in goodProperties

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, customPublishers, errors));
            }
            catch (TDPException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                Assert.IsTrue(false);

            // display the errors for visual comfort factor
            foreach (string error in errors)
            {
                string message = "NUNIT:TestTDPTraceListener. " + error;
                Console.WriteLine(message);
            }

            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod()]
        public void TDPTraceListenerUnknownCustomPublisherClassPassed()
        {
            bool exceptionThrown = false;
            List<string> errors = new List<string>();
            IEventPublisher[] customPublishers = new IEventPublisher[3];

            IPropertyProvider goodProperties = new MockPropertiesGood();

            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");
            // pass in a custom publisher that does not appear in good properties
            customPublishers[2] = new TDPPublisher3("CustomPublisher3");

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, customPublishers, errors));
            }
            catch (TDPException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                Assert.IsTrue(false);

            Assert.IsTrue(errors.Count == 1);

            // display the errors for visual comfort factor
            foreach (string error in errors)
            {
                string message = "NUNIT:TestTDPTraceListener. " + error;
                Console.WriteLine(message);
            }

        }

        [TestMethod()]
        public void TDPTraceListenerUnknownCustomPublisherIDPassed()
        {
            bool exceptionThrown = false;
            List<string> errors = new List<string>();
            IEventPublisher[] customPublishers = new IEventPublisher[2];

            IPropertyProvider goodProperties = new MockPropertiesGood();

            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            // intialise with a different ID to that specified in good properties
            customPublishers[1] = new TDPPublisher2("DifferentIDToProperties");

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, customPublishers, errors));
            }
            catch (TDPException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                Assert.IsTrue(false);

            Assert.IsTrue(errors.Count == 1);

            // display the errors for visual comfort factor
            foreach (string error in errors)
            {
                string message = "NUNIT:TestTDPTraceListener. " + error;
                Console.WriteLine(message);
            }

        }


        [TestMethod()]
        public void TDPTraceListenerUnknownCustomPublisherClassKnownIDPassed()
        {
            bool exceptionThrown = false;
            List<string> errors = new List<string>();
            IEventPublisher[] customPublishers = new IEventPublisher[2];

            IPropertyProvider goodProperties = new MockPropertiesGood();

            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            // provide unknown class with an id that IS specified in good properties
            customPublishers[1] = new TDPPublisher3("CustomPublisher2");

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, customPublishers, errors));
            }
            catch (TDPException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                Assert.IsTrue(false);

            Assert.IsTrue(errors.Count == 1);

            // display the errors for visual comfort factor
            foreach (string error in errors)
            {
                string message = "NUNIT:TestTDPTraceListener. " + error;
                Console.WriteLine(message);
            }

        }

        [TestMethod()]
        public void TDPTraceListenerNoCustomPublishersPassed()
        {
            bool exceptionThrown = false;
            List<string> errors = new List<string>();
            IPropertyProvider goodProperties = new MockPropertiesGood();
            IEventPublisher[] customPublishers = new IEventPublisher[0];

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, customPublishers, errors));
            }
            catch (TDPException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                Assert.IsTrue(false);

            Assert.IsTrue(errors.Count == 2);

            // display the errors for visual comfort factor
            foreach (string error in errors)
            {
                string message = "NUNIT:TestTDPTraceListener. " + error;
                Console.WriteLine(message);
            }

        }

        [TestMethod()]
        public void TDPTraceListenerNullArrayCustomPublishersPassed()
        {
            bool exceptionThrown = false;
            List<string> errors = new List<string>();
            IPropertyProvider goodProperties = new MockPropertiesGood();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, null, errors));
            }
            catch (TDPException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                Assert.IsTrue(false);

            Assert.IsTrue(errors.Count == 1);

            // display the errors for visual comfort factor
            foreach (string error in errors)
            {
                string message = "NUNIT:TestTDPTraceListener. " + error;
                Console.WriteLine(message);
            }

        }


        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void TDPTraceListenerBadCustomEvents()
        {
            List<string> errors = new List<string>();
            IPropertyProvider badCustomEvents = new MockPropertiesGoodPublishersBadEvents();
            IEventPublisher[] customPublishers = new IEventPublisher[2];
            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");

            
            Trace.Listeners.Add(new TDPTraceListener(badCustomEvents, customPublishers, errors));
            Assert.IsTrue(errors.Count>0);


            
        }


        [TestMethod()]
        public void TDPTraceListenerEventWriting()
        {
            MockPropertiesGood goodProperties = new MockPropertiesGood();

            Trace.Listeners.Clear();

            IEventPublisher[] customPublishers = new IEventPublisher[2];

            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");

            List<string> errors = new List<string>();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties,
                    customPublishers,
                    errors));
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            OperationalEvent oe1 = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "Message", "11");
            oe1.AuditPublishersOff = false; // so we can perform test below
            Trace.Write(oe1);
            Assert.AreEqual(oe1.PublishedBy.Trim(),"TDP.Common.EventLogging.QueuePublisher TDP.Common.EventLogging.EventLogPublisher");

           
            // test operational event level switch works -  following event should not be published because level is Error
            OperationalEvent oe2 = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, "Message", "11");
            oe2.AuditPublishersOff = false; // so we can perform test below
            Trace.Write(oe2);
            Assert.IsTrue(oe2.PublishedBy == "");
        }


        [TestMethod()]
        public void TDPTraceListenerParallelUsage()
        {
            MockPropertiesGood goodProperties = new MockPropertiesGood();

            IEventPublisher[] customPublishers = new IEventPublisher[2];

            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");

            List<string> errors = new List<string>();

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties,
                                    customPublishers,
                                    errors));
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            bool badCall = false;

            try
            {
                // call Write overload that is not implemented in TDPTraceListener
                string test = "TestTDPTraceListener.TDPTraceListenerParallelUsage. Test message - called using Write overload that is not implemented in TDPTraceListener, but is implemented in default listener.\n";
                Trace.Write(test);
            }
            catch (TDPException)
            {
                badCall = true;
            }

            Assert.IsTrue(!badCall);

            badCall = false;

            try
            {
                // call WriteLine - not implemented in TDPTraceListener
                string test = "TestTDPTraceListener.TDPTraceListenerParallelUsage. Test Message - called using WriteLine - this is not implemented in TDPTraceListener, but is implemented in default listener.";
                Trace.WriteLine(test);
            }
            catch (TDPException)
            {
                badCall = true;
            }

            Assert.IsTrue(!badCall);

            // ensure that TDPTraceListener does not effect any Debug calls
            try
            {
                string test = "TestTDPTraceListener.TDPTraceListenerParallelUsage. Test message (using Debug, with TDPTraceListener registered).";
                Debug.WriteLine(test);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }


            // remove TDPTraceListener - above calls should now work without exception being throw
            Trace.Listeners.Remove("TDPTraceListener");

            badCall = false;

            try
            {
                // call Write overload that is not implemented in TDPTraceListener
                string test = "TestTDPTraceListener.TDPTraceListenerParallelUsage. Test message (TDPTraceListener not registered).";
                Trace.Write(test);
            }
            catch (TDPException)
            {
                badCall = true;
            }

            Assert.IsTrue(!badCall);
        }

        /// <summary>
        /// Test handling when an unknown object is logged.
        /// </summary>
        [TestMethod()]
        public void TDPTraceListenerUnknownObject()
        {
            // set up TDPTraceListener with valid properties
            IPropertyProvider goodProperties = new MockPropertiesGood();
            IEventPublisher[] customPublishers = new IEventPublisher[2];
            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");
            List<string> errors = new List<string>();
            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties,
                                    customPublishers,
                                    errors));
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            bool badEventFound = false;
            try
            {
                // try logging TDPEvent3 - this does not derive from CustomEvent
                Trace.Write(new TDPEvent3());
            }
            catch (TDPException)
            {
                badEventFound = true;
            }

            Assert.IsTrue(badEventFound);
        }

        /// <summary>
        /// Test handling when a custom event is logged, which is unknown to the TDPTraceListener
        /// </summary>
        [TestMethod()]
        public void TDPTraceListenerUnknownCustomEvent()
        {
            // set up TDPTraceListener with valid properties
            IPropertyProvider goodProperties = new MockPropertiesGood();
            IEventPublisher[] customPublishers = new IEventPublisher[2];
            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");
            List<string> errors = new List<string>();
            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties,
                    customPublishers,
                    errors));
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            // attempt to log an event that the TDPTraceListener knows nothing about:
            bool badEventFound = false;
            try
            {
                // try logging TDPEvent4 - this derives from CustomEvent but is
                // not known by TDPTraceListener
                Trace.Write(new TDPEvent4());
            }
            catch (TDPException)
            {
                badEventFound = true;
            }

            Assert.IsTrue(badEventFound);
        }

       
    }
}
