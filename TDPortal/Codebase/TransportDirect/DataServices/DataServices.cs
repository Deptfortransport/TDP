// ***********************************************
// NAME 		: DataServices.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 20-Aug-2003
// DESCRIPTION 	: Data Services.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/DataServices.cs-arc  $
//
//   Rev 1.9   Dec 07 2012 15:58:02   dlane
//New find nearest TDAN page
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Nov 15 2012 14:07:18   DLane
//Addition of accessibility options to journey plan input and user profile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Feb 07 2012 12:43:56   DLane
//Check in part 1 for  BatchJourneyPlanner - edited classes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.6   Oct 05 2010 10:44:04   apatel
//Updated to add overload method to read categorised data with extra data
//Resolution for 5614: Rail Search By Price - Invalid day return tickets shown
//
//   Rev 1.5   Sep 21 2009 14:48:14   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Jan 11 2009 16:56:58   mmodi
//Updated for ZPBO data sets
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.3   Oct 13 2008 16:45:04   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.2   Oct 10 2008 15:42:28   mmodi
//Added UnitsSpeedDrop
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Jul 28 2008 13:03:04   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Jul 18 2008 13:50:00   mmodi
//Added a cycle journey type
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 10 2008 15:16:02   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.0   Nov 08 2007 12:20:46   mturner
//Initial revision.
//
//   Rev Devfactory Jan 08 2008 11:22:12 apatel
//   modified enum dataservices.DataServiceType to add new enum NewsIncidentTypeDrop
//
//   Rev 1.83   Mar 06 2007 13:43:42   build
//Automatically merged from branch for stream4358
//
//   Rev 1.82.1.0   Mar 02 2007 10:54:20   asinclair
//Updated CategorisedHashData to allow an extra column on CatergoriesHashes table
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.82   Jan 17 2007 17:58:56   mmodi
//Added Feedback status dataset
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.81   Jan 12 2007 14:07:44   mmodi
//Added Feedback search type dataset
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.80   Jan 08 2007 10:18:34   mmodi
//Added new Feedback datasets
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.79   Dec 07 2006 14:37:58   build
//Automatically merged from branch for stream4240
//
//   Rev 1.78.1.0   Nov 24 2006 11:41:06   mmodi
//Added Journey emissions fuel consumption list
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.78   Apr 05 2006 15:43:04   build
//Automatically merged from branch for stream0030
//
//   Rev 1.77.1.0   Mar 23 2006 10:14:28   mdambrine
//Addition of findabuscheck
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.77   Mar 13 2006 16:58:36   echan
//stream3353 manual merge
//
//   Rev 1.76   Dec 13 2005 11:32:02   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.75.1.0   Nov 28 2005 10:00:12   asinclair
//Added BusinessLinksLocation
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.75.2.2   Feb 03 2006 14:16:08   pcross
//Added new enumeration for new dropdown list for adjust increments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.75.2.1   Jan 13 2006 14:17:26   asinclair
//Added ExtendTransportOptions
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.75.2.0   Dec 15 2005 12:45:16   pcross
//Added new enumerations for dropdownlists required by extend journey
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.75   Nov 17 2005 11:51:10   ECHAN
//Manual merge for stream2880
//
//   Rev 1.74   Nov 09 2005 16:37:20   RPhilpott
//Merge for stream2818
//
//   Rev 1.73   Oct 31 2005 11:04:00   tolomolaiye
//Merge of stream 2638
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.72   Sep 29 2005 17:39:32   build
//Automatically merged from branch for stream2610
//
//   Rev 1.71.3.0   Sep 21 2005 10:18:06   halkatib
//Added White LabelPartnerID Dataservice
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.71   Jul 05 2005 12:23:50   NMoorhouse
//Code merge (stream2560 -> trunk)
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.70   Jun 27 2005 18:22:34   passuied
//added new DataServices Type MobileTimeRequestDrop for mobile UI
//Resolution for 2561: Del 8 Stream: Create WAP Prototype pages
//
//   Rev 1.69   Mar 30 2005 15:17:42   RPhilpott
//Only output a warning, instead of writing an error and throwing an exception, if properties for an enumertaion entry are missing. This allows different sets of dataservice entries to be loaded in different environments. 
//
//   Rev 1.68   Mar 23 2005 15:05:40   jgeorge
//Corrected irritating spelling mistake
//
//   Rev 1.67   Mar 17 2005 17:13:52   rgeraghty
//Added PartitionDrops
//
//   Rev 1.66   Mar 17 2005 16:21:54   RPhilpott
//Temp fix for temp fix ...
//
//   Rev 1.65   Mar 17 2005 16:08:00   rgeraghty
//commented out exception in LoadDataCache
//
//   Rev 1.64   Mar 17 2005 11:03:02   RPhilpott
//Added DisplayableSupplements
//
//   Rev 1.63   Mar 14 2005 18:48:24   rhopkins
//Added DataServicesTypes for SingleTicketTypeDrop and ReturnTicketTypeDrop
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.62   Mar 02 2005 15:27:50   RAlavi
//Car costing changes
//
//   Rev 1.61   Feb 24 2005 14:07:28   schand
//Added entry for Added Lookup_Travelline_TravelNews_Region
//
//   Rev 1.60   Feb 18 2005 14:43:32   tmollart
//Removes SearchTypeDrop enum, added AdultChildRadioList enum.
//
//   Rev 1.59   Feb 16 2005 17:05:30   RAlavi
//Removed WalkingTimeDrop
//
//   Rev 1.58   Feb 09 2005 13:47:52   RAlavi
//Updated version
//
//   Rev 1.57   Feb 09 2005 10:59:40   RAlavi
//Updated version
//
//   Rev 1.56   Jan 27 2005 15:24:06   ralavi
//Updated version
//
//   Rev 1.55   Jan 27 2005 15:17:06   asinclair
//Added UnitsDrop
//
//   Rev 1.54   Jan 27 2005 11:35:08   ralavi
//Updated version
//
//   Rev 1.53   Jan 18 2005 10:30:00   tmollart
//Added DateFlexbilityDrop enum for FindAFare.
//
//   Rev 1.52   Jan 17 2005 12:04:06   tmollart
//Added SearchTypeDrop enum for FindAFare.
//
//   Rev 1.51   Jan 06 2005 14:23:54   tmollart
//Added AdultChildDrop to DataServiceType enum for Find A Fare preferences control.
//
//   Rev 1.50   Dec 21 2004 18:22:44   rgreenwood
//Added FuelCostsDrop DataServiceType for Car Costing
//
//   Rev 1.49   Oct 29 2004 16:43:44   rgreenwood
//Added UserSurveyContactRadio enum type
//
//   Rev 1.48   Oct 29 2004 15:03:54   jmorrissey
//Q6 check boxes do not need to use data services. Removed entries.
//
//   Rev 1.47   Oct 27 2004 12:35:42   rgreenwood
//Changed UserSurveyQ13Drop to UserSurveyQ13Radio
//
//   Rev 1.46   Oct 27 2004 11:00:08   jmorrissey
//Added UserSurveyQ13Drop
//
//   Rev 1.45   Oct 19 2004 16:57:04   jmorrissey
//Added overloads of LoadListControl and GetTExt methods that take additional parameter of type ResourceManager. This allows the non-standard resource manager to be used for string look ups in resx files.
//
//   Rev 1.44   Oct 19 2004 16:36:56   rgreenwood
//Added UserSurveyCheck1-3 declarations.
//
//   Rev 1.43   Oct 14 2004 20:08:02   rgreenwood
//Added UserSurveyQ2 - Q15 Enums
//
//   Rev 1.42   Oct 14 2004 12:30:02   jmorrissey
//UserSurveyQ1Drop
//
//   Rev 1.41   Aug 26 2004 10:15:40   passuied
//Adding a extra DataSet in DataServices to have a different Search Type check box list in FindCarInput page
//Resolution for 1441: Find A Car : Add extra station/Airport search type in location selection
//
//   Rev 1.40   Jul 14 2004 15:40:44   jgeorge
//Added new DataServiceType for Find A...
//
//   Rev 1.39   Jul 13 2004 13:38:08   jmorrissey
//Added StationTypesCheck to DataServiceType enum
//
//   Rev 1.38   Jul 12 2004 17:54:34   JHaydock
//DEL 5.4.7 Merge: IR 1062
//
//   Rev 1.37   May 10 2004 12:16:16   passuied
//addition of a dropdown type
//
//   Rev 1.36   May 10 2004 11:18:32   jgeorge
//Added new services for Find Flight input page/controls. Associated data updates in DUD0030
//
//   Rev 1.35   Mar 31 2004 16:05:28   jgeorge
//Updated to implement IDataService interface
//
//   Rev 1.34   Dec 16 2003 09:52:00   PNorell
//Updated with a few more services.
//
//   Rev 1.33   Nov 11 2003 10:55:22   hahad
//TraceInfo replaced with TraceVerbose, the operational messages on trace level info even though what we log is verbose.
//
//   Rev 1.32   Nov 11 2003 10:42:22   hahad
//Missing ');' - end clause added
//
//   Rev 1.31   Nov 10 2003 15:46:50   hahad
//Logger.Info replaced with Logger.Verbose
//Resolution for 116: Some code still uses obsolete TDExcpetion methods
//
//   Rev 1.30   Nov 10 2003 15:40:30   hahad
//Updated obsolete TDExceptions to include TDExceptionIdentifier
//Resolution for 116: Some code still uses obsolete TDExcpetion methods
//
//   Rev 1.29   Oct 27 2003 20:58:46   acaunt
//Added DisplayableRailTickets enum
//
//   Rev 1.28   Oct 22 2003 17:29:58   acaunt
//Discount coach cards enum added
//
//   Rev 1.27   Oct 17 2003 15:45:52   asinclair
//Added dropdowns for feedback pages
//
//   Rev 1.26   Oct 12 2003 11:42:54   acaunt
//SCL Codes enum value added
//
//   Rev 1.25   Oct 06 2003 21:01:02   acaunt
//NatExCodes enum value added
//
//   Rev 1.24   Sep 23 2003 02:06:22   passuied
//added Selection helper
//
//   Rev 1.23   Sep 22 2003 19:34:36   passuied
//added method GetText()
//
//   Rev 1.22   Sep 21 2003 17:56:14   kcheung
//Added new enum
//
//   Rev 1.21   Sep 19 2003 21:21:10   passuied
//working version up to date
//
//   Rev 1.20   Sep 19 2003 16:38:14   passuied
//added GetResourceId method
//
//   Rev 1.19   Sep 18 2003 16:25:34   passuied
//Added ReturnMonthYearDrop implementation
//
//   Rev 1.18   Sep 18 2003 10:44:16   hahad
//Added ProblemTypeDrop
//
//   Rev 1.17   Sep 17 2003 16:45:52   passuied
//Added method to get the string value from a resourceID
//
//   Rev 1.16   Sep 16 2003 17:49:02   passuied
//add enumtype LocationTypeDrop
//
//   Rev 1.15   Sep 16 2003 15:37:24   passuied
//latest version
//
//   Rev 1.14   Sep 16 2003 13:38:38   passuied
//make it compile
//
//   Rev 1.13   Sep 16 2003 12:23:10   passuied
//changes for integration in Web with ServiceDiscovery
//
//   Rev 1.12   Sep 15 2003 15:38:00   pscott
//add adjusted route
//
//   Rev 1.11   Sep 15 2003 11:29:54   PScott
//Addition of wireframe drop down lists
//
//   Rev 1.10   Sep 10 2003 14:04:04   TKarsan
//Update to dropdown list, added value
//
//   Rev 1.9   Sep 02 2003 16:01:14   TKarsan
//FxCop fixes
//
//   Rev 1.8   Sep 02 2003 15:58:40   TKarsan
//Completed, with test harness.
//
//   Rev 1.7   Sep 02 2003 12:41:22   TKarsan
//Work in progress.
//
//   Rev 1.6   Sep 02 2003 11:18:00   TKarsan
//Work in progress
//
//   Rev 1.5   Sep 01 2003 17:42:00   TKarsan
//Work in progress
//
//   Rev 1.4   Aug 28 2003 17:39:44   TKarsan
//Work in progress
//
//   Rev 1.3   Aug 26 2003 15:27:34   TKarsan
//Work in progress
//
//   Rev 1.2   Aug 26 2003 11:44:30   TKarsan
//Work in progress
//
//   Rev 1.1   Aug 26 2003 11:30:52   TKarsan
//Work in progress
//
//   Rev 1.0   Aug 20 2003 16:19:34   TKarsan
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ResourceManager;

using System.Resources;

using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Enumerations to identify applicable country.
	/// </summary>
	[Flags]
	public enum DataServiceCountries
	{
		EnglandWales = 1,
		Scotland = 2
	}

	#region Categorised Hsh Data
	/// <summary>
	/// Struct like class for holding categorised hash data
	/// (data returned from a Hashtable that has both a value and a category)
	/// </summary>
	public class CategorisedHashData
	{
		private string value = string.Empty;
		private string category = string.Empty;
		private string group = string.Empty;
        private string[] extData = null;


		public CategorisedHashData(string value, string category, string group)
		{
			this.value = value;
			this.category = category;
			this.group = group;
		}

        public CategorisedHashData(string value, string category, string group, string[] extData)
        {
            this.value = value;
            this.category = category;
            this.group = group;
            this.extData = extData;
        }

		public string Value { get {return value;} }
		public string Category { get {return category;} }
		public string Group { get {return group;} }
        public string[] ExtData { get { return extData; } }
	}
	#endregion

	#region DS Drop Item
	/// <summary>
	/// This class contains info for a single item of a dropdown list.
	/// </summary>
	public class DSDropItem
	{
		private string resourceID ;
		private string itemValue  ;
		private bool   isSelected ;

		/// <summary>
		/// Private members are set only through the constructor,
		/// provides public read-only properties.
		/// </summary>
		/// <param name="key">Value used by application.</param>
		/// <param name="description">Resource ID for language translation.</param>
		/// <param name="isSelected">Flag indicating of selected by default.</param>
		public DSDropItem(string resourceID, string itemValue, bool isSelected)
		{
			this.resourceID  = resourceID;
			this.itemValue   = itemValue ;
			this.isSelected  = isSelected;
		}

		public string ResourceID 	{ get { return resourceID; } }
		public string ItemValue  	{ get { return itemValue ; } }
		public bool   IsSelected 	{ get { return isSelected; } }
	}
	#endregion



	/// <summary>
	/// Enumerations to identify a dataset.
	/// </summary>
	public enum DataServiceType
	{
		FromToDrop,
		ChangesFindDrop,
		ChangesSpeedDrop,
		WalkingSpeedDrop,
		WalkingMaxTimeDrop,
		DrivingFindDrop,
		DrivingMaxSpeedDrop,
		LocationTypeDrop,
		TrafficMapDrop,
		CarViaDrop,
		PTViaDrop,
		AltFromToDrop,
		MapTransport,
		MapAccomodation,
		MapSport,
		MapAttractions,
		MapHealth,
		MapEducation,
		MapPublicBuildings,
		MapsForThisJourneyDrop,
		SingleReturnDrop,
		DiscountClassDrop,
		DiscountRailCardDrop,
		DiscountCoachCardDrop,
		LeaveArriveDrop,
		NewsTransportDrop,
		NewsRegionDrop,
		NewsShowTypeDrop,
		NewsShowDetailDrop,
        NewsIncidentTypeDrop, //for travel news incident data type
		FeedbackTypeDrop,
		ComplaintRegardingDrop,
		AdjustedRouteDrop,
		AdjustedUnadjustedDrop,
		BankHolidays,
		PublicTransportsCheck,
		AltOptionsRadio,
		ProblemTypeDrop,
		ReturnMonthYearDrop,
		NatExCodes,
		SCLCodes,
		ProblemOccuredDropDownList,
		ProblemDropDownList,
		InformationDropDownList,
		SuggestionDropDownList,
		DiscountCoachCards,
		DisplayableRailTickets,
        FareTerminalZoneNLC,
        FareTravelcardZoneNLC,
        FareUndergroundZoneNLC,
        FareTravelcardTicketTypes,
		CheckInTimeDrop,
		StopOverTimeDrop,
		OperatorSelectionRadio,
		FindStationLocationDrop,
		FindCarLocationDrop,
		FSCResultsToDisplayDrop,
		StationTypesCheck,
		UserSurveyQ1Drop,
		UserSurveyQ2Radio,
		UserSurveyQ3Check,
		UserSurveyQ4Check,
		UserSurveyQ5Radio,		
		UserSurveyQ6Drop1,
		UserSurveyQ6Drop2,
		UserSurveyQ7Radio,
		UserSurveyQ8Check,
		UserSurveyQ9Radio,
		UserSurveyQ10Check,
		UserSurveyQ11Check,
		UserSurveyQ12Radio,
		UserSurveyQ13Radio,
		UserSurveyQ14Radio,
        UserSurveyQ15Radio,
		UserSurveyQ16Drop,
		UserSurveyContactRadio,
		FuelCostsDrop,
		AdultChildDrop,
		AdultChildRadioList,
		DateFlexibilityDrop,
		ListCarSizeDrop,
		ListFuelTypeDrop,
		FuelConsumptionUnitDrop,
		UnitsDrop,
        UnitsSpeedDrop,
		Lookup_Travelline_TravelNews_Region,
		FuelConsumptionSelectRadio,
		FuelCostSelectRadio,
		SingleTicketTypeDrop,
		ReturnTicketTypeDrop,
		DisplayableSupplements,		
		PartitionDoorDrop,
		PartitionQuickDrop,
		MobileTimeRequestDrop,
		MobileDeviceTypes, // for MobileBookMarkYpe Dropdown
		WhiteLabelPartnerId, // for White Labelling Partners
		VisitPlannerRouteSelection,	//for Visit Planner
		VisitPlannerLengthOfStay,	//for Visit Planner Length of stay drop down box
		VisitPlannerLocationSelection,
		FindLocationGazeteerOptions, // for FindAPlaceDropDown Location Gaz
		FindLocationShowOptions, // for FindAPlaceDropDown show options  
		ReturnsOnly,
		BusinessLinksLocation,
		ExtensionResultsViews,
		FullItineraryExtendPermitted,
		FullItineraryExtendNotPermitted,
		ItinerarySegmentsExtendPermitted,
		ItinerarySegmentsExtendNotPermitted,
		ExtendTransportOptions,
		JourneyAdjustmentTypes,
		FullItineraryAdjust,
		FullItineraryReplanOutward,
		FullItineraryReplanReturn,
		FullItineraryReplanNotPermitted,
		FindABusCheck,
		FuelConsumptionCO2SelectRadio,
		UserFeedbackType,
		UserFeedbackJourneyConfirm,
		UserFeedbackJourneyResult,
		UserFeedbackOtherOptions,
		UserFeedbackSearchType,
		UserFeedbackStatus,
        FindCycleLocationDrop,
        CycleViaLocationDrop,
        CycleJourneyType,
        FindEBCLocationDrop,
        EBCViaLocationDrop,
        AccessibleOptionsRadio,
        AccessibleTransportTypes,
//        BatchJourneyPlannerResultRadio,
        SpecialEvents,
        DataServiceTypeEnd// Should ALWAYS be the last one, represents maximum.        
	} 		

	/// <summary>
	/// The Data Services class.
	/// </summary>
	public class DataServices : IDataServices
	{
		/// <summary>
		/// Maintains a flag indicating of cache has been loaded.
		/// </summary>
		static private volatile bool CacheIsLoaded = false;

		/// <summary>
		/// Holds dataset ID as key and hash/array as value.
		/// </summary>
		static private Hashtable hash = null;

		static private TDResourceManager rm = null;

		#region Constructor
		/// <summary>
		/// Loads the cache if not already loaded in class variables.
		/// </summary>
		public DataServices(TDResourceManager rm)
		{
			if(CacheIsLoaded == false)
			{
				DataServices.rm = rm;
				lock(typeof(DataServices))  // Lock DataServices type at this stage.
				{
					// Test again in case it got loaded when lock was acquired.
					if(CacheIsLoaded == false)
					{
						hash = new Hashtable();
						CacheIsLoaded = true;
						LoadDataCache();
					}
				} // lock
			} // If cache is not loaded
		}
		#endregion

		#region Public Interface
		/// <summary>
		/// Returns an Array-List type object from cache, performs type checking.
		/// </summary>
		/// <param name="item">Enumeration must refer to Array-List type.</param>
		/// <returns>Structure could contain a list of date and dropdown items.</returns>
		public ArrayList GetList(DataServiceType item)
		{
			object o = hash[item];
			if(o.GetType() != typeof(ArrayList)) // Check type for completeness
			{
				string message = "Illegal type, GetList";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
					);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}
			return (ArrayList) ((ArrayList) o).Clone(); // Shallow copy.
		}

		public string GetResourceId (DataServiceType type, string value)
		{
			ArrayList items = GetList(type);

			foreach (DSDropItem item in items)
			{
				if ( item.ItemValue == value)
					return item.ResourceID;
			}
			return string.Empty;
		}

		public string GetValue (DataServiceType type, string resourceId)
		{
			ArrayList list = GetList(type);

			foreach (DSDropItem item in list)
			{
				if (item.ResourceID == resourceId)
					return item.ItemValue;
				
			}
			return string.Empty;
		}

		public string GetText (DataServiceType type, string value)
		{
			ArrayList list = GetList(type);

			string temp = "DataServices." + type.ToString() + ".";

			foreach (DSDropItem item in list)
			{
				if (item.ItemValue == value)
					
				{
					return rm.GetString(
						temp + item.ResourceID, System.Globalization.CultureInfo.CurrentUICulture);
						
				}
				
			}
			return string.Empty;
		}

		/// <summary>
		/// overloaded method allows a different resource manager to be used
		/// </summary>
		/// <param name="type"></param>
		/// <param name="value"></param>		
		/// <param name="rm">The resource manager to use for looking up lang strings</param>
		public string GetText (DataServiceType type, string value, TDResourceManager rm)
		{
			ArrayList list = GetList(type);

			string temp = "DataServices." + type.ToString() + ".";

			foreach (DSDropItem item in list)
			{
				if (item.ItemValue == value)
					
				{
					return rm.GetString(
						temp + item.ResourceID, System.Globalization.CultureInfo.CurrentUICulture);
						
				}
				
			}
			return string.Empty;
		}

		#region Selection Helper
		public void Select(ListControl list, string resourceId)
		{
			for (int i= 0; i< list.Items.Count; i++)
			{
				ListItem item = list.Items[i];
				if (item.Value == resourceId)
				{
					list.SelectedIndex = i;
					break;
				}
			}
		}
		public void SelectInCheckBoxList(CheckBoxList list, string resourceId)
		{
			for (int i= 0; i< list.Items.Count; i++)
			{
				ListItem item = list.Items[i];
				if (item.Value == resourceId)
				{
					item.Selected = true;
					break;
				}
			}
		}
		#endregion

		/// <summary>
		/// Returns an Hash-Table type object from cache, performs type checking.
		/// </summary>
		/// <param name="item">Enumeration must refer to Hash-Table type.</param>
		/// <returns>Structure contains list of key-value pairs.</returns>
		public Hashtable GetHash(DataServiceType item)
		{
			object o = hash[item];
			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, GetHash";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
																				);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}
			return (Hashtable) ((Hashtable) o).Clone(); // Shallow copy.
		}

		/// <summary>
		/// Performs a lookup on a specified hash-table.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public string FindHash(DataServiceType item, string key)
		{
			object o = hash[item]; Hashtable finder;

			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, FindHash";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
																		);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			finder = (Hashtable) o;
			return finder[key].ToString();
		}

		/// <summary>
		/// Performs a lookup on a specified categorised hash-table.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public CategorisedHashData FindCategorisedHash(DataServiceType item, string key)
		{
			object o = hash[item]; Hashtable finder;

			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, FindHash";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
					);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			finder = (Hashtable) o;
			return (CategorisedHashData)finder[key];
		}

		/// <summary>
		/// Returns true if a given date is a bank holiday.
		/// </summary>
		/// <param name="item">Cache data item to test for.</param>
		/// <param name="dateToTest">The date to test.</param>
		/// <param name="country">The country to test for.</param>
		/// <returns>True if date is a bank holiday.</returns>
		public bool IsHoliday(DataServiceType item, TDDateTime dateToTest, DataServiceCountries country)
		{
			object o = hash[item]; Hashtable finder; bool isHoliday = false;

			if(o.GetType() != typeof(Hashtable)) // Check type for completeness
			{
				string message = "Illegal type, FindHash";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
																);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			finder = (Hashtable) o;

			try
			{
				isHoliday = ((DataServiceCountries) finder[dateToTest] & country) == country;
			}
			catch(NullReferenceException)
			{
				// Do nothing, the key was not found.
			}


			return isHoliday;
		}

        /// <summary>
        /// Gets the name (in Content DB) of the message to display for special event text if applicable for date
        /// </summary>
        /// <param name="item">Cache data item to test for.</param>
        /// <param name="dateToTest">The date to test.</param>
        /// <returns>The value or string.empty if not found</returns>
        public string GetSpecialEventMessageName(DataServiceType item, TDDateTime dateToTest)
        {
            object o = hash[item]; Hashtable finder; 

            if (o.GetType() != typeof(Hashtable)) // Check type for completeness
            {
                string message = "Illegal type, FindHash";
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                    TDTraceLevel.Error, message
                                                                );
                Logger.Write(oe);
                throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
            }

            finder = (Hashtable)o;

            try
            {
                return finder[dateToTest].ToString() ;
            }
            catch (NullReferenceException)
            {
                // Return string.Empty, the key was not found.
                return string.Empty;
            }
            
        }



		/// <summary>
		/// Populates a list control with items.
		/// </summary>
		/// <param name="dataSet">The dataset that contains dropdown items.</param>
		/// <param name="control">The control, eg DropDownList, to be populated.</param>
		public void LoadListControl(DataServiceType dataSet, ListControl control)
		{
			ArrayList list;

			object o = hash[dataSet];
			
			if(o.GetType() != typeof(ArrayList)) // Check type for completeness
			{
				string message = "Illegal type, GetList";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
					);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			list = (ArrayList)o;

			string temp; DSDropItem itemCache;


			control.Items.Clear();
			temp = "DataServices." + dataSet.ToString() + ".";

			for (int i=0; i< list.Count; i++)
			{
				ListItem itemWeb = new ListItem();
				itemCache = (DSDropItem) list[i];

				// Get corresponding text from resource file.
				itemWeb.Text = rm.GetString(
					temp + itemCache.ResourceID, System.Globalization.CultureInfo.CurrentUICulture
					);

				itemWeb.Value    = itemCache.ResourceID;

				control.Items.Add(itemWeb); // Append this item to the list.

				if (itemCache.IsSelected)
				{
					if (control is DropDownList)
					{
						((DropDownList)control).SelectedIndex = i;
					}
					else if (control is RadioButtonList)
					{
						((RadioButtonList)control).SelectedIndex = i;
					}
					else
					{
						itemWeb.Selected = true;
					}
				}
			}
		}

		/// <summary>
		/// Overload of method that populates a list control with items.
		/// </summary>
		/// <param name="dataSet">The dataset that contains dropdown items.</param>
		/// <param name="control">The control, eg DropDownList, to be populated.</param>
		/// <param name="rm">The resource manager to use for looking up lang strings</param>
		public void LoadListControl(DataServiceType dataSet, ListControl control, TDResourceManager rm)
		{
			ArrayList list;

			object o = hash[dataSet];
			if(o.GetType() != typeof(ArrayList)) // Check type for completeness
			{
				string message = "Illegal type, GetList";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
					);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSMethodIllegalType);
			}

			list = (ArrayList)o;

			string temp; DSDropItem itemCache;


			control.Items.Clear();
			temp = "DataServices." + dataSet.ToString() + ".";

			for (int i=0; i< list.Count; i++)
			{
				ListItem itemWeb = new ListItem();
				itemCache = (DSDropItem) list[i];

				// Get corresponding text from resource file.
				itemWeb.Text = rm.GetString(
					temp + itemCache.ResourceID, System.Globalization.CultureInfo.CurrentUICulture
					);

				itemWeb.Value    = itemCache.ResourceID;

				control.Items.Add(itemWeb); // Append this item to the list.

				if (itemCache.IsSelected)
				{
					if (control is DropDownList)
					{
						((DropDownList)control).SelectedIndex = i;
					}
					else if (control is RadioButtonList)
					{
						((RadioButtonList)control).SelectedIndex = i;
					}
					else
					{
						itemWeb.Selected = true;
					}
				}
			}
		}


		/// <summary>
		/// Gets the default resource id for a list control data service.
		/// </summary>
		/// <param name="dataSet">The dataset involved</param>
		/// <returns>The value or string.empty if not found</returns>
		public string GetDefaultListControlValue(DataServiceType dataSet)
		{
			ArrayList list = GetList(dataSet) as ArrayList;
			if( list != null )
			{
				for (int i=0; i< list.Count; i++)
				{
					DSDropItem itemCache = (DSDropItem) list[i];
					if( itemCache.IsSelected )
					{
						return itemCache.ItemValue;
					}
				}
			}
			return string.Empty;
		}

		/// <summary>
		/// Gets the default resource id for a list control data service.
		/// </summary>
		/// <param name="dataSet">The dataset involved</param>
		/// <returns>The resource id or string.empty if not found</returns>
		public string GetDefaultListControlResourceId(DataServiceType dataSet)
		{
			ArrayList list = GetList(dataSet) as ArrayList;
			if( list != null )
			{
				for (int i=0; i< list.Count; i++)
				{
					DSDropItem itemCache = (DSDropItem) list[i];
					if( itemCache.IsSelected )
					{
						return itemCache.ResourceID;
					}
				}
			}		
			return string.Empty;
		}

		#endregion

		#region Load Data Cache
		/// <summary>
		/// Loads the datacache from databases.
		/// </summary>
		private void LoadDataCache()
		{
			SqlHelper sql = new SqlHelper();
			SqlDataReader reader;

			IPropertyProvider currProps = Properties.Current;

			int i, max = (int) DataServiceType.DataServiceTypeEnd;
			String parType, parDB, parQuery, parTemp, previousDB = "~#@|<>,.";

			for(i=0; i < max; i++)
			{
				parTemp = "TransportDirect.UserPortal.DataServices."
					+ ((DataServiceType) i).ToString();

				if(TDTraceSwitch.TraceVerbose)
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Verbose, ((DataServiceType) i).ToString())
															);

				parType  = currProps[ parTemp + ".type"  ];
				parDB    = currProps[ parTemp + ".db"    ];
				parQuery = currProps[ parTemp + ".query" ];

				// Ignore nulls
				if  ((parType == null) || (parDB == null) || (parQuery == null))
				{
					string message = "Parameter contains nulls for " + parTemp;
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Warning, message);
					Logger.Write(oe);
					continue;
				}

				// Re-open the database if it is different from previous.
				if(String.Compare(previousDB, parDB, true /* cast-insensitive */, new CultureInfo("en-GB") ) != 0)
				{
					SqlHelperDatabase db = FindDatabase(parDB);

					try // On error log then throw.
					{
						if(sql.ConnIsOpen)
							sql.ConnClose();
						sql.ConnOpen(db);

						if(TDTraceSwitch.TraceVerbose)
							Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Verbose, "Accissing " + db.ToString())
															);
						previousDB = parDB;
					}
					catch(SqlException eSQL) 
					{
						string message = "Error accessing database";
						OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
							TDTraceLevel.Error, message
														);
						Logger.Write(oe);
						throw new TDException(message, eSQL, false, TDExceptionIdentifier.DSDataAccessError);
					}
				} // if the database name is different.

				// Execute query and read data.
				try // On error log then throw.
				{
					ICollection dataset; reader = sql.GetReader(parQuery);

					switch(parType)
					{
						case "1": dataset = ReadList(reader); break;
						case "2": dataset = ReadHash(reader); break;
						case "3": dataset = ReadDrop(reader); break;
						case "4": dataset = ReadDate(reader); break;
						case "5": dataset = ReadCategorisedHash(reader); break;
                        case "6": dataset = ReadCategorisedHashWithAdditionalData(reader); break;
                        case "7": dataset = ReadSpecialEvent(reader); break;
						default:
							string message = "Type is unknown";
							OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Error, message
													);
							Logger.Write(oe);
							throw new TDException(message, false, TDExceptionIdentifier.DSUnknownType);
					}

					reader.Close();
					hash.Add((DataServiceType) i, dataset);

					if(TDTraceSwitch.TraceVerbose)
						Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
							TDTraceLevel.Verbose, "   Size " + dataset.Count.ToString())
										);
				}
				catch(SqlException eSQL) 
				{
					string message = "Error executing query";
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, message
									);
					Logger.Write(oe);
					throw new TDException(message, eSQL, false, TDExceptionIdentifier.DSQueryExecuteError);
				}
			} // For each enum
	
			if(sql.ConnIsOpen)
				sql.ConnClose();
		}
		#endregion

		#region Collection Readers
		/// <summary>
		/// Reads Array-List data for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadList(SqlDataReader reader)
		{
			ArrayList arrayList = new ArrayList(); string temp;

			while(reader.Read())
			{
				temp = reader.GetString(0).Trim();
				arrayList.Add(temp);
			}
			return arrayList;
		}

		/// <summary>
		/// Reads hash-table data for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadHash(SqlDataReader reader)
		{
			Hashtable hash = new Hashtable(); string temp1, temp2;

			while(reader.Read()) 
			{
				temp1 = reader.GetString(0).Trim();
				temp2 = reader.GetString(1).Trim();
				hash.Add(temp1, temp2);
			}
			return hash;
		}

		/// <summary>
		/// Reads hash-table data for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadCategorisedHash(SqlDataReader reader)
		{
			Hashtable hash = new Hashtable(); 
			string temp1, temp2, temp3, temp4;

			while(reader.Read()) 
			{
				temp1 = reader.GetString(0).Trim();
				temp2 = reader.GetString(1).Trim();
				temp3 = reader.GetString(2).Trim();
				temp4 = reader.GetString(3).Trim();
				CategorisedHashData data = new CategorisedHashData(temp2, temp3, temp4);
				hash.Add(temp1, data);
			}
			return hash;
		}

        /// <summary>
        /// Reads hash-table data for the cache with extra data to be read.
        /// </summary>
        /// <param name="reader">Contains items to read.</param>
        /// <returns>Oject to be appended to the list of cached items.</returns>
        private static ICollection ReadCategorisedHashWithAdditionalData(SqlDataReader reader)
        {
            Hashtable hash = new Hashtable();
            string temp1, temp2, temp3, temp4;

            List<string> tempData = new List<string>();

            while (reader.Read())
            {
                temp1 = reader.GetString(0).Trim();
                temp2 = reader.GetString(1).Trim();
                temp3 = reader.GetString(2).Trim();
                temp4 = reader.GetString(3).Trim();
                if (reader.FieldCount > 4)
                {
                    for (int fc = 4; fc < reader.FieldCount; fc++)
                    {
                        if (reader[fc] != DBNull.Value)
                        {
                            tempData.Add(reader.GetString(fc));
                        }
                        else
                        {
                            // In case of null value add empty string to keep the colum order consistant
                            tempData.Add(string.Empty);
                        }
                    }

                }
               
                CategorisedHashData data = new CategorisedHashData(temp2, temp3, temp4,tempData.ToArray());
                
                hash.Add(temp1, data);
                tempData.Clear();
            }
            return hash;
        }

		/// <summary>
		/// Reads data for the dropdown-list for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadDrop(SqlDataReader reader)
		{
			string resourceID, itemValue; bool isSelected;
			ArrayList arrayList = new ArrayList();

			while(reader.Read()) 
			{
				resourceID = reader.GetString(0).Trim();
				itemValue  = reader.GetString(1);
				isSelected = reader.GetInt32(2) != 0;

				if(itemValue != null)
					itemValue = itemValue.Trim();

				DSDropItem item = new DSDropItem(
					resourceID, itemValue, isSelected
						);

				arrayList.Add(item);
			}

			return arrayList;
		}

		/// <summary>
		/// Reads data for the bank-holidays for the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadDate(SqlDataReader reader)
		{
			Hashtable hash = new Hashtable();
			TDDateTime holiday; DataServiceCountries country;

			while(reader.Read())
			{
				holiday = reader.GetDateTime(0);
				country = (DataServiceCountries) reader.GetInt32(1);
				hash.Add(holiday, country);
			}
			return hash;
		}

        		/// <summary>
		/// Reads data for special events affecting transport from the cache.
		/// </summary>
		/// <param name="reader">Contains items to read.</param>
		/// <returns>Oject to be appended to the list of cached items.</returns>
		private static ICollection ReadSpecialEvent(SqlDataReader reader)
		{
			Hashtable hash = new Hashtable();
			TDDateTime eventDate; string messageText;

			while(reader.Read())
			{
				eventDate = reader.GetDateTime(0);
				messageText = reader.GetString(1);
				hash.Add(eventDate, messageText);
			}
			return hash;
		}

		#endregion

		#region Find Database
		/// <summary>
		/// Converts a string to SqlHelperDatabase enumeration value.
		/// </summary>
		/// <param name="DBName"></param>
		/// <returns></returns>
		private static SqlHelperDatabase FindDatabase(string DBName)
		{
			int i, db = -1, max = (int) SqlHelperDatabase.SqlHelperDatabaseEnd;

			for(i=0; i < max; i++) // Find matching enum strings
			{
				if(String.Compare(((SqlHelperDatabase) i).ToString(), DBName, true, new CultureInfo("en-GB")) == 0)
				{
					db = i;
					break;
				}
			} // for each db in SqlHelper

			if(db == -1)
			{
				string message = "SqlHelper database not found";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message
						);
				Logger.Write(oe);
				throw new TDException(message, false, TDExceptionIdentifier.DSSQLHelperDatabaseNotFound);
			}

			return (SqlHelperDatabase) db;
		}
		#endregion
	}
}
