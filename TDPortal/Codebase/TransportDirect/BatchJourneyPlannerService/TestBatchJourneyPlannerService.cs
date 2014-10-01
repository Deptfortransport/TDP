using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Text;
using NUnit.Framework;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace BatchJourneyPlannerService
{
    [TestFixture]
    public class TestBatchJourneyPlannerService
    {
        [Test]
        public void TestService()
        {
            TDServiceDiscovery.Init(new BatchJourneyPlannerServiceInitialisation());

            //Configure the hosted remoting objects to be a remote object
            RemotingConfiguration.Configure(@"D:\TDPortal\Codebase\TransportDirect\BatchJourneyPlannerService\remoting.config", false);
            
            IPropertyProvider props = Properties.Current;
            BatchProcessor processor = new BatchProcessor();
            processor.Run(props);
        }
    }
}
