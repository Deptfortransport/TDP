// *********************************************** 
// NAME                 : OperatorSelectionControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 20/04/2004
// DESCRIPTION			: Allows the user to include or exclude specific operators
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/OperatorSelectionControl.ascx.cs-arc  $
//
//   Rev 1.3   Apr 11 2008 09:45:08   apatel
//Page_Load event modified to correct selectedIndex problem on    
//operator options radio button list
//Resolution for 4853: Find a flight - advance options - oprator selection error
//
//   Rev 1.2   Mar 31 2008 13:22:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:48   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:17:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.1   Jan 30 2006 14:41:18   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4.1.0   Jan 10 2006 15:26:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Oct 01 2004 11:03:48   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.3   Aug 23 2004 16:06:30   jgeorge
//IR1256
//
//   Rev 1.2   Jun 28 2004 14:41:12   jgeorge
//Updated for label text
//
//   Rev 1.1   Jun 09 2004 17:00:24   jgeorge
//Find a flight

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Collections;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.AirDataProvider;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	/// Enumeration representing ways of displaying the control
	/// </summary>
	public enum OperatorSelectionDisplayMode
	{
		Normal,
		ReadOnly,
		Ambiguity
	}

	/// <summary>
	///		Summary description for OperatorSelectionControl.
	/// </summary>
	public partial  class OperatorSelectionControl : TDUserControl
	{

		private IDataServices populator;
		private IAirDataProvider adp;
		private ArrayList eventsRaised = new ArrayList();
		private bool blockEvents;
		private OperatorSelectionDisplayMode displayMode;

		#region Properties

		/// <summary>
		/// Gets or sets the value selected from the radio list - whether to include or exclude the
		/// specified operators.
		/// </summary>
		public bool OnlyUseSpecifiedOperators
		{
			get { return (rblistOperatorOptions.SelectedItem.Value == "DontUse") ? false : true; }
			set 
			{
				try
				{
					blockEvents = true;
					string valueToFind = value ? "Use" : "DontUse";
					rblistOperatorOptions.SelectedIndex = rblistOperatorOptions.Items.IndexOf(rblistOperatorOptions.Items.FindByValue(valueToFind));
				}
				finally
				{
					blockEvents = false;
				}
			}
		}

		/// <summary>
		/// Gets or sets the selected operator list.
		/// In the set, if an operator checkbox is disabled, it will not be set to checked even
		/// if that operator code is passed in.
		/// </summary>
		public string[] SelectedOperators
		{
			get
			{
				int maxOperators = dlistOperators.Items.Count;

				ArrayList returnData = new ArrayList(maxOperators);
				ScriptableCheckBox c;
				foreach (DataListItem i in dlistOperators.Items)
				{
					c = (ScriptableCheckBox)i.FindControl("checkOperator");
					if (c.Checked && c.Enabled)
						returnData.Add(c.Value);
				}

				if ((returnData.Count == maxOperators) && (!OnlyUseSpecifiedOperators))
				{
					// The user has asked to exclude all operators. We ignore this - the only
					// reason for this is that they are trying to catch the system out, or they
					// have made a mistake.
					return new string[0];
				}
				else
					return (string[])returnData.ToArray(typeof(string));
			}

			set
			{
				try
				{
					blockEvents = true;
					ScriptableCheckBox c;
					ArrayList items = new ArrayList(value);
					foreach (DataListItem item in dlistOperators.Items)
					{
						c = (ScriptableCheckBox)item.FindControl("checkOperator");
						c.Checked = false;
						if (c.Enabled && (value != null))
							c.Checked = items.Contains(c.Value);
						else
							c.Checked = false;
					}
				}
				finally
				{
					blockEvents = false;
				}
			}
		}

		/// <summary>
		/// How to display the selection
		/// </summary>
		public OperatorSelectionDisplayMode DisplayMode
		{
			get { return displayMode; }
			set { displayMode = value; }
		}

		/// <summary>
		/// Sets the error message that will be displayed above the dropdown. This is only
		/// visible if the DisplayAsAmbiguity property is set to true.
		/// </summary>
		public string AmbiguityMessage
		{
			get { return labelAmbiguityMessage.Text; }
			set { labelAmbiguityMessage.Text = value; }
		}

		/// <summary>
		/// Returns the ID of the control in which checkboxes are displayed. Used for
		/// client scripting
		/// </summary>
		public string CheckBoxListClientId
		{
			get { return dlistOperators.ClientID; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Disables checkboxes for operators not in the list.
		/// </summary>
		/// <param name="validOperators"></param>
		public void RestrictOperators(IList validOperators)
		{
			ScriptableCheckBox c;
			bool itemPresent;
			AirOperator tempOperator = null;
			foreach (DataListItem item in dlistOperators.Items)
			{
				c = (ScriptableCheckBox)item.FindControl("checkOperator");
				itemPresent = false;
				tempOperator = adp.GetOperator(c.Value);
				if (tempOperator == null)
					itemPresent = false;
				else
					itemPresent = validOperators.Contains(tempOperator);

				if (itemPresent)
					c.Enabled = true;
				else
				{
					c.Enabled = false;
					c.Checked = false;
				}
			}
		}

		/// <summary>
		/// Removes any restrictions on operator selection
		/// </summary>
		public void RemoveOperatorRestrictions()
		{
			foreach (DataListItem i in dlistOperators.Items)
				((ScriptableCheckBox)i.FindControl("checkOperator")).Enabled = true;
		}

		/// <summary>
		/// Uses the AirDataProvider to return the operator name. Data bound to the label
		/// control used in the read only data list
		/// </summary>
		/// <param name="operatorCode"></param>
		public string GetOperatorName(string operatorCode)
		{
			return adp.GetOperator(operatorCode).Name;
		}

		#endregion

		#region Event keys

		// These objects are used as keys for the EventHandlerList belonging to the usercontrol.
		private static readonly object OperatorOptionChangedKey = new object();
		private static readonly object OperatorSelectionChangedKey = new object();

		#endregion

		#region Events

		/// <summary>
		/// Event raised when the include/exclude option is changed.
		/// </summary>
		public event EventHandler OperatorOptionChanged
		{
			add { this.Events.AddHandler(OperatorOptionChangedKey, value); }
			remove { this.Events.RemoveHandler(OperatorOptionChangedKey, value); }
		}

		/// <summary>
		/// Event raised when the operator selection is modified
		/// </summary>
		public event EventHandler OperatorSelectionChanged
		{
			add { this.Events.AddHandler(OperatorSelectionChangedKey, value); }
			remove { this.Events.RemoveHandler(OperatorSelectionChangedKey, value); }
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Performs data binding and other setup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			adp = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			TDResourceManager rm = Global.tdResourceManager;

			labelSRrblistOperatorOptions.Text = rm.GetString(this.ID + ".labelSRrblistOperatorOptions", TDCultureInfo.CurrentCulture);
            // storing rblistOperatorOptions selected index in a variable
            int operatorlistselectedindex = rblistOperatorOptions.SelectedIndex;
            populator.LoadListControl(DataServiceType.OperatorSelectionRadio, rblistOperatorOptions);
			// setting rblistOperatorOptions selected index 
            rblistOperatorOptions.SelectedIndex = operatorlistselectedindex;

            displayMode = OperatorSelectionDisplayMode.Normal;

            dlistOperators.DataSource = adp.GetOperators();
            dlistOperators.DataBind();
        }

		/// <summary>
		/// Handles switching the control to "Read Only" mode if necessary
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if (displayMode == OperatorSelectionDisplayMode.Ambiguity)
			{
				highlightPanel.CssClass = "alertwarning";
				explanationPanel.Visible = false;
				ambiguityMessage.Visible = true;
				labelSRrblistOperatorOptions.Visible = true;
                dlistOperators.Visible = true;
                dlistOperatorsFixed.Visible = false;
                rblistOperatorOptions.Visible = true;
                labelOperatorOptionsFixed.Visible = false;
            } 
			else if (displayMode == OperatorSelectionDisplayMode.ReadOnly)
			{
				highlightPanel.CssClass = "";
				explanationPanel.Visible = false;
				ambiguityMessage.Visible = false;
				rblistOperatorOptions.Visible = false;
				dlistOperators.Visible = false;
				labelSRrblistOperatorOptions.Visible = false;

				string[] dataSource = this.SelectedOperators;
				if (dataSource.Length == 0)
				{
					labelOperatorOptionsFixed.Visible = false;
					dlistOperatorsFixed.Visible = false;
				}
				else
				{
					labelOperatorOptionsFixed.Visible = true;
					dlistOperatorsFixed.Visible = true;
					labelOperatorOptionsFixed.Text = rblistOperatorOptions.SelectedItem.Text;
					dlistOperatorsFixed.DataSource = this.SelectedOperators;
					dlistOperatorsFixed.DataBind();
				}
			}
			else
			{
				labelSRrblistOperatorOptions.Visible = true;
				highlightPanel.CssClass = "";
				explanationPanel.Visible = true;
				ambiguityMessage.Visible = false;
				rblistOperatorOptions.Visible = true;
				dlistOperators.Visible = true;
				labelOperatorOptionsFixed.Visible = false;
				dlistOperatorsFixed.Visible = false;
			}
		}

		/// <summary>
		/// Handler for the SelectedIndexChanged event of the radio button list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnOperatorOptionChanged(object sender, EventArgs e)
		{
			// See OnOperatorSelectionChanged for explanation of the 
			// eventsRaised collection
			if (!eventsRaised.Contains(OperatorOptionChangedKey) && !blockEvents)
			{
				EventHandler theDelegate = (EventHandler)this.Events[OperatorOptionChangedKey];
				if ( theDelegate != null )
					theDelegate(this, EventArgs.Empty);
				eventsRaised.Add(OperatorOptionChangedKey);
			}
		}

		/// <summary>
		/// Handler for the operator checkboxes CheckedChanged event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnOperatorSelectionChanged(object sender, EventArgs e)
		{
			// The CheckBox.CheckedChanged event that this handles could fire 
			// multiple times, but we only want to raise a single selection
			// changed event back to the clients. So when the event is first
			// raised, a value is added to the eventsRaised list. This is checked
			// each time the event handler runs to decide whether or not to 
			// raise the event back to the client.
			if (!eventsRaised.Contains(OperatorSelectionChangedKey) && !blockEvents)
			{
				EventHandler theDelegate = (EventHandler)this.Events[OperatorSelectionChangedKey];
				if ( theDelegate != null )
					theDelegate(this, EventArgs.Empty);
				eventsRaised.Add(OperatorSelectionChangedKey);
			}
		}

		/// <summary>
		/// Eventhandler for the itemcreated event of the databound list. Adds the event handler
		/// to the check box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlistOperators_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			ScriptableCheckBox c = (ScriptableCheckBox)e.Item.FindControl("checkOperator");
			c.CheckedChanged += new EventHandler(OnOperatorSelectionChanged);
		}

		#endregion

		#region Viewstate

		/// <summary>
		/// Loads the display mode from the viewstate
		/// </summary>
		/// <param name="savedState"></param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] myState = (object[])savedState;
				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				
				displayMode = myState[1] == null ? OperatorSelectionDisplayMode.Normal : (OperatorSelectionDisplayMode)myState[1];

			}
		}

		/// <summary>
		/// Saves the display mode in the viewstate
		/// </summary>
		/// <returns></returns>
		protected override object SaveViewState()
		{
			object[] stateData = new object[3];
			stateData[0] = base.SaveViewState();
			stateData[1] = displayMode;
			return stateData;
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
            this.dlistOperators.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.dlistOperators_ItemCreated);
            this.rblistOperatorOptions.SelectedIndexChanged += new EventHandler(this.OnOperatorOptionChanged);
        }
		#endregion

	}
}
