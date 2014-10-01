using TDP.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TDP.TestProject.Common
{
    
    
    /// <summary>
    ///This is a test class for TDPMessageTest and is intended
    ///to contain all TDPMessageTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPMessageTest
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
        ///A test for TDPMessage Constructor
        ///</summary>
        [TestMethod()]
        public void TDPMessageConstructorTest()
        {
            string messageText = "The message";
            string messageResourceId = "MessageResourceId";
            int majorNumber = 6;
            int minorNumber = 3;
            TDPMessageType type = TDPMessageType.Error;
            TDPMessage target = new TDPMessage(messageText, messageResourceId, majorNumber, minorNumber, type);

            Assert.AreEqual(messageText, target.MessageText, "Unexpected message text");
            Assert.AreEqual(messageResourceId, target.MessageResourceId, "Unexpected message resource id");
            Assert.AreEqual(majorNumber, target.MajorMessageNumber, "Unexpected major number");
            Assert.AreEqual(minorNumber, target.MinorMessageNumber, "Unexpected minor number");
            Assert.AreEqual(type, target.Type, "Unexpected message type");
            Assert.AreEqual(string.Empty, target.MessageResourceCollection, "Unexpected message resource collection");
            Assert.AreEqual(string.Empty, target.MessageResourceGroup, "Unexpected message resource group");
            Assert.AreEqual(0, target.MessageArgs.Count, "Unexpected number of message args");
        }

        /// <summary>
        ///A test for TDPMessage Constructor
        ///</summary>
        [TestMethod()]
        public void TDPMessageConstructorTest1()
        {
            string messageText = "The message";
            string messageResourceId = "MessageResourceId";
            int majorNumber = 6;
            int minorNumber = 3;
            TDPMessageType type = TDPMessageType.Warning;
            string messageResourceCollection = "MessageResourceCollection";
            string messageResourceGroup = "MessageResourceGroup";
            List<string> messageArgs = new List<string>(){"ArgOne", "ArgTwo"};
            TDPMessage target = new TDPMessage(messageText, messageResourceId, messageResourceCollection, messageResourceGroup, messageArgs, majorNumber, minorNumber, type);

            Assert.AreEqual(messageText, target.MessageText, "Unexpected message text");
            Assert.AreEqual(messageResourceId, target.MessageResourceId, "Unexpected message resource id");
            Assert.AreEqual(majorNumber, target.MajorMessageNumber, "Unexpected major number");
            Assert.AreEqual(minorNumber, target.MinorMessageNumber, "Unexpected minor number");
            Assert.AreEqual(type, target.Type, "Unexpected message type");
            Assert.AreEqual(messageResourceCollection, target.MessageResourceCollection, "Unexpected message resource collection");
            Assert.AreEqual(messageResourceGroup, target.MessageResourceGroup, "Unexpected message resource group");
            Assert.AreEqual(messageArgs, target.MessageArgs, "Unexpected message args");
        }

        /// <summary>
        ///A test for TDPMessage Constructor
        ///</summary>
        [TestMethod()]
        public void TDPMessageConstructorTest2()
        {
            TDPMessage target = new TDPMessage();

            Assert.AreEqual(string.Empty, target.MessageText, "Unexpected message text");
            Assert.AreEqual(string.Empty, target.MessageResourceId, "Unexpected message resource id");
            Assert.AreEqual(0, target.MajorMessageNumber, "Unexpected major number");
            Assert.AreEqual(0, target.MinorMessageNumber, "Unexpected minor number");
            Assert.AreEqual(TDPMessageType.Info, target.Type, "Unexpected message type");
            Assert.AreEqual(string.Empty, target.MessageResourceCollection, "Unexpected message resource collection");
            Assert.AreEqual(string.Empty, target.MessageResourceGroup, "Unexpected message resource group");
            Assert.AreEqual(0, target.MessageArgs.Count, "Unexpected number of message args");
        }

        /// <summary>
        ///A test for TDPMessage Constructor
        ///</summary>
        [TestMethod()]
        public void TDPMessageConstructorTest3()
        {
            string messageResourceId = "MessageResourceId";
            TDPMessageType type = TDPMessageType.Error;
            TDPMessage target = new TDPMessage(messageResourceId, type);

            Assert.AreEqual(string.Empty, target.MessageText, "Unexpected message text");
            Assert.AreEqual(messageResourceId, target.MessageResourceId, "Unexpected message resource id");
            Assert.AreEqual(0, target.MajorMessageNumber, "Unexpected major number");
            Assert.AreEqual(0, target.MinorMessageNumber, "Unexpected minor number");
            Assert.AreEqual(type, target.Type, "Unexpected message type");
            Assert.AreEqual(string.Empty, target.MessageResourceCollection, "Unexpected message resource collection");
            Assert.AreEqual(string.Empty, target.MessageResourceGroup, "Unexpected message resource group");
            Assert.AreEqual(0, target.MessageArgs.Count, "Unexpected number of message args");
        }

        /// <summary>
        ///A test for MajorMessageNumber
        ///</summary>
        [TestMethod()]
        public void MajorMessageNumberTest()
        {
            TDPMessage target = new TDPMessage(); 
            int expected = 3;
            int actual;
            target.MajorMessageNumber = expected;
            actual = target.MajorMessageNumber;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MessageArgs
        ///</summary>
        [TestMethod()]
        public void MessageArgsTest()
        {
            TDPMessage target = new TDPMessage();
            List<string> expected = new List<string>() { "ArgOne", "ArgTwo" };
            List<string> actual;
            target.MessageArgs = expected;
            actual = target.MessageArgs;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MessageResourceCollection
        ///</summary>
        [TestMethod()]
        public void MessageResourceCollectionTest()
        {
            TDPMessage target = new TDPMessage();
            string expected = "MessageResourceCollection";
            string actual;
            target.MessageResourceCollection = expected;
            actual = target.MessageResourceCollection;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MessageResourceGroup
        ///</summary>
        [TestMethod()]
        public void MessageResourceGroupTest()
        {
            TDPMessage target = new TDPMessage();
            string expected = "MessageResourceGroup";
            string actual;
            target.MessageResourceGroup = expected;
            actual = target.MessageResourceGroup;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MessageResourceId
        ///</summary>
        [TestMethod()]
        public void MessageResourceIdTest()
        {
            TDPMessage target = new TDPMessage();
            string expected = "MessageResourceId";
            string actual;
            target.MessageResourceId = expected;
            actual = target.MessageResourceId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MessageText
        ///</summary>
        [TestMethod()]
        public void MessageTextTest()
        {
            TDPMessage target = new TDPMessage();
            string expected = "MessageText";
            string actual;
            target.MessageText = expected;
            actual = target.MessageText;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MinorMessageNumber
        ///</summary>
        [TestMethod()]
        public void MinorMessageNumberTest()
        {
            TDPMessage target = new TDPMessage();
            int expected = 9;
            int actual;
            target.MinorMessageNumber = expected;
            actual = target.MinorMessageNumber;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            TDPMessage target = new TDPMessage();
            TDPMessageType expected = TDPMessageType.Warning;
            TDPMessageType actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TDPMessage Clone
        ///</summary>
        [TestMethod()]
        public void TDPMessageCloneTest()
        {
            string messageResourceId = "MessageResourceId";
            TDPMessageType type = TDPMessageType.Error;
            TDPMessage target = new TDPMessage(messageResourceId, type);

            TDPMessage clone = target.Clone();

            Assert.AreEqual(clone.MessageText, target.MessageText, "Unexpected message text");
            Assert.AreEqual(clone.MessageResourceId, target.MessageResourceId, "Unexpected message resource id");
            Assert.AreEqual(clone.MajorMessageNumber, target.MajorMessageNumber, "Unexpected major number");
            Assert.AreEqual(clone.MinorMessageNumber, target.MinorMessageNumber, "Unexpected minor number");
            Assert.AreEqual(clone.Type, target.Type, "Unexpected message type");
            Assert.AreEqual(clone.MessageResourceCollection, target.MessageResourceCollection, "Unexpected message resource collection");
            Assert.AreEqual(clone.MessageResourceGroup, target.MessageResourceGroup, "Unexpected message resource group");
            Assert.AreEqual(clone.MessageArgs.Count, target.MessageArgs.Count, "Unexpected number of message args");
        }
    }
}
