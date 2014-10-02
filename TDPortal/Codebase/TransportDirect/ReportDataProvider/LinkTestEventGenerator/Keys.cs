// *********************************************** 
// NAME                 : Keys.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 01/10/2003 
// DESCRIPTION  : Static class that defines keys to
// retrieve properties (relating to Generator) 
// from the Properties Service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/LinkTestEventGenerator/Keys.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:52   mturner
//Initial revision.
//
//   Rev 1.5   Dec 04 2003 15:58:22   geaton
//Updates to make test data more realistic.
//
//   Rev 1.4   Nov 22 2003 19:58:26   geaton
//Updated key name to reflect property.
//
//   Rev 1.3   Oct 03 2003 12:47:58   geaton
//Added DataGatewayEvent.
//
//   Rev 1.2   Oct 03 2003 10:47:34   geaton
//Added missing key.
//
//   Rev 1.1   Oct 03 2003 10:43:44   geaton
//Removed redundant keys-
//
//   Rev 1.0   Oct 03 2003 09:19:42   geaton
//Initial Revision

namespace TransportDirect.ReportDataProvider.LinkTestEventGenerator
{
	/// <summary>
	/// Container class used to hold key constants
	/// </summary>
	public class Keys
	{
		public const string Prefix = "LinkTestEventGenerator";
		
		public const string NumOfThreads = Prefix + ".Threads";
		
		public const string NumOfOperationalEvents = Prefix + ".OperationalEvents";
		public const string NumOfCustomEmailEvents = Prefix + ".CustomEmailEvents";
		public const string NumOfGazetteerEvents = Prefix + ".GazetteerEvents";
		public const string NumOfMapEvents = Prefix + ".MapEvents";
		public const string NumOfRetailerHandoffEvents = Prefix + ".RetailerHandoffEvents";
		public const string NumOfUserPreferenceSaveEvents = Prefix + ".UserPreferenceSaveEvents";
		public const string NumOfLocationRequestEvents = Prefix + ".LocationRequestEvents";
		public const string NumOfPageEntryEvents = Prefix + ".PageEntryEvents";
		public const string NumOfLoginEvents = Prefix + ".LoginEvents";
		public const string NumOfWebLogEntries = Prefix + ".WebLogEntries";
		public const string NumOfDataGatewayEvents= Prefix + ".DataGatewayEvents";

		public const string WebRequestDuration = Prefix + ".JourneyWebRequestDurationMs";

		public const string EmailAddresses = Prefix + ".EmailAddresses";

		public const string WorkloadEventURL = Prefix + ".WorkloadEventURL";

		public const string SessionId = Prefix + ".SessionId";

		public const string RetailerId1 = Prefix + ".Retailer1Id";
		public const string RetailerId2 = Prefix + ".Retailer2Id";

		public const string RegionCode1 = Prefix + ".RegionCode1";
		public const string RegionCode2 = Prefix + ".RegionCode2";

		public const string AdminArea1 = Prefix + ".AdminArea1";
		public const string AdminArea2 = Prefix + ".AdminArea2";

		public const string DataGatwayFile = Prefix + ".DataGatewayFile";
		public const string DataGatwayFeed = Prefix + ".DataGatewayFeed";

		public const string JourneyPlanRequestId1 = Prefix + ".JPRequestId1";
		public const string JourneyPlanRequestId2 = Prefix + ".JPRequestId2";
			
	}
}
