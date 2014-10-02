// *********************************************** 
// NAME                 : LeaveReturnDatesControlAdapter.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 26/10/2004 
// DESCRIPTION		    : Extends/modifies common functionality of base
// class to provide specific behaviour for non-Find A input pages
// ************************************************ 
using System;
using System.Web.UI.WebControls;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for LeaveReturnDatesControlAdapter.
	/// </summary>
	public sealed class LeaveReturnDatesControlAdapter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public LeaveReturnDatesControlAdapter()
		{

		}

		#region Public Methods
		/// <summary>
		/// Updates the journey parameters with the values of dateControl
		/// </summary>
		/// <param name="dateControl"></param>
		/// <param name="ambiguityMode"></param>
		/// <param name="journeyParams"></param>
		/// <param name="errors"></param>
		public void UpdateJourneyDates(FindLeaveReturnDatesControl dateControl, bool ambiguityMode, TDJourneyParameters journeyParams, ValidationError errors)
		{
			dateControl.LeaveDateControl.AmbiguityMode = ambiguityMode;
			dateControl.ReturnDateControl.AmbiguityMode = ambiguityMode;
			dateControl.LeaveDateControl.DateErrors = errors;
			dateControl.ReturnDateControl.DateErrors = errors;
            
			if (dateControl.LeaveDateControl.DateControl.DisplayMode != GenericDisplayMode.ReadOnly)
			{
				journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
				journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
				journeyParams.OutwardMinute = dateControl.LeaveDateControl.DateControl.Minute;
				journeyParams.OutwardHour = dateControl.LeaveDateControl.DateControl.Hour;
				journeyParams.OutwardAnyTime = dateControl.LeaveDateControl.DateControl.IsAnyTimeSelected;
				if (dateControl.LeaveDateControl.DateControl.DisplayMode != GenericDisplayMode.Ambiguity)
				{
					journeyParams.OutwardArriveBefore = dateControl.LeaveDateControl.DateControl.ArriveBefore;
				}
			}

			if (dateControl.ReturnDateControl.DateControl.DisplayMode != GenericDisplayMode.ReadOnly) 
			{
				journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
				journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
				journeyParams.ReturnMinute = dateControl.ReturnDateControl.DateControl.Minute;
				journeyParams.ReturnHour = dateControl.ReturnDateControl.DateControl.Hour;
				journeyParams.ReturnAnyTime = dateControl.ReturnDateControl.DateControl.IsAnyTimeSelected;
				journeyParams.IsReturnRequired = !dateControl.ReturnDateControl.DateControl.IsNoReturnSelected;
				if (dateControl.ReturnDateControl.DateControl.DisplayMode != GenericDisplayMode.Ambiguity) 
				{
					journeyParams.ReturnArriveBefore = dateControl.ReturnDateControl.DateControl.ArriveBefore;
				}
			}
		}

		/// <summary>
		/// Initialises the dateControl with journey parameters
		/// </summary>
		/// <param name="dateControl"></param>
		/// <param name="ambiguityMode"></param>
		/// <param name="journeyParams"></param>
		/// <param name="errors"></param>
		public void UpdateDateControl(FindLeaveReturnDatesControl dateControl, bool ambiguityMode, TDJourneyParameters journeyParams, ValidationError errors)
		{
			dateControl.LeaveDateControl.AmbiguityMode = ambiguityMode;
			dateControl.ReturnDateControl.AmbiguityMode = ambiguityMode;
			dateControl.LeaveDateControl.DateErrors = errors;
			dateControl.ReturnDateControl.DateErrors = errors;

			dateControl.LeaveDateControl.Populate(
				journeyParams.OutwardHour,
				journeyParams.OutwardMinute,
				journeyParams.OutwardDayOfMonth,
				journeyParams.OutwardMonthYear,
				journeyParams.OutwardArriveBefore);

			dateControl.ReturnDateControl.Populate(
				journeyParams.ReturnHour,
				journeyParams.ReturnMinute,
				journeyParams.ReturnDayOfMonth,
				journeyParams.ReturnMonthYear,
				journeyParams.ReturnArriveBefore);	
		}
		#endregion Public Methods
	}
}
