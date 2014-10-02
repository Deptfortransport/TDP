// *********************************************** 
// NAME                 : LoginRegister.aspx.cs 
// AUTHOR               : Someone
// DATE CREATED         : 01/03/2008
// DESCRIPTION			: Webform containing the template
// for the LoginRegister
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/LoginRegister.aspx.cs-arc  $
//
//   Rev 1.9   Jul 28 2011 16:17:30   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.8   Nov 08 2010 09:22:18   apatel
//updated to resolve the issue with Session timeout value increments to too big and throws an error.
//Resolution for 5633: Session time out value increases to unexpected value
//
//   Rev 1.7   Oct 29 2010 09:08:06   apatel
//updated to enable logged in user to have feature of extended session time out
//Resolution for 5625: Users not able to extend their session timeout
//
//   Rev 1.6   Dec 15 2008 09:54:04   apatel
//XHTML compliance changes 
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   Jul 15 2008 16:50:02   mmodi
//Added code to return user to the help page 
//Resolution for 5065: Log in issues - Find a map, and Help pages
//
//   Rev 1.4   Jun 26 2008 14:03:56   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3   May 27 2008 11:45:26   mmodi
//Corrected logging in success message logic and general tidy up
//Resolution for 5006: Login: Success message shown when invalid details entered
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.Web.Templates
{
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class LoginRegister : TDPage
    {
        # region control declarations

        protected TransportDirect.UserPortal.Web.Controls.LoginControl loginControl;
        protected TransportDirect.UserPortal.Web.Controls.RegisterControl registerControl;
        protected TransportDirect.UserPortal.Web.Controls.LogoutControl logoutControl;

        # endregion

        #region constructor

        public LoginRegister()
            : base()
        {
            pageId = PageId.LoginRegister;
        }

        # endregion

        # region Page Load
        /// <summary>
        /// Handler for the load event. Sets up the visibility of the encapsulated controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            SetControlVisibility();
            
            #region Text
            //CCN 0427 setting up help page url
            Helpbuttoncontrol1.HelpUrl = GetResource("LoginRegister.HelpPageUrl");

            labelTitleLoginRegister.Text = GetResource("loginRegisterPanel.loginRegisterLabel");
            loginControlText.Text = GetResource("LoginRegister.loginControlText.Text");
            registerControlText.Text = GetResource("LoginRegister.registerControlText.Text");
            whyRegisterControlText.Text = GetResource("LoginRegister.whyRegisterControlText.Text");
            buttonBack1.Text = GetResource("LoginRegisterPage.BackButton.Text");

            //Title
            this.PageTitle = GetResource("LoginRegisterPage.PageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            #endregion

            // Add javascript to the client side
            AddClientForUserNavigation();

            #region Left hand navigation
            // Set left hand menu
            if (TDSessionManager.Current.Authenticated)
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuLoggedIn);
                expandableMenuControl.AddExpandedCategory("UserLoggedIn");

                labelTitleLoginRegister.Text = GetResource("loginRegisterPanel.logoutUpdateLabel");
            }
            else
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuLoginRegister);
                expandableMenuControl.AddExpandedCategory("LoginRegister");
            }

            // Set Session time out to be 10 times default session timeout for login page
            if (Session.Timeout != GetDefaultSessionTimeOut() * 10)
            {
                Session.Timeout = GetDefaultSessionTimeOut() * 10;
            }

            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextLoginRegister);
            #endregion
        }

        # endregion

        #region Events
        /// <summary>
        /// Handles display of controls for logout option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Logout_Command(object sender, System.EventArgs e)
        {
            loginControl.Visible = false;
            registerControl.Visible = false;
            logoutControl.Visible = true;
        }

        private void loginControl_ButtonClicked(object sender, EventArgs e)
        {
            loginControl.Visible = true;

            // If we came here from a help page, then go back
            ReturnToHelpPage();
        }

        private void registerControl_ButtonClicked(object sender, EventArgs e)
        {
            registerControl.Visible = true;
        }

        /// <summary>
        /// This page back button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack1_Click(object sender, EventArgs e)
        {
            ITDSessionManager session =
                    (ITDSessionManager)TDServiceDiscovery.Current
                    [ServiceDiscoveryKey.SessionManager];

            // If we came here from a help page, then go back
            ReturnToHelpPage();

            session.Session[SessionKey.Transferred] = false;

            TDSessionManager.Current.SetOneUseKey(SessionKey.IndirectLocationPostBack, string.Empty);
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.LoginRegisterBack; // default one
        }

        /// <summary>
        /// Performs a redirect to a help page if user came to this page from the help page
        /// </summary>
        private void ReturnToHelpPage()
        {
            if (TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count > 0)
            {
                #region Check if from a journey Help page and redirect
                // Specific handling if user came from a journey Help page.
                // Need to do it this way because Global.asax has to translate a virtual help page to a
                // physical help page e.g. "Help/HelpFindATrainInput.aspx" is translated to 
                // "helpfulljp.aspx?id=_web2_help_helpfindatraininput"

                PageId returnPageId = (PageId)TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();

                if (returnPageId == PageId.HelpFullJP)
                {
                    // Get the original querystring value, and translate this in to the virtual help page
                    string queryString = TDSessionManager.Current.InputPageState.JourneyInputQueryString;
                    string virtualPageUrl = queryString.Replace("_", "/").Replace("id=", "");
                    virtualPageUrl += ".aspx";

                    // Clear out the saved query string value
                    TDSessionManager.Current.InputPageState.JourneyInputQueryString = string.Empty;

                    // Reset transition event
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.Empty;

                    // Need to ensure all data is properly saved away before exiting.
                    // This is needed because this redirection does not use the ScreenFlow framework.
                    TDSessionManager.Current.OnPreUnload();

                    Response.Redirect(virtualPageUrl, true);
                }
                #endregion

                // else, push back the return page id and let ScreenFlow handle
                if (returnPageId != PageId.Empty)
                    TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(returnPageId);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets the visibility of the login, register panels and controls
        /// </summary>
        private void SetControlVisibility()
        {
            if (TDSessionManager.Current.Authenticated)
            {
                // Login
                panelLogin.Visible = true;
                logoutControl.Visible = false;
                loginControl.Visible = true;

                // Register
                panelRegister.Visible = false;

                panelWhyRegister.Visible = false;
            }
            else
            {
                // Login
                panelLogin.Visible = true;
                loginControl.Visible = true;
                logoutControl.Visible = false;

                // Register
                panelRegister.Visible = true;
                registerControl.Visible = true;

                panelWhyRegister.Visible = true;
            }
        }

        /// <summary>
        /// Registers the client side script.
        /// </summary>
        private void AddClientForUserNavigation()
        {
            string javaScriptFileName = "UserNavigationLoginAction";
            string javaScriptDom = ((TDPage)Page).JavascriptDom;
            UserExperienceEnhancementHelper.RegisterClientScript(this.Page, javaScriptFileName);
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            buttonBack1.Click += new EventHandler(this.buttonBack1_Click);
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginControl.ButtonClicked += new EventHandler(loginControl_ButtonClicked);
            loginControl.Logout += new EventHandler(Logout_Command);
            registerControl.ButtonClicked += new EventHandler(registerControl_ButtonClicked);
        }
        #endregion
    }
}
