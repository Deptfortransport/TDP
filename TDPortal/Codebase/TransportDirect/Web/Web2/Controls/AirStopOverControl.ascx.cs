// *********************************************** 
// NAME                 : AirStopOverControl.aspx.cs 
// AUTHOR               : Jonathan George 
// DATE CREATED         : 
// DESCRIPTION			: Provides facility for user to specify stopover
// preferences for an air journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AirStopOverControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:50   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 19:16:16   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.1   Jan 30 2006 14:40:58   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6.1.0   Jan 10 2006 15:23:06   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Oct 01 2004 11:03:44   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.5   Aug 26 2004 16:46:34   COwczarek
//Changes to display journey preferences consistently in read only mode across all Find A pages
//Resolution for 1421: Find a ambiguity pages (QA)
//
//   Rev 1.4   Aug 02 2004 17:53:14   JHaydock
//Added this.dropStopoverOutward.SelectedIndexChanged event capture that was missing
//
//   Rev 1.3   Jun 29 2004 09:01:10   jgeorge
//Modifications for label text
//
//   Rev 1.2   Jun 09 2004 16:30:56   jgeorge
//Find a flight
//
//   Rev 1.1   Jun 02 2004 14:04:36   jgeorge
//Fnd a flight

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.AirDataProvider;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///		Summary description for AirStopOverControl.
	/// </summary>
	public partial  class AirStopOverControl : TDUserControl
	{

		IDataServices populator;
		IAirDataProvider airData;
		bool readOnly;
        private TDJourneyParametersFlight journeyParams;

		#region Properties

		/// <summary>
		/// If set to true, all fields will be read-only
		/// </summary>
		public bool ReadOnly
		{
			get { return readOnly; }
			set 
			{ 
				readOnly = value; 
				if (readOnly)
					FlyViaAmbiguityMessage = String.Empty;
			}
		}

		/// <summary>
		/// If this is set to a non-empty string, the via airport will be displayed as an 
		/// ambigutity (with a yellow background). Otherwise it will be displayed as normal
		/// </summary>
		public string FlyViaAmbiguityMessage
		{
			get { return labelFlyViaAmbiguity.Text; }
			set
			{
				labelFlyViaAmbiguity.Text = value;
				if (value.Length != 0)
				{
					ReadOnly = false;
					labelFlyViaAmbiguity.Visible = true;
				}
				else
				{
					labelFlyViaAmbiguity.Visible = false;
				}
			}
		}

		/// <summary>
		/// Allows the Stopover Airport to be retrieved or set
		/// </summary>
		public Airport StopOverAirport
		{
			get { return airData.GetAirport(dropFlyVia.SelectedItem.Value); }
			set 
			{ 
				if (value == null)
					populator.Select(dropFlyVia, ""); 
				else
					populator.Select(dropFlyVia, value.IATACode); 
			}
		}

		/// <summary>
		/// Allows the Stopover time for the outward journey to be retrieved or set
		/// </summary>
		public int StopoverTimeOutward
		{
			get { return Convert.ToInt32(dropStopoverOutward.SelectedItem.Value); }
			set 
			{ 
				populator.Select(dropStopoverOutward, value.ToString());
			}
		}

		/// <summary>
		/// Allows the Stopover time for the return journey to be retrieved or set
		/// </summary>
		public int StopoverTimeReturn
		{
			get { return Convert.ToInt32(dropStopoverReturn.SelectedItem.Value); }
			set 
			{ 
				populator.Select(dropStopoverReturn, value.ToString());
			}
		}


		#endregion

		#region Event keys

		// These three objects are used as keys for the EventHandlerList belonging to the usercontrol.
		private static readonly object StopOverAirportChangedKey = new object();
		private static readonly object StopoverTimeOutwardChangedKey = new object();
		private static readonly object StopoverTimeReturnChangedKey = new object();

		#endregion

		#region Events

		/// <summary>
		/// Event raised when the stopover airport is modified.
		/// </summary>
		public event EventHandler StopOverAirportChanged
		{
			add { this.Events.AddHandler(StopOverAirportChangedKey, value); }
			remove { this.Events.RemoveHandler(StopOverAirportChangedKey, value); }
		}

		/// <summary>
		/// Event raised when the outward stopover time is changed
		/// </summary>
		public event EventHandler StopoverTimeOutwardChanged
		{
			add { this.Events.AddHandler(StopoverTimeOutwardChangedKey, value); }
			remove { this.Events.RemoveHandler(StopoverTimeOutwardChangedKey, value); }
		}

		/// <summary>
		/// Event raised when the return stopover time is changed
		/// </summary>
		public event EventHandler StopoverTimeReturnChanged
		{
			add { this.Events.AddHandler(StopoverTimeReturnChangedKey, value); }
			remove { this.Events.RemoveHandler(StopoverTimeReturnChangedKey, value); }
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for the load event of the user control. Initialises the dropdowns if this is the
		/// first load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersFlight;

            if (!IsPostBack)
            {
                LoadData();
            } 
            labelFlyVia.Text = GetResource("panelVia.labelFlyVia") + " ";
            labelStopoverOutward.Text = GetResource("panelStopoverOutward.labelStopoverOutward") + " ";
            labelStopoverReturn.Text = GetResource("panelStopoverReturn.labelStopoverReturn") + " ";
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
            bool viaAmbiguous = labelFlyViaAmbiguity.Visible;
            bool readOnly = ReadOnly;

            displayDropVia((readOnly && !viaAmbiguous));
            displayStopOverOutward(readOnly || viaAmbiguous);
            displayStopOverReturn(readOnly || viaAmbiguous);

            if (viaAmbiguous) 
            {
                panelVia.CssClass = "alertwarning";
            } 
            else 
            {
                panelVia.CssClass = "";
            }

            this.Visible = !readOnly || (!AllOptionsDefault() && readOnly);            
		}

		/// <summary>
		/// Event handler for the IndexChanged event of the dropFlyVia control. Raises the 
		/// StopOverAirportChanged event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnStopOverAirportChanged(object sender, EventArgs e)
		{
			EventHandler theDelegate = (EventHandler)this.Events[StopOverAirportChangedKey];
			if ( theDelegate != null )
				theDelegate(this, e);
		}

		/// <summary>
		/// Event handler for the IndexChanged event of the dropStopoverOutward control. Raises the 
		/// StopoverTimeOutwardChanged event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnStopoverTimeOutwardChanged(object sender, EventArgs e)
		{
			EventHandler theDelegate = (EventHandler)this.Events[StopoverTimeOutwardChangedKey];
			if ( theDelegate != null )
				theDelegate(this, e);
		}

		/// <summary>
		/// Event handler for the IndexChanged event of the dropStopoverOutward control. Raises the 
		/// StopoverTimeReturnChanged event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnStopoverTimeReturnChanged(object sender, EventArgs e)
		{
			EventHandler theDelegate = (EventHandler)this.Events[StopoverTimeReturnChangedKey];
			if ( theDelegate != null )
				theDelegate(this, e);
		}

		#endregion

		#region Viewstate

		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] myState = (object[])savedState;
				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				
				readOnly = myState[1] == null ? false : (bool)myState[1];

			}
		}

		protected override object SaveViewState()
		{
			object[] stateData = new object[3];
			stateData[0] = base.SaveViewState();
			stateData[1] = readOnly;
			return stateData;
		}
		
		#endregion

		#region Private methods

		/// <summary>
		/// Initialises the dropdown list controls
		/// </summary>
		private void LoadData()
		{
			TDResourceManager rm = Global.tdResourceManager;

			// Add the airports - added one by one rather than data bound to ensure that this is only
			// necessary once.
			foreach (Airport a in airData.GetAirports())
				dropFlyVia.Items.Add(new ListItem(a.Name, a.IATACode));

			// Add the "I don't mind" item
			dropFlyVia.Items.Insert(0, new ListItem(rm.GetString("AirStopOverControl.FlyVia.Default", TDCultureInfo.CurrentCulture), String.Empty));
			dropFlyVia.SelectedIndex = 0;

			populator.LoadListControl(DataServiceType.StopOverTimeDrop, dropStopoverOutward);
			populator.LoadListControl(DataServiceType.StopOverTimeDrop, dropStopoverReturn);
		}

        /// <summary>
        /// Returns true if the choosen stop over airport is the drop down default value, false otherwise
        /// </summary>
        /// <returns>True if the choosen stop over airport is the default value, false otherwise</returns>
        private bool StopOverAirportIsDefault() 
        {
            return journeyParams.ViaSelectedAirport == null;
        }

        /// <summary>
        /// Returns true if the choosen outward stop over time is the drop down default value, false otherwise
        /// </summary>
        /// <returns>True if the choosen outward stop over time is the drop down default value, false otherwise</returns>
        private bool StopoverTimeOutwardIsDefault() 
        {
            string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.StopOverTimeDrop);
            return journeyParams.OutwardStopover == Convert.ToInt32(defaultItemValue);
        }

        /// <summary>
        /// Returns true if the choosen return stop over time is the drop down default value, false otherwise
        /// </summary>
        /// <returns>True if the choosen return stop over time is the drop down default value, false otherwise</returns>
        private bool StopoverTimeReturnIsDefault() 
        {
            string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.StopOverTimeDrop);
            return journeyParams.ReturnStopover == Convert.ToInt32(defaultItemValue);
        }

        /// <summary>
        /// Returns true if all fly via options (stop over airport, stop over outward time and stop over return time)
        /// are set to their drop down default values, false otherwise
        /// </summary>
        /// <returns>Returns true if all fly via options are set to their drop down default values, false otherwise</returns>
        public bool AllOptionsDefault() 
        {
            return StopOverAirportIsDefault() && StopoverTimeOutwardIsDefault() && StopoverTimeReturnIsDefault();
        }

        /// <summary>
        /// Display the stop over airport details in either input or read only mode. If for read only mode, only display
        /// if not set to the default value.
        /// </summary>
        /// <param name="readOnly">True if for read only, false otherwise</param>
        private void displayDropVia(bool readOnly) 
        {
            bool showStopoverAirport = !StopOverAirportIsDefault();

            dropFlyVia.Visible = !readOnly;
            dropFlyViaFixed.Visible = showStopoverAirport && readOnly;
            labelFlyVia.Visible = !readOnly;

            if (showStopoverAirport && readOnly) 
            {
                dropFlyViaFixed.Text = GetResource("panelVia.labelFlyVia") + ": " + 
                    dropFlyVia.SelectedItem.Text;
            }

            panelVia.Visible = true;

            labelSRdropFlyVia.Visible = showStopoverAirport || !readOnly;

        }

        /// <summary>
        /// Display the outward stop over time details in either input or read only mode. If for read only mode, only display
        /// if not set to the default value.
        /// </summary>
        /// <param name="readOnly">True if for read only, false otherwise</param>
        private void displayStopOverOutward(bool readOnly) 
        {
            bool showStopoverTimeOutward = !StopoverTimeOutwardIsDefault();

            labelStopoverOutward.Visible = !readOnly;
            labelStopoverOutwardTitle.Visible = showStopoverTimeOutward || !readOnly;

            if (showStopoverTimeOutward && readOnly) 
            {
                dropStopoverOutwardFixed.Text = GetResource("panelStopoverOutward.labelStopoverOutward") + ": " + 
                    dropStopoverOutward.SelectedItem.Text;
            }

            dropStopoverOutwardFixed.Visible = showStopoverTimeOutward && readOnly;
            dropStopoverOutward.Visible = !readOnly;

            panelStopoverOutward.Visible = showStopoverTimeOutward || !readOnly;

            labelSRdropStopoverOutward.Visible = showStopoverTimeOutward || !readOnly;

        }

        /// <summary>
        /// Display the return stop over time details in either input or read only mode. If for read only mode, only display
        /// if not set to the default value.
        /// </summary>
        /// <param name="readOnly">True if for read only, false otherwise</param>
        private void displayStopOverReturn(bool readOnly) 
        {
            bool showStopoverTimeReturn = !StopoverTimeReturnIsDefault();

            labelStopoverReturn.Visible = !readOnly;
            labelStopoverReturnTitle.Visible = showStopoverTimeReturn || !readOnly;

            if (showStopoverTimeReturn && readOnly) 
            {
                dropStopoverReturnFixed.Text = GetResource("panelStopoverReturn.labelStopoverReturn") + ": " +
                    dropStopoverReturn.SelectedItem.Text;
            }

            dropStopoverReturnFixed.Visible = showStopoverTimeReturn && readOnly;
            dropStopoverReturn.Visible = !readOnly;

            panelStopoverReturn.Visible = showStopoverTimeReturn || !readOnly;

            labelSRdropStopoverReturn.Visible = showStopoverTimeReturn || !readOnly;

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
            this.dropFlyVia.SelectedIndexChanged += new EventHandler(this.OnStopOverAirportChanged);
            this.dropStopoverOutward.SelectedIndexChanged += new EventHandler(this.OnStopoverTimeOutwardChanged);
            this.dropStopoverReturn.SelectedIndexChanged += new EventHandler(this.OnStopoverTimeReturnChanged);
 		}
		#endregion
	}
}
