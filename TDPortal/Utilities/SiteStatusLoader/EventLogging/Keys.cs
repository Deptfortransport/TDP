// *********************************************** 
// NAME                 : Keys.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Property keys
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Keys.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Container class used to hold key constants
    /// </summary>
    public class Keys
    {
        public const string Logging = "Logging";
        public const string Publishers = Logging + ".Publisher";
        public const string QueuePublishers = Publishers + ".Queue";
        public const string FilePublishers = Publishers + ".File";
        public const string EventLogPublishers = Publishers + ".EventLog";
        public const string ConsolePublishers = Publishers + ".Console";
        public const string CustomPublishers = Publishers + ".Custom";

        public const string DefaultPublisher = Publishers + ".Default";

        public const string QueuePublisherDelivery = QueuePublishers + ".{0}.Delivery";
        public const string QueuePublisherPriority = QueuePublishers + ".{0}.Priority";
        public const string QueuePublisherPath = QueuePublishers + ".{0}.Path";

        public const string EventLogPublisherName = EventLogPublishers + ".{0}.Name";
        public const string EventLogPublisherSource = EventLogPublishers + ".{0}.Source";
        public const string EventLogPublisherMachine = EventLogPublishers + ".{0}.Machine";
        public const string EventLogPublisherEntryType = EventLogPublishers + ".{0}.EntryType";

        public const string FilePublisherDirectory = FilePublishers + ".{0}.Directory";
        public const string FilePublisherRotation = FilePublishers + ".{0}.Rotation";

        public const string ConsolePublisherStream = ConsolePublishers + ".{0}.Stream";

        public const string CustomPublisherName = CustomPublishers + ".{0}.Name";
        public const string CustomPublisherAssembly = CustomPublishers + ".{0}.Assembly";

        public const string Events = Logging + ".Event";
        public const string OperationalEvents = Events + ".Operational";
        public const string OperationalTraceLevel = OperationalEvents + ".TraceLevel";

        public const string OperationalEventVerbosePublishers = OperationalEvents + ".Verbose.Publishers";
        public const string OperationalEventInfoPublishers = OperationalEvents + ".Info.Publishers";
        public const string OperationalEventWarningPublishers = OperationalEvents + ".Warning.Publishers";
        public const string OperationalEventErrorPublishers = OperationalEvents + ".Error.Publishers";


        public const string CustomEvents = Events + ".Custom";
        public const string CustomEventName = CustomEvents + ".{0}.Name";
        public const string CustomEventAssembly = CustomEvents + ".{0}.Assembly";
        public const string CustomEventPublishers = CustomEvents + ".{0}.Publishers";
        public const string CustomEventLevel = CustomEvents + ".{0}.Trace";

        public const string CustomEventsLevel = CustomEvents + ".Trace";

        // The following constants are not strictly "property" keys.
        public const string ConsolePublisherErrorStream = "Error";
        public const string ConsolePublisherOutputStream = "Out";

        /// <summary>
        /// Default constructor
        /// </summary>
        static Keys()
        { }
    }
}
