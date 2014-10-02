// *********************************************** 
// NAME                 : AirportDisplayControl.ascx.cs 
// AUTHOR               : Jonathan George 
// DATE CREATED         : 
// DESCRIPTION			: Displays airport selection to the user
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AirportDisplayControl.ascx.cs-arc  $
//
//   Rev 1.4   Mar 23 2010 16:45:04   pghumra
//Rearranged alignment of controls and added label for origin/destination controls
//Resolution for 5479: CODE FIX - INITIAL - DEL 10.x - Find nearest button not aligned in flight
//
//   Rev 1.3   May 02 2008 10:13:08   mmodi
//No change.
//Resolution for 4925: Control alignments: Find a flight
//
//   Rev 1.2   Mar 31 2008 13:18:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:48   mturner
//Initial revision.
//
//   Rev 1.6   Apr 27 2007 12:54:12   dsawe
//changed for error when planning air journey using single airport
//Resolution for 4398: when amending single airport air journey error occurs
//
//   Rev 1.5   Apr 10 2006 12:06:08   jbroome
//Fix for 3818
//Resolution for 3818: DN077 Landing Page: Find a Flight - Location text name is displayed when a single Naptan supplied
//
//   Rev 1.4   Feb 23 2006 19:16:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.1   Jan 30 2006 14:40:58   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3.1.0   Jan 10 2006 15:23:04   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Nov 03 2005 17:08:48   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.2.1.0   Oct 14 2005 15:45:56   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.2   Aug 17 2004 17:56:00   jmorrissey
//Interim check in as part of IR1327. Not Ready for release.
//
//   Rev 1.1   Jun 09 2004 16:29:00   jgeorge
//Find a flight

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Text;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.AirDataProvider;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///	Displays a list of selected airports/regions
	/// </summary>
	public partial  class AirportDisplayControl : TDUserControl
	{
		protected System.Web.UI.WebControls.Panel selectionDisplayPanel;
		protected System.Web.UI.WebControls.Label labelLocation;
		protected System.Web.UI.WebControls.Panel locationDisplayPanel;
		protected System.Web.UI.WebControls.Label resolvedLocationLabel;

		//private field for holding TDLocation session information for this control
		private TDLocation theLocation;



		#region Public events

		// Keys
		private static readonly object NewLocationClickEventKey = new object();

		/// <summary>
		/// Event that will be raised when the "New Location" button is clicked.
		/// </summary>
		public event EventHandler NewLocationClick
		{
			add { this.Events.AddHandler(NewLocationClickEventKey, value); }
			remove { this.Events.AddHandler(NewLocationClickEventKey, value); }
		}

		#endregion

		#region Public properties

        /// <summary>
        /// Label text for display control
        /// </summary>
        public string labelText
        {
            get
            {
                return labelFrom.Text;
            }
            set
            {
                labelFrom.Text = value;
            }
        }

		/// <summary>
		/// If true, the "New location" button will be displayed to the right of the
		/// airports list
		/// </summary>
		public bool ShowNewLocationButton
		{
			get { return buttonNewLocation.Visible; }
			set { buttonNewLocation.Visible = true; }
		}

		/// <summary>
		/// Returns the Client ID of the control which will contain a space-separated
		/// list of the IATA codes whose names are displayed in the airports list
		/// </summary>
		public string AirportIataCodesControlId
		{
			get { return airportCodes.ClientID; }
		}

		/// <summary>
		/// property holds the TDLocation object used to populate the resolvedLocationLabel	
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

		#endregion

		#region Public methods

		/// <summary>
		/// Sets the selected airports. If the region is not null, then its name
		/// is displayed in bold, and the airports are displayed in a list underneath
		/// </summary>
		/// <param name="region"></param>
		/// <param name="airports"></param>
		public void SetData(AirRegion region, Airport[] airports)
		{
			
			if (region != null)
			{
				regionDisplayPanel.Visible = true;
				labelRegion.Text = region.Name;
			}
			else if (region == null && theLocation != null)
			{
				labelRegion.Text = theLocation.Description;
			}
			else
			{
				regionDisplayPanel.Visible = false;
			}
			
			// Extra check for Landing Page functionality:
			// If only one naptan provided and location description 
			// is exactly the same as the airport name, then 
			// no need to show region text. This then mirrors 
			// someone selecting a single airport via the input screen
			if (airports != null &&  airports.Length == 1)
			{
				if(theLocation != null)
				{
					if (theLocation.Description == airports[0].Name)
					{
						labelRegion.Text = string.Empty;
					}
				}
			}
			
			dlistAirports.DataSource = AirportArrayToNameArray(airports);
			dlistAirports.DataBind();

			StringBuilder sb = new StringBuilder(airports.Length);
			foreach (Airport a in airports)
			{
				sb.Append(a.IATACode);
				sb.Append(" ");
			}
			airportCodes.Value = sb.ToString().Trim();
		}

		/// <summary>
		/// Sets the selected airports. No region name is displayed
		/// </summary>
		/// <param name="airports"></param>
		public void SetData(Airport[] airports, TDLocation resolvedLocation)
		{
			theLocation = resolvedLocation;
			SetData(null, airports);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Converts an array of airports to an array of airport names
		/// </summary>
		/// <param name="airports"></param>
		/// <returns></returns>
		private string[] AirportArrayToNameArray(Airport[] airports)
		{
			string[] results = new string[airports.Length];
			for (int i = 0 ; i < airports.Length ; i++)
				results[i] = airports[i].Name;
			return results;
		}

		/// <summary>
		/// sets the resolved location to display 
		/// </summary>
		private void SetResolvedLocation()
		{

			//get the resolved location description
			if (theLocation != null)
			{
				labelRegion.Text = theLocation.Description;
			}	
			else
			{
				labelRegion.Text = String.Empty;
			}
		}		

		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for Page Load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			buttonNewLocation.Text = this.resourceManager.GetString(this.ID + ".buttonNewLocation.Text", TDCultureInfo.CurrentUICulture);

		}

		/// <summary>
		/// Handle click events from the new location button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNewLocation_Click(object sender, EventArgs e)
		{
			// Raise event if necessary
			EventHandler theDelegate = (EventHandler)Events[NewLocationClickEventKey];
			if (theDelegate != null)
				theDelegate(this, EventArgs.Empty);
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
			this.buttonNewLocation.Click += new EventHandler(this.buttonNewLocation_Click);
		}
		#endregion

	}
}
