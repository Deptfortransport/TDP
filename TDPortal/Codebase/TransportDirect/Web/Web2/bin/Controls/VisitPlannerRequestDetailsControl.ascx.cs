// ************************************************************************************************ 
// NAME                 : VisitPlannerRequestDetailsControl.ascx.cs 
// AUTHOR               : Tolu Olomolaiye 
// DATE CREATED         : 08/09/2005 
// DESCRIPTION			: This control has two functions
//							(1) To display the visit details (either full or title only).
//							(2)	To enable changes to the journey using the amend button
// ************************************************************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/VisitPlannerRequestDetailsControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:40   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 07 2008 08:41:00 apatel
//  Added a property to show or hide amend button.
//
//   Rev 1.0   Nov 08 2007 13:18:46   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 16:14:38   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.6   Jan 25 2006 12:11:36   pcross
//Removed unnecessary tooltip
//Resolution for 3505: UEE: Inconsistency in use of tooltips
//
//   Rev 1.5   Nov 14 2005 12:12:46   halkatib
//Modification for IR3011 to change the image buttons to tdbuttons used in the UEE changes
//Resolution for 3011: UEE: Image Buttons still on VP Results page
//
//   Rev 1.4   Oct 28 2005 16:31:54   jbroome
//HIde unused rows when not needed to reduce vertical space
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 28 2005 15:00:04   tolomolaiye
//Changes from code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 18 2005 14:55:40   tolomolaiye
//Updated with fxcop comments
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 16 2005 16:12:40   tolomolaiye
//Check in for review
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 16 2005 12:31:04   tolomolaiye
//Initial revision.
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	using TransportDirect.Web.Support;

	/// <summary>
	///		Show the details of any journey planned
	/// </summary>
	public partial class VisitPlannerRequestDetailsControl : TDPrintableUserControl 
	{
		/// <summary>
		/// Button to amend the search
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton imageAmend;

		#region Variables for Properties
		private string stringFromLocation = String.Empty;
		private string stringFirstVisitLocation = String.Empty;
		private string stringSecondVisitLocation = String.Empty;
		private string stringVisitDateTime = String.Empty;
		private string stringFirstVisitLengthOfStay = String.Empty;
		private string stringSecondVisitLengthOfStay = String.Empty;
		private string stringReturnToOrigin = String.Empty;
        private bool isAmendButtonVisible = true;
		private bool showFullDisplay = true;		

		private string[] stringOptions;

		#endregion

		#region Properties and Events
		/// <summary>
		/// Set up the event handler for the ammend button
		/// </summary>
		public event CommandEventHandler AmendCommand;
		
		/// <summary>
		/// Read/write Property to store the "From" location
		/// </summary>
		public string FromLocation 
		{
			get {return stringFromLocation;}
			set {stringFromLocation = value;}
		}

		/// <summary>
		/// Read/write property of the description of the first location visited
		/// </summary>
		public string FirstVisitLocation 
		{
			get {return stringFirstVisitLocation;}
			set {stringFirstVisitLocation = value;}
		}

		/// <summary>
		/// Read/write property. Text description of the second location visited
		/// </summary>
		public string SecondVisitLocation 
		{
			get {return stringSecondVisitLocation;}
			set {stringSecondVisitLocation = value;}
		}

		/// <summary>
		/// Read/write property. Date and time of visit
		/// </summary>
		public string VisitDateTime 
		{
			get {return stringVisitDateTime;}
			set {stringVisitDateTime = value;}
		}
		
		/// <summary>
		/// Read/write property. A string array containing a list of journey options.
		/// Only displayed if the user has selected/deselected modes of transport 
		/// </summary>
		public string[] Options 
		{
			get {return stringOptions;}
			set {stringOptions = value;}
		}
		
		/// <summary>
		/// Read/write property. Length of time (in hours) the user wants to stay 
		/// at the first place visited
		/// </summary>
		public string FirstVisitLengthOfStay 
		{
			get {return stringFirstVisitLengthOfStay;}
			set {stringFirstVisitLengthOfStay = value;}
		}

		/// <summary>
		/// Read/write property. Length of time (in hours) the user 
		/// wants to stay at the second place visited
		/// </summary>
		public string SecondVisitLengthOfStay 
		{
			get {return stringSecondVisitLengthOfStay;}
			set {stringSecondVisitLengthOfStay = value;}
		}
		
		/// <summary>
		/// Read/write property. Boolean value to indicate if the user 
		/// wants to return to their destination.
		/// </summary>
		public string ReturnToOrigin 
		{
			get {return stringReturnToOrigin;}
			set {stringReturnToOrigin = value;}
		}

		/// <summary>
		/// Read/write property. Determines whether the journey 
		/// results are shown in full or partial view
		/// </summary>
		public bool FullDisplay 
		{
			get {return showFullDisplay;}
			set {showFullDisplay = value;}
		}

        /// <summary>
        /// Read/Write property make amend button visible/hide.
        /// </summary>
        public bool AmendButtonVisible
        {
            get { return isAmendButtonVisible; }
            set { isAmendButtonVisible = value; }
        }

		#endregion

		/// <summary>
		/// Constructor - set the resource file for the control
		/// </summary>
		public VisitPlannerRequestDetailsControl()
		{
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
		}

		/// <summary>
		/// Page load method. Get the values of all the string labels from the resource file
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			imageAmend.Command += AmendCommand;
		}

        protected void Page_PreRender(object sender, EventArgs e)
        {
            imageAmend.Visible = isAmendButtonVisible;
        }
		
		/// <summary>
		/// Show the labels and controls in the visitPlannerJourneyDetailsControl
		/// This method is called from VisitPlannerResults.aspx.cs
		/// </summary>
		public void ShowJourneyDetails()
		{
			labelRequestedVisit.Text = GetResource("VisitPlannerRequestDetailsControl.labelRequestedVisit.Text");
			// Amend button not visible if printing
			if (PrinterFriendly)
			{
				imageAmend.Visible = false;
			}
			else
			{
				imageAmend.Visible = true;
				// Get the text for the labels from the resource file
				imageAmend.Text = GetResource("VisitPlannerRequestDetailsControl.commandAmend.Text");
			}

			//only show the remaining labels if showFullDisplay is true or printing page
			if (showFullDisplay || PrinterFriendly)
			{
				row1.Visible = true;
				row2.Visible = true;
				row3.Visible = true;
				row4.Visible = true;
				row5.Visible = true;

				labelFrom.Text =  GetResource("VisitPlannerRequestDetailsControl.labelFrom.Text");
				labelFirstVisit.Text = GetResource("VisitPlannerRequestDetailsControl.labelFirstVisit.Text");
				labelLengthFirstVisit.Text = GetResource("VisitPlannerRequestDetailsControl.labelLengthFirstVisit.Text"); //"Length of stay";
				labelSecondVisit.Text = GetResource("VisitPlannerRequestDetailsControl.labelSecondVisit.Text"); //"Second Visit";
				labelVisitDateTime.Text = GetResource("VisitPlannerRequestDetailsControl.labelVisitDateTime.Text"); //@"Visit date/time";
				labelReturnToOrigin.Text = GetResource("VisitPlannerRequestDetailsControl.labelReturnToOrigin.Text"); //"Return to origin";
				labelReturnToOriginText.Text = GetResource("VisitPlannerRequestDetailsControl.labelReturnToOriginText.Text"); //"Yes";
				labelLengthSecondVisit.Text = GetResource("VisitPlannerRequestDetailsControl.labelLengthSecondVisit.Text"); //"Length of stay";

				labelFromText.Text = stringFromLocation;
				labelFirstVisitText.Text = stringFirstVisitLocation; 
				labelSecondVisitText.Text = stringSecondVisitLocation; 
				labelVisitDateTimeText.Text = stringVisitDateTime; 
				labelStayFirstTime.Text = string.Format(TDCultureInfo.CurrentUICulture, GetResource("VisitPlannerRequestDetailsControl.labelStayFirstTime.Text"),stringFirstVisitLengthOfStay); 
				labelStaySecondTime.Text = string.Format(TDCultureInfo.CurrentUICulture, GetResource("VisitPlannerRequestDetailsControl.labelStaySecondTime.Text"), stringSecondVisitLengthOfStay); 
				
				labelReturnToOriginText.Text = stringReturnToOrigin; 
			
				//only show options if there is data
				if (stringOptions.Length != 0)
				{
					labelOptions.Text = GetResource("VisitPlannerRequestDetailsControl.labelOptions.Text");
					repeaterOptions.DataSource = stringOptions;
					repeaterOptions.DataBind();
				}
			}
			else
			{
				// Hide unused rows to reduce vertical space
				row1.Visible = false;
				row2.Visible = false;
				row3.Visible = false;
				row4.Visible = false;
				row5.Visible = false;
			}
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
			this.repeaterOptions.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.repeaterOptions_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// Bind the data in the source to the repeater control
		/// </summary>
		private void repeaterOptions_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ( (e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem) ) 
			{
				Label labelRepeaterOption = e.Item.FindControl("labelOption") as Label;

				if (labelRepeaterOption != null)
				{
					labelRepeaterOption.Text = e.Item.DataItem.ToString();
				}
			}
		}

	}
}
