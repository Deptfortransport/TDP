// *********************************************** 
// NAME                 : JourneyBuilderControl.ascx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 23/01/2006
// DESCRIPTION			: A user control to facilitate the next step in a process to add
//						  an extension to an itinerary.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyBuilderControl.ascx.cs-arc  $
//
//   Rev 1.4   Mar 12 2010 14:37:24   apatel
//Updated GetTDDateTime method so it dont break when time supplies as 'Any'
//Resolution for 5453: Error message displayed when trying to Extend a journey
//
//   Rev 1.3   Feb 25 2010 16:20:56   pghumra
//Code fix applied to resolve issue with date not being displayed on journey details section in the door to door planner when date of travel is different to requested date
//Resolution for 5413: CODE FIX - NEW - DEL 10.x - Issue with seasonal information change from Del 10.8
//
//   Rev 1.2   Mar 31 2008 13:21:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:10   mturner
//Initial revision.
//
//   Rev 1.4   Mar 14 2006 13:20:20   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 08 2006 20:30:16   pcross
//FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Mar 08 2006 16:02:54   RGriffith
//FxCop Suggested Changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 20 2006 12:12:28   pcross
//Changed control so that it only ever has a single button on the right which adds extensions to itinerary
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 27 2006 14:03:30   pcross
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;

	/// <summary>
	///		Summary description for JourneyBuilderControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyBuilderControl : TDPrintableUserControl
	{

		#region Control Declarations


		#endregion

		#region Private member variables
		
		/// <summary>
		/// Itinerary manager used in this control (passed in as a property)
		/// </summary>
		TDItineraryManager itineraryManager;

		#endregion

		#region Properties

		/// <summary>
		/// Read/write property. Gets / sets the itinerary manager used in this control
		/// </summary>
		public TDItineraryManager ItineraryManager
		{
			get { return itineraryManager; }
			set { itineraryManager = value; }
		}

		#endregion

		#region Initialisation

		/// <summary>
		/// Contructor for control
		/// </summary>
		public JourneyBuilderControl()
		{
			// Set the resource file for the control
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion

		#region Private methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Populate text in labels, etc
			PopulateControls();
		}

		/// <summary>
		/// Populates the text in the labels, etc using the resource manager
		/// </summary>
		private void PopulateControls()
		{
			addButton.Text = GetResource("JourneyBuilderControl.AddButton.Text");
		}

        /// <summary>
        /// Creates a date time from the day and time passed
        /// </summary>
        /// <param name="monthYear"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        private TDDateTime GetTDDateTime(string monthYear, string day, string hour, string minute)
        {
            int intyear, intmonth, intday, inthour, intminute;
            string[] monthyearsplit = monthYear.Split(new string[] { "/" }, StringSplitOptions.None);
            if (monthyearsplit.Length == 2)
            {
                if (!int.TryParse(monthyearsplit[0], out intmonth))
                {
                    intmonth = 0;
                }

                if (!int.TryParse(monthyearsplit[1], out intyear))
                {
                    intyear = 0;
                }

                if (!int.TryParse(day, out intday))
                {
                    intday = 0;
                }

                inthour = 0;
                if (!int.TryParse(hour, out inthour))
                {
                    inthour = 0;
                }

                if (!int.TryParse(minute, out intminute))
                {
                    intminute = 0;
                }

                if (intmonth != 0 && intyear != 0 && intday != 0)
                {
                    return new TDDateTime(intyear, intmonth, intday, inthour, intminute, 0);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

		#endregion

		#region Event handlers
		
		/// <summary>
		/// Adds extension to itinerary
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addButton_Click(object sender, EventArgs e)
		{
            TDJourneyParameters journeyParams = TDSessionManager.Current.JourneyParameters;
            TDDateTime outwardDateTime = GetTDDateTime(journeyParams.OutwardMonthYear, journeyParams.OutwardDayOfMonth, journeyParams.OutwardHour, journeyParams.OutwardMinute);
            TDDateTime returnDateTime = GetTDDateTime(journeyParams.ReturnMonthYear, journeyParams.ReturnDayOfMonth, journeyParams.ReturnHour, journeyParams.ReturnMinute);
                                    
			// Add the selected extension(s) to the itinerary
			itineraryManager.AddExtensionToItinerary();

            TDSessionManager.Current.InputPageState.OriginalOutwardDateTime = outwardDateTime;
            TDSessionManager.Current.InputPageState.OriginalReturnDateTime = returnDateTime;
			// Load the Full journey itinerary screen to show the whole itinerary
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ExtendedFullItinerarySummary;
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraWiringEvents();
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

		/// <summary>
		/// Wire in custom events
		/// </summary>
		private void ExtraWiringEvents()
		{
			this.addButton.Click += new EventHandler(addButton_Click);
		}

	}
}
