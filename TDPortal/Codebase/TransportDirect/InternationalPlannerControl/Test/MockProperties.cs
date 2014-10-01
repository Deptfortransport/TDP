// *********************************************** 
// NAME			: MockProperties.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02/02/2010
// DESCRIPTION	: Class which mocks property provider for defining properties used for tests
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/Test/MockProperties.cs-arc  $
//
//   Rev 1.0   Feb 02 2010 10:03:20   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using TransportDirect.Common.Logging;
using System.IO;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// Mock property provider for defining properties used for tests.
    /// </summary>
    public class MockProperties : IPropertyProvider, IServiceFactory
    {
        private Hashtable current;
        private MockProperties instance;

        public string this[string key]
        {
            get
            {
                if (current.ContainsKey(key))
                {
                    return (string)current[key];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Dummy Read only Indexer property that also takes a partner ID. 		
        /// Since this method was added to the interface and 
        /// hence every class which is implementing  IPropertyProvider interface must implement this method.
        /// </summary>
        public string this[string key, int partnerID]
        {
            get { return string.Empty; }
        }

        public MockProperties()
        {
            current = new Hashtable();

            current[Keys.QueuePublishers] = "";
            current[Keys.DefaultPublisher] = "File1";
            current[Keys.FilePublishers] = "File1";
            current[Keys.EmailPublishers] = "";
            current[Keys.CustomPublishers] = "";
            current[Keys.EventLogPublishers] = "";
            current[Keys.ConsolePublishers] = "";

            current[String.Format(Keys.FilePublisherDirectory, "File1")] = @".\File1";
            current[String.Format(Keys.FilePublisherRotation, "File1")] = "10";

            current[Keys.EmailPublishersSmtpServer] = "localhost";

            current[Keys.OperationalTraceLevel] = "Verbose";

            current[Keys.OperationalEventVerbosePublishers] = "File1";
            current[Keys.OperationalEventInfoPublishers] = "File1";
            current[Keys.OperationalEventWarningPublishers] = "File1";
            current[Keys.OperationalEventErrorPublishers] = "File1";

            current[Keys.CustomEvents] = "IPREQ IPRES IPE";

            current[String.Format(Keys.CustomEventName, "IPREQ")] = "InternationalPlannerRequestEvent";
            current[String.Format(Keys.CustomEventAssembly, "IPREQ")] = "td.userportal.internationalplannercontrol";
            current[String.Format(Keys.CustomEventPublishers, "IPREQ")] = "File1";
            current[String.Format(Keys.CustomEventLevel, "IPREQ")] = "On";
            current[String.Format(Keys.CustomEventName, "IPE")] = "InternationalPlannerEvent";
            current[String.Format(Keys.CustomEventAssembly, "IPE")] = "td.userportal.internationalplannercontrol";
            current[String.Format(Keys.CustomEventPublishers, "IPE")] = "File1";
            current[String.Format(Keys.CustomEventLevel, "IPE")] = "On";
            current[String.Format(Keys.CustomEventName, "IPRES")] = "InternationalPlannerResultEvent";
            current[String.Format(Keys.CustomEventAssembly, "IPRES")] = "td.userportal.internationalplannercontrol";
            current[String.Format(Keys.CustomEventPublishers, "IPRES")] = "File1";
            current[String.Format(Keys.CustomEventLevel, "IPRES")] = "On";
            

            current[Keys.CustomEventsLevel] = "On";

            // create necessary publisher sinks
            DirectoryInfo di = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
            di.Create();

        }

        
        public event SupersededEventHandler Superseded;

        // following definition gets rid of compiler warning
        public void Supersede()
        {
            Superseded(this, new EventArgs());
        }

        public string ApplicationID
        {
            get { return ""; }
        }

        public string GroupID
        {
            get { return ""; }
        }

        public bool IsSuperseded
        {
            get { return false; }
        }

        public int Version
        {
            get { return 0; }
        }

        public object Get()
        {
            if (instance == null)
            {
                instance = new MockProperties();
            }
            return instance;
        }

    }
}
