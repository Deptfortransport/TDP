// *********************************************** 
// NAME                 : Details.aspx.cs
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 19/07/2005
// DESCRIPTION			: MCMS template for Contact Details page
// ************************************************ 
//$log:$
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


using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;

using TransportDirect.Web.Support;
using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for Details.
	/// </summary>
	public partial class Details : TDPage
	{
        private const string detailsChannel = "/Channels/TransportDirect/ContactUs/Details";

		public Details() : base()
		{
			pageId = PageId.ContactUsPage;
		}

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            panelBackTop.Visible = true;

            ControlPropertyCollection properties = ContentProvider.Instance["Details"].GetControlProperties();

            string bodytext = properties.GetPropertyValue("BodyText", detailsChannel);
            BodyText.Controls.Add(new LiteralControl(bodytext));

            string titletext = properties.GetPropertyValue("TitleText", detailsChannel);
            labelFindPageTitle.Text = titletext;

            // Set browser title
            this.PageTitle = GetResource("ContactUsPage.PageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            //Added for white labelling:
            ConfigureLeftMenu("Details.clientLink.BookmarkTitle", "Details.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            expandableMenuControl.AddExpandedCategory("Provide Feedback");

            
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextContactUsDetails);
            expandableMenuControl.AddExpandedCategory("Related links");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
