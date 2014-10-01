using TDP.UserPortal.SessionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common;
using System.Collections.Generic;
using TDP.Common.LocationService;

namespace TDP.TestProject.SessionManager
{
    
    
    /// <summary>
    ///This is a test class for InputPageStateTest and is intended
    ///to contain all InputPageStateTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InputPageStateTest
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
        ///A test for InputPageState Constructor
        ///</summary>
        [TestMethod()]
        public void InputPageStateConstructorTest()
        {
            InputPageState target = new InputPageState();
            Assert.IsNotNull(target, "Null object returned");
        }

        /// <summary>
        ///A test for AddMessage
        ///</summary>
        [TestMethod()]
        public void AddMessageTest()
        {
            InputPageState target = new InputPageState(); 
            TDPMessage message = new TDPMessage("ResourceId", TDPMessageType.Info);
            target.AddMessage(message);
            List<TDPMessage> messages = target.Messages;
            Assert.IsTrue(messages.Contains(message), "Message not stored");

            // Add multiple messages test
            message = new TDPMessage("ResourceId2", TDPMessageType.Info);
            target.AddMessage(message);

            // Add message again with same resource id
            target.AddMessage(message);
            messages = target.Messages;
            Assert.IsTrue(messages.Count == 2);
        }

        /// <summary>
        ///A test for AddMessages
        ///</summary>
        [TestMethod()]
        public void AddMessagesTest()
        {
            InputPageState target = new InputPageState(); 
            List<TDPMessage> messagesToAdd = new List<TDPMessage>();
            TDPMessage message = new TDPMessage("ResourceId", TDPMessageType.Info);
            messagesToAdd.Add(message);
            target.ClearMessages();
            target.AddMessages(messagesToAdd);
            Assert.IsTrue(target.Messages.Contains(message), "Messages not stored");
        }

        /// <summary>
        ///A test for ClearMessages
        ///</summary>
        [TestMethod()]
        public void ClearMessagesTest()
        {
            InputPageState target = new InputPageState(); 
            List<TDPMessage> messagesToAdd = new List<TDPMessage>();
            TDPMessage message = new TDPMessage("ResourceId", TDPMessageType.Info);
            messagesToAdd.Add(message);
            target.AddMessages(messagesToAdd);
            target.ClearMessages();
            Assert.IsTrue(!target.Messages.Contains(message), "Messages not cleared");
        }

        /// <summary>
        ///A test for IsDirty
        ///</summary>
        [TestMethod()]
        public void IsDirtyTest()
        {
            InputPageState target = new InputPageState();
            bool expected = true; 
            bool actual;
            target.IsDirty = expected;
            actual = target.IsDirty;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for JourneyIdOutward
        ///</summary>
        [TestMethod()]
        public void JourneyIdOutwardTest()
        {
            InputPageState target = new InputPageState(); 
            int expected = 7; 
            int actual;
            target.JourneyIdOutward = expected;
            actual = target.JourneyIdOutward;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for JourneyIdReturn
        ///</summary>
        [TestMethod()]
        public void JourneyIdReturnTest()
        {
            InputPageState target = new InputPageState(); 
            int expected = 3;
            int actual;
            target.JourneyIdReturn = expected;
            actual = target.JourneyIdReturn;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Journey Search object
        ///</summary>
        [TestMethod()]
        public void JourneySearchObjectTest()
        {
            InputPageState target = new InputPageState();

            LocationSearch search = new LocationSearch();
            target.OriginSearch = search;
            target.DestinationSearch = search;

            Assert.IsNotNull(target.OriginSearch);
            Assert.IsNotNull(target.DestinationSearch);
        }

        /// <summary>
        ///A test for JourneyLegDetailExpandedOutward
        ///</summary>
        [TestMethod()]
        public void JourneyLegDetailExpandedOutwardTest()
        {
            InputPageState target = new InputPageState(); 
            bool expected = true; 
            bool actual;
            target.JourneyLegDetailExpandedOutward = expected;
            actual = target.JourneyLegDetailExpandedOutward;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for JourneyLegDetailExpandedReturn
        ///</summary>
        [TestMethod()]
        public void JourneyLegDetailExpandedReturnTest()
        {
            InputPageState target = new InputPageState();
            bool expected = true; 
            bool actual;
            target.JourneyLegDetailExpandedReturn = expected;
            actual = target.JourneyLegDetailExpandedReturn;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for JourneyRequestHash
        ///</summary>
        [TestMethod()]
        public void JourneyRequestHashTest()
        {
            InputPageState target = new InputPageState(); 
            string expected = "hash";
            string actual;
            target.JourneyRequestHash = expected;
            actual = target.JourneyRequestHash;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Messages
        ///</summary>
        [TestMethod()]
        public void MessagesTest()
        {
            InputPageState target = new InputPageState(); 
            List<TDPMessage> expected = new List<TDPMessage>();
            expected.Add(new TDPMessage("SessionId", TDPMessageType.Info));
            List<TDPMessage> actual;
            target.Messages = expected;
            actual = target.Messages;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Show links
        ///</summary>
        [TestMethod()]
        public void ShowLinkTest()
        {
            InputPageState target = new InputPageState();

            target.ShowEarlierLinkOutwardRiver = true;
            target.ShowEarlierLinkReturnRiver = true;
            target.ShowLaterLinkOutwardRiver = true;
            target.ShowLaterLinkReturnRiver = true;

            Assert.IsTrue(target.ShowEarlierLinkOutwardRiver);
            Assert.IsTrue(target.ShowEarlierLinkReturnRiver);
            Assert.IsTrue(target.ShowLaterLinkOutwardRiver);
            Assert.IsTrue(target.ShowLaterLinkReturnRiver);
        }

        /// <summary>
        ///A test for StopEventJourneyIdOutward
        ///</summary>
        [TestMethod()]
        public void StopEventJourneyIdOutwardTest()
        {
            InputPageState target = new InputPageState();
            int expected = 8; 
            int actual;
            target.StopEventJourneyIdOutward = expected;
            actual = target.StopEventJourneyIdOutward;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StopEventJourneyIdReturn
        ///</summary>
        [TestMethod()]
        public void StopEventJourneyIdReturnTest()
        {
            InputPageState target = new InputPageState(); 
            int expected = 4; 
            int actual;
            target.StopEventJourneyIdReturn = expected;
            actual = target.StopEventJourneyIdReturn;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StopEventRequestHash
        ///</summary>
        [TestMethod()]
        public void StopEventRequestHashTest()
        {
            InputPageState target = new InputPageState(); 
            string expected = "hash2";
            string actual;
            target.StopEventRequestHash = expected;
            actual = target.StopEventRequestHash;
            Assert.AreEqual(expected, actual);
        }
    }
}
