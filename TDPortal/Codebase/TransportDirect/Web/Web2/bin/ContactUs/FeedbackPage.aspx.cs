//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration

//Rev DevFatory Feb 15th 13:17:00 dgath
//4 lines commented out in Page_Load due to removal of submenu from this page

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
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for FeedbackPage.
	/// </summary>
	public partial class FeedbackPage : TDPage
	{
		#region Controls



		#endregion

		#region Private variables

		private ITDSessionManager tdSessionManager;
		private TDJourneyParameters journeyParameters;
		protected System.Web.UI.WebControls.Label abelTravelinfoTP;
		private TDItineraryManager itineraryManager;

		#endregion

		#region Constructor
		/// <summary>
		/// Sets Page ID
		/// </summary>
		public FeedbackPage() : base()
		{
			pageId = PageId.FeedbackPage;

		}
		#endregion
        
		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				tdSessionManager = TDSessionManager.Current;
				journeyParameters = TDSessionManager.Current.JourneyParameters;
				itineraryManager = TDItineraryManager.Current;
			}

			//labelDetails.Text =  resourceManager.GetString( "FeedbackInitialPage.labelDetails.Text", TDCultureInfo.CurrentUICulture ); 
			//labelFeedback.Text = resourceManager.GetString( "FeedbackPage.labelFeedback.Text", TDCultureInfo.CurrentUICulture ); 
			labelTitle.Text = resourceManager.GetString( "FeedbackInitialPage.labelTitle.Text", TDCultureInfo.CurrentUICulture ); 
			labelPageTitle.Text = resourceManager.GetString ( "FeedbackInitialPage.ContactUsLabel", TDCultureInfo.CurrentUICulture );	
			labelIntroduction.Text = resourceManager.GetString( "FeedbackInitialPage.labelIntroduction.Text", TDCultureInfo.CurrentUICulture );
			//labelTravelinfoTP.Text = resourceManager.GetString( "FeedbackPage.labelTravelinfoTP.Text", TDCultureInfo.CurrentUICulture );
			//labelFurthertravel.Text = resourceManager.GetString( "FeedbackPage.labelFurthertravel.Text", TDCultureInfo.CurrentUICulture );

            imageFeedbackPage.ImageUrl = GetResource("HomeTipsTools.imageFeedback.ImageUrl");
            imageFeedbackPage.AlternateText = " ";

            // Add a browser title
            this.PageTitle = labelPageTitle.Text + " | Transport Direct";

			SetupSkipLinksAndScreenReaderText();

            //Added for white labelling:
            ConfigureLeftMenu("FeedbackPage.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFeedbackPage);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{
			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("Feedbackpage.imageMainContentSkipLink.AlternateText");

			imageFeedbackPanelSkipLink1.ImageUrl = skipLinkImageUrl;
			imageFeedbackPanelSkipLink1.AlternateText = GetResource("Feedbackpage.imageFeedbackPanelSkipLink.AlternateText");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
