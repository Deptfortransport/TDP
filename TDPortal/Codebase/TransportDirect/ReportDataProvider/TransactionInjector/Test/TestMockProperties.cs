// *********************************************** 
// NAME			: TestMockProperties.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Mock property classes used for testing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TestMockProperties.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:08   mturner
//Initial revision.
//
//   Rev 1.17   Feb 23 2006 19:15:48   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.2.0   Nov 25 2005 18:05:08   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.16   May 24 2005 15:06:26   NMoorhouse
//Post Del7 NUnit Updates
//
//   Rev 1.15   Apr 09 2005 15:15:46   schand
//Added mock key for property services
//
//   Rev 1.14   Nov 17 2004 11:18:34   passuied
//addition for TravelineChecker transaction
//
//   Rev 1.13   Jun 10 2004 17:10:40   passuied
//Added parameters for RequestJourneySleep
//
//   Rev 1.12   Mar 02 2004 09:17:40   geaton
//Updated test following removal of custom Transaction Injector File Publisher. (A standard File Publisher is now used instead.)
//
//   Rev 1.11   Jan 08 2004 19:43:24   PNorell
//Added new transactions to inject.
//
//   Rev 1.10   Dec 19 2003 13:47:26   PNorell
//Transaction injector patch.
//Resolution for 563: Formatting issue : Map page
//
//   Rev 1.9   Nov 13 2003 12:30:36   geaton
//Removed redundant mock properties.

using System;
using System.Collections;

using Logging = TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{	
	/// <summary>
	/// Invalid version of the properties.
	/// </summary>
	public class MockBadProperties : IPropertyProvider
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


		public MockBadProperties(): this("transactioninjector1")
		{
		}
		/// <summary>
		/// Default constructor initialises the properties object.
		/// </summary>
		public MockBadProperties(string serviceName)
		{
			current = new Hashtable();

			current[string.Format(Keys.TransactionInjectorTransactionType , serviceName)]						= "1 2 3";
			current[Keys.TransactionInjectorFrequency]							= "";
			current[Keys.TransactionInjectorWebService]							= "http://Nothing";
			current[string.Format(Keys.TransactionInjectorTransactionClass, 1)]	= "JourneyRequestTransactionInvalid";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  1)]	= @".\InvalidDir";
			current[string.Format(Keys.TransactionInjectorTransactionClass, 2)]	= "";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  2)]	= @".\AnotherDir";
			current[Keys.TransactionInjectorTemplateFileDirectory]				= "";
	
			current["Logging.Publisher.Queue"]						= "";
			current["Logging.Publisher.Email"]						= "";
			current["Logging.Publisher.EventLog"]					= "";
			current["Logging.Publisher.Console"]					= "";
			current["Logging.Publisher.Custom"]						= "";
		    current["Logging.Event.Custom.Trace"]					= "On";

			current["Logging.Event.Operational.Info.Publishers"]	= "F1";
			current["Logging.Event.Operational.Verbose.Publishers"] = "F1";
			current["Logging.Event.Operational.Warning.Publishers"] = "F1";
			current["Logging.Event.Operational.Error.Publishers"]	= "F1";

			current["Logging.Event.Custom.Trace"]					= "Verbose";			
		
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
	

	/// <summary>
	/// Valid set of properties.
	/// </summary>
	public class MockGoodProperties : IPropertyProvider, IServiceFactory
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

		public MockGoodProperties() : this("transactioninjector1")
		{
		
		}
		/// <summary>
		/// Default constructor initialises the properties object.
		/// </summary>
		public MockGoodProperties(string serviceName)
		{
			current = new Hashtable();


			current[string.Format(Keys.TransactionInjectorTransactionType, serviceName)]		= "1 2 4 5 6";
			current[Keys.TransactionInjectorFrequency]							= "10";
			current[Keys.TransactionInjectorWebService]							= "http://localhost/TDPWebServices/TransactionWebService/TDTransactionService.asmx";
			current[string.Format(Keys.TransactionInjectorTransactionClass, 1)]	= "JourneyRequestTransaction";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  1)]	= @".\JourneyRequestData";
			current[string.Format(Keys.TransactionInjectorTransactionClass, 2)]	= "SoftContentTransaction";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  2)]	= @".\SoftContentData";
			current[string.Format(Keys.TransactionInjectorTransactionClass, 3)]	= "GazetteerTransaction";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  3)]	= @".\GazetteerData";
			current[string.Format(Keys.TransactionInjectorTransactionClass, 4)]	= "JourneyRequestUsingGazetteerTransaction";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  4)]	= @".\JourneyRequestUsingGazetteerData";
			current[string.Format(Keys.TransactionInjectorTransactionClass, 5)]	= "JourneyRequestSleepTransaction";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  5)]	= @".\JourneyRequestSleepData";

			current[string.Format(Keys.TransactionInjectorTransactionClass, 6)]	= "TravelineCheckerTransaction";
			current[string.Format(Keys.TransactionInjectorTransactionPath,  6)]	= @".\TravelineCheckerData";
			current[Keys.TransactionInjectorTemplateFileDirectory]				= @".\Templates";
				
			current["Logging.Publisher.Default"]					= "F2";
			current["Logging.Publisher.File"]						= "F1 F2";
			current["Logging.Publisher.Queue"]						= "";
			current["Logging.Publisher.Email"]						= "";
			current["Logging.Publisher.EventLog"]					= "";
			current["Logging.Publisher.Console"]					= "";
			current["Logging.Publisher.Custom"]						= "";

			current["Logging.Event.Custom.Trace"]					= "On";
			current["Logging.Event.Custom"]							= "C1";
			current["Logging.Event.Custom.C1.Name"]					= "ReferenceTransactionEvent";
			current["Logging.Event.Custom.C1.Assembly"]				= "td.ReportDataProvider.TDPCustomEvents";
			current["Logging.Event.Custom.C1.Publishers"]			= "F1";
			current["Logging.Event.Custom.C1.Trace"]				= "On";

			current["Logging.Event.Operational.TraceLevel"]			= "Verbose";
			current["Logging.Event.Operational.Info.Publishers"]	= "F2";
			current["Logging.Event.Operational.Verbose.Publishers"] = "F2";
			current["Logging.Event.Operational.Warning.Publishers"] = "F2";
			current["Logging.Event.Operational.Error.Publishers"]	= "F2";

			current["Logging.Publisher.File.F1.Directory"]			= @".\RTETest";
			current["Logging.Publisher.File.F1.Rotation"]			= "1024";

			current["Logging.Publisher.File.F2.Directory"]			= @".\OPETest";
			current["Logging.Publisher.File.F2.Rotation"]			= "1024";
			current["ServiceDiscoveryKey.PropertyService"]			= "PropertyService";

			

		}

		public string RTEDir
		{
			get { return this["Logging.Publisher.File.F1.Directory"]; }
		}

		public string OPEDir
		{
			get { return this["Logging.Publisher.File.F2.Directory"]; }
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
			get { return "TransactionInjectorTest"; }
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
			return this;
		}
	}

	
}

