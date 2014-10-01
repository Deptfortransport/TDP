// *********************************************** 
// NAME             : TDPWeb.Master.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: TDPWeb Master page
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.PropertyManager;
using TDP.Common.Web;
using TDP.UserPortal.SessionManager;

namespace TDP.UserPortal.TDPWeb
{
    /// <summary>
    /// TDPWeb Master Page
    /// </summary>
    public partial class TDPWeb : System.Web.UI.MasterPage
    {
        #region Private members

        // List of local messages to be displayed on page
        protected List<TDPMessage> localMessages = new List<TDPMessage>();

        protected bool displayHeader = true;
        protected bool displayFooter = true;
        protected bool displaySideBarLeft = true;
        protected bool displaySideBarRight = true;

        protected TDPPage page;

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            page = ((TDPPage)Page);

            // Add class name to TDPPageContent div so content pages can override the styles
            TDPPageContent.Attributes["class"] = TDPPageContent.Attributes["class"] + " " + page.PageId.ToString().ToLower();

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
            page.AddStyleSheet("SJPColours.css");
            page.AddStyleSheet("SJPFonts.css");
            page.AddStyleSheet("SJPBackgrounds.css");
            page.AddStyleSheet("SJPMain.css");
            page.AddStyleSheet("SJPAccessibility.css");
            page.AddStyleSheet("SJPPrint.css", "print");
            
            #endregion

            #region Add javascript files

            // Add js files declared in properties (these should be absolute URLs)
            string jsfileKeyString = Properties.Current["Javscript.Files.Keys"];

            if (!string.IsNullOrEmpty(jsfileKeyString))
            {
                string[] jsfileKeys = jsfileKeyString.Split(',');

                foreach (string jsfileKey in jsfileKeys)
                {
                    string jsfile = Properties.Current[string.Format("Javscript.File.{0}", jsfileKey)];

                    if (!string.IsNullOrEmpty(jsfile))
                    {
                        page.AddJavascript(jsfile);
                    }
                }
            }

            page.AddJavascript("Common.js");
            
            #endregion
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        
        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            #region Add language/sitemode class

            // Add the language class if needed (for language styling)
            if (CurrentLanguage.Value == Language.Welsh)
            {
                if (!formTDP.Attributes["class"].Contains("cy"))
                    formTDP.Attributes["class"] = formTDP.Attributes["class"] + " cy";
            }
            else
            {
                formTDP.Attributes["class"] = formTDP.Attributes["class"].Replace(" cy", string.Empty);
            }

            // Add the sitemode class if needed (for sitemode styling)
            if (page.SiteModeDisplay == SiteMode.Paralympics)
            {
                if (!formTDP.Attributes["class"].Contains("paralympic"))
                    formTDP.Attributes["class"] = formTDP.Attributes["class"] + " paralympic";
            }
            else
            {
                formTDP.Attributes["class"] = formTDP.Attributes["class"].Replace(" paralympic", string.Empty);
            }

            #endregion

            SetupResources();

            SetupMessages();

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
                        lblMessage.Text = tdpMessage.MessageText;
                        lblMessage.CssClass = tdpMessage.Type.ToString().ToLower(); // "warning" or "error" or "info"
                    }
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
        /// Builds the TDPMessage list to show, retrieving actual text from Resource manager, and filtering 
        /// out duplicate messages
        /// </summary>
        /// <param name="sjpMessages"></param>
        /// <param name="messagesToDisplay"></param>
        public void BuildMessages(List<TDPMessage> sjpMessages, ref Dictionary<string, TDPMessage> messagesToDisplay)
        {
            TDPMessage message = null;

            foreach (TDPMessage tdpMessage in sjpMessages)
            {
                // For each message, get the display text, and add it to the display list.
                // Ensure duplicate messages are not added twice
                if (!messagesToDisplay.ContainsKey(tdpMessage.MessageResourceId))
                {
                    string messageText = string.Empty;

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
        /// Read/Write. Display the SideBarLeft. Default is true
        /// </summary>
        public bool DisplaySideBarLeft
        {
            get { return displaySideBarLeft; }
            set { displaySideBarLeft = value; }
        }

        /// <summary>
        /// Read/Write. Display the SideBarRight. Default is true
        /// </summary>
        public bool DisplaySideBarRight
        {
            get { return displaySideBarRight; }
            set { displaySideBarRight = value; }
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
        /// Determines if the venue map widget needs showing in right side bar on input page
        /// </summary>
        public bool ShowVenueMapOnInputPage
        {
            set { sideBarRightControl.ShowVenueMapOnInputPage = value; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads and sets common page resources
        /// </summary>
        private void SetupResources()
        {
            if (mainHeading != null)
                mainHeading.InnerText = page.GetResource("Heading.Text");
           
            if (sjpHeading != null)
                sjpHeading.InnerText = page.GetResource(string.Format("{0}.Heading.Text", page.PageId.ToString()));

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

                if (pageState.Messages.Count > 0)
                {
                    // Get messages
                    BuildMessages(pageState.Messages, ref messagesToDisplay);

                    // Clear to avoid messages being displayed again
                    pageState.ClearMessages();
                }

                // Check this page's message list
                if (localMessages.Count > 0)
                {
                    // Get messages
                    BuildMessages(localMessages, ref messagesToDisplay);
                }

                // Display all messages in repeater
                rptrMessages.DataSource = messagesToDisplay.Values;
                rptrMessages.DataBind();

                pnlMessages.Visible = messagesToDisplay.Count > 0;
            }
            
        }
                

        /// <summary>
        /// Sets the visibility of common controls on the page
        /// </summary>
        private void DisplayControls()
        {
            headerControl.Visible = displayHeader;
            footerControl.Visible = displayFooter;
            sideBarLeftControl.Visible = displaySideBarLeft;
            sideBarRightControl.Visible = displaySideBarRight;
        }

        #endregion
    }
}
