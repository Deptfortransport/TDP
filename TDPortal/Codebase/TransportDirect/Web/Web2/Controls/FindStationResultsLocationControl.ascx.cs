// *********************************************** 
// NAME                 : FindStationResulsLocationControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 14/05/2004 
// DESCRIPTION  : Control displaying the location selected by user to find nearby stations
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindStationResultsLocationControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:20:50   mturner
//Drop3 from Dev Factory
//
//  
//  Rev DevFactory Mar 02 2008 21:07:00 apatel
//  LocationNameTitle label Removed.
//  
//  Rev DevFactory Mar 02 2008 21:07:00 apatel
//  Added property for help label control
//
//  Rev DevFactory Feb 06 2008 08:43:00 apatel
//  Removed the help button and move it to FindStationResult.aspx page. Change the layout of controls.
//
//   Rev 1.0   Nov 08 2007 13:14:26   mturner
//Initial revision.
//
//   Rev 1.18   Feb 23 2006 19:16:42   build
//Automatically merged from branch for stream3129
//
//   Rev 1.17.1.0   Jan 10 2006 15:24:54   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.17   Nov 03 2005 17:01:32   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.16.1.0   Oct 13 2005 17:40:06   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.16   Nov 17 2004 16:27:12   SWillcock
//Fix for IR 1747
//Resolution for 1747: Find station airport help should be appropriate to current context
//
//   Rev 1.15   Sep 30 2004 13:31:08   RGeraghty
//Added default alt tag
//
//   Rev 1.14   Sep 02 2004 10:17:08   jmorrissey
//Added alt tags for Find A
//
//   Rev 1.13   Aug 26 2004 12:04:54   jmorrissey
//IR1317 - fixed small bug in control constructor
//
//   Rev 1.12   Aug 25 2004 10:27:04   jmorrissey
//IR1357 - now stores the resolved location in FindFlightPageState in SessionManager
//
//   Rev 1.11   Aug 10 2004 13:09:06   jmorrissey
//Added help labels for other FindA modes
//
//   Rev 1.10   Jul 27 2004 14:03:10   passuied
//FindStation Del6.1 :Finalised version 
//
//   Rev 1.9   Jul 26 2004 20:23:54   passuied
//Changes to implement AmendSeach Functionality. Created and Amend mode in the tristate to enable the display of a valid location inside the locationUnspecified control.
//We send this mode when a one use session key has been set by a click on AmendSearch button.
//Also tweak in toFromLocationControl to display the to and from location/station controls correctly
//
//   Rev 1.8   Jul 21 2004 10:51:00   passuied
//Re work for integration with FindStation del6.1. Working. Needs work on resources
//
//   Rev 1.7   Jul 14 2004 13:00:26   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.6   Jul 12 2004 14:13:46   passuied
//use of new property Mode of FindPageState base class
//
//   Rev 1.5   Jun 23 2004 17:27:14   passuied
//made help icon invisible for printable page
//
//   Rev 1.4   Jun 23 2004 11:20:16   passuied
//addition of help for findStation pages
//
//   Rev 1.3   Jun 02 2004 16:38:32   passuied
//working version
//
//   Rev 1.2   May 28 2004 17:48:18   passuied
//update as part of FindStation development
//
//   Rev 1.1   May 21 2004 15:49:50   passuied
//partly working Find station pages and controls. Check in for backup
//
//   Rev 1.0   May 14 2004 17:35:20   passuied
//Initial Revision


using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///		Summary description for FindStationResultsLocationControl.
	/// </summary>
	public partial  class FindStationResultsLocationControl : TDUserControl
	{

		private FindStationPageState stationPageState;
		private FindFlightPageState flightPageState;
		private FindPageState pageState;
		// default is not printable
		private bool boolPrintablePage = false;


		private const string RES_LOCATIONTITLE = "FindStationResultsLocationControl.labelTitle";

		public FindStationResultsLocationControl()
		{			
			stationPageState = TDSessionManager.Current.FindStationPageState;
			pageState = TDSessionManager.Current.FindPageState;

		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			labelLocationName.Text = stationPageState.CurrentLocation.Description;

			commandNewLocation.Text = Global.tdResourceManager.GetString("FindStationResultsLocationControl.commandNewLocation.Text");
			
			SetText();

			// button invisible if printable page
			commandNewLocation.Visible = !boolPrintablePage && (pageState.Mode != FindAMode.Station); // don't display if in station mode!

			//set help label according to the finda mode 
			switch (pageState.Mode)
			{

                case FindAMode.Flight:					
				{
					

					flightPageState = (FindFlightPageState)TDSessionManager.Current.FindPageState;
					switch (stationPageState.LocationType)
					{
						case FindStationPageState.CurrentLocationType.From:
							FindStationResultsHelpLabel.Text = GetResource( "FindStationResultsAirportHelpLabelSelectFrom");
							flightPageState.ResolvedFromLocation = stationPageState.CurrentLocation;
							break;
						case FindStationPageState.CurrentLocationType.To:
							FindStationResultsHelpLabel.Text = GetResource( "FindStationResultsAirportHelpLabelSelectTo");
							flightPageState.ResolvedToLocation = stationPageState.CurrentLocation;
							break;
					}
					break;
				}
				case FindAMode.Train:					
				{
					

					switch (stationPageState.LocationType)
					{
						case FindStationPageState.CurrentLocationType.From:
							FindStationResultsHelpLabel.Text = GetResource( "FindStationResultsTrainHelpLabelSelectFrom");
							break;
						case FindStationPageState.CurrentLocationType.To:
							FindStationResultsHelpLabel.Text = GetResource( "FindStationResultsTrainHelpLabelSelectTo");
							break;
					}
					break;
				}
				case FindAMode.Coach:			
				{

					

					switch (stationPageState.LocationType)
					{
						case FindStationPageState.CurrentLocationType.From:
							FindStationResultsHelpLabel.Text = GetResource( "FindStationResultsCoachHelpLabelSelectFrom");
							break;
						case FindStationPageState.CurrentLocationType.To:
							FindStationResultsHelpLabel.Text = GetResource( "FindStationResultsCoachHelpLabelSelectTo");
							break;
					}
					break;
				}
				default:
				{
					

					// determine which help to show - 'travel from' /to or 'next'
					if(TDSessionManager.Current.FindStationPageState.LocationFrom.Status != TDLocationStatus.Valid
						&& TDSessionManager.Current.FindStationPageState.LocationTo.Status != TDLocationStatus.Valid)
					{
						FindStationResultsHelpLabel.Text = GetResource("FindStationResultsHelpLabelSelectFromTo");						
					}
					else
					{
						FindStationResultsHelpLabel.Text = GetResource("FindStationResultsHelpLabelSelectNext");
					}

					break;
				}
			}

		}

		private void SetText()
		{

			string sStationType = FindStationHelper.GetStationTypeString();
			//sStationType.S += (char)32;
            

            // CCN 0427 locationNametitle removed
			//labelLocationNameTitle.Text = string.Format(
			//	GetResource(RES_LOCATIONTITLE),sStationType);

		}
		public bool Printable
		{
			set
			{
				boolPrintablePage = value;
			}
		}

        /// <summary>
        /// Read only property for HelpLabelControl
        /// </summary>
        public HelpLabelControl HelpLabel
        {
            get { return FindStationResultsHelpLabel; }
        }

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
			this.commandNewLocation.Click += new EventHandler(this.CommandNewLocationClick);

		}
		#endregion

		private void CommandNewLocationClick(object sender, EventArgs e)
		{
			stationPageState.CurrentLocation = new TDLocation();
			stationPageState.CurrentSearch = new LocationSearch();
			stationPageState.LocationControlType.Type = TDJourneyParameters.ControlType.Default;
			stationPageState.CurrentSearch.SearchType = SearchType.Locality;

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationResultsNewLocation;

		}

		
	}
}
