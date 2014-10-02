// *********************************************** 
// NAME                 : FindCarParkResultsLocationControl.ascx
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/08/2006 
// DESCRIPTION  : Control displaying the location selected by user to find car parks
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindCarParkResultsLocationControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Oct 01 2009 16:25:10   apatel
//Updates for Social Bookmark links
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.3   May 08 2008 11:41:02   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.2   Mar 31 2008 13:20:26   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 02 2008 17:51 apatel
//  Added read only property to give access to findCarParkResultsHelpLabelControl
//
//   Rev 1.0   Nov 08 2007 13:13:58   mturner
//Initial revision.
//
//   Rev 1.3   Sep 22 2006 14:16:00   mmodi
//Updated to prevent NewLocation being shown when coming from Find car route
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4197: Car Parking: New location button shown on Nearest car parks results
//
//   Rev 1.2   Aug 31 2006 14:39:24   MModi
//Updated to use correct Help control
//
//   Rev 1.1   Aug 14 2006 11:13:08   esevern
//Added find new location transition event
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 11 2006 13:56:10   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///		Summary description for FindCarParkResultsLocationControl.
	/// </summary>
	public partial class FindCarParkResultsLocationControl : TDUserControl
	{

		private FindCarParkPageState carParkPageState;
		private FindPageState pageState;
		private bool boolPrintablePage = false;

		private const string RES_LOCATIONTITLE = "FindCarParkResultsLocationControl.labelTitle";

		public FindCarParkResultsLocationControl()
		{			
			carParkPageState = TDSessionManager.Current.FindCarParkPageState;
			pageState = TDSessionManager.Current.FindPageState;
		}

        public override string ToString()
        {
            return string.Format("{0} {1}", GetResource(RES_LOCATIONTITLE), carParkPageState.CurrentLocation.Description);
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			labelLocationName.Text = carParkPageState.CurrentLocation.Description;
			commandNewLocation.Text = Global.tdResourceManager.GetString("FindCarParkResultsLocationControl.commandNewLocation.Text");
			
			SetText();

			// button invisible if printable page, or in Car Park mode, or if come from Find a car route
			if ( boolPrintablePage || (pageState.Mode == FindAMode.CarPark) 
					|| (pageState.Mode == FindAMode.Car) )
				commandNewLocation.Visible = false;

            // CCN 0427 Removed help label and put it at top right corner
			//helpIconSelect.Visible = !boolPrintablePage;

			//set alt tag for help button
            // CCN 0427 Removed help label and put it at top right corner
			//helpIconSelect.AlternateText = GetResource( "FindCarParkResults.AlternateText");
			
			// determine which help to show - 'travel from' /to or 'next'
			if(TDSessionManager.Current.FindCarParkPageState.LocationFrom.Status != TDLocationStatus.Valid
				&& TDSessionManager.Current.FindCarParkPageState.LocationTo.Status != TDLocationStatus.Valid)
			{
				findCarParkResultsHelpLabel.Text = GetResource("FindCarParkResultsHelpLabelSelectFromTo");
			}
			else
			{
				findCarParkResultsHelpLabel.Text = GetResource("FindCarParkResultsHelpLabelSelectFromTo");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void SetText()
		{
			labelLocationNameTitle.Text = GetResource(RES_LOCATIONTITLE);
		}

		protected void CommandNewLocationClick(object sender, EventArgs e)
		{
			carParkPageState.CurrentLocation = new TDLocation();
			carParkPageState.CurrentSearch = new LocationSearch();
			carParkPageState.LocationControlType.Type = TDJourneyParameters.ControlType.Default;
			carParkPageState.CurrentSearch.SearchType = SearchType.Locality;

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkResultsNewLocation;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Printable
		{
			set
			{
				boolPrintablePage = value;
			}
		}

        /// <summary>
        /// gives read only access to help label control findCarParkResultsHelpLabel
        /// </summary>
        public HelpLabelControl HelpLabel
        {
            get { return findCarParkResultsHelpLabel; }
        }

        public void SetNewLocationButtonVisibility(bool visible)
        {
            commandNewLocation.Visible = visible;
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.commandNewLocation.Click += new EventHandler(this.CommandNewLocationClick);
        }
		#endregion
	}
}
