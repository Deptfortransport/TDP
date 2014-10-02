// *********************************************** 
// NAME			: ErrorMessageAdapter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 28/04/08
// DESCRIPTION	: Provides methods to populate error controls.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/ErrorMessageAdapter.cs-arc  $
//
//   Rev 1.6   Jan 04 2013 15:34:34   mmodi
//Method to display errors from specified resource string ids
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Dec 05 2012 13:56:10   mmodi
//Display accessible location errors
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Feb 20 2010 19:28:12   mmodi
//Add further international planner errors
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 09 2010 09:45:18   apatel
//Updated for TD International planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Oct 12 2009 16:24:30   mmodi
//Updated for EBC unavailable error
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 13 2008 16:41:26   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.3   Sep 16 2008 17:15:46   mmodi
//Updated to display a configurable max distance message
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.2   Aug 01 2008 16:31:42   mmodi
//Added check for cycle planner available
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.1   Jul 28 2008 13:06:42   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.0   Jun 20 2008 14:41:08   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   May 01 2008 17:27:08   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections.Generic;


namespace TransportDirect.UserPortal.Web.Adapters
{
    public static class ErrorMessageAdapter
    {
        #region Public methods

        /// <summary>
        /// Displays messages in the error display control
        /// </summary>
        public static void UpdateErrorDisplayControl(Panel panelErrorDisplayControl, ErrorDisplayControl errorDisplayControl, List<string> resourceIds)
        {
            // Display errors in the session
            panelErrorDisplayControl.Visible = false;
            errorDisplayControl.Visible = false;

            if (resourceIds != null)
            {
                if (resourceIds.Count > 0)
                {
                    List<string> errorsList = new List<string>();

                    foreach (string errorId in resourceIds)
                    {
                        // Get the display text for the error
                        string text = Global.tdResourceManager.GetString(errorId);

                        if (!string.IsNullOrEmpty(text))
                            errorsList.Add(text);
                    }

                    if (errorsList.Count > 0)
                    {
                        errorDisplayControl.ErrorStrings = errorsList.ToArray();

                        errorDisplayControl.Type = ErrorsDisplayType.Error;

                        // Show the panel and control
                        panelErrorDisplayControl.Visible = true;
                        errorDisplayControl.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Displays messages in the error display control
        /// </summary>
        public static void UpdateErrorDisplayControl(Panel panelErrorDisplayControl, ErrorDisplayControl errorDisplayControl, InputSessionError[] inputSessionErrors)
        {
            // Display errors in the session
            panelErrorDisplayControl.Visible = false;
            errorDisplayControl.Visible = false;

            if (inputSessionErrors != null)
            {
                if (inputSessionErrors.Length > 0)
                {
                    ArrayList errorsList = new ArrayList();

                    foreach (InputSessionError error in inputSessionErrors)
                    {
                        // Get the display text for the error
                        string text = Global.tdResourceManager.GetString("InputSessionError." + error.ToString());

                        if (!string.IsNullOrEmpty(text))
                            errorsList.Add(text);
                    }

                    if (errorsList.Count > 0)
                    {
                        errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

                        errorDisplayControl.Type = ErrorsDisplayType.Error;

                        // Show the panel and control
                        panelErrorDisplayControl.Visible = true;
                        errorDisplayControl.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Displays messages in the error display control
        /// </summary>
        public static void UpdateErrorDisplayControl(Panel panelErrorDisplayControl, ErrorDisplayControl errorDisplayControl, ValidationError errors)
        {
            // Display errors in the session
            panelErrorDisplayControl.Visible = false;
            errorDisplayControl.Visible = false;

            // If the panel is not visible, and we have errors, then must be cycle planner specific.
            // So we process them here
            if (!panelErrorDisplayControl.Visible)
            {
                if (errors != null && errors.MessageIDs.Count > 0)
                {
                    ArrayList errorsList = new ArrayList();
                    
                    if (AreLocationsPointsInvalid(errors))
                    {
                        //get resource id of the error from the errors hashtable					
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.OriginLocationHasNoPoint].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.OriginLocationHasNoPoint);
                        errors.Remove(ValidationErrorID.DestinationLocationHasNoPoint);
                        errors.Remove(ValidationErrorID.CycleViaLocationHasNoPoint);
                    }
                    else if (AreLocationsInInvalidCycleArea(errors))
                    {
                        //get resource id of the error from the errors hashtable					
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.LocationInInvalidCycleArea].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.LocationInInvalidCycleArea);
                    }
                    else if (AreLocationsNotInSameCycleArea(errors))
                    {
                        //get resource id of the error from the errors hashtable					
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.LocationPointsNotInSameCycleArea].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.LocationPointsNotInSameCycleArea);
                    }
                    else if (AreLocationsTooFarApart(errors))
                    {
                        int maxJourneyDistanceMetres = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.MaxJourneyDistance.Metres"]);
                        int maxJourneyDistanceKm = Convert.ToInt32(maxJourneyDistanceMetres / 1000); 

                        //get resource id of the error from the errors hashtable					
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.DistanceBetweenLocationsTooGreat].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(
                                string.Format(
                                Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture),
                                maxJourneyDistanceKm)
                                + " ");

                       errors.Remove(ValidationErrorID.DistanceBetweenLocationsTooGreat);
                    }
                    else if (AreLocationsAndViaTooFarApart(errors))
                    {
                        int maxJourneyDistanceMetres = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.MaxJourneyDistance.Metres"]);
                        int maxJourneyDistanceKm = Convert.ToInt32(maxJourneyDistanceMetres / 1000); 

                        //get resource id of the error from the errors hashtable					
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.DistanceBetweenLocationsAndViaTooGreat].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(
                                string.Format(
                                Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture),
                                maxJourneyDistanceKm)
                                + " ");

                        errors.Remove(ValidationErrorID.DistanceBetweenLocationsAndViaTooGreat);
                    }
                    else if (IsCyclePlannerAvailable(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.CyclePlannerUnavailable].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.CyclePlannerUnavailable);
                    }
                    else if (IsEnvironmentalBenefitsCalculatorUnavailable(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.EnvironmentalBenefitsCalculatorUnavailable].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.EnvironmentalBenefitsCalculatorUnavailable);
                    }
                    else if (IsInternationalPlannerUnAvailable(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.InternationalPlannerUnavailable].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.InternationalPlannerUnavailable);
                    }
                    else if (IsInternationalPlannerJourneyNotPermitted(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.InternationalPlannerJourneyNotPermitted].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.InternationalPlannerJourneyNotPermitted);
                    }
                    else if (IsInternationalPlannerModeNotPermitted(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.InternationalPlannerInvalidMode].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.InternationalPlannerInvalidMode);
                    }
                    else if (IsInternationalOriginLocationInValid(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.OriginLocationHasNoNaptan].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.OriginLocationHasNoNaptan);
                    }
                    else if (IsInternationalDestinationLocationInValid(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.DestinationLocationHasNoNaptan].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.DestinationLocationHasNoNaptan);
                    }
                    else if (IsOriginLocationNotAccessible(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.OriginLocationNotAccessible].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.OriginLocationNotAccessible);
                    }
                    else if (IsDestinationLocationNotAccessible(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.DestinationLocationNotAccessible].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.DestinationLocationNotAccessible);
                    }
                    else if (IsPublicViaLocationNotAccessible(errors))
                    {
                        errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.PublicViaLocationNotAccessible].ToString();

                        if (!string.IsNullOrEmpty(errors.MsgResourceID))
                            errorsList.Add(Global.tdResourceManager.GetString(errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ");

                        errors.Remove(ValidationErrorID.PublicViaLocationNotAccessible);
                    }


                    if (errorsList.Count > 0)
                    {
                        errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

                        errorDisplayControl.Type = ErrorsDisplayType.Error;
                    }
                }

                // Set the panel visibility
                panelErrorDisplayControl.Visible = errorDisplayControl.ErrorStrings.Length != 0;
                errorDisplayControl.Visible = panelErrorDisplayControl.Visible;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// location has no Point value (to, from, cycle via)
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating location has no Point, false otherwise</returns>
        private static bool AreLocationsPointsInvalid(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.OriginLocationHasNoPoint)
            || errors.Contains(ValidationErrorID.DestinationLocationHasNoPoint)
            || errors.Contains(ValidationErrorID.CycleViaLocationHasNoPoint);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// locations are in an Invalid cycle area
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating location is in invalid cycle area, false otherwise</returns>
        private static bool AreLocationsInInvalidCycleArea(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.LocationInInvalidCycleArea);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// locations are not in the Same cycle area
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating location are not in the same cycle area, false otherwise</returns>
        private static bool AreLocationsNotInSameCycleArea(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.LocationPointsNotInSameCycleArea);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// locations are too far apart
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating locations are too far apart, false otherwise</returns>
        private static bool AreLocationsTooFarApart(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.DistanceBetweenLocationsTooGreat);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// locations and via are too far apart
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating locations and via are too far apart, false otherwise</returns>
        private static bool AreLocationsAndViaTooFarApart(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.DistanceBetweenLocationsAndViaTooGreat);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// cycle planner is unavailable
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicatingcycle planner is unavailable, false otherwise</returns>
        private static bool IsCyclePlannerAvailable(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.CyclePlannerUnavailable);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// EBC is unavailable
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool IsEnvironmentalBenefitsCalculatorUnavailable(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.EnvironmentalBenefitsCalculatorUnavailable);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// international planner is unavailable
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating international planner is unavailable, false otherwise</returns>
        private static bool IsInternationalPlannerUnAvailable(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.InternationalPlannerUnavailable);
        }

        /// <summary>
        /// Returns true if International Journey not permitted between selected journey locations
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating international planner unable to plan a journey betweet locations selected</returns>
        private static bool IsInternationalPlannerJourneyNotPermitted(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.InternationalPlannerJourneyNotPermitted);
        }

        /// <summary>
        /// Returns true if International Journey not permitted with the mode of journey specified
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating international planner unable to plan a journey with the specified mode of journey</returns>
        private static bool IsInternationalPlannerModeNotPermitted(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.InternationalPlannerInvalidMode);
        }

        /// <summary>
        /// Returns true if International Journey locations are valid
        /// </summary>
        private static bool IsInternationalOriginLocationInValid(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.OriginLocationHasNoNaptan);
        }

        /// <summary>
        /// Returns true if International Journey locations are valid
        /// </summary>
        private static bool IsInternationalDestinationLocationInValid(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.DestinationLocationHasNoNaptan);
        }

        /// <summary>
        /// True if origin location is not accessible
        /// </summary>
        private static bool IsOriginLocationNotAccessible(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.OriginLocationNotAccessible);
        }

        /// <summary>
        /// True if origin location is not accessible
        /// </summary>
        private static bool IsDestinationLocationNotAccessible(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.DestinationLocationNotAccessible);
        }

        /// <summary>
        /// True if origin location is not accessible
        /// </summary>
        private static bool IsPublicViaLocationNotAccessible(ValidationError errors)
        {
            return errors.Contains(ValidationErrorID.PublicViaLocationNotAccessible);
        }

        #endregion
    }
}
