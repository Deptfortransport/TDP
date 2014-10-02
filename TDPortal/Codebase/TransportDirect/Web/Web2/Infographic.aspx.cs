// *********************************************** 
// NAME                 : Infographic.aspx.cs 
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 14/03/2006 
// DESCRIPTION			: Page to show a custom notice in HTML format. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Infographic.aspx.cs-arc  $
//

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
	/// Infographic page
	/// </summary>
	public partial class Infographic : TDPage
	{
	
		#region constructors
		/// <summary>
		/// Constructor - sets the page id
		/// </summary>
		public Infographic() : base()
		{
			pageId = PageId.Infographic;
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
            bool accessible = false;
            if (!bool.TryParse(Request.QueryString["accessible"], out accessible))
            {
                LoadResources(false);
            }
            else
            {
                LoadResources(accessible);
            }

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
		private void LoadResources(bool accessible)
		{
			PageTitle = GetResource("Infographic.AppendPageTitle")
                + GetResource("JourneyPlanner.DefaultPageTitle");
	
            // main content retrieval
            if (accessible)
            {
                InfographicHtmlPlaceHolder.InnerHtml = GetResource("Infographic.AccessiblePageContent");
            }
            else
            {
                InfographicHtmlPlaceHolder.InnerHtml = GetResource("Infographic.PageContent");
            }
		}
		#endregion

	}
}
