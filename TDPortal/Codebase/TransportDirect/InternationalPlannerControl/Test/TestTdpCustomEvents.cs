// *********************************************** 
// NAME			: TestTdpCustomEvents.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02/02/2010
// DESCRIPTION	: Class which tests International Planner events
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/Test/TestTdpCustomEvents.cs-arc  $
//
//   Rev 1.1   Feb 16 2010 17:48:36   mmodi
//Updated tests
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 02 2010 10:03:22   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using System.Collections;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.InternationalPlannerControl.Test
{
    [TestFixture]
    public class TestTdpCustomEvents
    {
        /// <summary>
        /// Tests that all TDP Custom Events can be published successfully.
        /// </summary>
        [Test]
        public void PublishEvents()
        {
            IPropertyProvider mockProperties = new MockProperties();
            IEventPublisher[] customPublishers = new IEventPublisher[0];
            ArrayList errors = new ArrayList();

            try
            {
                Trace.Listeners.Add(new TDTraceListener(mockProperties, customPublishers, errors));
            }
            catch (TDException)
            {
                Assert.Fail();
            }

            // create instances of all events

            InternationalPlannerRequestEvent ipreq = new InternationalPlannerRequestEvent("123", false, "456");

            InternationalPlannerEvent ipe = new InternationalPlannerEvent(InternationalPlannerType.ExtendInDoorToDoor, true, "999");


            InternationalPlannerResultEvent ipres = new InternationalPlannerResultEvent("222", JourneyPlanResponseCategory.Results, true, "42");


            // turn auditing on to check that event was published
            ipreq.AuditPublishersOff = false;
            ipe.AuditPublishersOff = false;
            ipres.AuditPublishersOff = false;


            // publish events
            try
            {
                Trace.Write(ipreq);
                Trace.Write(ipe);
                Trace.Write(ipres);
            }
            catch (TDException ex)
            {
                Assert.Fail(ex.StackTrace);
            }

            // check that they were published. NB manual check required for events that are published to file in production
            Assert.IsTrue(ipreq.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
            Assert.IsTrue(ipe.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
            Assert.IsTrue(ipres.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");

        }

        [SetUp]
        public void SetUp()
        {
            //TDServiceDiscovery.ResetServiceDiscoveryForTest();
            //TDServiceDiscovery.Init(new TestInitialisation());

            Trace.Listeners.Remove("TDTraceListener");
        }


        [TearDown]
        public void Cleanup()
        {
            Trace.Listeners.Remove("TDTraceListener");
        }
    }
}
