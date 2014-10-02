// ************************************************ 
// NAME                 : LoginControl.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 06/09/2005
// DESCRIPTION          : A custom user control to
// allow users to log on to the portal.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LoginControl.ascx.cs-arc  $
//
//   Rev 1.11   May 17 2012 09:35:02   DLane
//Increasing the user id address length and handling users suffering from truncated user ids.
//Resolution for 5810: UserId length increase
//
//   Rev 1.10   Nov 08 2010 08:49:54   apatel
//Updated to resolve the issue with the extended session time out where page was throwing error after user press go multiple times
//Resolution for 5633: Session time out value increases to unexpected value
//
//   Rev 1.9   Oct 29 2010 09:08:12   apatel
//updated to enable logged in user to have feature of extended session time out
//Resolution for 5625: Users not able to extend their session timeout
//
//   Rev 1.8   Jan 20 2010 10:45:22   pghumra
//Fixed code to cater for users trying to login with an email address followed by trailing spaces.
//Resolution for 5369: TDP login/register functionality cannot deal with email addresses with trailing spaces in.
//
//   Rev 1.7   Jan 07 2009 12:51:38   apatel
//XHTML Compliance Changes - corrected the alignment issue with the username and password textboxes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.6   Dec 15 2008 09:53:56   apatel
//XHTML compliance changes 
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   May 27 2008 11:43:42   mmodi
//Corrected logging in success message logic and general tidy up
//Resolution for 5006: Login: Success message shown when invalid details entered
//
//   Rev 1.4   May 07 2008 14:51:22   apatel
//made login control to show error message when two different email entered while updating email.
//Resolution for 4897: When changing e-mail of an existing user, if the two new e-mail addresses entered do not match, no error is shown
//
//   Rev 1.3   May 01 2008 15:13:32   apatel
//When user logs in he/she should go to same page they come from. login button click event modified for these.
//Resolution for 4920: Login/Logout page issues
//
//   Rev 1.2   Mar 31 2008 13:21:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:08   mturner
//Initial revision.
//
//   Rev 1.14   Aug 08 2007 14:41:16   pscott
//IR 4475 Change button styles  so that they dynamically resize
//
//   Rev 1.13   May 21 2007 10:08:06   Pscott
//Post code review changes
//
//   Rev 1.12   May 18 2007 12:01:22   Pscott
//CCN0368
//
//   Rev 1.11   May 17 2007 10:31:22   Pscott
//CCN0368
//Amend Registered user details
//
//   Rev 1.10   Feb 23 2006 19:16:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.9.1.0   Jan 10 2006 15:26:04   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Nov 16 2005 13:46:44   RGriffith
//IR3063 Resolution - Setting Forgotten Password Button to a "Hyperlink" image button
//
//   Rev 1.8   Nov 07 2005 15:36:22   AViitanen
//TD089, ES020 html button changes
//
//   Rev 1.7   Nov 07 2005 15:15:40   AViitanen
//TD089, ES020: HTML button changes
//
//   Rev 1.6   Nov 07 2005 09:51:34   rgreenwood
//Manual merge stream2816: Corrected namespace for TDButton instance declarations
//
//   Rev 1.5   Nov 04 2005 12:08:46   ECHAN
//Merged for stream 2816 DEL8
//
//   Rev 1.4   Oct 11 2005 20:20:38   kjosling
//Reworked validation process to iron out missing validation. 
//Resolution for 2860: DN79:  UEE Enhancements - No error messages being shown when inputting an invalid email and password and submitting for Register/Login control
//
//   Rev 1.3   Oct 05 2005 16:43:20   kjosling
//Fix applied for IR 2828
//Resolution for 2828: CCN219 - Global Login Control persistance
//
//   Rev 1.2   Sep 21 2005 17:49:50   NMoorhouse
//Updated following review comment
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.1   Sep 16 2005 14:36:38   NMoorhouse
//DN079 UEE, TD092 Login and register - changes made during UT
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.0   Sep 13 2005 15:43:56   NMoorhouse
//Initial revision.
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.Mail;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;
	using TransportDirect.UserPortal.Web.Controls;
	using Logger = System.Diagnostics.Trace;
	using TransportDirect.Common.Logging;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using TransportDirect.UserPortal.Web.Events;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.UserPortal.Resource;

	/// <summary>
	/// Handles user login.  Logs in users, setting Auth and Profile Tickets.  
	/// Uses delegates to inform interested parties (eg jp page) that a refresh 
	/// is needed to re-display page elements only visible to logged in users.
	/// </summary>
	public partial  class LoginControl : TDUserControl
    {
        #region variables
        protected TransportDirect.UserPortal.Web.Controls.TDButton OkBtn;

        protected TransportDirect.UserPortal.Web.Controls.TDButton forgotPassBtn;
        protected TransportDirect.UserPortal.Web.Controls.TDButton changeEmailAddressBtn;
        protected TransportDirect.UserPortal.Web.Controls.TDButton deleteAccountBtn;

		
		protected bool success = false;
		public const int MINIMUM_LENGTH = 4;
		public const int MAXIMUM_LENGTH = 12;

		protected TransportDirect.UserPortal.Web.Controls.TDButton confirmButton;
		protected TransportDirect.UserPortal.Web.Controls.TDButton confirmCancelButton;
		protected TransportDirect.UserPortal.Web.Controls.HelpCustomControl helpCustomControlConfirm;
		protected TransportDirect.UserPortal.Web.Controls.TDButton Tdbutton2;
	
		

		public const int CHARACTER_LIMIT = 50;
		public event System.EventHandler ButtonClicked;
        public event EventHandler Logout;
        #endregion

        #region Page_Load
        /// <summary>
		/// Handles Page Load event
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// set error message text of validators
            if (!Page.IsPostBack) 
			{
				emailValidator.ErrorMessage = GetResource("loginPanel.emailValidator");
				passwordValidator.ErrorMessage = GetResource("loginPanel.passwordValidator");
				loginPanel.Visible = true;
				messageLabel.Text = string.Empty;
				messageLabel.Visible = false;
				messageLabel.ForeColor = Color.Black;

                forgotPasswordEmailValidator.ErrorMessage = GetResource("loginPanel.emailValidator");
                forgotPasswordMessageLabel.Text = string.Empty;
                forgotPasswordMessageLabel.Visible = false;
                forgotPasswordMessageLabel.ForeColor = Color.Black;

				success = false;
			}
			else 
			{
				messageLabel.Visible= false;
				emailInMessageLabel.Visible = false;

                forgotPasswordMessageLabel.Visible = false;
                forgotPasswordEmailInMessageLabel.Visible = false;
            }

            #region Text
            loginLabel.Text = GetResource("loginPanel.loginLabel");
            logoutLabel.Text = GetResource("loginPanel.logoutLabel.Text");
			emailLabel.Text = GetResource("loginPanel.emailLabel");
			pWordLabel.Text = GetResource("loginPanel.pWordLabel");
			
			forgotPassBtn.Text = GetResource("loginPanel.forgotPassLinkButton");
			forgotPassBtn.ToolTip = GetResource("loginPanel.forgotPassBtn.AlternateText");
			changeEmailAddressBtn.Text = GetResource("changeEmailPanel.changeEmailAddressBtn.Text");
			changeEmailAddressBtn.ToolTip = GetResource("loginPanel.changeEmailAddressBtn.AlternateText");
			deleteAccountBtn.Text = GetResource("loginPanel.deleteAccountBtn.AlternateText");
			deleteAccountBtn.ToolTip = GetResource("loginPanel.deleteAccountBtn.AlternateText");
	
			logonButton.Text = GetResource("loginPanel.logonButton.Text");
            logoutButton.Text = GetResource("LogoutControl.logoutTitelLabel.Text");

			deleteUserConfirmCancelLabel.Text = GetResource("deleteUserPanel.deleteUserConfirmCancelLabel.Text");
			deleteUserTitleLabel.Text = GetResource("deleteUserPanel.deleteUserTitleLabel.Text");	
			deleteUserConfirmCancelButton.Text =  GetResource("deleteUserPanel.deleteUserConfirmCancelButton.Text");
			deleteUserConfirmButton.Text = GetResource("deleteUserPanel.deleteUserConfirmButton.Text");

			changeEmailTitleLabel.Text = GetResource("changeEmailPanel.changeEmailTitleLabel.Text");	
			changeEmailCancelButton.Text = GetResource("deleteUserPanel.deleteUserConfirmCancelButton.Text");
			changeEmailConfirmButton.Text = GetResource("changeEmailPanel.changeEmailConfirmButton.Text");
			changeEmailAddressLabel1.Text =GetResource("changeEmailPanel.changeEmailAddressLabel1.Text");
			changeEmailAddressLabel2.Text =GetResource("changeEmailPanel.changeEmailAddressLabel2.Text");

            forgotPasswordTitle.Text = GetResource("forgotPasswordPanel.forgotPasswordTitle.Text");
            forgotPasswordEmailLabel.Text = GetResource("loginPanel.emailLabel");
            forgotPasswordCancelButton.Text = GetResource("deleteUserPanel.deleteUserConfirmCancelButton.Text");
            forgotPasswordConfirmButton.Text = GetResource("changeEmailPanel.changeEmailConfirmButton.Text");

            saveUserPreferencesCancelButton.Text = GetResource("userPreferencePanel.saveUserPreferencesCancelButton.Text");
            saveUserPreferencesConfirmButton.Text = GetResource("userPreferencePanel.saveUserPreferencesConfirmButton.Text");
            userPreferencesTitleLabel.Text = GetResource("userPreferencePanel.userPreferencesTitleLabel.Text");
            extendSessionTimeOutCheckBox.Text = GetResource("userPreferencePanel.extendSessionTimeOutCheckBox.Text");

            #endregion

            // Adding script to the textbox
			AddDefaultLoginAction();
			
            if (!IsLoggedIn())
            {
                changeEmailAddressBtn.Visible = false;
                deleteAccountBtn.Visible = false;
                changeUserPreferencesBtn.Visible = false;
                loginLabel.Visible = true;
                logoutLabel.Visible = false;
                logoutButton.Visible = false;
                logonButton.Visible = true;
            }
            else
            {
                string queryString = Request.QueryString["page"];

                queryString = string.IsNullOrEmpty(queryString) ? string.Empty : queryString.ToLower();

                if ((Parent != null && Parent.GetType().BaseType != typeof(LoginRegisterLogoutControl)) && !IsPostBack)
                {
                    switch (queryString)
                    {
                        case "updateemail":
                            changeEmailAddressBtn_Click(null, null);
                            break;
                        case "deleteaccount":
                            deleteUser();
                            break;
                        case "logout":
                            logoutButton_Click(null, null);
                            break;
                        case "preferences":
                            changeUserPreferencesBtn_Click(null, null);
                            break;
                        default:
                            loginLabel.Visible = false;
                            logoutLabel.Visible = true;
                            textboxPanel.Visible = false;
                            logoutButton.Visible = true;
                            logonButton.Visible = false;
                            forgotPassBtn.Visible = false;
                            break;
                    }
                }
                else
                {
                    loginLabel.Visible = false;
                    logoutLabel.Visible = true;
                    textboxPanel.Visible = false;
                    logoutButton.Visible = true;
                    logonButton.Visible = false;
                    forgotPassBtn.Visible = false;
                }

                               
            }

        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraWiringEvents();
			InitializeComponent();
			base.OnInit(e);
		}
        
		///     Required method for Designer support - do not modify
		///     the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraWiringEvents()
		{
			logonButton.Click += new EventHandler(this.logonButton_Click);
			forgotPassBtn.Click += new EventHandler(forgotPassBtn_Click);
			changeEmailAddressBtn.Click += new EventHandler(changeEmailAddressBtn_Click);
			deleteAccountBtn.Click += new EventHandler(this.deleteAccountBtn_Click);
			deleteUserConfirmButton.Click += new EventHandler(this.deleteUserConfirmButton_Click);
			deleteUserConfirmCancelButton.Click += new EventHandler(this.deleteUserConfirmCancelButton_Click);
			changeEmailConfirmButton.Click += new EventHandler(this.changeEmailConfirmButton_Click);
			changeEmailCancelButton.Click += new EventHandler(this.changeEmailCancelButton_Click);

            forgotPasswordCancelButton.Click += new EventHandler(this.forgotPasswordCancelButton_Click);
            forgotPasswordConfirmButton.Click += new EventHandler(this.forgotPasswordConfirmButton_Click);
            logoutButton.Click += new EventHandler(this.logoutButton_Click);

            loginHelpLabel.CloseButton.Click += new ImageClickEventHandler(helpCustomControlLogin_Click);

            saveUserPreferencesConfirmButton.Click += new EventHandler(saveUserPreferencesConfirmButton_Click);
            saveUserPreferencesCancelButton.Click += new EventHandler(saveUserPreferencesCancelButton_Click);

            changeUserPreferencesBtn.Click += new EventHandler(changeUserPreferencesBtn_Click);
		}

        

        
		#endregion

        #region Public properties
        /// <summary>
		/// Read only property, returning the password as entered by the user.
		/// </summary>
		public string Password
		{
			get
			{
				return HttpUtility.HtmlEncode(passwordTxtBox.Text.Trim());
			}
		}

		/// <summary>
		/// Read only property, returning the username as entered by the user.
		/// </summary>
		public string Username
		{
			get
			{
				return HttpUtility.HtmlEncode(emailTxtBox.Text.Trim());
			}
        }
        
        /// <summary>
		/// Checks the validity of the user entered password, 
		/// returning true if valid, false otherwise
		/// </summary>
		/// <returns>bool</returns>
		private bool IsValidPasswordLength()
		{
			if ((Password.Length >= MINIMUM_LENGTH) && (Password.Length <= MAXIMUM_LENGTH))
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		/// <summary>
		/// Checks the validity of the user entered email address, 
		/// returning true if valid, false otherwise
		/// </summary>
		/// <returns>bool</returns>
		private bool IsValidEmailAddress() 
		{
			return new EmailAddress(Username).IsValid;
		}


		/// <summary>
		/// Checks the authentication information to 
		/// determine whether the user is authenticated.  Returns true if
		/// authenticated, false otherwise.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsLoggedIn ()
		{
			return TDSessionManager.Current.Authenticated;
        }

        /// <summary>
        /// Gets the session time out specified in the web config file
        /// </summary>
        /// <returns></returns>
        public int GetDefaultSessionTimeOut()
        {
            System.Web.Configuration.SessionStateSection sessionStateSection =
                (System.Web.Configuration.SessionStateSection)System.Configuration.ConfigurationManager.GetSection("system.web/sessionState");
            TimeSpan sessionTimeOut = sessionStateSection.Timeout;
            return sessionTimeOut.Minutes;
        }
        #endregion

        #region Private methods
        /// <summary>
		/// Logs in a previously registered portal user. Attempts to retrieve the user's
		/// user profile from their supplied username.  If a profile is
		/// obtained, the password entered by the user and that stored with their profile
		/// are compared.  If they match the user is logged in (authenticated) and the 
		/// display is updated. 
		/// </summary>
		private bool LoginUser()
		{
			//if there is not a user with this username register them as a new user, otherwise continue
			TDUser user = new TDUser();
            bool fixTruncatedUsername = false;
            
			//if user not found, user is not registered         
			if (user.FetchUser( Username ) == false )           
			{
                // Cope with users whose username (email address) was truncated by
                // the ProfileData table's userid field length being 36
                if (Username.Length >= 36)
                {
                    string truncatedUsername = Username.Substring(0, 36);
                    if (user.FetchUser(truncatedUsername) == false)
                    {
                        messageLabel.Text = GetResource("loginPanel.messageLabel.unregistered");
                        messageLabel.Visible = true;
                        messageLabel.ForeColor = Color.Red;
                        return success;
                    }
                    else
                    {
                        fixTruncatedUsername = true;
                    }
                }
                else
                {
                    messageLabel.Text = GetResource("loginPanel.messageLabel.unregistered");
                    messageLabel.Visible = true;
                    messageLabel.ForeColor = Color.Red;
                    return success;
                }
			}

            if ((user.Logon(Password)) && (Page.IsValid))
            {
                if (fixTruncatedUsername)
                {
                    // Create a user using the fulle username (email)
                    string truncatedUsername = Username.Substring(0, 36);
                    TDSessionManager.Current.UnsavedUsername = Username;
                    TDSessionManager.Current.UnsavedPassword = Password;
                    user.UserProfile.Username = TDSessionManager.Current.UnsavedUsername;
                    user.Update();

                    // NOW DELETE OLD USER
                    TDUser deletedUser = new TDUser();
                    if (deletedUser.FetchUser(truncatedUsername) == true)
                    {
                        deletedUser.DeleteUser(Password);
                    }
                }

                HttpContext.Current.Session["authenticated"] = true;
                HttpContext.Current.Session[SessionKey.Username.ID] = Username;
                TDSessionManager.Current.CurrentUser = user;

                // Persist user profile
                user.Update();

                // log login event
                LoginEvent le = new LoginEvent(TDSessionManager.Current.Session.SessionID);
                Logger.Write(le);

                #region check for extended session time out

                bool sessionExtended = false;
                if (user.UserProfile.Properties[ProfileKeys.EXTENDED_SESSION].Value != null)
                {
                    Convert.ToBoolean(user.UserProfile.Properties[ProfileKeys.EXTENDED_SESSION].Value);
                }

                if (sessionExtended)
                {
                    if (Session.Timeout != GetDefaultSessionTimeOut() * 10)
                    {
                        Session.Timeout = GetDefaultSessionTimeOut() * 10;
                    }
                }
                else // set time out to the default value specified in the web config
                {
                    Session.Timeout = GetDefaultSessionTimeOut();
                }

                #endregion

                emailTxtBox.Text = string.Empty;

                success = true;

            }
            else
            {
                HttpContext.Current.Session["authenticated"] = false;
                // password didn't match, prompt user to re-enter it
                messageLabel.Text = GetResource("loginPanel.messageLabel.passError");
                messageLabel.Visible = true;
                messageLabel.ForeColor = Color.Red;
                passwordTxtBox.Text = string.Empty;
                success = false;
            }
    
			return success;
        }

        /// <summary>
        /// Deletes  a previously registered portal user. Attempts to retrieve the user's
        /// user profile from their supplied username.  If a profile is
        /// obtained, the password entered by the user and that stored with their profile
        /// are compared.  If they match the user account is deleted and the 
        /// display is updated. 
        /// </summary>
        private bool deleteUser()
        {
            TDUser user = new TDUser();
            TDUser currentUser = TDSessionManager.Current.CurrentUser;

            //if user not found, user is not registered         
            if (user.FetchUser(currentUser.Username) == false)
            {
                messageLabel.Text = GetResource("loginPanel.messageLabel.unregistered");
                messageLabel.Visible = true;
                messageLabel.ForeColor = Color.Red;
            }
            else
            {
                if (user.Logon(currentUser.Password))
                {
                    deleteUserPanel.Visible = true;
                    loginPanel.Visible = false;

                    //TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
                    this.Visible = true;
                }
                else
                {
                    HttpContext.Current.Session["authenticated"] = false;
                    // password didn't match, prompt user to re-enter it
                    messageLabel.Text = GetResource("loginPanel.messageLabel.passError");
                    messageLabel.Visible = true;
                    messageLabel.ForeColor = Color.Red;
                    passwordTxtBox.Text = string.Empty;
                    success = false;
                }

            }
            return success;
        }

        
       

        #endregion

        #region Event handlers
        /// <summary>
		/// Handles the forgotten password link button click event.  Attempts to 
		/// obtain the user's profile from the username entered.  If retrieved
		/// and password is confirmed, sends an email cont
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void forgotPassBtn_Click(object sender, EventArgs e)
		{

            forgotPasswordPanel.Visible = true;
            changeEmailPanel.Visible = false;
            deleteUserPanel.Visible = false;
            loginPanel.Visible = false;
            userPreferencePanel.Visible = false;
            this.Visible = true;

            if (ButtonClicked != null)
            {
                ButtonClicked(this, new System.EventArgs());
            }
		}


		
		/// <summary>
		/// Handles the Change email address button click event.  Attempts to 
		/// obtain the user's profile from the username entered.  If retrieved
		/// and password is confirmed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeEmailAddressBtn_Click(object sender, EventArgs e)
		{
            forgotPasswordPanel.Visible = false;
            changeEmailPanel.Visible = true;
            userPreferencePanel.Visible = false;
            deleteUserPanel.Visible = false;
            loginPanel.Visible = false;
            
            //TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
            this.Visible = true;
            //success = true;
	
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
		}

        /// <summary>
        /// Handles the change user preferences button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeUserPreferencesBtn_Click(object sender, EventArgs e)
        {
            TDUser user = TDSessionManager.Current.CurrentUser;
            bool sessionExtended = false;
            if (user.UserProfile.Properties[ProfileKeys.EXTENDED_SESSION].Value != null)
            {
               sessionExtended = Convert.ToBoolean(user.UserProfile.Properties[ProfileKeys.EXTENDED_SESSION].Value);
            }

            extendSessionTimeOutCheckBox.Checked = sessionExtended;
            
            forgotPasswordPanel.Visible = false;
            changeEmailPanel.Visible = false;
            deleteUserPanel.Visible = false;
            userPreferencePanel.Visible = true;
            loginPanel.Visible = false;

            //TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
            this.Visible = true;
            //success = true;

            if (ButtonClicked != null)
            {
                ButtonClicked(this, new System.EventArgs());
            }
        }

		/// <summary>
		/// Handles the Delete email address button click event.  Attempts to 
		/// obtain the user's profile from the username entered.  If retrieved
		/// and password is confirmed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteAccountBtn_Click(object sender, EventArgs e)
		{
            TDUser currentUser = TDSessionManager.Current.CurrentUser;
            if (currentUser.Username.Length == 0)
			{
				messageLabel.Text = GetResource("loginPanel.emailRequiredValidator");
				messageLabel.ForeColor = Color.Red;
				messageLabel.Visible = true;
			}
            else if (currentUser.Username.Length > 255)
			{
				messageLabel.Text = GetResource("loginPanel.emailValidator");
				messageLabel.ForeColor = Color.Red;
				messageLabel.Visible = true;
			}
            else if (currentUser.Password.Length != 0)  // password entered
			{
				if (Page.IsValid) // valid password and email
				{  
					deleteUser(); 
				}
			}
			else 
			{
				messageLabel.Text = GetResource("loginPanel.passwordRequiredValidator");
				messageLabel.ForeColor = Color.Red;
				messageLabel.Visible = true;
			}
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
        }
        
        /// <summary>
		/// Handles Logon Button Click Event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void logonButton_Click(object sender, EventArgs e)
		{
            emailTxtBox.Text = emailTxtBox.Text.Trim();
            Page.Validate();
			if(Username.Length == 0)
			{
				messageLabel.Text = GetResource("loginPanel.emailRequiredValidator");
				messageLabel.ForeColor = Color.Red;
				messageLabel.Visible = true;
			}
			else if(Username.Length > 255 || (!IsValidEmailAddress()))
			{
				messageLabel.Text = GetResource("loginPanel.emailValidator");
				messageLabel.ForeColor = Color.Red;
				messageLabel.Visible = true;
			}
            else if (Password.Length != 0)  // password entered
            {

                // valid password and email
                if (LoginUser())
                {
                    TDSessionManager.Current.SetOneUseKey(SessionKey.IndirectLocationPostBack, string.Empty);
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.LoginRegisterBack; // default one

                    this.Visible = false;
                }

            }
            else
            {
                messageLabel.Text = GetResource("loginPanel.passwordRequiredValidator");
                messageLabel.ForeColor = Color.Red;
                messageLabel.Visible = true;
            }
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
		}

        /// <summary>
		/// Handles Logout Button Click Event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void logoutButton_Click(object sender, EventArgs e)
        {
            //fire off logout event to any listening container class
            if (Logout != null)
            {
                Logout(this, e);
            }
        }

		/// <summary>
		/// Handles Cancel Button Click Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			emailTxtBox.Text = string.Empty;
			passwordTxtBox.Text = string.Empty;
			this.Visible = false;
		}

		/// <summary>
		/// Handles Cancel Button Click Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteUserConfirmCancelButton_Click(object sender, EventArgs e)
		{
            loginPanel.Visible = true;
			deleteUserPanel.Visible =false;
			changeEmailPanel.Visible =false;
            userPreferencePanel.Visible = false;
			this.Visible = true;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void deleteUserConfirmButton_Click(object sender, EventArgs e)
		{

            TDUser user = new TDUser();
			TDUser currentUser = TDSessionManager.Current.CurrentUser;
			//Load original user to obtain password etc.         
            if (user.FetchUser(currentUser.Username) == true)
            {
                user.Logon(currentUser.Password);
                user.DeleteUser(currentUser.Password);

                TDSessionManager.Current.CurrentUser = null;

                HttpContext.Current.Session["authenticated"] = false;
                //Clear Session
                Session.Abandon();

                //Redirect to Home
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.SessionAbandon;
            }		
		}


		/// <summary>
		/// Handles Cancel Button Click Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeEmailCancelButton_Click(object sender, EventArgs e)
		{
			loginPanel.Visible = true;
			deleteUserPanel.Visible =false;
			changeEmailPanel.Visible =false;
            userPreferencePanel.Visible = false;

			this.Visible = true;
		}

        /// <summary>
        /// Handles Cancel Button Click Event of forgotPassword Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forgotPasswordCancelButton_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
            deleteUserPanel.Visible = false;
            changeEmailPanel.Visible = false;
            userPreferencePanel.Visible = false;
            forgotPasswordPanel.Visible = false;

            this.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forgotPasswordConfirmButton_Click(object sender, EventArgs e)
        {
            string forgotPasswordEmail = HttpUtility.HtmlEncode(forgotPasswordEmailTxtBox.Text.Trim()); 

            if (forgotPasswordEmail == String.Empty)
            {
                forgotPasswordMessageLabel.Text = GetResource("loginPanel.emptyUsername");
                forgotPasswordMessageLabel.ForeColor = Color.Red;
                forgotPasswordMessageLabel.Visible = true;
            }
            else
            {
                //FIX: (KJ) Added validation for invalid email address
                if (!(new EmailAddress(forgotPasswordEmail).IsValid) || forgotPasswordEmail.Length > 255)
                {
                    forgotPasswordMessageLabel.Text = GetResource("loginPanel.emailValidator");
                    forgotPasswordMessageLabel.ForeColor = Color.Red;
                    forgotPasswordMessageLabel.Visible = true;
                }
                else
                {
                    // obtain users profile from supplied username
                    TDUser user = new TDUser();
                    if (user.FetchUser(forgotPasswordEmail) == false)
                    {
                        forgotPasswordMessageLabel.Text = GetResource("loginPanel.messageLabel.UserNotRecognised");
                        forgotPasswordMessageLabel.ForeColor = Color.Red;
                        forgotPasswordMessageLabel.Visible = true;
                        // reset email field
                        forgotPasswordEmailTxtBox.Text = string.Empty;
                    }
                    else
                    {
                        string subject = GetResource("LoginControl.EmailSubject");

                        //Check the password
                        string pwd = user.Password;

                        //disclaimer message - user should not reply to this email address
                        string disclaimer = GetResource("LoginControl.Disclaimer");

                        StringBuilder sb = new StringBuilder();
                        sb.Append(GetResource("LoginControl.emailBodyText")
                            + " " + pwd
                            + ".   " + disclaimer);
                        string bodyText = sb.ToString();

                        try
                        {
                            // create custom event and log it to send email 
                            CustomEmailEvent sendPasswordEvent = new CustomEmailEvent(forgotPasswordEmail,
                                bodyText, subject);
                            Logger.Write(sendPasswordEvent);

                            // inform the user that password sent
                            forgotPasswordMessageLabel.Text = GetResource("loginPanel.messageLabel.PasswordSent");
                            forgotPasswordMessageLabel.Visible = true;
                            forgotPasswordMessageLabel.ForeColor = Color.Black;

                            string email = "(" + forgotPasswordEmail + ")";
                            string tempText;

                            if (email.Length > CHARACTER_LIMIT)
                            {
                                tempText = string.Empty;
                                int remlen = CHARACTER_LIMIT;


                                for (int i = 0; i < email.Length; i += CHARACTER_LIMIT)
                                {
                                    tempText += email.Substring(i, remlen) + "<br>";

                                    remlen = email.Substring(i + remlen).Length;
                                    if (remlen > CHARACTER_LIMIT)
                                    {
                                        remlen = CHARACTER_LIMIT;
                                    }
                                }
                            }
                            else
                            {
                                tempText = email;
                            }
                            forgotPasswordEmailInMessageLabel.Text = tempText;
                            forgotPasswordEmailInMessageLabel.Visible = true;

                        }
                        catch (TDException tde)
                        {
                            // log exception and re-throw TDException
                            string msg = "Emailing of user password failed ";

                            OperationalEvent operationalEvent = new OperationalEvent
                                (TDEventCategory.Business, TDTraceLevel.Error, msg);
                            Logger.Write(operationalEvent);

                            throw new TDException(msg, tde, true, TDExceptionIdentifier.BTCSendPasswordFailed);
                        }

                    }
                }
            }
            if (ButtonClicked != null)
            {
                ButtonClicked(this, new System.EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void changeEmailConfirmButton_Click(object sender, EventArgs e)
		{
			bool addressOK = false;

            TDUser currentUser = TDSessionManager.Current.CurrentUser;
			TDUser user = new TDUser();
			TDUser changedUser = new TDUser();
            //Load original user to obtain password etc.         
            if (user.FetchUser(currentUser.Username) == false)           
			{
				messageLabel.Text = GetResource("loginPanel.messageLabel.unregistered");
				messageLabel.Visible = true;
				messageLabel.ForeColor = Color.Red;
			}
			else
			{
				user.Logon(currentUser.Password );
			}

			// ensure something typed in both boxes
			if(changeEmailAddressTextbox1.Text.Length == 0 || changeEmailAddressTextbox2.Text.Length == 0)
			{
				changeEmailMessageLabel.Text =  GetResource("changeEmailPanel.changeEmailNotEntered.Text");
			}
			// ensure fields match
			else if(changeEmailAddressTextbox1.Text != changeEmailAddressTextbox2.Text)
			{
				changeEmailMessageLabel.Text =  GetResource("changeEmailPanel.changeEmailNoMatch.Text");
			}
			// validate email address
			else if(!(new EmailAddress(changeEmailAddressTextbox1.Text).IsValid) || changeEmailAddressTextbox1.Text.Length > 255) 
			{
				changeEmailMessageLabel.Text = GetResource("loginPanel.emailValidator");
			}
			// ensure doesnt already exist
			else if (changedUser.FetchUser( changeEmailAddressTextbox1.Text ) == true)
			{
				changeEmailMessageLabel.Text = GetResource("loginPanel.messageLabel.username");
			}
			else
			{
				addressOK = true;
			}
			
			if (addressOK)
			{
				// Create a user using the info from session data
				TDSessionManager.Current.UnsavedUsername = changeEmailAddressTextbox1.Text;
				TDSessionManager.Current.UnsavedPassword = currentUser.Password;
				user.UserProfile.Username = TDSessionManager.Current.UnsavedUsername;
				user.Update();

				//clear user from temp storage in session after cs update
				TDSessionManager.Current.UnsavedPassword = string.Empty;
				TDSessionManager.Current.UnsavedUsername = string.Empty;
				TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
				this.Visible = false;


				// NOW DELETE OLD USER
				TDUser deletedUser = new TDUser();
                if (deletedUser.FetchUser(currentUser.Username) == true)           
				{
                    deletedUser.DeleteUser(currentUser.Password);
				}

               
                if(user.FetchUser(changeEmailAddressTextbox1.Text))
                {
                    if (user.Logon(currentUser.Password))
                    {
                        HttpContext.Current.Session["authenticated"] = true;
                        HttpContext.Current.Session[SessionKey.Username.ID] = changeEmailAddressTextbox1.Text;
                        TDSessionManager.Current.CurrentUser = user;

                    }
                }

				success = true;
				loginPanel.Visible = true;
				deleteUserPanel.Visible =false;
				changeEmailPanel.Visible =false;
			}
			else
			{
				// something wrong so issue errormessage
				changeEmailMessageLabel.ForeColor = Color.Red;
				changeEmailMessageLabel.Visible = true;
				

                loginPanel.Visible = false;
                deleteUserPanel.Visible = false;
                changeEmailPanel.Visible = true;
			}
		}
	
		/// <summary>
		/// Add client side event and its handler to the given text box.
		/// </summary>
		private void AddDefaultLoginAction()
		{
			UserExperienceEnhancementHelper.TakeDefaultLoginAction(emailTxtBox, this.Page);
			UserExperienceEnhancementHelper.TakeDefaultLoginAction(passwordTxtBox, this.Page);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void helpCustomControlLogin_Click(object sender, EventArgs e)
		{
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void helpCustomControlLogin_Click(object sender, ImageClickEventArgs e)
		{
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
		}
		private void helpCustomControlChangeEmail_Click(object sender, EventArgs e)
		{
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void helpCustomControlChangeEmail_Click(object sender, ImageClickEventArgs e)
		{
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
        }

        /// <summary>
        /// Event handler for cancle button click event in user preference panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveUserPreferencesCancelButton_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
            deleteUserPanel.Visible = false;
            userPreferencePanel.Visible = false;
            changeEmailPanel.Visible = false;
            this.Visible = true;
        }

        /// <summary>
        /// Event handler for confirm button click event in user preference panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveUserPreferencesConfirmButton_Click(object sender, EventArgs e)
        {
            TDUser currentUser = TDSessionManager.Current.CurrentUser;

            if (currentUser != null)
            {
                currentUser.UserProfile.Properties[ProfileKeys.EXTENDED_SESSION].Value = extendSessionTimeOutCheckBox.Checked;

                if (extendSessionTimeOutCheckBox.Checked)
                {
                    if (Session.Timeout != GetDefaultSessionTimeOut() * 10)
                    {
                        Session.Timeout = GetDefaultSessionTimeOut() * 10;
                    }
                }

                // Persist user profile
                currentUser.Update();
            }
        }

        #endregion
    }
}



