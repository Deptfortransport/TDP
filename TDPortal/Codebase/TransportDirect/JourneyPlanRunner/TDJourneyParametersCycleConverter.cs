// ****************************************************************
// NAME         : TDJourneyParametersCycleConverter.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 10 June 2008
// DESCRIPTION  : Class to place the TDJourneyParameters values in to the TDCyclePlannerRequest values
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/TDJourneyParametersCycleConverter.cs-arc  $
//
//   Rev 1.8   Oct 15 2010 10:54:44   apatel
//Updated to accept multiple Cycle algorithm dlls (Doc Ref ATO687)
//Resolution for 5622: Update CTP to accept multiple function dlls (Doc Ref: ATO687)
//
//   Rev 1.7   Dec 03 2008 17:18:12   mturner
//Added serializable tag
//
//   Rev 1.6   Nov 25 2008 14:19:10   mturner
//Updated to populate some of the user preferences from the journey params object. Fix for IR5164.
//Resolution for 5164: Cycle Planner - Some of the UI check boxes don't  do anything
//
//   Rev 1.5   Oct 31 2008 16:00:00   mturner
//Updated to read default user preferences from DB properties rather than having them hardcoded.
//
//   Rev 1.4   Oct 10 2008 15:56:56   mmodi
//Updated to populate user preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Sep 02 2008 10:42:28   mmodi
//Update penalty function logic
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:12:24   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 04 2008 10:22:24   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:36:46   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// TDJourneyParametersCycleConverter class.
    /// Does NOT inherit from ITDJourneyParameterConverter.cs as it returns a TDCyclePlannerRequest object
    /// </summary>
    [Serializable]
    public class TDJourneyParametersCycleConverter
    {
        #region Private members

        //private string PENALTYFUNCTION_LOCATION = "CyclePlanner.PlannerControl.PenaltyFunction.Location";
        private string PENALTYFUNCTION_PREFIX = "CyclePlanner.PlannerControl.PenaltyFunction.{0}.Prefix";
        private string NUMBER_OF_PREFERENCES = "CyclePlanner.TDUserPreference.NumberOfPreferences";
        private string PENALTYFUNCTION_FOLDER = "CyclePlanner.PlannerControl.PenaltyFunction.Folder";
        private string PENALTYFUNCTION_DLL = "CyclePlanner.PlannerControl.PenaltyFunction.{0}.Dll";

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TDJourneyParametersCycleConverter()
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Converts journey parameters in to an TDCyclePlannerRequest
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="outwardDateTime"></param>
        /// <param name="returnDateTime"></param>
        /// <returns></returns>
        public ITDCyclePlannerRequest Convert(TDJourneyParameters parameters, TDDateTime outwardDateTime, TDDateTime returnDateTime)
        {
            ITDCyclePlannerRequest request = new TDCyclePlannerRequest();

            TDJourneyParametersMulti jp = (TDJourneyParametersMulti)parameters;

            #region locations
            request.OriginLocation = jp.OriginLocation;
            request.DestinationLocation = jp.DestinationLocation;

            request.CycleViaLocations = new TDLocation[1];
            request.CycleViaLocations[0] = jp.CycleViaLocation;
            #endregion

            #region dates and times
            request.IsReturnRequired = jp.IsReturnRequired && returnDateTime != null;

            request.OutwardArriveBefore = jp.OutwardArriveBefore;
            request.ReturnArriveBefore = jp.ReturnArriveBefore;

            if (outwardDateTime != null)
            {
                request.OutwardDateTime = new TDDateTime[1];
                request.OutwardDateTime[0] = outwardDateTime;
            }
            else
            {
                request.OutwardDateTime = new TDDateTime[0];
            }

            if (returnDateTime != null)
            {
                request.ReturnDateTime = new TDDateTime[1];
                request.ReturnDateTime[0] = returnDateTime;
            }
            else
            {
                request.ReturnDateTime = new TDDateTime[0];
            }
            #endregion

            #region penalty function

            // penalty function must be formatted as 
            // "Call <location of penalty function assembly file>,<penalty function type name>"
            // e.g. "Call C:\CyclePlannerService\Services\RoadInterfaceHostingService\atk.cp.PenaltyFunctions.dll,
            // AtkinsGlobal.JourneyPlanning.PenaltyFunctions.Fastest"

            StringBuilder penaltyFunction = new StringBuilder();
            
            // if user has specified a penalty function override, otherwise construct penalty
            // function using the selected Cycle journey type
            if (string.IsNullOrEmpty(jp.CyclePenaltyFunctionOverride))
            {
                string penaltyFunctionPath = Properties.Current[PENALTYFUNCTION_FOLDER];

                penaltyFunction.Append("Call ");
                penaltyFunction.Append(penaltyFunctionPath);
                if (!penaltyFunctionPath.EndsWith("/"))
                {
                    penaltyFunction.Append("/");
                }
                penaltyFunction.Append(Properties.Current[string.Format(PENALTYFUNCTION_DLL,jp.CycleJourneyType)]);
                penaltyFunction.Append(", ");

                string strPenaltyFunction = Properties.Current[string.Format(PENALTYFUNCTION_PREFIX,jp.CycleJourneyType)] 
                    + "." 
                    + jp.CycleJourneyType;

                penaltyFunction.Append(strPenaltyFunction);
                
                // Save to the journey parameters for use by the Journey results pages
                jp.CyclePenaltyFunction = strPenaltyFunction;
            }
            else
            {
                penaltyFunction.Append(jp.CyclePenaltyFunctionOverride);

                // Save to the journey parameters for use by the Journey results pages
                jp.CyclePenaltyFunction = jp.CyclePenaltyFunctionOverride;
            }

            request.CycleJourneyType = jp.CycleJourneyType;
            request.PenaltyFunction = penaltyFunction.ToString();
            #endregion

            #region user preferences

            // The ID of each user preference must match the IDs specified in the cycle planner configuration file.
            ArrayList userPreferences = new ArrayList();

            TDCycleUserPreference tdUserPreference = null;

            // A property that denotes the size of the array of ser preferences expected by the Atkins CTP
            int numOfProperties = System.Convert.ToInt32(Properties.Current[NUMBER_OF_PREFERENCES]);

            // Build the actual array of user preferences from permanent portal properties
            // these are used in the request sent to the Atkins CTP.
            //
            // NOTE: Although for completeness the Permanent Portal properties DB contains all 15
            // user preferences numbers 5, 6, 12, 13 and 14 are ALWAYS overridden by the code below!
            for (int i = 0; i < numOfProperties; i++)
            {
                switch (i)
                {
                    case 5: // Max Speed
                        double cycleSpeed = System.Convert.ToDouble(jp.CycleSpeedMax) / 1000;
                        tdUserPreference = new TDCycleUserPreference(i.ToString(),
                            cycleSpeed.ToString("F0", TDCultureInfo.CurrentCulture.NumberFormat));
                        break;
                    case 6:  // Avoid Time Based Restrictions
                        tdUserPreference = new TDCycleUserPreference(i.ToString(),
                            System.Convert.ToString(jp.CycleAvoidTimeBased));
                        break;
                    case 12: // Avoid Steep Climbs
                        tdUserPreference = new TDCycleUserPreference(i.ToString(),
                            System.Convert.ToString(jp.CycleAvoidSteepClimbs));
                        break;
                    case 13: // Avoid Unlit Roads
                        tdUserPreference = new TDCycleUserPreference(i.ToString(),
                            System.Convert.ToString(jp.CycleAvoidUnlitRoads));
                        break;
                    case 14: // Avoid Walking your bike
                        tdUserPreference = new TDCycleUserPreference(i.ToString(),
                            System.Convert.ToString(jp.CycleAvoidWalkingYourBike));
                        break;
                    default: 
                        tdUserPreference = new TDCycleUserPreference(i.ToString(), 
                            Properties.Current["CyclePlanner.TDUserPreference.Preference" + i.ToString()]);
                        break;
                }
                userPreferences.Add(tdUserPreference);
            }

            request.UserPreferences = (TDCycleUserPreference[])userPreferences.ToArray(typeof(TDCycleUserPreference));
            #endregion

            return request;
        }

        #endregion
        
        
    }
}
