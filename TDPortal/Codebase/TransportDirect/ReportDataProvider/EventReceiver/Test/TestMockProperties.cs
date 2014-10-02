// *********************************************** 
// NAME                 : TestMockProperties.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Mock properties used by the test classes.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/TestMockProperties.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:38   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:15:48   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.2.0   Nov 25 2005 18:04:38   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Nov 06 2003 19:54:28   geaton
//Removed redundant key.
//
//   Rev 1.2   Oct 30 2003 12:26:02   geaton
//Added property for ReceivedOperationalEvent.
//
//   Rev 1.1   Sep 05 2003 09:49:34   jtoor
//Changes made to comply with Code Review.
//
//   Rev 1.0   Aug 22 2003 11:49:48   jtoor
//Initial Revision

using System;
using System.Collections;

using Logging = TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.EventReceiver
{	
	/// <summary>
	/// Mock properties class used for event write test.
	/// </summary>
	public class MockPropertiesEventWrite : IPropertyProvider
	{
		private Hashtable current;

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
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}


		public MockPropertiesEventWrite()
		{
			current = new Hashtable();

			current[Keys.ReceiverQueue]								= "1";
			current[string.Format(Keys.ReceiverQueuePath, 1)]		= @".\private$\mockpropertieseventwrite";
				
			current["Logging.Publisher.Default"]					= "F1";
			current["Logging.Publisher.Custom"]						= "";
			current["Logging.Publisher.Queue"]						= "";
			current["Logging.Publisher.Email"]						= "";
			current["Logging.Publisher.EventLog"]					= "";
			current["Logging.Publisher.Console"]					= "";

			current["Logging.Event.Operational.TraceLevel"]			= "Info";
			current["Logging.Event.Operational.Info.Publishers"]	= "F1";
			current["Logging.Event.Operational.Verbose.Publishers"] = "F1";
			current["Logging.Event.Operational.Warning.Publishers"] = "F1";
			current["Logging.Event.Operational.Error.Publishers"]	= "F1";

			current["Logging.Publisher.File"]						= "F1";
			current["Logging.Publisher.File.F1.Directory"]			= @".\testout";
			current["Logging.Publisher.File.F1.Rotation"]			= "1024";				

			current["Logging.Event.Custom.Trace"]					= "On";
			current["Logging.Event.Custom"]							= "ROE";
			current["Logging.Event.Custom.ROE.Name"] = "ReceivedOperationalEvent";
			current["Logging.Event.Custom.ROE.Assembly"] = "td.reportdataprovider.tdpcustomevents";
			current["Logging.Event.Custom.ROE.Publishers"] = "F1";
			current["Logging.Event.Custom.ROE.Trace"] = "On";
			
		}


		// Required from interface - not actually used by the mock

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
	}	

	/// <summary>
	/// Good mock properties used for validation test.
	/// </summary>
	public class MockPropertiesGoodProperties : IPropertyProvider
	{
		private Hashtable current;

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
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public MockPropertiesGoodProperties()
		{
			current = new Hashtable();

			current[Keys.ReceiverQueue]								= "1";
			current[string.Format(Keys.ReceiverQueuePath, 1)]		= @".\private$\mockpropertiesgoodproperties";
				
			current["Logging.Publisher.Queue"]						= "";
			current["Logging.Publisher.Email"]						= "";
			current["Logging.Publisher.EventLog"]					= "";
			current["Logging.Publisher.Console"]					= "";

			current["Logging.Event.Operational.Info.Publishers"]	= "F1";
			current["Logging.Event.Operational.Verbose.Publishers"] = "F1";
			current["Logging.Event.Operational.Warning.Publishers"] = "F1";
			current["Logging.Event.Operational.Error.Publishers"]	= "F1";

			current["Logging.Event.Custom.Trace"]					= "On";		
			current["Logging.Event.Custom"]							= "ROE";
			current["Logging.Event.Custom.ROE.Name"] = "ReceivedOperationalEvent";
			current["Logging.Event.Custom.ROE.Assembly"] = "td.reportdataprovider.tdpcustomevents";
			current["Logging.Event.Custom.ROE.Publishers"] = "F1";
			current["Logging.Event.Custom.ROE.Trace"] = "On";

			current["Logging.Publisher.File"]						= "F1";
			current["Logging.Publisher.File.F1.Directory"]			= @".\testout";
			current["Logging.Publisher.File.F1.Rotation"]			= "1024";		
		}


		// Required from interface - not actually used by the mock

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
	}
	
}
