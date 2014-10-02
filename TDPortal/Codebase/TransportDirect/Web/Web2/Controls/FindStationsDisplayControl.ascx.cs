// *************************************************************************** 
// NAME                 : FindStationsDisplayControl.ascx
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 08/07/2004 
// DESCRIPTION			: This control displays the names selected as the  
// origin or destination selected using the 'find a nearest station' functionality
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindStationsDisplayControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:20:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:30   mturner
//Initial revision.
//
//   Rev 1.13   Feb 23 2006 19:16:42   build
//Automatically merged from branch for stream3129
//
//   Rev 1.12.1.1   Jan 30 2006 14:41:06   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.12.1.0   Jan 10 2006 15:24:56   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.12   Nov 15 2005 11:18:28   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.11   Nov 03 2005 16:10:22   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.10.2.1   Oct 25 2005 12:19:24   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.10.2.0   Oct 04 2005 16:29:40   mtillett
//Update the display of the location controls
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.11   Oct 04 2005 15:57:42   mtillett
//Update location control to meet requirements of TD093
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.10   Nov 01 2004 18:03:52   passuied
//changes for FindPlace new functionality
//
//   Rev 1.9   Oct 05 2004 17:00:22   passuied
//hide help button when in FindTrunkInput page
//
//   Rev 1.8   Oct 05 2004 14:19:14   passuied
//removed new location button when in FindTrunkInput page
//
//   Rev 1.7   Aug 13 2004 14:06:32   jmorrissey
//Updated the layout to show the resolved location as well as the list of stations.
//
//   Rev 1.6   Aug 12 2004 12:00:40   jmorrissey
//Updates to help text
//
//   Rev 1.5   Aug 10 2004 13:11:56   jmorrissey
//Removed unnecessary additional help labels and updated setting of help text
//
//   Rev 1.4   Jul 22 2004 18:05:56   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.3   Jul 21 2004 10:51:16   passuied
//Re work for integration with FindStation del6.1. Working. Needs work on resources
//
//   Rev 1.2   Jul 15 2004 11:58:04   jmorrissey
//Coding complete and FxCop errors fixed. Needs integration testing with web pages and help label test to be added to langStrings.
//
//   Rev 1.1   Jul 13 2004 09:59:02   jmorrissey
//Interim version for use in development of new Find a pages
//
//   Rev 1.0   Jul 08 2004 17:20:32   jmorrissey
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	#region .Net namespaces

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	#endregion

	#region Transport Direct namespaces

	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.SessionManager;

	# endregion
	/// <summary>
	///		Summary description for FindStationsDisplayControl.
	/// </summary>
	public partial  class FindStationsDisplayControl : TDUserControl
	{
		#region Controls


		#endregion
		
		#region private variables

		//private field for holding display mode of this control
		private GenericDisplayMode displayMode;		
		//private field for holding TDLocation session information for this control
		private TDLocation theLocation;
		//private field for holding location type of this control e.g. To/From
		private CurrentLocationType locationType;

		#endregion

		/// <summary>
		/// Page Load method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Visible)
			{
				//sets the directionLabel text
				SetDirectionLabelText();	

				//sets the directionLabel text
				SetResolvedLocation();
	
				// sets text button label
				newLocationButton.Text = GetResource("AmbiguousLocationSelectControl2.commandNewLocation.Text");

				// set the data source for the data list
				string[] naptanNames = FindStationHelper.BuildStationAirportNames(theLocation);
				SetNaptansToDisplay(naptanNames);

				
			}
		}

		
		#region private methods

		/// <summary>
		/// method takes an array of Naptans to display on the stationsDataList control
		/// </summary>
		/// <param name="naptanArray"></param>
		private void SetNaptansToDisplay(string[] naptansToDisplay)
		{
			stationsDataList.DataSource = naptansToDisplay;
			stationsDataList.DataBind();
		}		

		/// <summary>
		/// sets the resolved location to display 
		/// </summary>
		private void SetResolvedLocation()
		{

			//evaluate the locationType
			switch (locationType)
			{

				case CurrentLocationType.None:

					resolvedLocationLabel.Text = String.Empty;
					break;

				case CurrentLocationType.From:

					resolvedLocationLabel.Text = theLocation.Description ;
					break;

				case CurrentLocationType.To:

					resolvedLocationLabel.Text = theLocation.Description;
					break;							

				default:

					resolvedLocationLabel.Text = String.Empty;
					break;

			}

		}

		private const string RES_FROM = "FindLocationControl.directionLabelFrom";
		private const string RES_TRAVELFROM = "FindLocationControl.directionLabelTravelFrom";
		private const string RES_TO = "FindLocationControl.directionLabelTo";
		private const string RES_TRAVELTO = "FindLocationControl.directionLabelTravelTo";
		/// <summary>
		/// sets the direction label text according to the LocationType property of this control
		/// </summary>
		private void SetDirectionLabelText()
		{
			string resFrom = string.Empty;
			string resTo = string.Empty;

			if (PageId == Common.PageId.FindStationInput)
			{
				resFrom = RES_TRAVELFROM;
				resTo = RES_TRAVELTO;
			}
			else
			{
				resFrom = RES_FROM;
				resTo = RES_TO;
			}

			//evaluate the location type
			switch (locationType)
			{

				case CurrentLocationType.None:

					directionLabel.Text = String.Empty;
					break;

				case CurrentLocationType.From:

					directionLabel.Text = Global.tdResourceManager.GetString(resFrom, TDCultureInfo.CurrentUICulture);
					break;

				case CurrentLocationType.To:

					directionLabel.Text = Global.tdResourceManager.GetString(resTo, TDCultureInfo.CurrentUICulture);
					break;							

				default:

					directionLabel.Text = String.Empty;
					break;
			}	

		}
	
		#endregion

		#region Public Properties
		/// <summary>
		/// property for display mode of the control;
		/// options are Normal, ReadOnly or Ambiguity
		/// </summary>
		public GenericDisplayMode DisplayMode
		{
			get
			{
				return displayMode;
			}
			set
			{
				displayMode = value;
			}
		}

		/// <summary>
		/// property holds the location type
		/// this determines the label used for the direction label e.g. To or From
		/// </summary>
		public CurrentLocationType LocationType
		{
			get
			{
				return locationType;
			}
			set
			{
				locationType = value;
			}
		}

		/// <summary>
		/// property holds the TDLocation object used to populate the tristateLocationControl	
		/// </summary>
		public TDLocation Location
		{
			get
			{
				return theLocation;
			}
			set
			{
				theLocation = value;
			}

		}

		/// <summary>
		/// Read only property returning the StationsDataList
		/// </summary>
		public DataList StationsDataList 
		{
			get 
			{
				return stationsDataList;
			}
		}

		/// <summary>
		/// If true, the "New location" button will be displayed to the right of the
		/// airports list
		/// </summary>
		public bool newLocationButtonVisible
		{
			get { return newLocationButton.Visible; }
			set { newLocationButton.Visible = value; }
		}

		#endregion
		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraEventWireUp();
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			newLocationButton.Click += new EventHandler(this.newLocationButton_Click);
		}
		#endregion

		private void newLocationButton_Click(object sender, EventArgs e)
		{
			// Raise event if necessary
			EventHandler theDelegate = (EventHandler)Events[NewLocationClickEventKey];
			if (theDelegate != null)
				theDelegate(this, EventArgs.Empty);
		}

		#region Public events

		// Keys
		private static readonly object NewLocationClickEventKey = new object();

		/// <summary>
		/// Event that will be raised when the "New Location" button is clicked.
		/// </summary>
		public event EventHandler NewLocationClick
		{
			add { this.Events.AddHandler(NewLocationClickEventKey, value); }
			remove { this.Events.RemoveHandler(NewLocationClickEventKey, value); }
		}

		#endregion
	}
}
