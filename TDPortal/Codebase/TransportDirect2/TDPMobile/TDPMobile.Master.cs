// *********************************************** 
// NAME             : TDPMobile.Master.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Feb 2012
// DESCRIPTION  	: TDPMobile Master page
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web.UI;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.Web;
using TDP.UserPortal.SessionManager;
using System.Web.UI.WebControls;
using TDP.UserPortal.TDPMobile.Controls;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// TDPMobile Master page
    /// </summary>
    public partial class TDPMobile : System.Web.UI.MasterPage
    {
        #region Private members

        // Page
        private TDPPageMobile page = null;

        // List of local messages to be displayed on page
        protected List<TDPMessage> localMessages = new List<TDPMessage>();
        protected PageId redirectPageId = PageId.Empty;

        protected bool displayHeader = true;
        protected bool displayFooter = true;
        protected bool displayNavigation = true;
        protected bool displayBack = true;
        protected bool displayBrowserBack = false;
        protected bool displayNext = false;

        protected PageId pageIdNextButton = PageId.Empty;

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            page = ((TDPPageMobile)Page);

            // Add class name to SJPPageContent div so content pages can override the styles
            SJPPageContent.Attributes["class"] = SJPPageContent.Attributes["class"] + " " + page.PageId.ToString().ToLower();

            #region Add style sheets

            // Add stylesheets declared in properties (these should be absolute URLs)
            string cssfileKeyString = Properties.Current["StyleSheet.Files.Keys"];

            if (!string.IsNullOrEmpty(cssfileKeyString))
            {
                string[] cssfileKeys = cssfileKeyString.Split(',');

                foreach (string cssfileKey in cssfileKeys)
                {
                    string cssfile = Properties.Current[string.Format("StyleSheet.File.{0}", cssfileKey)];

                    if (!string.IsNullOrEmpty(cssfile))
                    {
                        page.AddStyleSheet(cssfile);
                    }
                }
            }

            // Default styles needed for an Page, placed in the Init method so they are added first
            // before any sub page adds their style sheets
            page.AddStyleSheet("TDPMain.css");

            #endregion

            #region Add javascript files

            // Add js files declared in properties (these should be absolute URLs)
            string jsfileKeyString = Properties.Current["Javscript.Files.Keys"];

            if (!string.IsNullOrEmpty(jsfileKeyString))
            {
                string[] jsfileKeys = jsfileKeyString.Split(',');

                foreach (string jsfileKey in jsfileKeys)
                {
                    string jsfile = Properties.Current[string.Format("Javscript.File.{0}",jsfileKey)];

                    if (!string.IsNullOrEmpty(jsfile))
                    {
                        page.AddJavascript(jsfile);
                    }
                }
            }

            page.AddJavascript("Common.js");

            #endregion

            headerControl.BackClickHandler += new OnBackClick(backBtn_Click);
            headerControl.NextClickHandler += new OnNextClick(nextBtn_Click);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add the default device stylesheet, 
            // javascript will override the path to be the device specific if needed
            page.AddStyleSheet("Device/Default.css");
        }
        
        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            SetupMessages();

            SetupNext();

            SetupBack();

            DisplayControls();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event handler for rptrMessages_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptrMessages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                TDPMessage tdpMessage = (TDPMessage)e.Item.DataItem;

                if (tdpMessage != null)
                {
                    Label lblMessage = (Label)e.Item.FindControl("lblMessage");

                    if (lblMessage != null)
                    {
                        string message = tdpMessage.MessageText;

                        // Temporary, this might change - remove any <strong> tags (because messages might be
                        // shared from Web which does need them)
                        message = message.Replace("<strong>", string.Empty);
                        message = message.Replace("</strong>", string.Empty);

                        // Temporary, all messages are of error type
                        lblMessage.Text = message;
                        lblMessage.CssClass = TDPMessageType.Error.ToString().ToLower();
                            //tdpMessage.Type.ToString().ToLower(); // "warning" or "error" or "info"
                    }
                }
            }
        }

        /// <summary>
        /// Back button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void backBtn_Click(object sender, EventArgs e)
        {
            switch (page.PageId)
            {
                case PageId.MobileDirection:
                    page.SetPageTransfer(PageId.MobileDetail);
                    page.AddQueryStringForPage(PageId.MobileDetail);
                    break;
                case PageId.MobileDetail:
                    page.SetPageTransfer(PageId.MobileSummary);
                    page.AddQueryStringForPage(PageId.MobileSummary);
                    break;
                case PageId.MobileSummary:
                    if (!Properties.Current["JourneySummary.JourneyInputControl.Visible.Switch"].Parse(false))
                    {
                        page.SetPageTransfer(PageId.MobileInput);
                    }
                    else
                    {
                        page.SetPageTransfer(PageId.MobileDefault);
                        page.AddQueryStringForPage(PageId.MobileDefault);
                    }
                    break;
                case PageId.MobileMap:
                    // Displaying for journey, from the detail page
                    if (IsNavigationForDetail())
                    {
                        page.SetPageTransfer(PageId.MobileDetail);
                        page.AddQueryStringForPage(PageId.MobileDetail);
                    }
                    // Displaying for location, from the summary page
                    else if (IsNavigationForSummary())
                    {
                        page.SetPageTransfer(PageId.MobileSummary);
                        page.AddQueryStringForPage(PageId.MobileSummary);
                    }
                    // Displaying for location, from the input page
                    else
                    {
                        page.SetPageTransfer(PageId.MobileInput);
                    }
                    break;
                case PageId.MobileTravelNews:
                    // Displaying for journey, from the detail page
                    if (IsNavigationForDetail())
                    {
                        page.SetPageTransfer(PageId.MobileDetail);
                        page.AddQueryStringForPage(PageId.MobileDetail);
                    }
                    // Displaying for journey, from the summary page
                    else if (IsNavigationForSummary())
                    {
                        page.SetPageTransfer(PageId.MobileSummary);
                        page.AddQueryStringForPage(PageId.MobileSummary);
                    }
                    else
                    {
                        page.SetPageTransfer(PageId.MobileDefault);
                    }
                    break;
                case PageId.MobileTravelNewsDetail:
                    page.SetPageTransfer(PageId.MobileTravelNews);

                    if (IsNavigationForLondonUndergroundNews())
                    {
                        page.AddQueryString(QueryStringKey.NewsMode, TravelNewsHelper.NewsViewMode_LUL);
                    }

                    break;
                case PageId.MobileAccessibilityOptions:
                    page.SetPageTransfer(PageId.MobileInput);
                    break;
                case PageId.MobileStopInformation:
                    page.SetPageTransfer(PageId.MobileDefault);
                    break;
                case PageId.MobileStopInformationDetail:
                    page.SetPageTransfer(PageId.MobileStopInformation);
                    page.AddQueryStringForPage(PageId.MobileStopInformation);
                    break;
                case PageId.MobileInput:
                case PageId.MobileError:
                case PageId.MobilePageNotFound:
                case PageId.MobileSorry:
                default:
                    page.SetPageTransfer(PageId.MobileDefault);
                    page.AddQueryStringForPage(PageId.MobileDefault);
                    break;
            }
        }

        /// <summary>
        /// Next button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nextBtn_Click(object sender, EventArgs e)
        {
            // Specific next page has been specified
            if (pageIdNextButton != PageId.Empty)
            {
                page.SetPageTransfer(pageIdNextButton);
                page.AddQueryStringForPage(pageIdNextButton);
            }
            else
            {
                // Use current page to determine next page transfer
                switch (page.PageId)
                {
                    case PageId.MobileInput:
                        page.SetPageTransfer(PageId.MobileMap);
                        page.AddQueryStringForPage(PageId.MobileMap);
                        break;
                    case PageId.MobileSummary:
                        page.SetPageTransfer(PageId.MobileMap);
                        page.AddQueryStringForPage(PageId.MobileMapSummary);
                        break;
                    case PageId.MobileDetail:
                        page.SetPageTransfer(PageId.MobileDirection);
                        page.AddQueryStringForPage(PageId.MobileDirection);
                        break;
                    case PageId.MobileDirection:
                        page.SetPageTransfer(PageId.MobileMap);
                        page.AddQueryStringForPage(PageId.MobileMapJourney);
                        break;
                    default:
                        // Do nothing
                        break;
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to add an TDPMessage to display on the page
        /// </summary>
        /// <param name="tdpMessage"></param>
        public void DisplayMessage(TDPMessage tdpMessage)
        {
            if (tdpMessage != null)
            {
                localMessages.Add(tdpMessage);
            }
        }

        /// <summary>
        /// Method to add an TDPMessage to display on the page, with optional 
        /// redirect to page (for javascript user) when message is closed
        /// </summary>
        /// <param name="tdpMessage"></param>
        public void DisplayMessage(TDPMessage tdpMessage, PageId redirectPageId)
        {
            if (tdpMessage != null)
            {
                localMessages.Add(tdpMessage);

                // Set redirect page
                if (redirectPageId != PageId.Empty)
                    this.redirectPageId = redirectPageId;
            }
        }

        /// <summary>
        /// Builds the TDPMessage list to show, retrieving actual text from Resource manager, and filtering 
        /// out duplicate messages
        /// </summary>
        /// <param name="tdpMessages"></param>
        /// <param name="messagesToDisplay"></param>
        public void BuildMessages(List<TDPMessage> tdpMessages, ref Dictionary<string, TDPMessage> messagesToDisplay)
        {
            TDPMessage message = null;

            foreach (TDPMessage tdpMessage in tdpMessages)
            {
                // For each message, get the display text, and add it to the display list.
                // Ensure duplicate messages are not added twice
                if (!messagesToDisplay.ContainsKey(tdpMessage.MessageResourceId))
                {
                    string messageText = string.Empty;

                    // Try to get a mobile-specific version
                    messageText = page.GetResourceMobile(tdpMessage.MessageResourceId);

                    if (!string.IsNullOrEmpty(tdpMessage.MessageResourceGroup) && !string.IsNullOrEmpty(tdpMessage.MessageResourceCollection))
                    {
                        messageText = page.GetResource(tdpMessage.MessageResourceGroup, tdpMessage.MessageResourceCollection, tdpMessage.MessageResourceId);
                    }

                    // Not got the messages text with the resource collection and group specified for the message
                    // Try get the message text with the generic resource collection
                    if (string.IsNullOrEmpty(messageText))
                    {
                        messageText = page.GetResource(tdpMessage.MessageResourceId);
                    }

                    // Not got the message text from the resource, check if message text was directly specified
                    if ((string.IsNullOrEmpty(messageText)) && (!string.IsNullOrEmpty(tdpMessage.MessageText)))
                    {
                        messageText = tdpMessage.MessageText;
                    }

                    if (!string.IsNullOrEmpty(messageText))
                    {
                        try
                        {
                            // Check if need to format the string with args for the message
                            if (tdpMessage.MessageArgs != null && tdpMessage.MessageArgs.Count > 0)
                            {
                                messageText = string.Format(messageText, tdpMessage.MessageArgs.ToArray());
                            }
                        }
                        catch
                        {
                            // Ignore any exceptions, we still have some sort of message to display
                        }

                        message = new TDPMessage(messageText, string.Empty, -1, -1, tdpMessage.Type);

                        string key = tdpMessage.MessageResourceId;

                        if (string.IsNullOrEmpty(key))
                        {
                            key = messageText;
                        }

                        messagesToDisplay.Add(tdpMessage.MessageResourceId, message);

                    }
                }
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Display the Header. Default is true
        /// </summary>
        public bool DisplayHeader
        {
            get { return displayHeader; }
            set { displayHeader = value; }
        }

        /// <summary>
        /// Read/Write. Display the Footer. Default is true
        /// </summary>
        public bool DisplayFooter
        {
            get { return displayFooter; }
            set { displayFooter = value; }
        }

        /// <summary>
        /// Read/Write. Display the Navigation panel containing the Back and Next buttons. Default is true
        /// </summary>
        public bool DisplayNavigation
        {
            get { return displayNavigation; }
            set { displayNavigation = value; }
        }

        /// <summary>
        /// Read/Write. Display the Back button. Default is true
        /// </summary>
        public bool DisplayBack
        {
            get { return displayBack; }
            set { displayBack = value; }
        }

        /// <summary>
        /// Read/Write. Display the Back button with javascript to return user back to previous page. Default is false
        /// </summary>
        public bool DisplayBrowserBack
        {
            get { return displayBrowserBack; }
            set { displayBrowserBack = value; }
        }

        /// <summary>
        /// Read/Write. Display the Next button. Default is false
        /// </summary>
        public bool DisplayNext
        {
            get { return displayNext; }
            set { displayNext = value; }
        }

        /// <summary>
        /// Read only property exposing local display messages, 
        /// so content pages can use them to bind to custom error message display
        /// </summary>
        public List<TDPMessage> PageMessages
        {
            get { return localMessages; }
        }

        /// <summary>
        /// exposes the script manager to content pages
        /// </summary>
        public ScriptManager PageScriptManager
        {
            get { return ScriptManager1; }
        }

        /// <summary>
        /// Next button control
        /// </summary>
        public System.Web.UI.WebControls.Button ButtonNext
        {
            get { return headerControl.ButtonNext; }
            set { headerControl.ButtonNext = value; }
        }

        /// <summary>
        /// Read/Write to specify a page for the next button click to go to, 
        /// instead of using the default logic for current page
        /// </summary>
        public PageId ButtonNextPage
        {
            get { return pageIdNextButton; }
            set { pageIdNextButton = value; }
        }
        
        #endregion

        #region Private methods

        /// <summary>
        /// Loads and sets common page resources
        /// </summary>
        private void SetupResources()
        {
            // Page heading title
            if (pageHeading != null)
            {
                pageHeading.InnerText = page.GetResourceMobile(string.Format("{0}.Heading.Text", page.PageId.ToString()));

                // No heading text, check for screenreader heading text (to allow a page to contain a h1 element)
                if (string.IsNullOrEmpty(pageHeading.InnerText))
                {
                    pageHeading.InnerText = page.GetResourceMobile(string.Format("{0}.Heading.ScreenReader.Text", page.PageId.ToString()));

                    // No screenreader heading text, hide the heading div
                    if (string.IsNullOrEmpty(pageHeading.InnerText))
                    {
                        headingDiv.Visible = false;
                    }
                    else
                    {
                        // Apply screen reader class to prevent the heading from being visible
                        string headingDivClass = headingDiv.Attributes["class"];
                        if (!headingDivClass.Contains("screenReaderOnly"))
                        {
                            headingDiv.Attributes["class"] = "screenReaderOnly";
                        }
                        pageHeading.Attributes["class"] = "screenReaderOnly";
                    }
                }
            }

            // Site version
            hdnSiteVersion.Value = Properties.Current["Site.VersionNumber"];
        }

        /// <summary>
        /// Loads any messages currently in the Session
        /// </summary>
        private void SetupMessages()
        {
            if (pnlMessages != null && rptrMessages != null)
            {
                // Messages to be displayed on page
                Dictionary<string, TDPMessage> messagesToDisplay = new Dictionary<string, TDPMessage>();

                // Check session for messages
                InputPageState pageState = TDPSessionManager.Current.PageState;

                // Check this page's message list
                if (localMessages.Count > 0)
                {
                    // Get messages
                    BuildMessages(localMessages, ref messagesToDisplay);

                    // Clear to avoid messages being displayed again
                    pageState.ClearMessages();
                }
                else if (pageState.Messages.Count > 0)
                {
                    // Get messages
                    BuildMessages(pageState.Messages, ref messagesToDisplay);

                    // Clear to avoid messages being displayed again
                    pageState.ClearMessages();
                }

                // Display all messages in repeater
                rptrMessages.DataSource = messagesToDisplay.Values;
                rptrMessages.DataBind();

                // Always display to allow javascript to use for local client messages
                pnlMessages.Visible = true; 

                // If a redirect page has been set, include the url for javascript to use
                if (redirectPageId != PageId.Empty)
                {
                    string redirectUrl = ResolveClientUrl(page.GetPageTransferDetail(redirectPageId).PageUrl);

                    if (!string.IsNullOrEmpty(redirectUrl))
                        closeinfodialog.Attributes["data-redirecturl"] = redirectUrl;
                }

                closeinfodialog.ToolTip = page.GetResourceMobile("JourneyInput.Close.ToolTip");
            }
        }

        /// <summary>
        /// Sets up the back button
        /// </summary>
        private void SetupBack()
        {
            string resourceBackText = string.Empty;
            string resourceBackToolTip = string.Empty;

            switch (page.PageId)
            {
                case PageId.MobileDirection:
                    resourceBackText = "JourneyInput.Back.MobileDetail.Text";
                    resourceBackToolTip = "JourneyInput.Back.MobileDetail.ToolTip";
                    break;
                case PageId.MobileDetail:
                    resourceBackText = "JourneyInput.Back.MobileSummary.Text";
                    resourceBackToolTip = "JourneyInput.Back.MobileSummary.ToolTip";
                    break;
                case PageId.MobileAccessibilityOptions:
                    resourceBackText = "JourneyInput.Back.MobileInput.Text";
                    resourceBackToolTip = "JourneyInput.Back.MobileInput.ToolTip";
                    break;
                case PageId.MobileInput:
                    resourceBackText = "JourneyInput.Back.MobileDefault.Text";
                    resourceBackToolTip = "JourneyInput.Back.MobileDefault.ToolTip";
                    break;
                case PageId.MobileSummary:
                    if (!Properties.Current["JourneySummary.JourneyInputControl.Visible.Switch"].Parse(false))
                    {
                        resourceBackText = "JourneyInput.Back.MobileInput.Text";
                        resourceBackToolTip = "JourneyInput.Back.MobileInput.ToolTip";
                    }
                    else
                    {
                        resourceBackText = "JourneyInput.Back.MobileDefault.Text";
                        resourceBackToolTip = "JourneyInput.Back.MobileDefault.ToolTip";
                    }
                    break;
                case PageId.MobileMap:
                    if (IsNavigationForDetail())
                    {
                        resourceBackText = "JourneyInput.Back.MobileDetail.Text";
                        resourceBackToolTip = "JourneyInput.Back.MobileDetail.ToolTip";
                    }
                    else if (IsNavigationForSummary())
                    {
                        resourceBackText = "JourneyInput.Back.MobileSummary.Text";
                        resourceBackToolTip = "JourneyInput.Back.MobileSummary.ToolTip";
                    }
                    else
                    {
                        resourceBackText = "JourneyInput.Back.MobileInput.Text";
                        resourceBackToolTip = "JourneyInput.Back.MobileInput.ToolTip";
                    }
                    break;
                case PageId.MobileTravelNews:
                    if (IsNavigationForDetail())
                    {
                        resourceBackText = "JourneyInput.Back.MobileDetail.Text";
                        resourceBackToolTip = "JourneyInput.Back.MobileDetail.ToolTip";
                    }
                    else if (IsNavigationForSummary())
                    {
                        resourceBackText = "JourneyInput.Back.MobileSummary.Text";
                        resourceBackToolTip = "JourneyInput.Back.MobileSummary.ToolTip";
                    }
                    else
                    {
                        resourceBackText = "JourneyInput.Back.Text";
                        resourceBackToolTip = "JourneyInput.Back.ToolTip";
                    }
                    break;
                case PageId.MobileStopInformation:
                case PageId.MobileStopInformationDetail:
                    resourceBackText = "JourneyInput.Back.Text";
                    resourceBackToolTip = "JourneyInput.Back.ToolTip";
                    break;
                default:
                    resourceBackText = "JourneyInput.Back.Text";
                    resourceBackToolTip = "JourneyInput.Back.ToolTip";
                    break;
            }

            headerControl.ResourceBackText = resourceBackText;
            headerControl.ResourceBackToolTip = resourceBackToolTip;
        }

        /// <summary>
        /// Sets up the next button
        /// </summary>
        private void SetupNext()
        {
            string resourceNextText = string.Empty;
            string resourceNextToolTip = string.Empty;

            // Specific next page has been specified
            if (pageIdNextButton != PageId.Empty)
            {
                resourceNextText = string.Format("JourneyInput.Next.{0}.Text", pageIdNextButton.ToString());
                resourceNextToolTip = string.Format("JourneyInput.Next.{0}.ToolTip", pageIdNextButton.ToString());
            }
            else
            {
                // Use current page to determine next page
                switch (page.PageId)
                {
                    case PageId.MobileInput:
                    case PageId.MobileSummary:
                    case PageId.MobileSummaryPartialUpdate:
                    case PageId.MobileSummaryResult:
                    case PageId.MobileSummaryWait:
                        resourceNextText = "JourneyInput.Next.MobileMap.Text";
                        resourceNextToolTip = "JourneyInput.Next.MobileMap.ToolTip";
                        break;
                    case PageId.MobileDetail:
                        resourceNextText = "JourneyInput.Next.MobileDirection.Text";
                        resourceNextToolTip = "JourneyInput.Next.MobileDirection.ToolTip";
                        break;
                    case PageId.MobileDirection:
                        resourceNextText = "JourneyInput.Next.MobileMapJourney.Text";
                        resourceNextToolTip = "JourneyInput.Next.MobileMapJourney.ToolTip";
                        break;
                    default:
                        resourceNextText = "JourneyInput.Next.Text";
                        resourceNextToolTip = "JourneyInput.Next.ToolTip";
                        break;
                }
            }

            headerControl.ResourceNextText = resourceNextText;
            headerControl.ResourceNextToolTip = resourceNextToolTip;
        }

        /// <summary>
        /// Sets the visibility of common controls on the page
        /// </summary>
        private void DisplayControls()
        {
            // Only display the navigation (header and footer) controls if required,
            // session default is false if key missing, but default is to show navigation
            bool navigationRequired = !TDPSessionManager.Current.Session[SessionKey.IsNavigationNotRequired];

            headerControl.Visible = navigationRequired && displayHeader;
            footerControl.Visible = navigationRequired && displayFooter;

            headerControl.DisplayNavigation = displayNavigation;
            headerControl.DisplayBack = displayBack;
            headerControl.DisplayBrowserBack = displayBrowserBack;
            headerControl.DisplayNext = displayNext;
        }

        /// <summary>
        /// If page querystring contains a journey request hash
        /// </summary>
        /// <returns></returns>
        private bool IsNavigationForSummary()
        {
            return (page.QueryStringContains(QueryStringKey.JourneyRequestHash));
        }

        /// <summary>
        /// If page querystring contains a journey request hash and journey id
        /// </summary>
        /// <returns></returns>
        private bool IsNavigationForDetail()
        {
            return (page.QueryStringContains(QueryStringKey.JourneyRequestHash)
                    && 
                    (page.QueryStringContains(QueryStringKey.JourneyIdOutward) || page.QueryStringContains(QueryStringKey.JourneyIdReturn))
                   );
        }

        // If page querystring containg a travel news mode for london underground
        private bool IsNavigationForLondonUndergroundNews()
        {
            return (page.QueryStringContains(QueryStringKey.NewsMode));

        }

        #endregion
    }
}
