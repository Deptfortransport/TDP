using TDP.UserPortal.StateServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TDP.TestProject.StateServer
{


    /// <summary>
    ///This is a test class for ITDPStateServerTest and is intended
    ///to contain all ITDPStateServerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StateServerMessagesTest
    {

        /// <summary>
        ///A test for Messages
        ///</summary>
        [TestMethod()]
        public void MessagesTest()
        {
            Messages messages = new Messages();

            Assert.IsNotNull(messages, "No object returned");
        }
    }
}
