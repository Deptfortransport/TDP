// *********************************************** 
// NAME                 : FeedbackJourneyControl.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 05/01/2007
// DESCRIPTION          : Control to allow display of journey searched for
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackJourneyControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 12 2009 14:46:20   mmodi
//Updated formatting for times
//
//   Rev 1.3   Oct 13 2008 16:41:38   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Aug 22 2008 10:28:00   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:20:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:46   mturner
//Initial revision.
//
//   Rev 1.2   Jan 24 2007 12:33:22   mmodi
//Added code to display the transport mode type
//Resolution for 4344: Contact Us: Feedback Viewer page shows error text when viewing session info
//
//   Rev 1.1   Jan 12 2007 14:13:48   mmodi
//Updated code as part of development
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 08 2007 10:23:52   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;

	/// <summary>
	///	Control to display details of the journey search for.
	/// </summary>
	public partial class FeedbackJourneyControl : TDUserControl
	{

		#region Controls
		




		


		public const int MINUTES_OR_HOURS = 10;

		#endregion

		#region Private variables

		string parameterInputSummary = string.Empty;
		string journeyReferenceNumber = string.Empty; 

		#endregion

        #region Page Load / Pre Render
		
		/// <summary>
		/// Page Load Method.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (ShowControl) 
			{
				// Make sure outer div is visible
				outerDiv.Visible = true;
				setUpControls();
			}
			else
			{
				// Not showing control - hide outer div
				outerDiv.Visible = false;
			}
		}

		/// <summary>.
		/// OnPreRender Method
		/// </summary>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{			
			//base.OnPreRender(e);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Method which sets visibilities and text of all table cells.
		/// </summary>
		private void setUpControls()
		{

			ITDSessionManager tdSessionManager = TDSessionManager.Current;
			TDJourneyParameters journeyParameters = tdSessionManager.JourneyParameters;

			// Get the journey info
			// Checking if session journey reference number is blank 
			if((tdSessionManager.Partition == TDSessionPartition.TimeBased)&&(tdSessionManager!= null && tdSessionManager.JourneyResult != null) && (tdSessionManager.JourneyResult.JourneyReferenceNumber > 0))
			{   	
				parameterInputSummary =	 tdSessionManager.JourneyParameters.InputSummary();
				journeyReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();   
				journeyParameters = tdSessionManager.JourneyParameters;
			}
            else if ((tdSessionManager.Partition == TDSessionPartition.TimeBased) && (tdSessionManager != null && tdSessionManager.CycleResult != null) && (tdSessionManager.CycleResult.JourneyReferenceNumber > 0))
            {
                parameterInputSummary = tdSessionManager.JourneyParameters.InputSummary();
                journeyReferenceNumber = tdSessionManager.CycleResult.JourneyReferenceNumber.ToString();
                journeyParameters = tdSessionManager.JourneyParameters;
            }
            else
            {
                TDItineraryManager itineraryManager = TDItineraryManager.Current;
                TDSegmentStore lastJourneyStore;

                // Getting last stored itinerary journey
                lastJourneyStore = itineraryManager.GetLastAddedSegment();

                // Checking if itinerary journey reference number.
                if (lastJourneyStore != null && tdSessionManager.Partition == TDSessionPartition.TimeBased && lastJourneyStore.JourneyResult != null && (lastJourneyStore.JourneyResult.JourneyReferenceNumber > 0))
                {
                    parameterInputSummary = lastJourneyStore.JourneyParameters.InputSummary();
                    journeyReferenceNumber = lastJourneyStore.JourneyResult.JourneyReferenceNumber.ToString();
                    journeyParameters = lastJourneyStore.JourneyParameters;
                }
            }

			literalTitle.Text = GetResource("FeedbackJourneyControl.Title");
			cellTo.Text = GetResource("FeedbackJourneyControl.Seperator");

			cellDepartLocation.Text = journeyParameters.OriginLocation.Description;
			cellArriveLocation.Text = journeyParameters.DestinationLocation.Description;

			// Outward time
			string outwardTime;
			if (journeyParameters.OutwardArriveBefore)
				outwardTime = "Outward journey: Arriving by ";
			else
				outwardTime = "Outward journey: Leaving after ";

			if (journeyParameters.OutwardHour != "Any")
				outwardTime += journeyParameters.OutwardHour.PadLeft(2, '0') + ":" + journeyParameters.OutwardMinute.PadLeft(2, '0');
			else
				outwardTime += "Anytime";

			outwardTime += ", " + journeyParameters.OutwardDayOfMonth + "/" + journeyParameters.OutwardMonthYear;


			// Return time
			string returnTime;
			if (journeyParameters.IsReturnRequired)
			{
				if (journeyParameters.ReturnArriveBefore)
					returnTime = "Return journey: Arriving by ";
				else
					returnTime = "Return journey: Leaving after ";

				if (journeyParameters.ReturnHour != "Any")
					returnTime += journeyParameters.ReturnHour.PadLeft(2,'0') + ":" + journeyParameters.ReturnMinute.PadLeft(2, '0');
				else
					returnTime += "Anytime";

				returnTime += ", " + journeyParameters.ReturnDayOfMonth + "/" + journeyParameters.ReturnMonthYear;
			}
			else
			{
				returnTime = "Return journey: None";
			}

			// Populate the Outward journey time
			cellOutwardTime.Text = outwardTime;
			cellReturnTime.Text = returnTime;

			string modes = "Using: ";
			try
			{	// Cast to the appropriate JourneyParameters type and then extract the transport modes
				TDJourneyParametersMulti jpm = journeyParameters as TDJourneyParametersMulti;
				if (jpm != null)
				{
					foreach (ModeType mymode in jpm.PublicModes)
					{
						modes += mymode.ToString() + " ";
					}

					if (jpm.PrivateRequired)
					{
						modes += "Car";
					}
				}
				TDJourneyParametersFlight jpf = journeyParameters as TDJourneyParametersFlight;
				if (jpf != null)
				{
					modes += "Air";
				}
				TDJourneyParametersVisitPlan jpv = journeyParameters as TDJourneyParametersVisitPlan;
				if (jpv != null)
				{
					modes += "Public transport";
				}
			}
			catch
			{
				// if an error occurs, dont show any modes
				modes = string.Empty;
			}

			cellTransportModes.Text = modes;		
		}

		/// <summary>
		/// Read only property which checks Session information to to determine whether the 
		/// control will be displayed on the screen or not.
		/// </summary>
		public bool ShowControl
		{
			get 
			{
				bool showControl = false;
				ITDSessionManager tdSessionManager = TDSessionManager.Current;
				if((tdSessionManager.Partition == TDSessionPartition.TimeBased)&&(tdSessionManager!= null && tdSessionManager.JourneyResult != null) && (tdSessionManager.JourneyResult.JourneyReferenceNumber > 0))
				{   	
					showControl = true;
				}
                else if((tdSessionManager.Partition == TDSessionPartition.TimeBased)&&(tdSessionManager!= null && tdSessionManager.CycleResult != null) && (tdSessionManager.CycleResult.JourneyReferenceNumber > 0))
                {
                    showControl = true;
                }
				else
				{
					TDItineraryManager itineraryManager = TDItineraryManager.Current;
					TDSegmentStore lastJourneyStore; 

					// Getting last stored itinerary journey
					lastJourneyStore = itineraryManager.GetLastAddedSegment(); 
				
					// Checking if itinerary journey reference number.
					if (lastJourneyStore != null && tdSessionManager.Partition == TDSessionPartition.TimeBased && lastJourneyStore.JourneyResult != null && (lastJourneyStore.JourneyResult.JourneyReferenceNumber > 0))
					{
						showControl = true;
					}  
				}

				return showControl;
			}																									
		}

		/// <summary>
		/// Formats a specified time in HH:mm with text from
		/// langStrings for display on the control.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="depart"></param>
		/// <returns>formatted time string </returns>
		private string AssembleTimeString(TDDateTime time, bool depart)
		{
			string theTime = string.Empty;

			// Round up if necessary for consistency.
			if(time.Second >= 30)
				time = time.AddMinutes(1);

			// construct the leaving time string
			string formattedHoursMins = time.ToString("HH:mm");
			string formattedDate = time.ToString("dd/MM");

			if(depart)
				theTime += GetResource("JourneySearchedForControl.DepartureTime");
			else
				theTime += GetResource("JourneySearchedForControl.ArrivalTime");

			return string.Format(theTime, formattedHoursMins, formattedDate);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Returns summary of journey input details
		/// </summary>
		public string JourneyInputSummary
		{
			get { return parameterInputSummary; }
		}


		/// <summary>
		/// Returns journey reference number
		/// </summary>
		public string JourneyReferenceNumber
		{
			get { return journeyReferenceNumber; }
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
