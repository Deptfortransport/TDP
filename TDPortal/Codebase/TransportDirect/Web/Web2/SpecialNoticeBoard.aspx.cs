// *********************************************** 
// NAME                 : SpecialNoticeBoard.aspx.cs 
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 14/03/2006 
// DESCRIPTION			: Page to show a custom notice in HTML format. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/SpecialNoticeBoard.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:26:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:26   mturner
//Initial revision.
//
//   Rev 1.4   Apr 28 2006 11:30:04   halkatib
//Resolution for IR3981. Added back button to page. Used the same functionality as present in the seasonal notice board. 
//Resolution for 3918: Special Noticeboard: Change: Add a back button to the page
//
//   Rev 1.3   Mar 17 2006 16:24:40   mdambrine
//code review changes
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.2   Mar 15 2006 16:25:58   mdambrine
//had to put the placeholder control in a table with no cellpadding.
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.1   Mar 15 2006 15:08:34   mdambrine
//update the placeholder
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.0   Mar 14 2006 11:49:32   mdambrine
//Initial revision.
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// SpecialNoticeBoard page
	/// </summary>
	public partial class SpecialNoticeBoard : TDPage
	{
	
		#region constructors
		/// <summary>
		/// Constructor - sets the page id
		/// </summary>
		public SpecialNoticeBoard() : base()
		{
			pageId = PageId.SpecialNoticeBoard;
		}
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl informationLinksControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
        //protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			ExtraWiringEvents();
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Page Events
		/// <summary>
		/// Page load event handler for this page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Setting text for back button 
			backButton.Text = GetResource("JourneyPlannerSummary.backButton.Text");

			// Put user code to initialize the page here
			LoadResources();

            // Left hand navigation menu set up
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenu);

            //added for white-labelling Related link part of side menu
            relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextSpecialNoticeBoard);
		}

        

		/// <summary>
		/// Wiring up of the back button click event 
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.backButton.Click += new EventHandler(this.backButton_Click);
		}

		/// <summary>
		/// Handler for the Back button click event.
		/// The page can only be accessed from Home so that's where to go back to.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backButton_Click(object sender, EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			sessionManager.Session[SessionKey.Transferred] = false;
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoHome;
		}		
		#endregion

		#region Private Methods
		/// <summary>
		/// Loads strings from the resourcemanager project
		/// </summary>
		private void LoadResources()
		{
			PageTitle = GetResource("JourneyPlanner.DefaultPageTitle") 
				+ GetResource("SpecialNoticeBoard.AppendPageTitle");							
		}
		#endregion

	}
}
