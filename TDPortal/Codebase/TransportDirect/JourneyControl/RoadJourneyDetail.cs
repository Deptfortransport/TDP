// *********************************************** 
// NAME			: RoadJourneyDetail.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the RoadJourneyDetail class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/RoadJourneyDetail.cs-arc  $
//
//   Rev 1.3   Sep 01 2011 10:43:22   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.2   Mar 14 2011 15:11:52   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.1   Mar 25 2010 16:18:40   MTurner
//Added code to exclude Toll Links if no valid URL has been supplied.
//Resolution for 5484: Some Tolls supply invalid URL
//
//   Rev 1.0   Nov 08 2007 12:23:58   mturner
//Initial revision.
//
//   Rev 1.40   Jan 12 2007 13:18:38   jfrank
//Changed for IR4277 - Adding and end instruction to a road journey ending in the congegestion zone at a time when a charge applies, if it entered at a time when a charge didn't apply.
//Resolution for 4277: Congestion Charge Addendum
//
//   Rev 1.39   Nov 29 2006 16:46:14   mmodi
//Exposed CongestionZoneEnd properties
//Resolution for 4277: Congestion Charge Addendum
//
//   Rev 1.38   Nov 24 2006 15:28:30   PScott
//CCN0342a Draft Changes
//
//   Rev 1.37   Apr 07 2006 16:45:30   mguney
//A new property called JunctionDriveSectionWithoutJunctionNo introduced.
//Resolution for 3849: Car Journey Instructions Update after stream0030
//
//   Rev 1.35   Mar 14 2006 14:12:50   asinclair
//Mannual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.34   Mar 08 2006 17:03:16   CRees
//Fix for road journey toll pricing on same-day return journeys. Vantive 4050754
//
//   Rev 1.33   Mar 03 2006 14:16:32   build
//Manual update for merge for stream0004. Auto update failed as there was a lock on the file.
//
//   Rev 1.32.1.0   Dec 21 2005 16:18:56   mguney
//RoadSplits readonly property included.
//Resolution for 4: DN083: Car Journey Planning Enhancements - Ambiguity
//
//   Rev 1.32   Sep 26 2005 16:38:08   rhopkins
//Merge stream 2596 back into trunk
//
//   Rev 1.28.1.3   Sep 15 2005 16:49:48   kjosling
//Merged with version 1.31
//
//   Rev 1.28.1.2   Jul 15 2005 11:04:54   rgeraghty
//Updated to ensure CJP URL is returned even if not in External Links table
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.28.1.1   Jun 27 2005 12:33:34   rgeraghty
//Updated with code review comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.28.1.0   Jun 15 2005 10:05:38   rgeraghty
//Check added to verify validity of URLs from CJP
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.28   May 13 2005 15:08:58   asinclair
//Fix for IR 2497
//
//   Rev 1.27   May 07 2005 15:09:52   asinclair
//Fix for IR 2430
//
//   Rev 1.26   May 06 2005 10:33:54   asinclair
//Fix for IR 2450
//
//   Rev 1.25   May 05 2005 10:32:08   asinclair
//Added code to deal with missing Nodes and Node TOIDS
//
//   Rev 1.24   May 04 2005 16:06:46   asinclair
//Updated for new DEL 7 mapping functionality
//
//   Rev 1.23   Apr 26 2005 09:35:14   rgeraghty
//Use stopover section name as the ferryroute name,  toll name and Congestion name rather than the textsectionfeature name
//Resolution for 2233: DEl 7 - Car costing - identifying ferry
//
//   Rev 1.22   Apr 23 2005 11:35:16   asinclair
//Added code for StopoverSection of type 'UndefinedWait' Fix for IR 2244
//
//   Rev 1.21   Apr 16 2005 18:28:26   asinclair
//Fix for IR 1989
//
//   Rev 1.20   Apr 06 2005 16:26:48   asinclair
//Commented out code to check for StopoverSection type returned from CJP
//
//   Rev 1.19   Mar 30 2005 19:28:38   asinclair
//Updated with temporary fix to IR1974 issue no 2
//
//   Rev 1.18   Mar 24 2005 19:55:56   asinclair
//Added code to deal with a StopOverSection of type 'Wait'
//
//   Rev 1.17   Mar 24 2005 09:51:10   asinclair
//Checked in for Code Review
//
//   Rev 1.16   Mar 21 2005 15:12:46   asinclair
//Fixed error in planning motorway journeys
//
//   Rev 1.15   Mar 18 2005 16:30:00   asinclair
//Set congestion level correctly
//
//   Rev 1.14   Mar 01 2005 15:58:36   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.13   Jan 20 2005 10:19:58   asinclair
//Work in progress - DEL 7 Car Costing
//
//   Rev 1.12   Dec 02 2004 14:58:26   jbroome
//Fix for IR 1800
//Resolution for 1800: Motorway Junctions - Motorways without Junction Numbers
//
//   Rev 1.11   Dec 01 2004 10:34:16   jbroome
//Amended formatting of Road Name string, to handle presence of Slip Road.
//
//   Rev 1.10   Nov 26 2004 13:50:18   jbroome
//DEL6.3.1. Motorway Junctions enhancements
//
//   Rev 1.9   Apr 21 2004 15:00:10   asinclair
//Made "FERRY" a static readonly string
//
//   Rev 1.8   Apr 20 2004 11:57:28   asinclair
//Ferry Distance - remove check for Road Name, as should now only use Road Number
//
//   Rev 1.7   Apr 02 2004 17:46:50   AWindley
//DEL 5.2 Change : Ferry distance - include check for road number in addition to road name.
//
//   Rev 1.6   Mar 12 2004 12:04:14   asinclair
//Del 5.2 change - Ferry distance not included in car journey distance
//
//   Rev 1.5   Nov 15 2003 18:06:38   RPhilpott
//Leave stopover sections out of RoadJourneyDetails.
//Resolution for 189: Continuous Wait Screen
//
//   Rev 1.4   Oct 15 2003 13:30:10   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.3   Sep 11 2003 16:34:14   jcotton
//Made Class Serializable
//
//   Rev 1.2   Aug 20 2003 17:55:52   AToner
//Work in progress
using System;
using System.Collections;
using TransportDirect.Common.Logging;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.ExternalLinkService;
using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for RoadJourneyDetail.
	/// </summary>
	[Serializable()]
	public class RoadJourneyDetail
	{
		private string roadName;
		private string roadNumber;
		private int distance;
		private long duration; // in seconds
		private int turnCount;
		private TurnDirection turnDirection;
		private TurnAngle turnAngle;
		private bool roundabout;
		private bool throughRoute;
		private bool congestionLevel = false;
		private string[] toid;
		private bool stopOver;
		private bool junctionSection;
		private string junctionNumber = string.Empty;
		private JunctionType junctionType;
		private string placeName = string.Empty;
		private bool ferry;
		private bool slipRoad;
		//Added for DEL 7 StopOver Events
		private int tollCost;
		private int congestionZoneCost;
		private int ferryCost;
		private string tollEntry;
		private string congestionZoneEntry;
		private string companyUrl;
		private string congestionZoneExit;
		private string congestionZoneEnd;
		private string ferryCheckIn;
		private bool ferryCheckOut = false;
		private int carCost;
		private RoadJourneyChargeItem roadJourneyChargeItem;
		private RoadJourneyDetailMapInfo roadJourneyDetailMapInfo;
		private bool ViaLocation;
		private bool Wait;
		private bool congestionExit = false;
		private bool congestionEntry = false;
		private bool congestionEnd = false;
		private bool UndefindedWait;
		private string NodeToid;
		private bool DisplayTollIcon = false;
		private bool DisplayFerryIcon = false;
		private bool FerryEntry = false;
		private string tollExit;
		//flag for ambiguous junction
		private bool roadSplits;

		private bool junctionDriveSectionWithoutJunctionNo;

        private string limitedAccessText = string.Empty;

        // Used for travel news incidents affecting this detail
        private List<string> travelNewsIncidentID = new List<string>();
        
		private static readonly string ROAD_NUMBER_FERRY = "FERRY";

		private static readonly string ROAD_NAME_SLIPROAD = "Sliproad";
		private static readonly string ROAD_NAME_SLIP_ROAD = "Slip Road";

		private const string CONGESTION_LEVEL = "JourneyDetailsCongestionWarning.Value";
        private const string NULL_TOLL_LINK = "JourneyDetailsNullTollLink.Value";


		/// <summary>
		/// Take a CJP "section" and create a RoadJourneyDetail
		/// </summary>
		/// <param name="section">TransportDirect.JourneyPlanning.CJPInterface.Section The section to use
		/// for the RoadJourneyDetail information</param>
		public RoadJourneyDetail( Section cjpSection )
		{
			duration = cjpSection.time.Ticks / TimeSpan.TicksPerSecond;
			IExternalLinks currentExternalLinksInstance = ExternalLinks.Current;
            string nullTollLink = (Properties.Current[NULL_TOLL_LINK]);

			// Is this a stop over section, drive section or a junction drive section?
			Type sectionType = cjpSection.GetType();

			if( sectionType == typeof( StopoverSection ) )
			{
				// Stop Over section information
				StopoverSection thisSection = cjpSection as StopoverSection;
				
				roadName = (thisSection.name != null ? thisSection.name : string.Empty);
				
				if (thisSection.node == null  || thisSection.node.TOID == null)
				{
					nodeToid = string.Empty;

					Logger.Write(new OperationalEvent(  TDEventCategory.Infrastructure,
						TDTraceLevel.Error, String.Format("No Node or Node TOID was found for a StopoverSection of name: " + thisSection.name + " and type: " + thisSection.type)));
				}
				else
				{
					nodeToid = thisSection.node.TOID;

				}
				//Creates a RoadJourneyDetailMapInfo object using first and last toids. 
				//Used for Del 7 drive section maps.
				string First = String.Empty;
				string Last = String.Empty;
				roadJourneyDetailMapInfo = new RoadJourneyDetailMapInfo(First, Last);
				
				stopOver = true;
				junctionSection = false;
				ferry = false;
				slipRoad = false;

				tollCost = thisSection.toll;

				//Need to establish what type of Stopover it is
				
				//CongestionZoneEntry
				if (thisSection.type == StopoverSectionType.CongestionZoneEntry)
				{
					congestionZoneCost = thisSection.toll;
					congestionZoneEntry = thisSection.name;
					CongestionEntry = true;

					if (thisSection.sectionFeatures.Length !=0) 
					{
						//need to extract company name and URL from stopoversection section features	
						foreach (SectionFeature sec in thisSection.sectionFeatures) 
						{
							TextSectionFeature tsec = (TextSectionFeature) sec;
							switch (tsec.id)
							{
								case 0:		//Extract company name
									break;
								case 13:	//Extract url of website
									ExternalLinkDetail url = currentExternalLinksInstance.FindUrl(tsec.value.Trim());
									companyUrl = tsec.value;
									break;

							} 
						}
							
						//Create a RoadJourneyChargeItem for this section - used for Costs page
						// IR 3518 - appended stopovertype
						roadJourneyChargeItem = new RoadJourneyChargeItem(congestionZoneEntry, companyUrl, congestionZoneCost, thisSection.type);							
					}
				}

				//CongestionZoneExit
				if (thisSection.type == StopoverSectionType.CongestionZoneExit)
				{

					CongestionExit = true;

					congestionZoneExit = thisSection.name;
					congestionZoneCost = thisSection.toll;
				
					if (thisSection.sectionFeatures.Length !=0) 
					{
						//need to extract company name and URL from stopoversection section features	
						foreach (SectionFeature sec in thisSection.sectionFeatures) 
						{
							TextSectionFeature tsec = (TextSectionFeature) sec;
							switch (tsec.id)
							{
								case 0:		//Extract company name
									//congestionZoneExit = tsec.value;
									break;
								case 13:		//Extract url of website
									ExternalLinkDetail url = currentExternalLinksInstance.FindUrl(tsec.value.Trim());
									companyUrl = tsec.value;
									break;
							}
						}

						roadJourneyChargeItem = new RoadJourneyChargeItem(congestionZoneExit, companyUrl, congestionZoneCost, thisSection.type);							
					}
				}

				// this section to be uncommented when Atkins implement the zone end 
				//CongestionZoneEnd
				if (thisSection.type == StopoverSectionType.CongestionZoneEnd)
				{

					CongestionEnd = true;

					congestionZoneEnd = thisSection.name;
					congestionZoneCost = thisSection.toll;
				
					if (thisSection.sectionFeatures.Length !=0) 
					{
						//need to extract company name and URL from stopoversection section features	
						foreach (SectionFeature sec in thisSection.sectionFeatures) 
						{
							TextSectionFeature tsec = (TextSectionFeature) sec;
							switch (tsec.id)
							{
								case 0:		//Extract company name
									break;
								case 13:		//Extract url of website
									ExternalLinkDetail url = currentExternalLinksInstance.FindUrl(tsec.value.Trim());
									companyUrl = tsec.value;
									break;
							}
						}

						roadJourneyChargeItem = new RoadJourneyChargeItem(congestionZoneEnd, companyUrl, congestionZoneCost, thisSection.type);							
					}
				}



				//FerryCheckIn
				if (thisSection.type == StopoverSectionType.FerryCheckIn)
				{
					ferryCost = thisSection.toll;
					ferryCheckIn = thisSection.name;
					DisplayFerryIcon = true;
					FerryEntry = true;
				
					if (thisSection.sectionFeatures.Length !=0) 
					{
						string ferryRouteUrl = string.Empty;
						//need to extract company name and URL from stopoversection section features	
						foreach ( SectionFeature sec in thisSection.sectionFeatures) 
						{
							TextSectionFeature tsec = (TextSectionFeature) sec;
							switch (tsec.id)
							{
								case 0:		//Extract company name
									break;
								case 13:		//Extract url of website
									ExternalLinkDetail url = currentExternalLinksInstance.FindUrl(tsec.value.Trim());
									companyUrl = tsec.value;
									break;
								case 14:		
									ferryRouteUrl = tsec.value;
									break;
							}

							// The companyUrl property actually exposes either the ferry route Url or the company Url.
							// The ferry route Url takes precedence.
							
							if (ferryRouteUrl != null && ferryRouteUrl.Length > 0)
								companyUrl = ferryRouteUrl;
						}
												
						//Create a RoadJourneyChargeItem for this section - used for Costs page
						//IR 3518 - Appended stopovertype
						roadJourneyChargeItem = new RoadJourneyChargeItem(ferryCheckIn, companyUrl, ferryCost, thisSection.type);
						
					}
				}

				//FerryCheckOut
				if (thisSection.type == StopoverSectionType.FerryCheckOut)
				{
					//Cost not displayed on exiting a ferry
					ferryCheckOut = true;
					DisplayFerryIcon = true;

				}

				//TollEntry
				if (thisSection.type == StopoverSectionType.TollEntry)
				{
					tollCost = thisSection.toll;
					tollEntry = thisSection.name;
					DisplayTollIcon = true;
				
					if (thisSection.sectionFeatures.Length !=0) 
					{
						//need to extract company name and URL from stopoversection section features	
						foreach (SectionFeature sec in thisSection.sectionFeatures) 
						{
							TextSectionFeature tsec = (TextSectionFeature) sec;
							switch (tsec.id)
							{
								case 0:		//Extract company name
									break;
								case 13:		//Extract url
                                    if (tsec.value == nullTollLink)
                                    {
                                        companyUrl = string.Empty;
                                    }
                                    else
                                    {
                                        companyUrl = tsec.value;
                                    }
									break;
							}
						}
						
						// IR 3518 - append stopovertype
						roadJourneyChargeItem = new RoadJourneyChargeItem(tollEntry, companyUrl, tollCost, thisSection.type);
						
					}
				}

				//TollExit
				if (thisSection.type == StopoverSectionType.TollExit)
				{
					tollCost = thisSection.toll;
					tollExit = thisSection.name;
					DisplayTollIcon = true;
				
					if (thisSection.sectionFeatures.Length !=0) 
					{
						//need to extract company name and URL from stopoversection section features	
						foreach (SectionFeature sec in thisSection.sectionFeatures) 
						{
							TextSectionFeature tsec = (TextSectionFeature) sec;
							switch (tsec.id)
							{
								case 0:		//Extract company name
									break;
								case 13:		//Extract url
                                    if (tsec.value == nullTollLink)
                                    {
                                        companyUrl = string.Empty;
                                    }
                                    else
                                    {
                                        companyUrl = tsec.value;
                                    }
                                    break;
							}

						}
						// IR 3518 - append stopovertype
						roadJourneyChargeItem = new RoadJourneyChargeItem(tollExit, companyUrl, tollCost, thisSection.type);
						
					}
				}

				//If it is a Via Section
				if (thisSection.type == StopoverSectionType.Via)
				{

					roadName = "VIALOCAION";
					ViaLocation = true;
					
				}

				if (thisSection.type == StopoverSectionType.Wait)
				{
					//If it is a wait type, we need to add the time to the journey duration
					//but we don't want to display any journey directions for the wait.
					roadName = "WAIT";
					Wait = true;

				}

				if (thisSection.type == StopoverSectionType.UndefinedWait)
				{
					UndefindedWait = true;					
				}
			}

			else if( sectionType == typeof( DriveSection ) )
			{
				// Drive section
				DriveSection thisSection = cjpSection as DriveSection;
				
				roadName = (thisSection.name != null ? thisSection.name : string.Empty);
				roadNumber = (thisSection.number != null ? thisSection.number : string.Empty);
				
				// Don't include Ferry Journeys in the total distance, only in
				// total time. Updated to include check for road number in addition to road name. 
				if (roadNumber != ROAD_NUMBER_FERRY) 
				{
					distance = thisSection.distance;
					ferry = false;
				}
				else
				{
					// Section is a ferry section - set flag for formatters
					ferry = true;
				}

				slipRoad = checkSlipRoad(roadName);

				turnCount = thisSection.turnCount;
				turnDirection = thisSection.turnDirection;
				turnAngle = thisSection.turnAngle;
				roundabout = thisSection.roundabout;
				throughRoute = thisSection.throughRoute;

				//flag for ambiguous junction
				roadSplits = thisSection.roadSplits;
				
				//Need to loop through every link to check for congestion levels
				int congestionlevel = Convert.ToInt32(Properties.Current[CONGESTION_LEVEL]);
				int i;
				toid = new string[thisSection.links.Length];
				for ( i = 0; i < thisSection.links.Length; i++)
				{
					toid[i] = thisSection.links[i].TOID;

					if (thisSection.links[i].congestion > congestionlevel)
					{
						congestionLevel = true;
					}
				}

				//Creates a RoadJourneyDetailMapInfo object using first and last toids. 
				string First = thisSection.links[0].TOID.ToString();
				string Last = thisSection.links[(thisSection.links.Length -1)].TOID.ToString();
				roadJourneyDetailMapInfo = new RoadJourneyDetailMapInfo(First, Last);

				placeName = thisSection.heading;
				carCost = thisSection.cost;
				
				junctionSection = false;
				stopOver = false;
			}

			else if ( sectionType == typeof ( JunctionDriveSection ) )
			{
				// Junction Drive Section
				JunctionDriveSection thisSection = cjpSection as JunctionDriveSection;
				
				roadName = (thisSection.name != null ? thisSection.name : string.Empty);
				roadNumber = (thisSection.number != null ? thisSection.number : string.Empty);
		
				if (roadNumber != ROAD_NUMBER_FERRY) 
				{
					distance = thisSection.distance;
					ferry = false;
				}
				else
				{
					// Section is a ferry section - set flag for formatters
					ferry = true;
				}
				
				// Treat all junction sections as slip roads
				slipRoad = true;
				
				turnCount = thisSection.turnCount;
				turnDirection = thisSection.turnDirection;
				turnAngle = thisSection.turnAngle;
				roundabout = thisSection.roundabout;
				throughRoute = thisSection.throughRoute;

				//flag for ambiguous junction
				roadSplits = thisSection.roadSplits;
				
				//Need to loop through every link to check for congestion levels
				int i;
				toid = new string[thisSection.links.Length];
				int congestionlevel = Convert.ToInt32(Properties.Current[CONGESTION_LEVEL]);
				for ( i = 0; i < thisSection.links.Length; i++)
				{
					toid[i] = thisSection.links[i].TOID;

					if (thisSection.links[i].congestion > congestionlevel)
					{
						congestionLevel = true;
					}
				}

		
				//Creates a RoadJourneyDetailMapInfo object using first and last toids. 
				string First = thisSection.links[0].TOID.ToString();
				string Last = thisSection.links[(thisSection.links.Length -1)].TOID.ToString();
				roadJourneyDetailMapInfo = new RoadJourneyDetailMapInfo(First, Last);
				
				//toid = thisSection.links[0].TOID.ToString();
				placeName = thisSection.heading;
				carCost = thisSection.cost;
				
				// Although dealing with JunctionDriveSection, if no junction number 
				// present then need to override and treat as normal DriveSection.
				junctionNumber = thisSection.junctionNumber;
				junctionSection = ((junctionNumber != null)
					&& (junctionNumber.Length != 0));
				
				junctionType = thisSection.type;

				junctionDriveSectionWithoutJunctionNo = (junctionNumber == null) || (junctionNumber.Length == 0);

				stopOver = false;

			}

		}

		/// <summary>
		/// formatRoadNameString
		/// Defensive formatting code
		/// No guarantee over how slip road data is returned so try 
		/// to reduce potential for inconsistent data in road name output.
		/// </summary>
		/// <param name="roadName">unformatted string</param>
		/// <returns>formatted string</returns>
		private string formatRoadNameString (string roadName)
		{
			// 1.Check for presence of "Sliproad" at beginning of string
			// Convert to "Slip Road"

			roadName = roadName.Replace(ROAD_NAME_SLIPROAD, ROAD_NAME_SLIP_ROAD);
				
			// 2. Check for presence of "+" 
			// Convert to "-" e.g. "Slip Road - Start of Motorway"
			roadName = roadName.Replace("+", "-");
		
			return roadName;

		}

		/// <summary>
		/// Function parses the road number string to see if it 
		/// contains the "slip road" string. Returns bool value 
		/// accordingly.
		/// </summary>
		/// <param name="roadNumber">string to be parsed</param>
		/// <returns>true if road number contains "slip road"</returns>
		private bool checkSlipRoad (string roadName)
		{
			// Convert to upper case for consistency
			string s = roadName.ToUpper();
			// Check if string starts with "Slip Road". Need to avoid situations like "Ruislip Road"
			if (s.StartsWith(ROAD_NAME_SLIP_ROAD.ToUpper()))
				return true;
			else
				return false;
		}

		public RoadJourneyDetailMapInfo RoadJourneyDetailMapInfo
		{
			get {return roadJourneyDetailMapInfo; }
		}

		public string RoadName
		{
			get { return roadName; }
		}	

		public string RoadNumber 
		{
			get { return roadNumber; }
		}	

		public int Distance
		{
			get { return distance; }
		}	

		public long Duration
		{
			get { return duration; }
		}	

		public int TurnCount
		{
			get { return turnCount; }
		}

		public TurnDirection Direction
		{
			get { return turnDirection; }
		}

		public TurnAngle Angle
		{
			get { return turnAngle; }
		}

		public bool Roundabout
		{
			get { return roundabout; }
		}

		public bool ThroughRoute
		{
			get { return throughRoute; }
		}

		public bool CongestionLevel
		{
			get { return congestionLevel; }
		}

		public string[] Toid
		{
			get { return toid; }
		}
                
        
		public bool IsStopOver
		{
			get { return stopOver; }
		}

		public bool IsFerry
		{
			get { return ferry; }
		}

		public bool IsSlipRoad
		{
			get { return slipRoad; }
		}

		public bool IsJunctionSection
		{
			get { return junctionSection; }
		}
		
		public string JunctionNumber
		{
			get { return junctionNumber; }
		}
		
		public JunctionType JunctionAction
		{
			get { return junctionType; }
		}
		
		public string PlaceName
		{
			get { return placeName; }
		}

		//For Del 7
		public int TollCost
		{
			get { return tollCost; }
		}

		public int CongestionZoneCost
		{
			get { return congestionZoneCost; }
		}

		public int FerryCost
		{
			get { return ferryCost; }
		}

		public string TollEntry
		{
			get { return tollEntry; }
		}

		public string CongestionZoneEntry
		{
			get { return congestionZoneEntry; }
		}

		public string CongestionZoneExit
		{
			get { return congestionZoneExit; }
		}

		public string CongestionZoneEnd
		{
			get { return congestionZoneEnd; }
		}

		public string FerryCheckIn
		{
			get { return ferryCheckIn; }
		}

		public bool FerryCheckOut
		{
			get { return ferryCheckOut; }
		}

		public string CompanyUrl
		{
			get { return companyUrl; }
		}

		public int CarCost
		{
			get { return carCost; }
		}

		public RoadJourneyChargeItem ChargeItem
		{
			get { return roadJourneyChargeItem; }
		}

		public bool viaLocation
		{
			get { return ViaLocation;  }
			set { ViaLocation = value; }
		}

		public bool wait
		{
			get { return Wait;  }
			set { Wait = value; }
		}

		public bool CongestionExit
		{
			get { return congestionExit;  }
			set { congestionExit = value; }
		}

		public bool CongestionEntry
		{
			get { return congestionEntry;  }
			set { congestionEntry = value; }
		}

		public bool CongestionEnd
		{
			get { return congestionEnd;  }
			set { congestionEnd = value; }
		}

		public bool undefindedWait
		{
			get { return UndefindedWait; }
			set { UndefindedWait = value; }
		}

		public string nodeToid
		{
			get { return NodeToid; }
			set { NodeToid = value; }
		}

		public bool displayTollIcon
		{
			get { return DisplayTollIcon; }
			set { DisplayTollIcon = value; }
		}

		public bool displayFerryIcon
		{
			get { return DisplayFerryIcon; }
			set { DisplayFerryIcon = value; }
		}

		public string TollExit
		{
			get { return tollExit; }
		}

		public bool ferryEntry
		{
			get { return FerryEntry; }
			set { FerryEntry = value; }
		}

		/// <summary>
		/// Readonly. Flag for ambiguous junction.		
		/// </summary>
		public bool RoadSplits
		{
			get { return roadSplits; }			
		}

        public string LimitedAccessText
        {
            get { return limitedAccessText; }
            set { limitedAccessText = value; }
        }

		/// <summary>
		/// Readonly. Flag for junction section without junction no.		
		/// </summary>
		public bool JunctionDriveSectionWithoutJunctionNo
		{
			get { return junctionDriveSectionWithoutJunctionNo; }			
		}

        
        /// <summary>
        /// Read/Write. List of Travel News Incident IDs affecting this detail
        /// </summary>
        public List<string> TravelNewsIncidentIDs
        {
            get { return travelNewsIncidentID; }
            set { travelNewsIncidentID = value; }
        }

        
	}
}
