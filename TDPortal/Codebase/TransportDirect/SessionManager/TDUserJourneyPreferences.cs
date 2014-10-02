// ***********************************************
// NAME 		: TDUserJourneyPreferences.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 01/12/2003
// DESCRIPTION 	: The journey preferences for the user
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDUserJourneyPreferences.cs-arc  $
//
//   Rev 1.3   Nov 16 2012 14:00:50   DLane
//Setting walk parameters in the journey request
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Nov 15 2012 14:06:20   DLane
//Addition of accessibility options to journey plan input and user profile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 13 2008 16:46:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.1   Oct 10 2008 15:58:06   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.0   Sep 09 2008 13:21:00   mmodi
//Updated for saved cycle preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:48:44   mturner
//Initial revision.
//
//   Rev 1.4   Apr 06 2005 16:32:12   PNorell
//Updated for FX Cops.
//
//   Rev 1.3   Apr 04 2005 10:48:40   PNorell
//Fix for IR1991.
//
//FavouriteJourney error.
//Resolution for 1991: Del 7  - unable to plan journey if logged in as a user registered pre-del 7
//
//   Rev 1.2   Mar 17 2005 17:06:24   PNorell
//Fix for Public Transport preferences.
//
//   Rev 1.1   Mar 16 2005 15:45:46   PNorell
//Fix for backwards compatibility.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.0   Mar 08 2005 09:37:10   PNorell
//Initial revision.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)


using System;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.JourneyPlanning.CJPInterface;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// The journey preferences for the user
	/// </summary>
	/// 
	public class TDUserJourneyPreferences
	{
		private TDProfile userProfile;

		public TDUserJourneyPreferences(TDProfile profile)
		{
			userProfile = profile;
			
			// Transfer car preferences from DEL6 to DEL7
			bool del6Profile = false;

			// If this is unset, there is a risk that there exists a DEL6 profile for car
			if( !CarPreferencesSet )
			{

				// Only avoid needs to be translated to DoNotUseMotorways
				bool avoid = GetBool( ProfileKeys.AVOID_MOTORWAY );				
				if( avoid )
				{
					profile.Properties.Remove( ProfileKeys.AVOID_MOTORWAY );
					DoNotUseMotorways = avoid;
					del6Profile = true;
				}

				del6Profile = del6Profile || profile.Properties.ContainsKey( ProfileKeys.DRIVING_SPEED );
				del6Profile = del6Profile || profile.Properties.ContainsKey( ProfileKeys.DRIVING_TYPE );

				// If del6Profile is now set to true, then this must be a del6profile.
				// Ensure that default values are set to be "wanted"
				if( del6Profile )
				{
					DefaultFuelConsumption = true;
					DefaultFuelCost = true;
				}

				// Indicate we have car profile set
				CarPreferencesSet = del6Profile;
			}
			// If this is unset, there might exist a DEL6 profile for public transport
			if( !PublicTransportPreferencesSet )
			{
				// Reset profile since they are now split into two (and actually are already)
				// in the normal GUI as well for the finders.
				del6Profile = false;

				// If the algorithm is set - there must be some sort of profile here.
				del6Profile = profile.Properties.ContainsKey( ProfileKeys.INTERCHANGE_LIMIT );

				// No other checks are needed since there is no translation from old data
				// to new.

				// Indicate we have public transport profile set accordingly
				PublicTransportPreferencesSet = del6Profile;
			}
		}

		#region Public properties

		#region Neutral journey preferences
		public bool CarPreferencesSet
		{
			get 
			{
				return GetBool( ProfileKeys.CAR_PREFERENCES_SET );
			}
			set 
			{
				SetBool( ProfileKeys.CAR_PREFERENCES_SET, value);
			}
		}

		public bool PublicTransportPreferencesSet
		{
			get 
			{
				return GetBool( ProfileKeys.PT_PREFERENCES_SET );
			}
			set 
			{
				SetBool( ProfileKeys.PT_PREFERENCES_SET, value);
			}
		}

        public bool CyclePreferencesSet
        {
            get
            {
                return GetBool(ProfileKeys.CYCLE_PREFERENCES_SET);
            }
            set
            {
                SetBool(ProfileKeys.CYCLE_PREFERENCES_SET, value);
            }
        }

        public bool AccessibilityPreferencesSet
        {
            get
            {
                return GetBool(ProfileKeys.ACCESSIBILITY_PREFERENCES_SET);
            }
            set
            {
                SetBool(ProfileKeys.ACCESSIBILITY_PREFERENCES_SET, value);
            }
        }

		#endregion

		#region Car Journey preferences

		[CLSCompliant(false)]
		public PrivateAlgorithmType DrivingType
		{
			get 
			{ 
				object val = userProfile.Properties[ProfileKeys.DRIVING_TYPE].Value;
				if (val is PrivateAlgorithmType)
				{
					 return (PrivateAlgorithmType)val;
				}
				else if( val != null )
				{
					int carEnum = Int32.Parse(val.ToString(), CultureInfo.InvariantCulture.NumberFormat);
					return (PrivateAlgorithmType)carEnum; // convert to enum
				}
				return PrivateAlgorithmType.Fastest;
			}
			set { userProfile.Properties[ ProfileKeys.DRIVING_TYPE ].Value = value; }
		}

		public int DrivingSpeed
		{
			get { return GetInt( ProfileKeys.DRIVING_SPEED); }
			set { userProfile.Properties[ ProfileKeys.DRIVING_SPEED ].Value = value; }
		}

		public bool DoNotUseMotorways
		{
			get 
			{
				return GetBool( ProfileKeys.DONOTUSE_MOTORWAYS );
			}
			set 
			{
				SetBool( ProfileKeys.DONOTUSE_MOTORWAYS, value);
			}
		}

		public string CarSize
		{
			get { return GetString( ProfileKeys.CAR_SIZE); }
			set { userProfile.Properties[ ProfileKeys.CAR_SIZE ].Value = value; }
		}

		public string CarFuelType 
		{
			get { return GetString( ProfileKeys.CAR_FUELTYPE); }
			set { userProfile.Properties[ ProfileKeys.CAR_FUELTYPE ].Value = value; }
		}

		public bool DefaultFuelConsumption
		{
			get 
			{
				return GetBool( ProfileKeys.CAR_DEFAULT_FUEL_CONSUMPTION );
			}
			set 
			{
				SetBool( ProfileKeys.CAR_DEFAULT_FUEL_CONSUMPTION, value);
			}
		}


		public string FuelConsumption
		{
			get { return GetString( ProfileKeys.CAR_FUEL_CONSUMPTION); }
			set 
			{
				userProfile.Properties[ ProfileKeys.CAR_FUEL_CONSUMPTION ].Value = value; 
			}
		}
		public int FuelConsumptionUnit
		{
			get { return GetInt( ProfileKeys.CAR_FUEL_CONSUMPTION_TYPE ); }
			set 
			{ 
				userProfile.Properties[ ProfileKeys.CAR_FUEL_CONSUMPTION_TYPE ].Value = value; 
			}
		}

		// Fuel metrics (preference)
		public bool DefaultFuelCost
		{
			get 
			{
				return GetBool( ProfileKeys.CAR_DEFAULT_FUEL_COST );
			}
			set 
			{
				SetBool( ProfileKeys.CAR_DEFAULT_FUEL_COST, value);
			}
		}

		public string FuelCost
		{
			get { return GetString( ProfileKeys.CAR_FUEL_COST); }
			set 
			{
				userProfile.Properties[ ProfileKeys.CAR_FUEL_COST ].Value = value; 
				DefaultFuelCost = value != null && value.Length == 0;
			}
		}
		
		#endregion

        #region Accessibility preferences

        public bool RequireSpecialAssistance
        {
            get { return GetBool(ProfileKeys.ACCESSIBILITY_REQUIRE_SPECIAL_ASSISTANCE); }
            set { SetBool(ProfileKeys.ACCESSIBILITY_REQUIRE_SPECIAL_ASSISTANCE, value); }
        }

        public bool RequireStepFreeAccess
        {
            get { return GetBool(ProfileKeys.ACCESSIBILITY_REQUIRE_STEP_FREE_ACCESS); }
            set { SetBool(ProfileKeys.ACCESSIBILITY_REQUIRE_STEP_FREE_ACCESS, value); }
        }

        public bool RequireFewerChanges
        {
            get { return GetBool(ProfileKeys.ACCESSIBILITY_REQUIRE_FEWER_CHANGES); }
            set { SetBool(ProfileKeys.ACCESSIBILITY_REQUIRE_FEWER_CHANGES, value); }
        }

        public int AccessibleWalkSpeed
        {
            get { return GetInt(ProfileKeys.ACCESSIBILITY_WALK_SPEED); }
            set { userProfile.Properties[ProfileKeys.ACCESSIBILITY_WALK_SPEED].Value = value; }
        }

        public int AccessibleWalkDistance
        {
            get { return GetInt(ProfileKeys.ACCESSIBILITY_WALK_DISTANCE); }
            set { userProfile.Properties[ProfileKeys.ACCESSIBILITY_WALK_DISTANCE].Value = value; }
        }

        #endregion

        #region Public transport preferences
        public int InterChangeSpeed
		{
			get { return GetInt( ProfileKeys.INTERCHANGE_SPEED); }
			set { userProfile.Properties[ ProfileKeys.INTERCHANGE_SPEED ].Value = value; }
		}

		[CLSCompliant(false)]
		public PublicAlgorithmType PublicTransportAlgorithm
		{
			get 
			{ 
				// Compatibility issue
				object val = userProfile.Properties[ProfileKeys.INTERCHANGE_LIMIT].Value; 
				if (val is PublicAlgorithmType)
				{
					return (PublicAlgorithmType)val;
				}
				else if( val != null )
				{
					int ptEnum = Int32.Parse(val.ToString() , CultureInfo.InvariantCulture.NumberFormat);
					return (PublicAlgorithmType)ptEnum; // convert to enum
				}
				// Default as standard return
				return PublicAlgorithmType.Default;
			}

			set { userProfile.Properties[ ProfileKeys.INTERCHANGE_LIMIT ].Value = value; }
		}

		public int WalkingSpeed
		{
			get { return GetInt( ProfileKeys.WALKING_SPEED); }
			set { userProfile.Properties[ ProfileKeys.WALKING_SPEED ].Value = value; }
		}

		public int MaxWalkingTime
		{
			get { return GetInt( ProfileKeys.WALKING_TIME); }
			set { userProfile.Properties[ ProfileKeys.WALKING_TIME ].Value = value; }
		}

		#endregion

        #region Cycle Journey preferences

        /// <summary>
        /// Sets/gets the cycle journey type
        /// </summary>
        public string CycleJourneyType
        {
            get { return GetString(ProfileKeys.CYCLE_JOURNEY_TYPE); }
            set { userProfile.Properties[ProfileKeys.CYCLE_JOURNEY_TYPE].Value = value; }
        }

        /// <summary>
        /// Sets/gets the max cycle speed text entered 
        /// </summary>
        public string CycleSpeedText
        {
            get { return GetString(ProfileKeys.CYCLE_SPEED_TEXT); }
            set { userProfile.Properties[ProfileKeys.CYCLE_SPEED_TEXT].Value = value; }
        }

        /// <summary>
        /// Sets/gets the cycle speed unit
        /// </summary>
        public string CycleSpeedUnit
        {
            get { return GetString(ProfileKeys.CYCLE_SPEED_UNIT); }
            set { userProfile.Properties[ProfileKeys.CYCLE_SPEED_UNIT].Value = value; }
        }

        /// <summary>
        /// Sets/gets if unlit roads should be avoided or not
        /// </summary>
        public bool CycleAvoidUnlitRoads
        {
            get { return GetBool(ProfileKeys.CYCLE_AVOID_UNLITROADS); }
            set { userProfile.Properties[ProfileKeys.CYCLE_AVOID_UNLITROADS].Value = value; }
        }

        /// <summary>
        /// Sets/gets if walking your bike should be avoided or not
        /// </summary>
        public bool CycleAvoidWalkingBike
        {
            get { return GetBool(ProfileKeys.CYCLE_AVOID_WALKINGBIKE); }
            set { userProfile.Properties[ProfileKeys.CYCLE_AVOID_WALKINGBIKE].Value = value; }
        }

        /// <summary>
        /// Sets/gets if steep climbs should be avoided or not
        /// </summary>
        public bool CycleAvoidSteepClimbs
        {
            get { return GetBool(ProfileKeys.CYCLE_AVOID_STEEPCLIMBS); }
            set { userProfile.Properties[ProfileKeys.CYCLE_AVOID_STEEPCLIMBS].Value = value; }
        }

        /// <summary>
        /// Sets/gets if time based restrictions should be avoided or not
        /// </summary>
        public bool CycleAvoidTimeBased
        {
            get { return GetBool(ProfileKeys.CYCLE_AVOID_TIMEBASED); }
            set { userProfile.Properties[ProfileKeys.CYCLE_AVOID_TIMEBASED].Value = value; }
        }

        #endregion

		#endregion

		#region Public methods
		public void Update()
		{
			userProfile.Update();
		}
		#endregion


		#region Private methods
		private bool GetBool(string key)
		{
			object val = null;
			if( userProfile.Properties.ContainsKey( key ) )
			{
				val = userProfile.Properties[ key ].Value;
			}
			if( val == null )
			{
				return false; 
			}
			return (bool)val;
		}

		private void SetBool(string key, bool val)
		{
			if( val )
			{
				userProfile.Properties[ key ].Value = val;
			}
			else
			{
				userProfile.Properties.Remove( key );
			}
		}

		private int GetInt(string key)
		{
			object val = null;
			if( userProfile.Properties.ContainsKey( key ) )
			{
				val = userProfile.Properties[ key ].Value;
			}

			string strval = val as string;
			if( val is int )
			{
				return (int)val;
			}
			else if( strval != null )
			{
				// transform;
				try
				{
					return Int32.Parse(strval, CultureInfo.CurrentCulture.NumberFormat);
				}
				catch
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Warning, "Profile["+userProfile.Username+"] has corrupt "+key+" value was: "+strval) );
					// If we cant make it to an int - fall through and return default value
				}
			}
			return 0;
		}

		private string GetString(string key)
		{
			object val = null;
			if( userProfile.Properties.ContainsKey( key ) )
			{
				val = userProfile.Properties[ key ].Value;
			}
			if( val == null )
			{
				return string.Empty; 
			}
			return (string)val;
		}

		#endregion

	}
}
