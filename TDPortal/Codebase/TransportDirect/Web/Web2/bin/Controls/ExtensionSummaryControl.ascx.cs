// *********************************************** 
// NAME                 : ExtensionSummaryControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 12/05/2004 
// DESCRIPTION          : Custom control to display 
//						  the origin and destination 
//						  location of the current 
//						  journey extension.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ExtensionSummaryControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:20:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:14   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:16:30   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:24:04   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Sep 30 2004 13:13:06   jbroome
//Extend Journey additional changes (DD02101d)
//
//   Rev 1.2   Sep 03 2004 14:57:52   RHopkins
//IR1387 Change to display fixed-end location description from Itinerary rather than current Request, so that the text is the same in both "journeys found" panels.
//
//   Rev 1.1   May 26 2004 13:46:36   ESevern
//added display of extension origin and destination
//
//   Rev 1.0   May 12 2004 16:52:50   ESevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
		
	/// <summary>
	///	Displays the current journey search (extension) origin and destination locations
	/// </summary>
	public partial  class ExtensionSummaryControl : TDUserControl 
	{

		#region Declaration




		#endregion

		#region Page Load / Pre Render
		/// <summary>.
		/// Obtains journey origin and destination description from the current session manager
		/// journey request
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			if (this.Visible)
			{
				setUpControls();
			}
			base.OnPreRender(e);
		}

		
		/// <summary>
		/// Sets label text and control visibility
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (((TDPage)Page).PageId == PageId.JourneyPlannerInput)
			{
				this.Visible = false;
			}
		}

		#endregion

		#region Set Up Controls
		/// <summary>
		/// Method which sets visibilities and text of all table cells.
		/// </summary>
		private void setUpControls()
		{
			literalTitle.Text = GetResource("ExtensionSummaryControl.Header");

			// JourneyRequest to be obtained from SessionManager (since it has not been 
			// added to the itinerary yet)
			ITDJourneyRequest journeyRequest = TDSessionManager.Current.JourneyRequest;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			if(journeyRequest != null)
			{
				if (itineraryManager.OutwardExists || itineraryManager.ReturnExists)
				{
					if (itineraryManager.OutwardExists)
					{
						cellTo.Text = GetResource("JourneysSearchedForControl.Seperator");
						cellTitle.Visible = (itineraryManager.ReturnLength > 0);
						cellTitle.Text = GetResource("JourneysSearchedForControl.labelOutward");
						if (itineraryManager.ExtendEndOfItinerary)
						{
							cellDepartLocation.Text = itineraryManager.OutwardArriveLocation().Description;
							cellArriveLocation.Text = journeyRequest.DestinationLocation.Description;
						}
						else
						{
							cellDepartLocation.Text = journeyRequest.OriginLocation.Description;
							cellArriveLocation.Text = itineraryManager.OutwardDepartLocation().Description;
						}
					}
					else
					{
						// No outward journey info
						rowOutwardLocation.Visible = false;			
					}
					if (itineraryManager.ReturnExists)
					{
						cellReturnTitle.Text = GetResource("JourneysSearchedForControl.labelReturn");
						cellReturnTo.Text = GetResource("JourneysSearchedForControl.Seperator");
						if (itineraryManager.ExtendEndOfItinerary)
						{
							cellReturnDepartLocation.Text = journeyRequest.DestinationLocation.Description;	
							cellReturnArriveLocation.Text = itineraryManager.ReturnDepartLocation().Description;
						}
						else
						{
							cellReturnDepartLocation.Text =	itineraryManager.ReturnArriveLocation().Description;
							cellReturnArriveLocation.Text = journeyRequest.OriginLocation.Description;
						}
					}
					else
					{
						// No return journey info
						rowReturnLocation.Visible = false;
					}
				}
				else
				{
					// No journey info returned - error
					// hide control
					outerDiv.Visible = false;
				}
			}
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}

}
