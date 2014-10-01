﻿using TDP.UserPortal.StateServer;
using TDP.Common.ServiceDiscovery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Soss.Client;

namespace TDP.TestProject.StateServer
{
    
    
    /// <summary>
    ///This is a test class for TDPStateServerTest and is intended
    ///to contain all TDPStateServerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPStateServerTest
    {


        private TestContext testContextInstance;
        private string sessionId = "TestSessionId";
        private string key = "TestKey";
        private string keyValue = "TestValue";
        private string appName = "TestApp";

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
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();

            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());
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


        /// <summary>
        ///A test for TDPStateServer Constructor
        ///</summary>
        [TestMethod()]
        public void TDPStateServerConstructorTest()
        {
            using (TDPStateServer target = new TDPStateServer(appName))
            {
                if (target == null)
                {
                    Assert.Fail("No StateServer object returned by constructor");
                }
            }
        }


        /// <summary>
        ///A test for TDPStateServer Constructor
        ///</summary>
        [TestMethod()]
        public void TDPStateServerConstructorTest1()
        {
            using (TDPStateServer target = new TDPStateServer(appName))
            {
                if (target == null)
                {
                    Assert.Fail("No StateServer object returned by constructor");
                }
            }
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            using (TDPStateServer target = new TDPStateServer(appName))
            {
                try
                {
                    target.Save(sessionId, key, keyValue);

                    target.Delete(sessionId, key);

                    object actual = target.Read(sessionId, key);
                    Assert.IsNull(actual, "Key not deleted");
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("Unexpected exception returned: {0}", ex.ToString()));
                }
            }
        }

        /// <summary>
        ///A test for Deserialize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void DeserializeTest()
        {
            using (TDPStateServer_Accessor target = new TDPStateServer_Accessor())
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, keyValue);
                    byte[] bytes = ms.ToArray();
                    string actual = (string)target.Deserialize(bytes);
                    Assert.AreEqual(keyValue, actual);
                }
            }
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void DisposeTest()
        {
            TDPStateServer_Accessor target = new TDPStateServer_Accessor();
            bool disposing = false;
            target.Dispose(disposing);

            target = new TDPStateServer_Accessor();
            disposing = true;
            target.Dispose(disposing);
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest1()
        {
            TDPStateServer target = new TDPStateServer();
            target.Dispose();
        }

        /// <summary>
        ///A test for Finalize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void FinalizeTest()
        {
            using (TDPStateServer_Accessor target = new TDPStateServer_Accessor())
            {
                target.Finalize();
            }
        }

        /// <summary>
        ///A test for GetDataAccessor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void GetDataAccessorTest()
        {
            using (TDPStateServer_Accessor target = new TDPStateServer_Accessor())
            {
                DataAccessor actual = target.GetDataAccessor(sessionId + key);
                Assert.AreNotEqual(0, actual.Key.AppId, "Non-zero appId expected");
            }
        }

        /// <summary>
        ///A test for Lock
        ///</summary>
        [TestMethod()]
        public void LockTest()
        {
            // Create a separate state server to check the key is locked
            using (TDPStateServer otherServer = new TDPStateServer(appName))
            {
                try
                {
                    TDPStateServer target = new TDPStateServer(appName);

                    try
                    {
                        target.Save(sessionId, key, keyValue);

                        target.Lock(sessionId, new string[] { key });

                        // Try Read, Delete, Save and Lock methods with the otherServer so as to up coverage of exception code
                        try
                        {
                            otherServer.Read(sessionId, key);
                            Assert.Fail("Expected exception not raised");
                        }
                        catch (Exception ex)
                        {
                            // Check it's the correct exception!
                            string message = ex.Message;
                            string combinedKey = sessionId + key;

                            if (!message.Equals(string.Format("Error reading object with key[{0}] from StateServer: Failed locking object with key[{0}].", combinedKey), StringComparison.InvariantCultureIgnoreCase))
                            {
                                // wrong exception
                                Assert.Fail(string.Format("Unexpected exception received: {0}", ex.ToString()));
                            }
                        }

                        try
                        {
                            otherServer.Save(sessionId, key, keyValue);
                            Assert.Fail("Expected exception not raised");
                        }
                        catch (Exception ex)
                        {
                            // Check it's the correct exception!
                            string message = ex.Message;
                            string combinedKey = sessionId + key;

                            if (!message.Equals(string.Format("Error saving object with key[{0}] to StateServer: Failed locking object with key[{0}].", combinedKey), StringComparison.InvariantCultureIgnoreCase))
                            {
                                // wrong exception
                                Assert.Fail(string.Format("Unexpected exception received: {0}", ex.ToString()));
                            }
                        }

                        //try
                        //{
                        //    otherServer.Delete(sessionId, key);
                        //    Assert.Fail("Expected exception not raised");
                        //}
                        //catch (Exception ex)
                        //{
                        //    // Check it's the correct exception!
                        //    string message = ex.Message;
                        //    string combinedKey = sessionId + key;

                        //    if (!message.Equals(string.Format("Error reading object with key[{0}] from StateServer: Failed locking object with key[{0}].", combinedKey), StringComparison.InvariantCultureIgnoreCase))
                        //    {
                        //        // wrong exception
                        //        Assert.Fail(string.Format("Unexpected exception received: {0}", ex.ToString()));
                        //    }
                        //}

                        //try
                        //{
                        //    otherServer.Lock(sessionId, new string[] {key});
                        //    Assert.Fail("Expected exception not raised");
                        //}
                        //catch (Exception ex)
                        //{
                        //    // Check it's the correct exception!
                        //    string message = ex.Message;
                        //    string combinedKey = sessionId + key;

                        //    if (!message.Equals(string.Format("Error reading object with key[{0}] from StateServer: Failed locking object with key[{0}].", combinedKey), StringComparison.InvariantCultureIgnoreCase))
                        //    {
                        //        // wrong exception
                        //        Assert.Fail(string.Format("Unexpected exception received: {0}", ex.ToString()));
                        //    }
                        //}
                    }
                    finally
                    {
                        // Remove the original server then check lock is lifted
                        target.Dispose();
                    }

                    try
                    {
                        otherServer.Read(sessionId, key);
                    }
                    catch (Exception ex)
                    {
                        // Didn't want this exception
                        Assert.Fail(string.Format("Unexpected exception received: {0}", ex.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("Unexpected exception returned: {0}", ex.ToString()));
                }
            }
        }

        /// <summary>
        ///A test for ObjectLockedWait
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void ObjectLockedWaitTest()
        {
            using (TDPStateServer_Accessor target = new TDPStateServer_Accessor())
            {
                int count = 4;
                target.ObjectLockedWait(key, count);
            }
        }

        /// <summary>
        ///A test for Read
        ///</summary>
        [TestMethod()]
        public void ReadTest()
        {
            using (TDPStateServer target = new TDPStateServer(appName))
            {
                try
                {
                    target.Save(sessionId, key, keyValue);

                    string actual;
                    actual = (string)target.Read(sessionId, key);
                    Assert.AreEqual(keyValue, actual, "Different key value returned");
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("Unexpected exception returned: {0}", ex.ToString()));
                }
            }
        }

        /// <summary>
        ///A test for RemoveDataAccessor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void RemoveDataAccessorTest()
        {
            using (TDPStateServer_Accessor target = new TDPStateServer_Accessor())
            {
                DataAccessor actual = target.GetDataAccessor(sessionId + key);
                Assert.AreNotEqual(0, actual.Key.AppId, "Non-zero appId expected");

                // Can only watch for exceptions which will fail the test
                target.RemoveDataAccessor(sessionId + key);
            }
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            using (TDPStateServer target = new TDPStateServer(appName))
            {
                try
                {
                    target.Save(sessionId, key, keyValue);
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("Unexpected exception received: {0}", ex.ToString()));
                }
            }
        }

        /// <summary>
        ///A test for SetApplicationName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void SetApplicationNameTest()
        {
            using (TDPStateServer_Accessor target = new TDPStateServer_Accessor())
            {
                target.SetApplicationName(appName);
                Assert.AreEqual(appName, target.appName, "Application name not set");
            }
        }

        /// <summary>
        ///A test for SetStaticValues
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.stateserver.dll")]
        public void SetStaticValuesTest()
        {
            using (TDPStateServer_Accessor target = new TDPStateServer_Accessor())
            {
                target.SetStaticValues();
            }
        }
    }
}
