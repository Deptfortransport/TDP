// *********************************************** 
// NAME                 : PeopleTravellingControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 05/01/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PeopleTravellingControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:56   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 19:17:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.1   Jan 30 2006 14:41:18   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6.1.0   Jan 10 2006 15:26:42   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Mar 21 2005 16:59:34   jgeorge
//FxCop changes
//
//   Rev 1.5   Mar 17 2005 16:30:34   jgeorge
//Additional validation to make sure user can't select 0 people.
//
//   Rev 1.4   Mar 08 2005 16:20:24   jgeorge
//Added screenreader text
//
//   Rev 1.3   Mar 04 2005 09:28:14   jgeorge
//Changed to use GetResource
//
//   Rev 1.2   Feb 23 2005 17:12:00   jgeorge
//Added JavaScript, correct event handling and raising, appropriate viewstate.
//
//   Rev 1.0   Jan 18 2005 11:44:38   jgeorge
//Initial revision.

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Displays the journey and discount card information. Note - viewstate is disabled,
	/// so properties will have to be set every time
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PeopleTravellingControl : TDPrintableUserControl , ILanguageHandlerIndependent
	{
		#region Control declarations


		#endregion

		#region Private members and constants

		private GenericDisplayMode displayMode = GenericDisplayMode.Normal;

		/// <summary>
		/// Maximum number of passengers. Defaulted to 8, but the appropriate value is 
		/// loaded from the properties service (if it exists) in Page_Load.
		/// </summary>
		private int maxPassengers = 8; 
		
		#endregion

		#region Event

		/// <summary>
		/// Raised when the user changes the selected value in either Adult or Child dropdown.
		/// Clients can handle this event then examine the Adults and Children properties
		/// to get the new values
		/// </summary>
		public event EventHandler PeopleNumbersChanged;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets to local resource manager.
		/// </summary>
		public PeopleTravellingControl() : base()
		{
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for the Init event. Wires additional events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			dropAdult.SelectedIndexChanged += new EventHandler(dropDown_SelectedIndexChanged);
			dropChild.SelectedIndexChanged += new EventHandler(dropDown_SelectedIndexChanged);
		}

		/// <summary>
		/// Handler for the load event. Sets the text in static controls and loads the max
		/// passengers property
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			IPropertyProvider properties = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			
			string maxPassengersTemp = properties[ "PeopleTravellingControl.MaxPassengersAllowed" ];
			if ((maxPassengersTemp != null) && (maxPassengersTemp.Length != 0))
                maxPassengers = Convert.ToInt32( maxPassengersTemp, TDCultureInfo.InvariantCulture );

			// Set up label text
			labelInstructions.Text = String.Format( TDCultureInfo.InvariantCulture, GetResource( "PeopleTravellingControl.labelInstructions" ), maxPassengers.ToString(TDCultureInfo.InvariantCulture));
			labelPeopleTravelling.Text = GetResource( "PeopleTravellingControl.labelPeopleTravelling" );
			labelAdult.Text = GetResource( "PeopleTravellingControl.labelAdult" );
			labelAdultSR.Text = GetResource( "PeopleTravellingControl.labelAdultSR" );
			labelChild.Text = GetResource("PeopleTravellingControl.labelChild" );
			labelChildSR.Text = GetResource("PeopleTravellingControl.labelChildSR" );
			
			// Populate the dropdowns
			setupDropDown(dropAdult);
			setupDropDown(dropChild);

			DetermineMode();
		}

		/// <summary>
		/// Handler for the SelectedIndexChanged event of both dropdowns. Causes the
		/// PeopleNumbersChanged event to be raised.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dropDown_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			OnPeopleNumbersChanged(EventArgs.Empty);
		}

		/// <summary>
		/// Handler for the prerender event. Sets control visibility for error mode if necessary
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			DetermineMode();

			switch (displayMode)
			{
				case GenericDisplayMode.Normal:
					labelAdultRO.Visible = false;
					labelChildRO.Visible = false;
					dropAdult.Visible = true;
					dropChild.Visible = true;
					dataCell.Attributes["class"] = "";
					break;
					
				case GenericDisplayMode.ReadOnly:
					labelAdultRO.Visible = true;
					labelChildRO.Visible = true;
					labelAdultRO.Text = dropAdult.SelectedItem.Text;
					labelChildRO.Text = dropChild.SelectedItem.Text;
					dropAdult.Visible = false;
					dropChild.Visible = false;
					dataCell.Attributes["class"] = "";
					break;

				case GenericDisplayMode.Ambiguity:
					labelAdultRO.Visible = false;
					labelChildRO.Visible = false;
					dropAdult.Visible = true;
					dropChild.Visible = true;
					dataCell.Attributes["class"] = "peopletravellingerror";
					break;
			}

			SetupJavascript();

		}

		#endregion

		#region Private methods

		/// <summary>
		/// Raises the PeopleNumbersChanged event
		/// </summary>
		/// <param name="e"></param>
		private void OnPeopleNumbersChanged(EventArgs e)
		{
			EventHandler theDelegate = PeopleNumbersChanged;
			if (theDelegate != null)
				theDelegate(this, e);
		}

		/// <summary>
		/// Loads the values into the dropdown
		/// </summary>
		/// <param name="dropDown"></param>
		/// <param name="maxNumber"></param>
		private void setupDropDown(DropDownList dropDown)
		{
			for (int i = dropDown.Items.Count ; i <= maxPassengers; i++)
				dropDown.Items.Add(new ListItem(i.ToString(TDCultureInfo.InvariantCulture), i.ToString(TDCultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Sets the displaymode property depending on the current values of 
		/// PrinterFriendly and IsValid. PrinterFriendly takes precedence over
		/// IsValid (if the control is being displayed in read only mode and
		/// contains invalid data, then the mode will be ReadOnly not Ambiguity
		/// </summary>
		private void DetermineMode()
		{
			if (PrinterFriendly)
				DisplayMode = GenericDisplayMode.ReadOnly;
			else if (!IsValid)
				DisplayMode = GenericDisplayMode.Ambiguity;
			else
				DisplayMode = GenericDisplayMode.Normal;
		}

		/// <summary>
		/// Sets the currently selected item in the dropdown to the item with the 
		/// given value
		/// </summary>
		/// <param name="list"></param>
		/// <param name="valueToSelect"></param>
		private static void SelectInDropdown(DropDownList list, int valueToSelect)
		{
			list.SelectedIndex = list.Items.IndexOf( list.Items.FindByValue(valueToSelect.ToString(TDCultureInfo.InvariantCulture)) );
		}

		/// <summary>
		/// Retrieves the value of the currently selected item from the dropdown
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		private static int GetFromDropdown(DropDownList list)
		{
			return Convert.ToInt32( list.SelectedItem.Value, TDCultureInfo.InvariantCulture );
		}

		/// <summary>
		/// Checks if JavaScript is enabled, and if it does then sets up the scripts 
		/// on the controls. The selected items in the dropdowns should be set before
		/// calling this methods, and not changed subsequently.
		/// </summary>
		private void SetupJavascript()
		{
			string scriptName = "PeopleTravellingControl";
			string scriptActions = "handlePeopleTravellingChange('{0}', '{1}', {2});";
			TDPage thePage = (TDPage)Page;
			if (thePage.IsJavascriptEnabled)
			{
				dropAdult.ScriptName = scriptName;
				dropAdult.Action = String.Format( TDCultureInfo.InvariantCulture, scriptActions, dropAdult.ClientID, dropChild.ClientID, maxPassengers );
				dropAdult.EnableClientScript = true;

				dropChild.ScriptName = scriptName;
				dropChild.Action = String.Format( TDCultureInfo.InvariantCulture, scriptActions, dropChild.ClientID, dropAdult.ClientID, maxPassengers );
				dropChild.EnableClientScript = true;

				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
				
				thePage.ClientScript.RegisterClientScriptBlock(typeof(PeopleTravellingControl), scriptName, scriptRepository.GetScript(scriptName, thePage.JavascriptDom));

				// Finally, we need to ensure that the dropdowns are in the correct situation
				// First remove appropriate elements from the adults dropdown
				while ((dropAdult.Items.Count - 1) > (maxPassengers - GetFromDropdown(dropChild)))
					dropAdult.Items.RemoveAt(dropAdult.Items.Count - 1);

				while ((dropChild.Items.Count - 1) > (maxPassengers - GetFromDropdown(dropAdult)))
					dropChild.Items.RemoveAt(dropChild.Items.Count - 1);

			}
			else
			{
				dropAdult.EnableClientScript = false;
				dropChild.EnableClientScript = false;
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The number of adults travelling
		/// </summary>
		public int Adults
		{
			get { return GetFromDropdown(dropAdult); }
			set { SelectInDropdown(dropAdult, value); }
		}

		/// <summary>
		/// The number of children travelling
		/// </summary>
		public int Children
		{
			get { return GetFromDropdown(dropChild); }
			set { SelectInDropdown(dropChild, value); }
		}

		/// <summary>
		/// The total number of people travelling should not exceed a configurable value retrieved from the properties
		/// service. IsValid returns false if this is not the case.
		/// </summary>
		public bool IsValid
		{
			get 
			{
				int totalPassengers = Adults + Children;
				return (totalPassengers <= maxPassengers ) && (totalPassengers > 0); 
			}
		}

		/// <summary>
		/// Readonly is used when displaying a printer friendly page
		/// Ambiguity is used when IsValid = false
		/// </summary>
		public GenericDisplayMode DisplayMode
		{
			get { return displayMode; }
			set { displayMode = value; }
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
