// ***************************************************************** 
// NAME                 : VisitPlannerLocationControl.ascx.cs 
// AUTHOR               : Tolu Olomolaiye 
// DATE CREATED         : 15/09/2005
// DESCRIPTION			: A single location panel for VisitPlanner
// ***************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/VisitPlannerLocationControl.ascx.cs-arc  $
//
//   Rev 1.3   Jun 26 2008 14:04:22   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:23:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:44   mturner
//Initial revision.
//
//   Rev 1.13   Feb 23 2006 16:14:34   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.12   Jan 04 2006 10:06:24   tolomolaiye
//Updates folllowing Visit Planner code review
//
//   Rev 1.11   Nov 24 2005 17:01:54   tolomolaiye
//Fix for IR 3174
//Resolution for 3174: Visit Planner - Amend button doesn't allow Length of Stay to be edited
//
//   Rev 1.10   Nov 17 2005 09:09:22   asinclair
//Added SetNewLocation method
//Resolution for 3013: Visit Planner: Clicking New Location does not clear correctly
//
//   Rev 1.9   Nov 09 2005 12:07:32   asinclair
//Fixed bugs with selecting a new location and displaying return after an ambiguity resolution
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.8   Nov 07 2005 09:58:36   tolomolaiye
//Modifications for Visit Planner
//
//   Rev 1.7   Oct 29 2005 12:50:26   asinclair
//Added 'Return' label
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Oct 26 2005 14:46:44   tolomolaiye
//Added check for drop down list
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 25 2005 12:59:08   tolomolaiye
//Added more comments and PreRender method
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 25 2005 11:41:20   jbroome
//Added populate method
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 20 2005 09:31:44   asinclair
//Work in progress 
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 11 2005 12:32:32   asinclair
//Added Properties
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 20 2005 12:07:02   asinclair
//Check in for pre-merge
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 16 2005 17:20:46   tolomolaiye
//Initial revision.
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.LocationService;	
	using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

	/// <summary>
	///		Summary description for VisitPlannerLocationControl.
	/// </summary>
	public partial class VisitPlannerLocationControl : TDUserControl
	{
		#region Variables and Properties

		protected TriStateLocationControl2 triStateLocationControl;

		private IDataServices populator;

		//private field for holding location type of this control
		private CurrentLocationType locationType;
		
		private LocationSelectControlType theLocationSelectControlType;
		private LocationSearch theSearch;
		private TDLocation theLocation;
		private DataServiceType type;
		private bool disableMapSelection;
		private bool acceptPostcodes;
		private bool acceptPartPostcodes;
		private bool checkInput;
		private int valueLengthOfStay;
		private InputPageState pageState;

		/// <summary>
		/// Read/Write.  Determines if the control is being used for Origin, VisitPlace1, or VisitPlace2
		/// </summary>
		public CurrentLocationType LocationType
		{
			get {return locationType;}
			set {locationType = value;}
		}

		/// <summary>
		/// Public property that sets the value of the length of stay drop down list
		/// </summary>
		public int StayLengthValue
		{
			set {valueLengthOfStay = value;}
		}

		/// <summary>
		/// Read only.  Exposes the triStateLocationControl TriStateLocationControl2
		/// </summary>
		public TriStateLocationControl2 LocationControl
		{
			get {return triStateLocationControl;}
		}
		
		/// <summary>
		/// Read only.  Exposes the returnToStartingPoint checkbox
		/// </summary>
		public CheckBox ReturnToOrigin
		{
			get {return returnToStartingPoint;}
		}

		/// <summary>
		/// Read only.  Exposes the listLengthOfStay Dropdownlist
		/// </summary>
		public DropDownList LenghtOfStay
		{
			get {return listLengthOfStay;}
		}

		#endregion

		#region Page Load and Constructor events
		/// <summary>
		/// Constructor - set the local resource manager
		/// </summary>

		public VisitPlannerLocationControl()
		{
			//use the Visit Planner resource file
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			populator.LoadListControl(DataServiceType.VisitPlannerLengthOfStay, listLengthOfStay, this.resourceManager);

			labelLengthOfStay.Text = GetResource("VisitPlannerLocationControl.labelLengthOfStay.Text");
			labelHour.Text = GetResource("VisitPlannerLocationControl.labelHour.Text");
			labelRetunToStartPoint.Text = GetResource("VisitPlannerLocationControl.labelRetunToStartPoint.Text");
			labelReturnJourney.Text = GetResource("VisitPlannerLocationControl.labelReturnJourney.Text");

			triStateLocationControl.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
			triStateLocationControl.Visible = true;

			ITDSessionManager sessionManager = TDSessionManager.Current;
			pageState = sessionManager.InputPageState;
		}

		/// <summary>
		/// On Render event to show/hide objects on the location controls
		/// </summary>
		protected void OnPreRender(object sender, EventArgs e)
		{
			switch (locationType)
			{
				case CurrentLocationType.VisitPlannerOrigin:
					UpdateVisitOrigin();
					break;

				case CurrentLocationType.VisitPlannerVisitPlace1:
				case CurrentLocationType.VisitPlannerVisitPlace2:
					UpdateVisitPlannerPlace();
					break;
			}

            //setting the tristatelocation control text for screen reader
            triStateLocationControl.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("langstrings", "FindStationInput.labelSRLocation");
		}

		/// <summary>
		/// Updates the location control if the type is Visit Planner Origin
		/// </summary>
		private void UpdateVisitOrigin()
		{
			labelFrom.Text = GetResource("VisitPlannerLocationControl.labelFrom.Text");

			//set the visibility of length of stay, hour to false. These labels are only used 
			//on the first and second visit locations
			labelLengthOfStay.Visible = false;
			listLengthOfStay.Visible = false;
			labelHour.Visible = false;

			//set the visibility of the drop down based on teh status of the tristate control
			switch (triStateLocationControl.GetLocation.Status)
			{
				case TDLocationStatus.Unspecified:
					returnToStartingPoint.Visible = true;
					labelRetunToStartPoint.Visible = true;
					labelReturnJourney.Visible = true;
					break;

				case TDLocationStatus.Ambiguous:
					returnToStartingPoint.Visible = false;
					labelRetunToStartPoint.Visible = false;
					labelReturnJourney.Visible = false;
					break;

				case TDLocationStatus.Valid:

					labelReturnJourney.Visible = true;
					labelRetunToStartPoint.Visible = true;

					//if we are in amend mode ensure the return to origin check box is visible
					if (pageState.VisitAmendMode)
					{
						//we are in amend mode - set the visibility of the return to starting point 
						//drop down as if we are in location unspecified mode
						returnToStartingPoint.Visible = true;
					}
					else
					{
						//set the read only label
						if (returnToStartingPoint.Checked)
						{
							labelRetunToStartPoint.Text = GetResource("VisitPlannerLocationControl.labelRetunToStartPoint.Return");
						}
						else
						{
							labelRetunToStartPoint.Text = GetResource("VisitPlannerLocationControl.labelRetunToStartPoint.Noreturn");
						}

						returnToStartingPoint.Visible = false;
					}

					break;
				}
			}

		/// <summary>
		/// Updates the location control if the type is VisitPlannerPlace1 or VisitPlannerPlace2
		/// </summary>
		private void UpdateVisitPlannerPlace()
		{
			//the return to starting point label should only be shown on Visit Planner Origin
			returnToStartingPoint.Visible = false;
			labelRetunToStartPoint.Visible = false;
			labelReturnJourney.Visible = false;
			
			if(locationType == CurrentLocationType.VisitPlannerVisitPlace1)
			{
				labelFrom.Text = GetResource("VisitPlannerLocationControl.VisitPlannerVisitPlace1.Text");
			}
			else
			{
				labelFrom.Text = GetResource("VisitPlannerLocationControl.VisitPlannerVisitPlace2.Text");
			}

			//show/hide the length of stay drop down based on the Status of the tristatelocation control
			switch (triStateLocationControl.GetLocation.Status)
			{
				case TDLocationStatus.Unspecified:
					DisplayUnspecifiedControls();
					break;

				case TDLocationStatus.Ambiguous:
					labelHour.Visible = false;
					labelLengthOfStay.Visible = false;
					listLengthOfStay.Visible = false;
					break;

				case TDLocationStatus.Valid:

					if (pageState.VisitAmendMode)
					{
						DisplayUnspecifiedControls();
					}
					else
					{
						labelLengthOfStay.Visible = true;
						listLengthOfStay.Visible = false;
						labelHour.Visible = true;
						labelHour.Text = string.Format(GetResource("VisitPlannerLocationControl.LengthOfStayDescription"), valueLengthOfStay);
					}
					break;
			}
		}

		/// <summary>
		/// Show/hide controls on the page when the location type is unspecified or 
		/// when the location type is valid AND pageState.VisitAmendMode is true
		/// </summary>
		private void DisplayUnspecifiedControls()
		{
			labelHour.Visible = true;
			labelLengthOfStay.Visible = true;
			listLengthOfStay.Visible = true;

			ListItem itemStayLength =  listLengthOfStay.Items.FindByValue(valueLengthOfStay.ToString("##00"));

			if (itemStayLength == null)
			{
				listLengthOfStay.SelectedIndex = 0;
			}
			else
			{
				listLengthOfStay.SelectedIndex = listLengthOfStay.Items.IndexOf(itemStayLength);
			}
		}

		#endregion

		#region Control methods
		/// <summary>
		/// Populates the tri state control
		/// </summary>
		/// <param name="type"></param>
		/// <param name="locationType"></param>
		/// <param name="theSearch"></param>
		/// <param name="theLocation"></param>
		/// <param name="theLocationSelectControlType"></param>
		/// <param name="disableMap"></param>
		/// <param name="postCodes"></param>
		/// <param name="partPostCodes"></param>
		/// <param name="checkInput"></param>
		public void Populate(DataServiceType type, CurrentLocationType locationType, ref LocationSearch theSearch, ref TDLocation theLocation, ref LocationSelectControlType theLocationSelectControlType, bool disableMap, bool acceptPostcodes, bool acceptPartPostcodes, bool checkInput)
		{
			this.theSearch = theSearch;
			this.theLocation = theLocation;
			this.theLocationSelectControlType = theLocationSelectControlType;
			this.type = type;
			this.locationType = locationType;
			this.disableMapSelection = disableMap;
			this.acceptPartPostcodes = acceptPartPostcodes;
			this.acceptPostcodes = acceptPostcodes;
			this.checkInput = checkInput;
            
			triStateLocationControl.Populate(type, locationType, ref theSearch, ref theLocation, ref theLocationSelectControlType,  disableMap, acceptPostcodes, acceptPartPostcodes, checkInput);
		}

		/// <summary>
		/// Sets the control back into the default state that allows a location to be entered by user.
		/// </summary>
		public void SetLocationUnspecified() 
		{
			theLocation.Status = TDLocationStatus.Unspecified;
			theLocationSelectControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
			theSearch.ClearSearch();
			triStateLocationControl.Populate(type, locationType, ref theSearch, ref theLocation, ref theLocationSelectControlType,  disableMapSelection, acceptPostcodes, acceptPartPostcodes, checkInput);		
		}

		/// <summary>
		/// Sets the control back into the default state that allows a location to be entered by user.
		/// </summary>
		public void SetNewLocation() 
		{
			theLocation.Status = TDLocationStatus.Unspecified;
			theLocationSelectControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
			theSearch.ClearAll();
			triStateLocationControl.Populate(type, locationType, ref theSearch, ref theLocation, ref theLocationSelectControlType,  disableMapSelection, acceptPostcodes, acceptPartPostcodes, checkInput);		
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
			this.PreRender += new System.EventHandler(this.OnPreRender);

		}
		#endregion

	}
}
