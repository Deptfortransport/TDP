// *********************************************** 
// NAME                 : SocialBookMarkControl.ascx.cs 
// AUTHOR               : Phil Scott
// DATE CREATED         : 2/09/2009
// ************************************************ 
// $Log::  

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SocialBookMarkingService;
using TD.ThemeInfrastructure;
using TransportDirect.UserPortal.Web.Controls;
using System.Collections.Generic;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Displays a link to the user that allows them to add a bookmark to the current journey result.
	/// </summary>
	/// <remarks>
	/// The main purpose of the control is to encapsulate the logic that is used to control the display 
	/// of the link and the actual creation of the bookmark.  However, the control will have basic 
	/// functionality that allows it to be used to make a simple bookmark of the current page without 
	/// requiring additional coding.
	/// The control will only work when placed on a page derived from TDPage.
	/// </remarks>
    
   
    public partial class SocialBookMarkLinkControl : TDUserControl, ILanguageHandlerIndependent
    {

        #region Private Fields
        private int sbmID;
        private string sbmDescription;
        private string sbmDisplayIconPath;
        private string sbmDisplayText;
        private string sbmLandingPartnerCode;
        private string sbmURLTemplate;

        private string sbmFormattedhyperlinkUrl;
        private string bookmarkDescription;

        private SocialBookMark[] socialBookMarks;
        private SocialBookMarkCatalogue socialBookmarkCatalogue;
        private List<string[]> socialBookMarksFormatted = new List<string[]>();
        #endregion

        #region Web Form Designer generated code

        /// <summary>
        /// socialBookMarkLink control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl socialBookMarkLink;
        /// <summary>
        /// linkContainer control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl linkContainer;

        /// <summary>
        /// linkStar control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected TransportDirect.UserPortal.Web.Controls.TDImage sbmLink;
        protected System.Web.UI.WebControls.HyperLink hyperlinkSocialBookmark;
		protected System.Web.UI.WebControls.Label labelSocialBookmark;
        protected TransportDirect.UserPortal.Web.Controls.TDImage imageSocialBookMark;
        protected TransportDirect.UserPortal.Web.Controls.TDImage imageOpensNewPage;
        

      /// <summary>
        /// cycleDetailsRepeater control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Repeater socialBookMarkRepeater;

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


        #region Page_Init, Page_Load, Page_PreRender
        /// <summary>
        /// Page init event
        /// </summary>
        protected void Page_Init(object sender, EventArgs e)
        {
            socialBookMarkRepeater.ItemDataBound += new RepeaterItemEventHandler(socialBookMarkRepeater_ItemDataBound);
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSocialBookmark();

            ResultsAdapter adapter = new ResultsAdapter();
            adapter.PopulateJourneyResultsClientLink(clientLink);

            imageEmail.ImageUrl = GetResource("SocialBookmarkLinkControl.imageEmail.Url");
            imageEmail.AlternateText = GetResource("SocialBookmarkLinkControl.DoorToDoor.emailLink.AltText");
            imageEmail.ToolTip =  GetResource("SocialBookmarkLinkControl.DoorToDoor.emailLink.AltText");
            labelEmail.Text = GetResource("SocialBookmarkLinkControl.lableEmail.Text");
            

            imageEmailLink.ImageUrl = GetResource("SocialBookmarkLinkControl.imageEmail.Url");
            imageEmailLink.ToolTip = GetResource("SocialBookmarkLinkControl.DoorToDoor.emailLink.AltText");
            imageEmailLink.AlternateText = GetResource("SocialBookmarkLinkControl.DoorToDoor.emailLink.AltText");
            labelEmailLink.Text = GetResource("SocialBookmarkLinkControl.lableEmail.Text");
            
            emailLink.ToolTip = GetResource("SocialBookmarkLinkControl.DoorToDoor.emailLink.AltText");
            emailLinkButton.ToolTip = GetResource("SocialBookmarkLinkControl.DoorToDoor.emailLink.AltText");
                      

            if (!((TDPage)this.Page).IsJavascriptEnabled)
            {
                emailLink.Visible = true;
                emailLinkButton.Visible = false;
            }

            linkTitle.Text = GetResource("SocialBookmarkLinkControl.linkTitle.Text");
            clientLink.LinkAltText = GetResource("SocialBookmarkLinkControl.DoorToDoor.clientLink.AltText");

            if (TDSessionManager.Current.FindAMode == FindAMode.CarPark)
            {
                linkTitle.Text = GetResource("SocialBookmarkLinkControl.linkTitle.Text");
                clientLink.LinkAltText = GetResource("SocialBookmarkLinkControl.FindCarPark.clientLink.AltText");

                emailLink.Visible = false;
                emailLinkButton.Visible = false;

                clientLink.BookmarkUrl = adapter.GenerateLandingPageUrl(Properties.Current["ClientLinks.PartnerId"]);
                clientLink.BookmarkTitle = string.Format(GetResource("ClientLinks.FindCarPark.BookmarkTitle"), TDSessionManager.Current.FindCarParkPageState.CurrentSearch.InputText);
                clientLink.Visible = true;
            }
        }

        /// <summary>
        /// Page prerender event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            clientLink.LinkText = GetResource("SocialBookmarkLinkControl.clientLink.LinkText");
           
            this.Visible = IsSocialLinkControlVisible();
        }

        /// <summary>
        /// Retruns true if SocialLinkControl shoul be visible
        /// </summary>
        /// <returns></returns>
        private bool IsSocialLinkControlVisible()
        {
            bool showClientLinks;

            ITDSessionManager sessionManager = TDSessionManager.Current;
            
            if (TDItineraryManager.Current.ExtendInProgress)
            {
                showClientLinks = false;
            }
            else
            {
                switch (sessionManager.FindAMode)
                {
                    case FindAMode.None:
                    case FindAMode.Bus:
                    case FindAMode.Train:
                    case FindAMode.Coach:
                    case FindAMode.Car:
                    case FindAMode.Flight:
                        // Must have request data, an origin location and a destination location,
                        // and origin and destination location must contain no coach naptans
                        if (
                            (sessionManager.JourneyRequest == null) ||
                            (sessionManager.JourneyRequest.OriginLocation == null) ||
                            (sessionManager.JourneyRequest.DestinationLocation == null)
                            )
                            showClientLinks = false;
                        else
                            showClientLinks = true;

                        break;
                    case FindAMode.CarPark:
                        showClientLinks = true;
                        break;
                    case FindAMode.Cycle:
                        if (
                           (sessionManager.CycleRequest == null) ||
                           (sessionManager.CycleRequest.OriginLocation == null) ||
                           (sessionManager.CycleRequest.DestinationLocation == null)
                           )
                            showClientLinks = false;
                        else
                            showClientLinks = true;
                        break;
                    default:
                        showClientLinks = false;
                        break;
                }
            }
            return showClientLinks;
        }

        #endregion

 
        
		#region Public properties
        /// <summary>
        /// Read Only property providing access to client link control
        /// </summary>
        public ClientLinkControl ClientLink
        {
            get
            {
                return this.clientLink;
            }
        }

        /// <summary>
        /// Read only property providing access to email link button
        /// </summary>
        public TDLinkButton EmailLinkButton
        {
            get
            {
                return this.emailLinkButton;
            }
        }

        /// <summary>
        /// Read only propery providing access to email link hyperlink
        /// </summary>
        public HyperLink EmailLink
        {
            get
            {
                return this.emailLink;
            }
        }


        /// <summary>
        /// Write only property for bookmark description
        /// </summary>
        public string BookmarkDescription
        {
            set
            {
                bookmarkDescription = value;
            }
        }

		#endregion

        #region Event handlers

        /// <summary>
        /// SocialBookMarkRepeater item data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void socialBookMarkRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string sbImageUrl   = (string)((object[])e.Item.DataItem)[0];
                string sbHyperlink  = (string)((object[])e.Item.DataItem)[1];
                string sbLabel      = (string)((object[])e.Item.DataItem)[2];
                string sbAltText    = (string)((object[])e.Item.DataItem)[3];
                

                // Find the image control
                TDImage imageSocialBookmark = (TDImage)e.Item.FindControl("imageSocialBookMark");
                if (imageSocialBookmark != null)
                {
                    imageSocialBookmark.ImageUrl = sbImageUrl;
                    imageSocialBookmark.AlternateText = sbAltText;
                }
                            
                HyperLink hyperlinkSocialBookMark = (HyperLink) e.Item.FindControl("hyperlinkSocialBookMark");
                if (hyperlinkSocialBookMark != null)
                {
                    hyperlinkSocialBookMark.NavigateUrl = sbHyperlink;
                    hyperlinkSocialBookMark.ToolTip     =sbAltText; 
                   
                }
                Label labelSocialBookmark = (Label) e.Item.FindControl("labelSocialBookmark");
                if (labelSocialBookmark != null)
                {
                    labelSocialBookmark.Text = string.Format("{0} {1}", sbLabel, GetResource("ExternalLinks.OpensNewWindowImage"));
                    
                }
            
            }

        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Loads Social BookMark links data from database
        /// </summary>
        private void LoadSocialBookmark()
        {
            int themeid = ThemeProvider.Instance.GetTheme().Id;
            // Getting the catalogue from service discovery
            if (socialBookmarkCatalogue == null)
            {
                socialBookmarkCatalogue = (SocialBookMarkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.SocialBookMarkingService];
            }
            // Get bookmarks out of the catalogue convert template to landing URL and load into arraylist for display

            socialBookMarks = socialBookmarkCatalogue.GetSocialBookMark(themeid);

            ResultsAdapter adapter = new ResultsAdapter();

            string bookmarkTitle = bookmarkDescription;

            // Process bookmarks
            //Loop through socialBookMarks and Convert to landing page Array
            for (int sbm = 0; sbm < socialBookMarks.Length; sbm++)
            {
                sbmID = socialBookMarks[sbm].SBMId;
                sbmDescription = socialBookMarks[sbm].SBMDescription;
                sbmDisplayIconPath = socialBookMarks[sbm].SBMDisplayIconPath;
                sbmDisplayText = socialBookMarks[sbm].SBMDisplayText;
                sbmLandingPartnerCode = socialBookMarks[sbm].SBMLandingPartnerCode;
                sbmURLTemplate = socialBookMarks[sbm].SBMURLTemplate;

                // convert sbmURLTemplate to LandingPage

                if(string.IsNullOrEmpty(bookmarkTitle))
                {
                    bookmarkTitle = adapter.GenerateLandingPageTitle();
                }
                string linkUrl = adapter.GenerateLandingPageUrl(sbmLandingPartnerCode);
                if (!string.IsNullOrEmpty(linkUrl))
                {
                    sbmFormattedhyperlinkUrl = sbmURLTemplate.Replace("[TDPLANDINGURL]", Server.UrlEncode(linkUrl));

                    sbmFormattedhyperlinkUrl = sbmFormattedhyperlinkUrl.Replace("[TDPLANDINGTITLE]", bookmarkTitle);
                    // Add this bookmark to the array
                    string[] sb = new string[4];

                    string tooltipStr = GetResource("SocialBookmarkLinkControl.DoorToDoor.hyperlinkSocialBookMark.AltText");

                    if (TDSessionManager.Current.FindAMode == FindAMode.CarPark)
                    {
                        tooltipStr = GetResource("SocialBookmarkLinkControl.FindCarPark.hyperlinkSocialBookMark.AltText");
                    }

                    sb[0] = sbmDisplayIconPath;
                    sb[1] = sbmFormattedhyperlinkUrl;
                    sb[2] = sbmDisplayText;
                    sb[3] = string.Format(tooltipStr, sbmDescription);


                    socialBookMarksFormatted.Add(sb);
                }

            }


            socialBookMarkRepeater.DataSource = socialBookMarksFormatted;
            socialBookMarkRepeater.DataBind();
        }

       
        #endregion

       
	}
}




