// ***********************************************
// NAME 		: FavouriteJourney.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 01/12/2003
// DESCRIPTION 	: A representation for favourite journeys.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FavouriteJourney.cs-arc  $
//
//   Rev 1.7   Mar 11 2013 14:21:38   mmodi
//Updated to load Locality location corretly, not populating its naptans
//Resolution for 5898: Favourite journey with locality location does not plan journey
//
//   Rev 1.6   Jan 30 2013 10:35:28   mmodi
//Updated to save Telecabine in favourite journey
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.5   Jan 20 2013 16:26:26   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.4   Dec 11 2012 14:00:50   mmodi
//Save favourite journey accessible options 
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Sep 27 2012 14:45:52   mmodi
//Updated logging of location being built
//Resolution for 5852: Gaz - Favourite journeys are not displayed
//
//   Rev 1.2   Mar 14 2011 15:11:58   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.1   Oct 13 2008 16:46:38   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.1   Oct 10 2008 15:47:14   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.0   Sep 02 2008 11:38:32   mmodi
//Updated to hold cycle journey parameters
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:48:22   mturner
//Initial revision.
//
//   Rev 1.13   Mar 09 2006 15:52:26   pscott
//SCR3592 Save favourite Journey Changes
//vantives 3976884/4171275
//
//   Rev 1.12   Apr 29 2005 17:14:30   Ralavi
//Created a new TDRoad object for UseRoad to ensure that useRoads are displayed for favourite journeys.
//
//   Rev 1.11   Apr 08 2005 17:07:54   PNorell
//Fix for favourite journes backwards compatibility.
//Resolution for 1991: Del 7  - unable to plan journey if logged in as a user registered pre-del 7
//
//   Rev 1.10   Apr 06 2005 16:32:26   PNorell
//Updated for FX Cops.
//
//   Rev 1.9   Apr 04 2005 10:48:38   PNorell
//Fix for IR1991.
//
//FavouriteJourney error.
//Resolution for 1991: Del 7  - unable to plan journey if logged in as a user registered pre-del 7
//
//   Rev 1.8   Feb 24 2005 11:37:30   PNorell
//Updated for favourite details.
//
//   Rev 1.7   Aug 17 2004 14:54:58   jbroome
//IR 1368 - Problem with Favourite Journeys. Set correct value of RequestPlaceType on the TDLocation object.
//
//   Rev 1.6   Aug 02 2004 13:37:02   COwczarek
//Replace hardcoded key values with constants defined in ProfileKeys class.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.5   Apr 28 2004 16:19:48   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.4   Feb 06 2004 10:37:24   PNorell
//Fix for the if statement for previous fix to include all stations as well as it should have.
//
//   Rev 1.3   Feb 05 2004 15:05:50   PNorell
//Updated and corrected the problems with Public Vias (which also was a problem in Private Vias but did not show as default gazetteer was coordinate based for them).
//
//   Rev 1.2   Jan 23 2004 16:27:12   PNorell
//Removed public vias, again.
//
//   Rev 1.1   Jan 23 2004 16:03:50   PNorell
//Favourite journey updates.
//
//   Rev 1.0   Jan 21 2004 10:42:48   PNorell
//Initial Revision

using System;
using System.Collections;
using System.Globalization;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.ReportDataProvider.TDPCustomEvents;

using TransportDirect.UserPortal.LocationService;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Favourite journey encapsulates all nessecary information for handling favourite journeys.
	/// </summary>
	public class FavouriteJourney
	{

		#region Event handleers
		/// <summary>
		/// Event fired after this favourite journey's GUID has changed and been saved to the profile store. 
		/// This currently only happens the first time it is changed.
		/// </summary>
		public event EventHandler GuidChanged;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a favourite journey given an index
		/// </summary>
		/// <param name="index">The index (1 - MAX) inclusive</param>
		public FavouriteJourney(int index) : this( index, null )
		{
			// No guid set initially
		}

		/// <summary>
		/// Creates a favourite journey with an index and a given GUID.
		/// </summary>
		/// <param name="index">The index (1 - MAX) inclusive</param>
		/// <param name="guid">The GUID</param>
		public FavouriteJourney(int index, string guid)
		{
			this.index = index;
			this.guid = guid;
			// Load journey details
		}
		#endregion

        #region Constant definitions

        /// <summary>
        /// The display name format pattern.
        /// </summary>
        private const string DISPLAYNAME_FORMAT = "{0}. {1}";

		#endregion

		#region Public Properties
		/// <summary>
		/// The GUID
		/// </summary>
		private string guid = null;

		/// <summary>
		/// Gets the GUID for this journey
		/// </summary>
		public string Guid
		{
			get { return guid; }
		}

		/// <summary>
		/// The index number for this journey
		/// </summary>
		private int index = 0;
		/// <summary>
		/// Gets the index number for this journey (1 - MAX) inclusive
		/// </summary>
		public int Index
		{
			get { return index; }
		}



		/// <summary>
		/// Gets the display name.
		/// </summary>
		public string DisplayName
		{
			get { return string.Format( DISPLAYNAME_FORMAT, (index), name ); }
		}

		/// <summary>
		/// The given name of the journey
		/// </summary>
		private string name = string.Empty;
		/// <summary>
		/// Gets/sets the given name of the journey
		/// </summary>
		public string Name
		{
			get 
			{
				return name;
			}
			set { name = value; }
		}

		/// <summary>
		/// The origin location for the journey
		/// </summary>
		private TDLocation originLocation = null;
		/// <summary>
		/// Gets/Sets the origin location for the journey
		/// </summary>
		[CLSCompliant(false)]
		public TDLocation OriginLocation
		{
			get 
			{ 
				TDProfile favouriteJourney = TDSessionManager.Current.CurrentUser.UserProfile;
				if( originLocation == null && favouriteJourney != null)
				{
					// Fetch
					#region Origin location
					string description = (string)favouriteJourney.Properties[guid+ProfileKeys.ORIGIN_LOCATION_NAME].Value;
					if( description != null ) 
					{

						// Load org.SearchType
                        TDLocation org = CreateTDLocationFromProfileValues(favouriteJourney, "Origin", OriginType, guid, description);
						originLocation = org;

					}
					#endregion

				}
				return originLocation; 
			}
			set { originLocation = value; }
		}

		/// <summary>
		/// The location search type for the origin.
		/// </summary>
		private SearchType originType = SearchType.AddressPostCode;
		/// <summary>
		/// Gets/Sets the location search type for the origin.
		/// </summary>
		[CLSCompliant(false)]
		public SearchType OriginType
		{
			get { return originType; }
			set { originType = value; }
		}

		/// <summary>
		/// The destination location
		/// </summary>
		private TDLocation destinationLocation = null;
		/// <summary>
		/// Gets/Sets the destination location
		/// </summary>
		[CLSCompliant(false)]
		public TDLocation DestinationLocation
		{
			get 
			{
				TDProfile favouriteJourney = TDSessionManager.Current.CurrentUser.UserProfile;
				if( destinationLocation == null && favouriteJourney != null)
				{
					#region Destination location
					string description = (string)favouriteJourney.Properties[guid+ProfileKeys.DESTINATION_LOCATION_NAME].Value;
					if( description != null ) 
					{
                        TDLocation dest = CreateTDLocationFromProfileValues(favouriteJourney, "Destination", DestinationType, guid, description);
						destinationLocation = dest;
					}
					#endregion


				}
				return destinationLocation; 
			}
			set { destinationLocation = value; }
		}

		/// <summary>
		/// The destination location search type
		/// </summary>
		private SearchType destinationType = SearchType.AddressPostCode;
		/// <summary>
		/// Sets/gets the destination location search type
		/// </summary>
		[CLSCompliant(false)]
		public SearchType DestinationType
		{
			get { return destinationType; }
			set { destinationType = value; }
		}

		/// <summary>
		/// The public via location
		/// </summary>
		private TDLocation publicViaLocation = null;
		/// <summary>
		/// Sets/gets the public via location
		/// </summary>
		[CLSCompliant(false)]
		public TDLocation PublicViaLocation
		{
			get 
			{
				TDProfile favouriteJourney = TDSessionManager.Current.CurrentUser.UserProfile;

				if( publicViaLocation == null && favouriteJourney != null )
				{
					#region Public via
					string description = (string)favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_PT].Value;
					if( description != null ) 
					{

                        TDLocation pubVia = CreateTDLocationFromProfileValues(favouriteJourney, "ViaLocationPublic", PublicViaType, guid, description);
						publicViaLocation = pubVia;
					}

					#endregion
				}
				return publicViaLocation; 
			}
			set { publicViaLocation = value; }
		}

		/// <summary>
		/// The public via location search type
		/// </summary>
		private SearchType publicViaType = SearchType.AddressPostCode;
		/// <summary>
		/// Gets/sets public via location search type
		/// </summary>
		[CLSCompliant(false)]
		public SearchType PublicViaType
		{
			get { return publicViaType; }
			set { publicViaType = value; }
		}

		/// <summary>
		/// The private via location
		/// </summary>
		private TDLocation privateViaLocation = null;
		/// <summary>
		/// Gets/sets the private via location
		/// </summary>
		[CLSCompliant(false)]
		public TDLocation PrivateViaLocation
		{
			get 
			{
				TDProfile favouriteJourney = TDSessionManager.Current.CurrentUser.UserProfile;
				if( privateViaLocation == null && favouriteJourney != null )
				{
					#region Car Via
					string description = (string)favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_CAR].Value;
					if( description != null ) 
					{
                        TDLocation privVia = CreateTDLocationFromProfileValues(favouriteJourney, "ViaLocationCar", PrivateViaType, guid, description);
						privateViaLocation = privVia;
					}
					#endregion
				}
				return privateViaLocation;
			}
			set { privateViaLocation = value; }
		}

		/// <summary>
		/// The private via location search type.
		/// </summary>
		private SearchType privateViaType = SearchType.AddressPostCode;
		/// <summary>
		/// Gets/sets the private via location search type.
		/// </summary>
		[CLSCompliant(false)]
		public SearchType PrivateViaType
		{
			get { return privateViaType; }
			set { privateViaType = value; }
		}

        /// <summary>
        /// The cycle via location
        /// </summary>
        private TDLocation cycleViaLocation = null;
        /// <summary>
        /// Gets/sets the cycle via location
        /// </summary>
        [CLSCompliant(false)]
        public TDLocation CycleViaLocation
        {
            get
            {
                TDProfile favouriteJourney = TDSessionManager.Current.CurrentUser.UserProfile;
                if (cycleViaLocation == null && favouriteJourney != null)
                {
                    #region Cycle Via
                    string description = (string)favouriteJourney.Properties[guid + ProfileKeys.VIA_LOCATION_CYCLE].Value;
                    if (description != null)
                    {
                        TDLocation cycleVia = CreateTDLocationFromProfileValues(favouriteJourney, "ViaLocationCycle", CycleViaType, guid, description);
                        cycleViaLocation = cycleVia;
                    }
                    #endregion
                }
                return cycleViaLocation;
            }
            set { cycleViaLocation = value; }
        }

        /// <summary>
        /// The cycle via location search type.
        /// </summary>
        private SearchType cycleViaType = SearchType.AddressPostCode;
        /// <summary>
        /// Gets/sets the cycle via location search type.
        /// </summary>
        [CLSCompliant(false)]
        public SearchType CycleViaType
        {
            get { return cycleViaType; }
            set { cycleViaType = value; }
        }

		/// <summary>
		/// If return is required or not. Currently not in use.
		/// </summary>
		private bool returnRequired = false;
		/// <summary>
		/// Sets/gets if return is required or not. Currently not in use.
		/// </summary>
		public bool ReturnRequired
		{
			get { return returnRequired; }
			set { returnRequired = value; }
		}

		/// <summary>
		/// If private journey is required or not
		/// </summary>
		private bool privateRequired = false;
		/// <summary>
		/// Sets/gets if private journey is required.
		/// </summary>
		public bool PrivateRequired
		{
			get { return privateRequired; }
			set { privateRequired = value; }
		}

	
		/// <summary>
		/// If public journey is required. Local variable could be ditched as this could be derived from modes.
		/// </summary>
		private bool publicRequired = false;
		/// <summary>
		/// Gets/sets if a public journey is required.
		/// </summary>
		public bool PublicRequired
		{
			get { return publicRequired; } 
			set { publicRequired = value; }
		}

        /// <summary>
        /// If cycle journey is required. Local variable could be ditched as this could be derived from modes.
        /// </summary>
        private bool cycleRequired = false;
        /// <summary>
        /// Gets/sets if a cycle journey is required.
        /// </summary>
        public bool CycleRequired
        {
            get { return cycleRequired; }
            set { cycleRequired = value; }
        }

		/// <summary>
		/// If air mode is required.
		/// </summary>
		private bool airMode = false;

		/// <summary>
		/// Sets/gets if air mode is required
		/// </summary>
		public bool AirMode
		{
			get { return airMode; }
			set { airMode = value; }
		}

		/// <summary>
		/// If bus mode is required
		/// </summary>
		private bool busMode = false;
		/// <summary>
		/// Sets/gets if bus mode is required.
		/// </summary>
		public bool BusMode
		{
			get { return busMode; }
			set { busMode = value; }
		}

		/// <summary>
		/// If ferry is required
		/// </summary>
		private bool ferryMode = false;
		/// <summary>
		/// Sets/gets if ferry is required.
		/// </summary>
		public bool FerryMode
		{
			get { return ferryMode; }
			set { ferryMode = value; }
		}

		/// <summary>
		/// If rail mode is required
		/// </summary>
		private bool railMode = false;
		/// <summary>
		/// Sets/gets if rail mode is required
		/// </summary>
		public bool RailMode
		{
			get { return railMode; }
			set { railMode = value; }
		}

        /// <summary>
        /// If telecabine mode is required
        /// </summary>
        private bool telecabineMode = false;
        /// <summary>
        /// Sets/gets if telecabine mode is required.
        /// </summary>
        public bool TelecabineMode
        {
            get { return telecabineMode; }
            set { telecabineMode = value; }
        }

		/// <summary>
		/// If tram mode is required
		/// </summary>
		private bool tramMode = false;
		/// <summary>
		/// Sets/gets if tram mode is required.
		/// </summary>
		public bool TramMode
		{
			get { return tramMode; }
			set { tramMode = value; }
		}

		/// <summary>
		/// If underground is required
		/// </summary>
		private bool undergroundMode = false;
		/// <summary>
		/// Sets/gets if underground mode is required.
		/// </summary>
		public bool UndergroundMode
		{
			get { return undergroundMode; }
			set { undergroundMode = value; }
		}

        /// <summary>
        /// If cycle is required
        /// </summary>
        private bool cycleMode = false;
        /// <summary>
        /// Sets/gets if cycle mode is required.
        /// </summary>
        public bool CycleMode
        {
            get { return cycleMode; }
            set { cycleMode = value; }
        }

		private bool avoidMotorWays = false;
		/// <summary>
		/// Sets/gets if motorways should be avoided
		/// </summary>
		public bool AvoidMotorWays
		{
			get { return avoidMotorWays; }
			set { avoidMotorWays = value; }
		}


        private bool avoidTolls = false;
        /// <summary>
        /// Sets/gets if tolls should be avoided
        /// </summary>
        public bool AvoidTolls
        {
            get { return avoidTolls; }
            set { avoidTolls = value; }
        }


        private bool banLimitedAccess = false;
        /// <summary>
        /// Sets/gets if unknown limited access should be avoided
        /// </summary>
        public bool BanLimitedAccess
        {
            get { return banLimitedAccess; }
            set { banLimitedAccess = value; }
        }


        private bool avoidFerries = false;
		/// <summary>
		/// Sets/gets if ferries should be avoided or not
		/// </summary>
		public bool AvoidFerries
		{
			get { return avoidFerries; }
			set { avoidFerries = value; }
		}

		/// <summary>
		/// List of roads to avoid
		/// </summary>
		private TDRoad[] avoidRoad = new TDRoad[0]; // { new TDRoad(), new TDRoad(), new TDRoad(), 
													//  new TDRoad(), new TDRoad(), new TDRoad() };
		/// <summary>
		/// Sets/gets the list of roads to avoid
		/// </summary>
		public TDRoad[] AvoidRoad
		{
			get { return avoidRoad; }
			set { avoidRoad = value; }
		}

		/// <summary>
		/// List of roads to avoid
		/// </summary>
		private TDRoad[] useRoad = new TDRoad[0]; // { new TDRoad(), new TDRoad(), new TDRoad(), 
												 // new TDRoad(), new TDRoad(), new TDRoad() };
		/// <summary>
		/// Sets/gets the list of roads to use
		/// </summary>
		public TDRoad[] UseRoad 
		{
			get { return useRoad; }
			set { useRoad = value; }
		}

		// generate this from all other information
		/// <summary>
		/// Sets/gets the required mode types for this journey
		/// </summary>
		[CLSCompliant(false)]
		public ModeType[] ModeTypes
		{
			get 
			{ 
				ArrayList types = new ArrayList();
				if( AirMode ) 
				{
					types.Add(ModeType.Air);		
				}
				if( BusMode ) 
				{
					types.Add(ModeType.Bus);	
					types.Add(ModeType.Coach);
				}
				if( FerryMode ) 
				{
					types.Add(ModeType.Ferry);		
				}
				if( RailMode ) 
				{
					types.Add(ModeType.Rail);		
				}
                if (TelecabineMode)
                {
                    types.Add(ModeType.Telecabine);
                }
				if( TramMode ) 
				{
					types.Add(ModeType.Tram);	
				}
				if( UndergroundMode )
				{
					types.Add(ModeType.Underground);
					types.Add(ModeType.Metro);
				}
                if (CycleMode)
                {
                    types.Add(ModeType.Cycle);
                }
				ModeType[] t = new ModeType[ types.Count ];
				types.CopyTo(0,t,0,t.Length);
				return t;
			}
			set 
			{ 
				ModeType[] modes = value; // = journeyParameters.PublicModes;
				AirMode = false;
				BusMode = false;
				FerryMode = false;
				RailMode = false;
                TelecabineMode = false;
				TramMode = false;
				UndergroundMode = false;
                CycleMode = false;

				foreach (ModeType type in modes)
				{
					switch (type)
					{
						case ModeType.Air:
							AirMode = true;
							break;
						case ModeType.Bus:
							BusMode = true;
							break;
						case ModeType.Ferry:
							FerryMode = true;
							break;
						case ModeType.Rail:
							RailMode = true;
							break;
                        case ModeType.Telecabine:
                            TelecabineMode = true;
                            break;
						case ModeType.Tram:
							TramMode = true;
							break;
						case ModeType.Underground:
							UndergroundMode = true;
							break;
                        case ModeType.Cycle:
                            CycleMode = true;
                            break;
					}
				}
				// generate all other booleans 
			}
		}

		/// <summary>
		/// The search distance, defaulted to an arbitrary number
		/// </summary>
		private int searchDistance = 1600;
		/// <summary>
		/// The search distance to use for when restoring a location
		/// </summary>
		public int SearchDistance
		{
			get { return searchDistance; }
			set { searchDistance = value; }
        }

        #region Accessible options

        private bool requireSpecialAssistance = false;
        /// <summary>
        /// Read/Write. Accessible option for RequireSpecialAssistance
        /// </summary>
        public bool RequireSpecialAssistance
        {
            get { return requireSpecialAssistance; }
            set { requireSpecialAssistance = value; }
        }

        private bool requireStepFreeAccess = false;
        /// <summary>
        /// Read/Write. Accessible option for RequireStepFreeAccess
        /// </summary>
        public bool RequireStepFreeAccess
        {
            get { return requireStepFreeAccess; }
            set { requireStepFreeAccess = value; }
        }

        private bool requireFewerInterchanges = false;
        /// <summary>
        /// Read/Write. Accessible option for RequireFewerInterchanges
        /// </summary>
        public bool RequireFewerInterchanges
        {
            get { return requireFewerInterchanges; }
            set { requireFewerInterchanges = value; }
        }

        #endregion

        #region Cycle

        private string cycleJourneyType = string.Empty;
        /// <summary>
        /// Sets/gets the cycle journey type
        /// </summary>
        public string CycleJourneyType
        {
            get { return cycleJourneyType; }
            set { cycleJourneyType  = value; }
        }

        private string cycleSpeedText = string.Empty;
        /// <summary>
        /// Sets/gets the max cycle speed text entered 
        /// </summary>
        public string CycleSpeedText
        {
            get { return cycleSpeedText; }
            set { cycleSpeedText = value; }
        }

        private string cycleSpeedUnit = string.Empty;
        /// <summary>
        /// Sets/gets the cycle speed unit
        /// </summary>
        public string CycleSpeedUnit
        {
            get { return cycleSpeedUnit; }
            set { cycleSpeedUnit = value; }
        }

        private bool cycleAvoidUnlitRoads = false;
        /// <summary>
        /// Sets/gets if unlit roads should be avoided or not
        /// </summary>
        public bool CycleAvoidUnlitRoads
        {
            get { return cycleAvoidUnlitRoads; }
            set { cycleAvoidUnlitRoads = value; }
        }

        private bool cycleAvoidWalkingBike = false;
        /// <summary>
        /// Sets/gets if walking your bike should be avoided or not
        /// </summary>
        public bool CycleAvoidWalkingBike
        {
            get { return cycleAvoidWalkingBike; }
            set { cycleAvoidWalkingBike = value; }
        }

        private bool cycleAvoidSteepClimbs = false;
        /// <summary>
        /// Sets/gets if steep climbs should be avoided or not
        /// </summary>
        public bool CycleAvoidSteepClimbs
        {
            get { return cycleAvoidSteepClimbs; }
            set { cycleAvoidSteepClimbs = value; }
        }

        private bool cycleAvoidTimeBased = false;
        /// <summary>
        /// Sets/gets if time based restrictions should be avoided or not
        /// </summary>
        public bool CycleAvoidTimeBased
        {
            get { return cycleAvoidTimeBased; }
            set { cycleAvoidTimeBased = value; }
        }

        #endregion

        #endregion

        #region Public methods
        /// <summary>
		/// Updates the changes made to this favourite journey.
		/// This persists the favourite journey.
		/// </summary>
		public void Update()
		{
			TDProfile favouriteJourney = TDSessionManager.Current.CurrentUser.UserProfile;
			bool isNew = guid == null;

			// Fetch/create journey in CS
			if( isNew )
			{
				//create new preferencesID
				guid = System.Guid.NewGuid().ToString();
			}
			
			favouriteJourney.Properties[guid+ProfileKeys.JOURNEY_NAME].Value = Name;

			#region Origin
			//name and guid already set - add from/to details
			favouriteJourney.Properties[guid+ProfileKeys.ORIGIN_LOCATION_NAME].Value = OriginLocation.Description; 
			//convert enum type to string
			int originEnum = (int)OriginType;
			favouriteJourney.Properties[guid+ProfileKeys.ORIGIN_LOCATION_TYPE].Value = originEnum.ToString(CultureInfo.InvariantCulture.NumberFormat);

			SetProfileFromTDLocationValues( favouriteJourney, "Origin", OriginType, OriginLocation, guid );
			#endregion 

			#region Destination
			favouriteJourney.Properties[guid+ProfileKeys.DESTINATION_LOCATION_NAME].Value = DestinationLocation.Description;
			//convert enum type to string
			int destinationEnum = (int)DestinationType; 
			favouriteJourney.Properties[guid+ProfileKeys.DESTINATION_TYPE].Value = destinationEnum.ToString(CultureInfo.InvariantCulture.NumberFormat);
			
			SetProfileFromTDLocationValues( favouriteJourney, "Destination", DestinationType, DestinationLocation, guid );

			#endregion

            favouriteJourney.Properties[guid+ProfileKeys.RETURN_JOURNEY].Value = ReturnRequired;
            favouriteJourney.Properties[guid+ProfileKeys.FIND_CAR].Value = PrivateRequired;
            favouriteJourney.Properties[guid+ProfileKeys.FIND_PT].Value = PublicRequired;
            favouriteJourney.Properties[guid + ProfileKeys.FIND_CYCLE].Value = CycleRequired;

            // run through array, if a mode is present, set true, false otherwise 
			favouriteJourney.Properties[guid+ProfileKeys.MODE_PLANE].Value = AirMode;
			favouriteJourney.Properties[guid+ProfileKeys.MODE_BUS_COACH].Value = BusMode;	
			favouriteJourney.Properties[guid+ProfileKeys.MODE_FERRY].Value = FerryMode;	
			favouriteJourney.Properties[guid+ProfileKeys.MODE_TRAIN].Value = RailMode;
            favouriteJourney.Properties[guid + ProfileKeys.MODE_TELECABINE].Value = TelecabineMode;
            favouriteJourney.Properties[guid+ProfileKeys.MODE_TRAM_LR].Value = TramMode;	
            favouriteJourney.Properties[guid+ProfileKeys.MODE_UG_METRO].Value = UndergroundMode;
            favouriteJourney.Properties[guid + ProfileKeys.MODE_CYCLE].Value = CycleMode;

			// advanced route options
			#region Avoid [insert witty avoid here]
			// Create an array list containing strings out of TDRoad objects
			// This enables us to avoid complicated long-term serialisation compatibility
			ArrayList tmpAvoidRoad = new ArrayList();
			for(int i = 0; i < avoidRoad.Length; i++)
			{
				if(  avoidRoad[i] != null && avoidRoad[i].RoadName.Length != 0)
				{
					tmpAvoidRoad.Add( avoidRoad[i].RoadName );
				}
			}

			favouriteJourney.Properties[guid+ProfileKeys.AVOID_ROAD].Value = tmpAvoidRoad;

			// Create an array list containing strings out of TDRoad objects
			// This enables us to avoid complicated long-term serialisation compatibility
			ArrayList tmpUseRoad = new ArrayList();
			for(int i = 0; i < useRoad.Length; i++)
			{
				if( useRoad[i] != null && useRoad[i].RoadName.Length != 0 )
				{
					tmpUseRoad.Add( useRoad[i].RoadName );
				}
			}

			favouriteJourney.Properties[guid+ProfileKeys.USE_ROAD].Value = tmpUseRoad;
			favouriteJourney.Properties[guid+ProfileKeys.AVOID_FERRIES].Value = avoidFerries;
			favouriteJourney.Properties[guid+ProfileKeys.AVOID_MOTORWAY].Value = avoidMotorWays;
			favouriteJourney.Properties[guid+ProfileKeys.AVOID_TOLLS].Value = avoidTolls;
			#endregion

            #region Accessible options

            favouriteJourney.Properties[guid + ProfileKeys.ACCESSIBILITY_REQUIRE_SPECIAL_ASSISTANCE].Value = RequireSpecialAssistance;
            favouriteJourney.Properties[guid + ProfileKeys.ACCESSIBILITY_REQUIRE_STEP_FREE_ACCESS].Value = RequireStepFreeAccess;
            favouriteJourney.Properties[guid + ProfileKeys.ACCESSIBILITY_REQUIRE_FEWER_CHANGES].Value = RequireFewerInterchanges;

            #endregion

            #region Cycle advanced options

            favouriteJourney.Properties[guid + ProfileKeys.CYCLE_JOURNEY_TYPE].Value = cycleJourneyType;
            favouriteJourney.Properties[guid + ProfileKeys.CYCLE_SPEED_TEXT].Value = cycleSpeedText;
            favouriteJourney.Properties[guid + ProfileKeys.CYCLE_SPEED_UNIT].Value = cycleSpeedUnit;
            favouriteJourney.Properties[guid + ProfileKeys.CYCLE_AVOID_STEEPCLIMBS].Value = cycleAvoidSteepClimbs;
            favouriteJourney.Properties[guid + ProfileKeys.CYCLE_AVOID_UNLITROADS].Value = cycleAvoidUnlitRoads;
            favouriteJourney.Properties[guid + ProfileKeys.CYCLE_AVOID_WALKINGBIKE].Value = cycleAvoidWalkingBike;
            favouriteJourney.Properties[guid + ProfileKeys.CYCLE_AVOID_TIMEBASED].Value = cycleAvoidTimeBased;

            #endregion

            #region Private Via
            favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_CAR].Value = PrivateViaLocation.Description;
			//convert enum type to string
			int pEnum = (int)PrivateViaType;
			favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_TYPE_CAR].Value = pEnum.ToString(CultureInfo.InvariantCulture.NumberFormat);

			SetProfileFromTDLocationValues( favouriteJourney, "ViaLocationCar", PrivateViaType , PrivateViaLocation, guid);

			#endregion

			#region Public Via
			favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_PT].Value = PublicViaLocation.Description;
			int ptEnum = (int)PublicViaType;
			favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_TYPE_PT].Value = ptEnum.ToString(CultureInfo.InvariantCulture.NumberFormat);

			// Fails on Naptan
			SetProfileFromTDLocationValues( favouriteJourney, "ViaLocationPublic", PublicViaType , PublicViaLocation, guid);

			#endregion

            #region Cycle Via
            favouriteJourney.Properties[guid + ProfileKeys.VIA_LOCATION_CYCLE].Value = CycleViaLocation.Description;
            int ctEnum = (int)CycleViaType;
            favouriteJourney.Properties[guid + ProfileKeys.VIA_LOCATION_TYPE_CYCLE].Value = ctEnum.ToString(CultureInfo.InvariantCulture.NumberFormat);

            // Fails on Naptan
            SetProfileFromTDLocationValues(favouriteJourney, "ViaLocationCycle", CycleViaType, CycleViaLocation, guid);

            #endregion

            try
			{
				// persist favourite journey
				favouriteJourney.Update();
				// log save preferences event
				UserPreferenceSaveEvent upe = new UserPreferenceSaveEvent(UserPreferenceSaveEventCategory.JourneyPlanningOptions, TDSessionManager.Current.Session.SessionID);
				Logger.Write(upe);

				if( isNew && GuidChanged != null)
				{
					GuidChanged( this, EventArgs.Empty );
				}
			}
			catch (ArgumentException ae)
			{
				throw new TDException("Argument exception Message = "+ae.Message + ": Param Name = "+ae.ParamName+": Helplink = "+ae.HelpLink, ae, false, TDExceptionIdentifier.BTCRetrievalOfProfileFailed );				
			}
		}

		/// <summary>
		/// Loads most of the data associated favourite journey.
		/// </summary>
		public void Load()
		{
			if( guid == null )
			{
				// Ignore for a favourite without guid
				return;
			}

			TDProfile favouriteJourney = TDSessionManager.Current.CurrentUser.UserProfile;
		
			Name = (string)favouriteJourney.Properties[guid+ProfileKeys.JOURNEY_NAME].Value;
			ReturnRequired = (bool)favouriteJourney.Properties[guid+ProfileKeys.RETURN_JOURNEY].Value;

			PrivateRequired = (bool)favouriteJourney.Properties[guid+ProfileKeys.FIND_CAR].Value;
			PublicRequired = (bool)favouriteJourney.Properties[guid+ProfileKeys.FIND_PT].Value;
            
			// advanced route options
			#region Avoid [insert witty avoid here]
			// Avoid roads -> if nothing is saved, nothing is restored
			object tmpAvoidRoad = favouriteJourney.Properties[guid+ProfileKeys.AVOID_ROAD].Value;
			if( tmpAvoidRoad is ArrayList )
			{
				ArrayList arr = (ArrayList)tmpAvoidRoad;
				avoidRoad = new TDRoad[ arr.Count ];
				for(int i = 0; i < arr.Count && i < avoidRoad.Length; i++)
				{
					avoidRoad[i] = new TDRoad( arr[i].ToString() );
				}
			}
			else if( tmpAvoidRoad is string )
			{
				avoidRoad = new TDRoad[ 1 ];
				// Backwards compatibility
				AvoidRoad[0] = new TDRoad( (string)tmpAvoidRoad);
			}
			else if( tmpAvoidRoad != null )
			{
				// Just in case it is just plain wrong - warning message
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "Favourite["+guid+"] journey contains illegal avoid-road type["+ tmpAvoidRoad.GetType().Name+"]"));
			}



			object tmpUseRoad = favouriteJourney.Properties[guid+ProfileKeys.USE_ROAD].Value;
			// Avoid roads -> if nothing is saved, nothing is restored
			if( tmpUseRoad is ArrayList )
			{
				ArrayList arr = (ArrayList)tmpUseRoad;
                useRoad = new TDRoad[ arr.Count ];
				for(int i = 0; i < arr.Count && i < useRoad.Length; i++)
				{
					useRoad[i] = new TDRoad( arr[i].ToString() );
				}
			}

			avoidFerries = GetAsBool(favouriteJourney, guid + ProfileKeys.AVOID_FERRIES );
			avoidMotorWays = GetAsBool(favouriteJourney,  guid + ProfileKeys.AVOID_MOTORWAY );
			avoidTolls = GetAsBool(favouriteJourney, guid + ProfileKeys.AVOID_TOLLS );


			#endregion

			#region Location types (TDLocations are deferred until needed)
			int type = Convert.ToInt32( favouriteJourney.Properties[guid+ProfileKeys.ORIGIN_LOCATION_TYPE].Value, CultureInfo.CurrentCulture.NumberFormat );
			OriginType = (SearchType)type;
			type = Convert.ToInt32( favouriteJourney.Properties[guid+ProfileKeys.DESTINATION_TYPE].Value, CultureInfo.CurrentCulture.NumberFormat );
			DestinationType = (SearchType)type;

			type = Convert.ToInt32(favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_TYPE_CAR].Value, CultureInfo.CurrentCulture.NumberFormat);
			PrivateViaType = (SearchType)type;


			type = Convert.ToInt32(favouriteJourney.Properties[guid+ProfileKeys.VIA_LOCATION_TYPE_PT].Value, CultureInfo.CurrentCulture.NumberFormat);
			PublicViaType = (SearchType)type;
			#endregion

			#region Modes
			AirMode = (bool)favouriteJourney.Properties[guid+ProfileKeys.MODE_PLANE].Value;
			BusMode = (bool)favouriteJourney.Properties[guid+ProfileKeys.MODE_BUS_COACH].Value;
			FerryMode = (bool)favouriteJourney.Properties[guid+ProfileKeys.MODE_FERRY].Value;
			RailMode = (bool)favouriteJourney.Properties[guid+ProfileKeys.MODE_TRAIN].Value;
            TramMode = (bool)favouriteJourney.Properties[guid + ProfileKeys.MODE_TRAM_LR].Value;
			UndergroundMode = (bool)favouriteJourney.Properties[guid+ProfileKeys.MODE_UG_METRO].Value;

            if (favouriteJourney.Properties[guid + ProfileKeys.MODE_TELECABINE] != null
                && favouriteJourney.Properties[guid + ProfileKeys.MODE_TELECABINE].Value != null)
                TelecabineMode = (bool)favouriteJourney.Properties[guid + ProfileKeys.MODE_TELECABINE].Value;

            #endregion

            #region Accessible options

            RequireSpecialAssistance = GetAsBool(favouriteJourney, guid + ProfileKeys.ACCESSIBILITY_REQUIRE_SPECIAL_ASSISTANCE);
            RequireStepFreeAccess = GetAsBool(favouriteJourney, guid + ProfileKeys.ACCESSIBILITY_REQUIRE_STEP_FREE_ACCESS);
            RequireFewerInterchanges = GetAsBool(favouriteJourney, guid + ProfileKeys.ACCESSIBILITY_REQUIRE_FEWER_CHANGES);

            #endregion

            // load cycle journeys
            // Need to place in a try-catch because if user has saved a journey pre Del 10.3, the 
            // journey will not contain the cycle specific ProfileKeys.
            try
            {
                CycleRequired = (bool)favouriteJourney.Properties[guid + ProfileKeys.FIND_CYCLE].Value;

                CycleJourneyType = (string)favouriteJourney.Properties[guid + ProfileKeys.CYCLE_JOURNEY_TYPE].Value;
                CycleSpeedText = (string)favouriteJourney.Properties[guid + ProfileKeys.CYCLE_SPEED_TEXT].Value;
                CycleSpeedUnit = (string)favouriteJourney.Properties[guid + ProfileKeys.CYCLE_SPEED_UNIT].Value;

                CycleAvoidSteepClimbs = GetAsBool(favouriteJourney, guid + ProfileKeys.CYCLE_AVOID_STEEPCLIMBS);
                CycleAvoidUnlitRoads = GetAsBool(favouriteJourney, guid + ProfileKeys.CYCLE_AVOID_UNLITROADS);
                CycleAvoidWalkingBike = GetAsBool(favouriteJourney, guid + ProfileKeys.CYCLE_AVOID_WALKINGBIKE);
                CycleAvoidTimeBased = GetAsBool(favouriteJourney, guid + ProfileKeys.CYCLE_AVOID_TIMEBASED);

                type = Convert.ToInt32(favouriteJourney.Properties[guid + ProfileKeys.VIA_LOCATION_TYPE_CYCLE].Value, CultureInfo.CurrentCulture.NumberFormat);
                CycleViaType = (SearchType)type;

                CycleMode = (bool)favouriteJourney.Properties[guid + ProfileKeys.MODE_CYCLE].Value;
            }
            catch
            {
                // If an error occurs, this was orignally a journey which did not contain the cycle 
                // keys. So set them all to default values
                CycleRequired = false;
                CycleAvoidSteepClimbs = false;
                CycleAvoidUnlitRoads = false;
                CycleAvoidWalkingBike = false;
                CycleAvoidTimeBased = false;
                CycleViaType = SearchType.AddressPostCode;
                CycleMode = false;
            }
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Sets the profile with the data from this favourite journey.
		/// </summary>
		/// <param name="fj">The profile</param>
		/// <param name="sLoc">The type of fields to be updated (Destination, Origin, ViaLocationCar and ViaLocationPublic). As of writing the last alternative does not work</param>
		/// <param name="st">The search type</param>
		/// <param name="loc">The location to grab all values from</param>
		private void SetProfileFromTDLocationValues(TDProfile fj, string sLoc, SearchType st, TDLocation loc, string guid)
		{

			fj.Properties[ guid + string.Format( ProfileKeys.LOCALITY, sLoc) ].Value = loc.Locality;
			fj.Properties[ guid + string.Format( ProfileKeys.EASTING, sLoc) ].Value = loc.GridReference.Easting;
			fj.Properties[ guid + string.Format( ProfileKeys.NORTHING, sLoc) ].Value = loc.GridReference.Northing;
			string[] naptan= null;
			string[] easting = null;
			string[] northing = null;
			ExtractLocationData( st, loc, ref naptan, ref easting, ref northing );


			fj.Properties[ guid + string.Format( ProfileKeys.NAPTAN, sLoc) ].Value = naptan;
			fj.Properties[ guid + string.Format( ProfileKeys.NAPTAN_EASTING, sLoc) ].Value = easting;
			fj.Properties[ guid + string.Format( ProfileKeys.NAPTAN_NORTHING, sLoc) ].Value = northing;
		}

		/// <summary>
		/// Creates a TDLocation object from a given set of profile values.
		/// </summary>
		/// <param name="fj">The profile</param>
		/// <param name="sLoc">The type of fields to use for the location (Destination, Origin, ViaLocationCar and ViaLocationPublic). 
        /// As of writing the last alternative does not work</param>
		/// <param name="st">The search type</param>
		/// <returns>A TDLocation</returns>
		private TDLocation CreateTDLocationFromProfileValues(TDProfile fj, string sLoc, SearchType st, 
            string guid, string description)
		{
			IGisQuery gq = (IGisQuery)TDServiceDiscovery.Current[ ServiceDiscoveryKey.GisQuery ];
			QuerySchema qs = null;

			TDLocation loc = new TDLocation();
			loc.SearchType = st;

            if (description == null)
                description = string.Empty;

            loc.Description = description;
			loc.Locality = (string)fj.Properties[ guid + string.Format( ProfileKeys.LOCALITY, sLoc) ].Value;
			object easting = fj.Properties[guid + string.Format( ProfileKeys.EASTING, sLoc ) ].Value;
			object northing = fj.Properties[guid + string.Format( ProfileKeys.NORTHING, sLoc ) ].Value;
			loc.GridReference = CreateGridReference(easting , northing );

			// If the gridreference are set to -1, there is no location.
			// If the gridreference is set to 0, then they have an old version of the profile
			// and the status can not be set to valid.
			if( loc.GridReference.Easting == -1 || loc.GridReference.Easting == 0 )
			{
				return loc;
			}

			if( st == SearchType.MainStationAirport || st == SearchType.AllStationStops )
			{
				// If search type is not coordinate based
				// Load naptans
				object[] dnid = (object[])fj.Properties[guid + string.Format( ProfileKeys.NAPTAN,sLoc )].Value;
				object[] de = (object[])fj.Properties[ guid + string.Format( ProfileKeys.NAPTAN_EASTING, sLoc) ].Value;
				object[] dn = (object[])fj.Properties[ guid + string.Format( ProfileKeys.NAPTAN_NORTHING, sLoc) ].Value;
				loc.NaPTANs = CreateNaptans( dnid, de, dn );

			}
            else if (st == SearchType.Locality)
            {
                // Locality location has no naptans
                loc.NaPTANs = new TDNaptan[0];
                loc.RequestPlaceType = RequestPlaceType.Locality;
            }
            else
            {
                // find naptans via gis query and northing, easting
                qs = gq.FindStopsInRadius(loc.GridReference.Easting, loc.GridReference.Northing, searchDistance);
                TDNaptan[] naptans = new TDNaptan[qs.Stops.Count];
                int srindex = 0;
                foreach (QuerySchema.StopsRow sr in qs.Stops.Rows)
                {
                    TDNaptan naptan = new TDNaptan();
                    naptan.Naptan = sr.atcocode;
                    naptan.GridReference = new OSGridReference((int)sr.X, (int)sr.Y);
                    naptans[srindex++] = naptan;
                }
                loc.NaPTANs = naptans;
                loc.RequestPlaceType = RequestPlaceType.Coordinate;
            }
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("Found {0} naptans for gridref[{1},{2}] for location[{3}] with searchtype[{4}]",
                    loc.NaPTANs.Length,
                    loc.GridReference.Easting,
                    loc.GridReference.Northing,
                    loc.Description,
                    loc.SearchType.ToString()
                    )));
            }

			// Load toids
			qs = gq.FindNearestITNs(loc.GridReference.Easting, loc.GridReference.Northing);


			string[] toids = new string[ qs.ITN.Count ];
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("Found {0} toids for gridref[{1},{2}] for location[{3}]",
                    toids.Length,
                    loc.GridReference.Easting,
                    loc.GridReference.Northing,
                    loc.Description
                    )));
            }

			int rrindex = 0;
			foreach( QuerySchema.ITNRow ir in qs.ITN.Rows )
			{
				toids[rrindex] = ir.toid;
				rrindex++;
			}
			loc.Toid = toids;

			loc.Status = TDLocationStatus.Valid;
			return loc;

		}

		/// <summary>
		/// Creates a list of naptans from an equal set of object arrays containing naptan, easting and northing.
		/// </summary>
		/// <param name="naptan">The list of naptans</param>
		/// <param name="easting">The corresponding list of easting</param>
		/// <param name="northing">The corresponding list of northings</param>
		/// <returns>An array of naptans</returns>
		private TDNaptan[] CreateNaptans(object[] naptan, object[] easting, object[] northing)
		{
			if( naptan == null )
			{
				return new TDNaptan[0];
			}
			TDNaptan[] result = new TDNaptan[ naptan.Length ];
			for(int i = 0; i < naptan.Length; i++)
			{
				OSGridReference osg = CreateGridReference( easting[i], northing[i] );
				result[i] = new TDNaptan( (string)naptan[i], osg );
			}
			return result;
		}


		/// <summary>
		/// Creates OS Gridreference for the given easting/northing
		/// </summary>
		[CLSCompliant(false)]
		public OSGridReference CreateGridReference(object easting, object northing)
		{
			// Tansform to doubles
			int e = Convert.ToInt32( easting );
			int n = Convert.ToInt32( northing );
			
			return new OSGridReference(e,n);
		}

		/// <summary>
		/// Extracts all the location data into separate fields if it is a naptan based search. 
		/// Coordinate based searches will leave the referenced variables untouched.
		/// </summary>
		/// <param name="st">The search type for the location</param>
		/// <param name="loc">The location used</param>
		/// <param name="naptan">The naptans to be populated</param>
		/// <param name="easting">The corresponding easting to be populated</param>
		/// <param name="northing">The corresponding northings to be populated</param>
		private static void ExtractLocationData( SearchType st, TDLocation loc, ref string[] naptan, ref string[] easting, ref string[] northing)
		{
			// Note that the profile system does not like multi-value arrays with 0 elements in them, so skip that.
			if( loc.NaPTANs.Length > 0 && (st == SearchType.AllStationStops || st == SearchType.MainStationAirport ) )
			{
				naptan = new string[ loc.NaPTANs.Length ];
				easting = new string[ loc.NaPTANs.Length ];
				northing = new string[ loc.NaPTANs.Length ];
				for(int i = 0; i < loc.NaPTANs.Length; i++)
				{
					TDNaptan tdn = loc.NaPTANs[i];
					naptan[i] = tdn.Naptan;
					easting[i] = tdn.GridReference.Easting.ToString();
					northing[i] = tdn.GridReference.Northing.ToString();
				}
			}
		}

		/// <summary>
		/// Get a value from a profile as a boolean.
		/// If the value is missing it is treated as being false.
		/// </summary>
		/// <param name="profile">The profile to check</param>
		/// <param name="key">The key to look for</param>
		/// <returns>True or false. False will be returned if the value is missing</returns>
		private static bool GetAsBool( TDProfile profile, string key )
		{
			if( !profile.Properties.ContainsKey( key ) || profile.Properties[key] == null )
			{
				return false;
			}
			return (bool)profile.Properties[key].Value;
		}
		#endregion
	}
}