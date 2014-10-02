// *********************************************** 
// NAME                 : FeedbackJourneyInputControl.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 05/01/2007
// DESCRIPTION          : Control to allow user to enter journey details
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackJourneyInputControl.ascx.cs-arc  $
//
//   Rev 1.5   Jan 20 2013 16:26:36   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.4   Jan 16 2009 13:27:22   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:41:38   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.1   Oct 07 2008 10:13:30   mmodi
//Corrected to retain user seletced values
//Resolution for 5123: Cycle Planner - Feedback details displayed on 'Feedback viewer page 'for a particular 'Feedback Id' are inappropriate
//Resolution for 5124: Cycle Planner - Mail sent to helpdesk (CJP Level 2 user) using ' Report a problem ' as feedback type contains incorrect 'Feedback Details'
//
//   Rev 1.2.1.0   Aug 22 2008 10:29:08   mmodi
//Updated for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:20:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:46   mturner
//Initial revision.
//
//   Rev 1.3   Jan 22 2007 13:59:50   rbroddle
//Fix to avoid server error with null JourneyParameters object - correction to 1.2 below - added check to Page_preRender.
//
//   Rev 1.2   Jan 18 2007 16:13:42   rbroddle
//Fix to avoid server error with null JourneyParameters object.
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.1   Jan 17 2007 18:07:06   mmodi
//Placed try-catch between date control due to error on first time FeedbackPage is loaded
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 12 2007 14:18:10   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.JourneyPlanning.CJPInterface;

	using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;

	/// <summary>
	///		Summary description for FeedbackJourneyInputControl.
	/// </summary>
	public partial class FeedbackJourneyInputControl : TDUserControl
	{
		

		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;

		private ControlPopulator populator;
		private LeaveReturnDatesControlAdapter inputDateAdapter;
		private TDJourneyParameters journeyParameters;

		private string locationTypeOutward;
		private string locationTypeReturn;
		private string transportModes;
		
		#region Page_Load, Page_PreRender

		/// <summary>
		/// Page load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			inputDateAdapter = new LeaveReturnDatesControlAdapter();
			journeyParameters = TDSessionManager.Current.JourneyParameters; 
			if (journeyParameters == null)
			{
				journeyParameters = new TDJourneyParametersMulti();
				journeyParameters.Initialise();
			}

			#region Labels

			searchTypeLabel.Text = GetResource("FeedbackJourneyInputControl.searchTypeLabel.Text");
			fromLabel.Text = GetResource("FeedbackJourneyInputControl.fromLabel.Text");
			toLabel.Text = GetResource("FeedbackJourneyInputControl.toLabel.Text");
			labelModesOfTransport.Text = GetResource("FeedbackJourneyInputControl.modesOfTransport.Text");

			checkBoxPublicTransport.Text = GetResource("PlanAJourneyControl.checkboxPublicTransport.Text");
			checkBoxCarRoute.Text = GetResource("PlanAJourneyControl.checkboxCarRoute.Text");
            checkBoxCycle.Text = GetResource("PlanAJourneyControl.checkboxCycle.Text");

			#endregion

            #region Load lists

            int listSearchTypeIndex = listSearchType.SelectedIndex;
            int checklistModesPublicTransportIndex = checklistModesPublicTransport.SelectedIndex;
            int listLocationTypeFromIndex = listLocationTypeFrom.SelectedIndex;
            int listLocationTypeToIndex = listLocationTypeTo.SelectedIndex;

            populator.LoadListControl(DataServiceType.UserFeedbackSearchType, listSearchType);
			populator.LoadListControl(DataServiceType.PublicTransportsCheck, checklistModesPublicTransport);
			populator.LoadListControl(DataServiceType.LocationTypeDrop, listLocationTypeFrom);
			populator.LoadListControl(DataServiceType.LocationTypeDrop, listLocationTypeTo);

            // Ensure we retain user selected values
            if (Page.IsPostBack)
            {
                listSearchType.SelectedIndex = listSearchTypeIndex;
                checklistModesPublicTransport.SelectedIndex = checklistModesPublicTransportIndex;
                listLocationTypeFrom.SelectedIndex = listLocationTypeFromIndex;
                listLocationTypeTo.SelectedIndex = listLocationTypeToIndex;
            }

            #endregion

            #region DateControl
            if (!Page.IsPostBack)
            {
                // Placed a try catch around here because on some occasions, an error is thrown (object ref null)
                // if user enters FeedbackPage straight after starting browser and navigating to Portal
                // Not sure why this occurs!
                try
                {
                    inputDateAdapter.UpdateDateControl(dateControl, false, journeyParameters, TDSessionManager.Current.ValidationError);
                }
                catch
                {
                    // do nothing
                }
            }
			#endregion

			// Set the locations
			locationTypeOutward = listLocationTypeFrom.SelectedValue.ToString();
			locationTypeReturn = listLocationTypeTo.SelectedValue.ToString();
		}

		/// <summary>
		/// Page PreRender
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			journeyParameters = TDSessionManager.Current.JourneyParameters;	
			if (journeyParameters == null)
			{
				journeyParameters = new TDJourneyParametersMulti();
				journeyParameters.Initialise();
			}

            if (!Page.IsPostBack)
            {
                inputDateAdapter.UpdateDateControl(dateControl, false, journeyParameters, TDSessionManager.Current.ValidationError);
            }
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read property to return all journey details entered
		/// </summary>
		/// <returns>String of journey details</returns>
		public string GetJourneyDetails()
		{
			StringBuilder journeyDetails = new StringBuilder();

			string newline = "\r\n";
			string seperator = ": ";
			string comma = ", ";
			string space = " ";

			journeyDetails.Append( GetResource("FeedbackJourneyInputControl.EnteredByUser.Text") );
			journeyDetails.Append( newline );

			#region Search type
			journeyDetails.Append( searchTypeLabel.Text );
			journeyDetails.Append( seperator );
			journeyDetails.Append( listSearchType.SelectedValue );
			journeyDetails.Append( newline );
			#endregion

			#region Locations
			// From
			journeyDetails.Append( fromLabel.Text );
			journeyDetails.Append( seperator );
			journeyDetails.Append( HttpUtility.HtmlEncode(fromTextBox.Text) );
			journeyDetails.Append( space );
			journeyDetails.Append( "(" + locationTypeOutward + ")" );
			//journeyDetails.Append( "(" + listLocationTypeFrom.SelectedValue + ")" );
			journeyDetails.Append( newline );

			// To
			journeyDetails.Append( toLabel.Text );
			journeyDetails.Append( seperator );
			journeyDetails.Append( HttpUtility.HtmlEncode(toTextbox.Text) );
			journeyDetails.Append( space );
			journeyDetails.Append( "(" + locationTypeReturn + ")" );
			//journeyDetails.Append( "(" + listLocationTypeTo.SelectedValue + ")" );
			journeyDetails.Append( newline );
			#endregion

			#region Outward/Return date time
			// Outward date
			StringBuilder outwardDateTime = new StringBuilder();
			outwardDateTime.Append( dateControl.LeaveDateControl.DateControl.Day.ToString() );
			outwardDateTime.Append( "/");
			outwardDateTime.Append(	dateControl.LeaveDateControl.DateControl.MonthYear.ToString() );
			outwardDateTime.Append( space );
			if (dateControl.LeaveDateControl.DateControl.ArriveBefore)
				outwardDateTime.Append( "Arriving by " );
			else
				outwardDateTime.Append( "Leaving at " );
			outwardDateTime.Append(	dateControl.LeaveDateControl.DateControl.Hour.ToString() );
			outwardDateTime.Append(	":" );
			outwardDateTime.Append( dateControl.LeaveDateControl.DateControl.Minute.ToString() );

			// Return date
			StringBuilder returnDateTime = new StringBuilder();
			// Don't add the return text if its NoReturn or OpenReturn selected
			if ((dateControl.ReturnDateControl.DateControl.MonthYear.ToString().Trim() != "NoReturn")
				&&
				(dateControl.ReturnDateControl.DateControl.MonthYear.ToString().Trim() != "OpenReturn"))
			{
				returnDateTime.Append( dateControl.ReturnDateControl.DateControl.Day.ToString() );
				returnDateTime.Append( "/");
				returnDateTime.Append( dateControl.ReturnDateControl.DateControl.MonthYear.ToString() );
				returnDateTime.Append( space );
				if (dateControl.ReturnDateControl.DateControl.ArriveBefore)
					returnDateTime.Append( "Arriving by " );
				else
					returnDateTime.Append( "Leaving at " );
				returnDateTime.Append(	dateControl.ReturnDateControl.DateControl.Hour.ToString() );
				returnDateTime.Append(	":" );
				returnDateTime.Append( dateControl.ReturnDateControl.DateControl.Minute.ToString() );
			}
			else
			{
				returnDateTime.Append( dateControl.ReturnDateControl.DateControl.MonthYear.ToString() );
			}
            
			journeyDetails.Append( "Leave on" );
			journeyDetails.Append( seperator );
			journeyDetails.Append( outwardDateTime.ToString() );
			journeyDetails.Append( newline );
			journeyDetails.Append( "Return on" );
			journeyDetails.Append( seperator );
			journeyDetails.Append( returnDateTime.ToString() );
			journeyDetails.Append( newline );
			#endregion

			#region Transport mode
			journeyDetails.Append( labelModesOfTransport.Text );
			
			journeyDetails.Append( seperator );			
			if (checkBoxPublicTransport.Checked)
			{
				journeyDetails.Append( checkBoxPublicTransport.Text );
				
				TransportModesSelected();

				if (transportModes.Length > 0)
				{
					journeyDetails.Append( transportModes );		
					journeyDetails.Append( comma );
				}
			}

			if (checkBoxCarRoute.Checked)
			{
				journeyDetails.Append( checkBoxCarRoute.Text );
			}

            if (checkBoxCycle.Checked)
            {
                journeyDetails.Append( checkBoxCycle.Text );
            }

			journeyDetails.Append( newline );
			#endregion

			return journeyDetails.ToString();
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Read/write 
		/// Array of public transport modes.
		/// </summary>
		private ModeType[] PublicModes
		{
			get
			{
				ArrayList modes = new ArrayList();
					
				// Loop through each check box and build up list of 
				// associated mode types
				for (int i = 0; i< checklistModesPublicTransport.Items.Count; i++)
				{
					foreach ( ModeType mode in selectedModes(i))
					{
						modes.Add(mode);
					}
				}

				// Return list of all mode types for selected check boxes
				return (ModeType[])modes.ToArray(typeof(ModeType));
			}

			set
			{
				checklistModesPublicTransport.SelectedIndex = -1;

				// Check all relevant check boxes associated with mode type array
				foreach (ModeType type in value)
				{
					string resourceId = populator.GetResourceId(
						DataServiceType.PublicTransportsCheck, 
						Enum.GetName(typeof(ModeType),type));
					switch (type)
					{
						case ModeType.Air:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Bus:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Ferry:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Rail:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
                        case ModeType.Telecabine:
                            populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);
                            break;
						case ModeType.Tram:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Underground:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
					}
				}
			}
		}


		/// <summary>
		///  Returns array of selected public transport modes.
		/// </summary>
		/// <param name="index">The index of the selected mode</param>
		/// <returns></returns>
		private ModeType[] selectedModes( int index)
		{
		
			ListItem item = checklistModesPublicTransport.Items[index];
		
			if (item.Selected)
			{

				// get ModeType from DataService
				ModeType type = (ModeType)
					Enum.Parse(typeof(ModeType), populator.GetValue
					(DataServiceType.PublicTransportsCheck, item.Value));

				// Determine the mode(s) associated with the selected check box
				switch (type)
				{
					case ModeType.Rail:		
						return new ModeType[]{ModeType.Rail};
					case ModeType.Bus:
						return new ModeType[]{ModeType.Bus, ModeType.Coach};
					case ModeType.Underground:
						return new ModeType[]{ModeType.Metro, ModeType.Underground};
                    case ModeType.Telecabine:
                        return new ModeType[] { ModeType.Telecabine };
					case ModeType.Tram:
						return new ModeType[]{ModeType.Tram};
					case ModeType.Ferry:
						return new ModeType[]{ModeType.Ferry};
					case ModeType.Air:
						return new ModeType[]{ModeType.Air};
					default:
						return new ModeType[0];

				}
			}
			else
				// If nothing selected, return empty array
				return new ModeType[0];
		}

		/// <summary>
		/// Sets the transport modes to private variable
		/// </summary>
		private void TransportModesSelected()
		{
			transportModes = string.Empty;

			foreach (ListItem li in checklistModesPublicTransport.Items)
			{
				if (li.Selected)
				{
					transportModes = transportModes + ", " + li.Text;
				}
			}
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Event handler called when date selected from calendar control. The journey parameters for the outward
		/// date are updated with the calendar date selection.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e)
		{
			journeyParameters.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
			journeyParameters.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
			TDSessionManager.Current.JourneyParameters = journeyParameters as TDJourneyParameters;
		}

		/// <summary>
		/// Event handler called when date selected from calendar control. The journey parameters for the return
		/// date are updated with the calendar date selection.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e) 
		{
			journeyParameters.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
			journeyParameters.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
			TDSessionManager.Current.JourneyParameters = journeyParameters as TDJourneyParameters;
		}

		/// <summary>
		/// Event handler called when location type Outward selected. 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void listLocationTypeFrom_SelectedIndexChanged(object sender, EventArgs e) 
		{
			locationTypeOutward = listLocationTypeFrom.SelectedValue.ToString();
		}

		/// <summary>
		/// Event handler called when location type Return selected. 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void listLocationTypeTo_SelectedIndexChanged(object sender, EventArgs e) 
		{
			locationTypeReturn = listLocationTypeTo.SelectedValue.ToString();
		}


		/// <summary>
		/// Event handler called when transport mode check boxes selected. 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void checklistModesPublicTransport_SelectedIndexChanged(object sender, EventArgs e) 
		{
			TransportModesSelected();
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
			dateControl.LeaveDateControl.DateChanged +=
				new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged +=
				new EventHandler(dateControlReturnDateControl_DateChanged);
            listLocationTypeFrom.SelectedIndexChanged +=
                new EventHandler(this.listLocationTypeFrom_SelectedIndexChanged);
            listLocationTypeTo.SelectedIndexChanged +=
                new EventHandler(this.listLocationTypeTo_SelectedIndexChanged);
            checklistModesPublicTransport.SelectedIndexChanged +=
                new EventHandler(this.checklistModesPublicTransport_SelectedIndexChanged);
		}
		#endregion
	}
}
