// *********************************************** 
// NAME             : CustomEmailPublisherTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 19 Sep 2013
// DESCRIPTION  	: CustomEmailPublisher test
// ************************************************
// 
                
using TDP.Common.EventLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using TDP.Common;

namespace TDP.TestProject.EventLogging
{
    
    
    /// <summary>
    ///This is a test class for CustomEmailPublisherTest and is intended
    ///to contain all CustomEmailPublisherTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CustomEmailPublisherTest
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
        ///A test for WriteEvent
        ///</summary>
        [TestMethod()]
        public void CustomEmailPublisherWriteEventTest()
        {
            // Valid email publisher
            string identifier = "EMAIL";
            string from = "noreply@transportdirect.info";
            MailPriority priority = MailPriority.Normal;
            string smtpServer = "localhost";
            string workingDirectoryPath = "D:\\Temp";
            List<string> errors = new List<string>();

            CustomEmailPublisher target = new CustomEmailPublisher(identifier, from, priority, smtpServer, workingDirectoryPath, errors);

            // Invalid log event type
            try
            {
                target.WriteEvent(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, "Not an email event"));
                Assert.Fail("Expected exception to be thrown");
            }
            catch
            {
                // Expecting error
            }

            // Valid email
            string origin = "from@transportdirect.info";
            string destination = "to@transportdirect.info";
            string bodyText = "leicester to beeston";
            string subject = "test";

            string attachmentPath = "TestAttachmentPath";
            string attachmentName = string.Empty;

            CustomEmailEvent emailEvent = new CustomEmailEvent(origin, destination, bodyText, subject);

            try
            {
                target.WriteEvent(emailEvent);
            }
            catch
            {
                Assert.Inconclusive("Email smtp server may not be available on machine.");
            }

            // Valid email - missing from
            emailEvent = new CustomEmailEvent(string.Empty, destination, bodyText, subject);

            try
            {
                target.WriteEvent(emailEvent);
            }
            catch
            {
                Assert.Inconclusive("Email smtp server may not be available on machine.");
            }

            // Valid email - attachment path
            attachmentPath = @"D:\TDPortal\Codebase\TransportDirect2\TDPMobile\Version\Images\logos\TDLogo.gif";
            attachmentName = "TDLogo";
            emailEvent = new CustomEmailEvent(origin, destination, bodyText, subject, attachmentPath, attachmentName);

            try
            {
                target.WriteEvent(emailEvent);
            }
            catch
            {
                Assert.Inconclusive("Email smtp server may not be available on machine.");
            }

            // Valid email - attachment stream
            using (Image image = Image.FromFile(attachmentPath))
            {
                using (Stream attachmentStream = new MemoryStream())
                {
                    image.Save(attachmentStream, ImageFormat.Gif);
                    
                    emailEvent = new CustomEmailEvent(origin, destination, bodyText, subject, attachmentStream, attachmentName);

                    try
                    {
                        target.WriteEvent(emailEvent);
                    }
                    catch
                    {
                        Assert.Inconclusive("Email smtp server may not be available on machine.");
                    }
                }
            }

            // Valid email - invalid attachment path
            attachmentPath = @"D:\DOESNOTEXIST\TDLogo.gif";
            emailEvent = new CustomEmailEvent(origin, destination, bodyText, subject, attachmentPath, attachmentName);

            try
            {
                target.WriteEvent(emailEvent);
            }
            catch (TDPException tdpEx)
            {
                // Exception shouldn't be thrown for missing attachment, but currently is due to the publisher
                // catching everything and throwing, so check for correct id
                if (tdpEx.Identifier != TDPExceptionIdentifier.ELSCustomEmailPublisherWritingEvent)
                {
                    Assert.Fail("Unexpected exception thrown");
                }
            }

            // Valid email - invalid attachment stream
            using (Stream attachmentStream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(attachmentStream);

                writer.Write("Attachment");

                emailEvent = new CustomEmailEvent(origin, destination, bodyText, subject, attachmentStream, attachmentName);

                try
                {
                    target.WriteEvent(emailEvent);
                    Assert.Fail ("Exception expected for invalid attachment stream");
                }
                catch
                {
                    // Exception expected
                }
            }
        }

        /// <summary>
        ///A test for CustomEmailPublisher Constructor
        ///</summary>
        [TestMethod()]
        public void CustomEmailPublisherConstructorTest()
        {
            // Valid email publisher
            string identifier = "EMAIL";
            string from = "noreply@transportdirect.info";
            MailPriority priority = MailPriority.Normal;
            string smtpServer = "localhost";
            string workingDirectoryPath = "D:\\Temp";
            List<string> errors = new List<string>();

            CustomEmailPublisher target = new CustomEmailPublisher(identifier, from, priority, smtpServer, workingDirectoryPath, errors);

            Assert.IsTrue(errors.Count == 0);

            try
            {
                // Invalid email publisher
                target = new CustomEmailPublisher(string.Empty, string.Empty, priority, string.Empty, "D:\\DOESNOTEXIST", errors);

                Assert.Fail("Expected exception to be thrown");
            }
            catch
            {
                // Expected error
            }

            try
            {
                // Invalid email sender address
                target = new CustomEmailPublisher(identifier, "invalidemail", priority, smtpServer, workingDirectoryPath, errors);

                Assert.Fail("Expected exception to be thrown");
            }
            catch
            {
                // Expected error
            }

        }
    }
}
