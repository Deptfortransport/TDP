// *********************************************** 
// NAME                 : RegisterControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 21/01/2004 
// DESCRIPTION          : TDUserControl class providing  
//                        registration facility
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RegisterControl.ascx.cs-arc  $ 
//
//   Rev 1.7   Jul 20 2010 12:05:58   mmodi
//Updated to perform default Register action (instead of using Login action)
//Resolution for 5010: Cannot submit new user registration details using "Enter" key
//
//   Rev 1.6   Jan 20 2010 10:46:10   pghumra
//Fixed code to cater for users trying to register with an email address followed by trailing spaces.
//Resolution for 5369: TDP login/register functionality cannot deal with email addresses with trailing spaces in.
//
//   Rev 1.5   Dec 15 2008 09:54:02   apatel
//XHTML compliance changes 
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   May 27 2008 11:44:16   mmodi
//Corrected logging in success message logic and general tidy up
//Resolution for 5006: Login: Success message shown when invalid details entered
//
//   Rev 1.3   Apr 09 2008 13:14:02   scraddock
//Steve B: Fixed bug with registering the same address twice.
//Resolution for 4850: Trying to register with an already registered e-mail address causes excpetion
//
// Rev DevFactory Apr 9 2008 sbarker
// Solved bug where registering with an existing email causes yellow screen
//
//   Rev 1.2   Mar 31 2008 13:22:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:12   mturner
//Initial revision.
//
//   Rev 1.20   Feb 23 2006 19:17:02   build
//Automatically merged from branch for stream3129
//
//   Rev 1.19.1.0   Jan 10 2006 15:26:56   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.19   Nov 07 2005 15:19:30   AViitanen
//TD089, ES020: HTML button changes
//
//   Rev 1.18   Nov 04 2005 12:24:14   ralonso
//Manual merge of stream2816
//
//   Rev 1.17   Oct 27 2005 11:14:50   kjosling
//Forced password validation. NOTE: Validation for this control is to be re-factored. See document ES00601 for more details
//Resolution for 2918: No password validation for Registration
//
//   Rev 1.16   Oct 11 2005 20:23:48   kjosling
//Removed redundant method. 
//Resolution for 2860: DN79:  UEE Enhancements - No error messages being shown when inputting an invalid email and password and submitting for Register/Login control
//
//   Rev 1.15   Oct 11 2005 20:22:40   kjosling
//Reworked validation routine to iron out missing validation. 
//Resolution for 2860: DN79:  UEE Enhancements - No error messages being shown when inputting an invalid email and password and submitting for Register/Login control
//
//   Rev 1.14   Oct 05 2005 16:44:16   kjosling
//Fix applied for IR 2828
//Resolution for 2828: CCN219 - Global Login Control persistance
//   Rev 1.13.1.1   Oct 17 2005 18:32:32   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.13   Sep 29 2005 12:49:48   build
//Automatically merged from branch for stream2673
//
//   Rev 1.12.1.1   Sep 16 2005 14:36:38   NMoorhouse
//DN079 UEE, TD092 Login and register - changes made during UT
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.12.1.0   Sep 13 2005 16:58:24   NMoorhouse
//DN079 UEE, TD092 Login and Register enhancements
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.12   Mar 24 2005 14:24:28   rgeraghty
//Changes made to widen controls for Home Page
//
//   Rev 1.11   May 25 2004 15:18:40   cshillan
//Moved insertion of session variable to indicate successful logon into correct location (i.e., within the "CONFIRM_BUTTON_CLICK")
//
//   Rev 1.10   Apr 28 2004 16:20:06   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.9   Apr 05 2004 15:32:32   ESevern
//Added logged in event when registered for correct display of login/favourite controls
//Resolution for 719: Login/Register link visibility error
//
//   Rev 1.8   Apr 05 2004 11:51:42   ESevern
//DEL5.2 QA changes - increased number of characters allowed before wrapping email address
//Resolution for 689: Additional DEL5.2 QA Changes for Login/Registration
//
//   Rev 1.7   Mar 26 2004 11:34:18   asinclair
//Added extra help label for Del 5.2
//
//   Rev 1.6   Mar 23 2004 12:12:40   ESevern
//QA changes
//
//   Rev 1.5   Mar 12 2004 18:01:18   asinclair
//Added new font for emailaddresslabel
//
//   Rev 1.4   Mar 04 2004 17:46:28   esevern
//removed password check if user already registered
//
//   Rev 1.3   Feb 24 2004 17:08:14   esevern
//added new message when duplicate email/password entered when registering
//
//   Rev 1.2   Feb 13 2004 13:29:22   esevern
//DEL 5.2 seperation of login and register
//
//   Rev 1.1   Feb 12 2004 15:04:40   esevern
//DEL5.2 - seperation of login and registration
//
//   Rev 1.0   Jan 21 2004 15:19:14   esevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.Logging;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Web.Events;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Resource;
	using Logger = System.Diagnostics.Trace;

	/// <summary>
	/// Handles user registration.  Creates new user profiles and logs in the new user 
	/// setting Auth and Profile Tickets. 
	/// </summary>
	public partial  class RegisterControl : TDUserControl
    {
        #region Variables
        protected System.Web.UI.WebControls.Panel confirmationPanel;
		protected TDUser user = null;
		public const int CHARACTER_LIMIT = 50;
		public event System.EventHandler ButtonClicked;
        #endregion

        #region Page_Load
        /// <summary>
		/// Sets error message text for validators
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            registerLogoutPanel.Visible = false;

			//confirmation ok button should not be visible
			if(!Page.IsPostBack) 
			{
				messageLabel.Visible = false;

				emailValidator.ErrorMessage = GetResource("loginPanel.emailValidator");

				passwordValidator.ErrorMessage = GetResource("loginPanel.passwordValidator");
				
				pwordConfirmationValidator.ErrorMessage = GetResource("loginPanel.passwordValidator");

            }

            #region Text
            registerLabel.Text = GetResource("registerPanel.registerLabel");
			emailLabel.Text = GetResource("registerPanel.emailLabel");
			pWordLabel.Text = GetResource("registerPanel.pWordLabel");
			passwordInfoLabel.Text = GetResource("registerPanel.passwordInfoLabel");
			pWordConfirmationLabel.Text = GetResource("registerPanel.pWordConfirmationLabel");
			
			registerButton.Text = GetResource("registerPanel.registerButton.Text");
            cancelButton.Text = GetResource("registerPanel.cancelButton.Text");
			
			registerTitleLabel.Text = GetResource("registerLogoutPanel.registerTitleLabel");
			emailEnteredLabel.Text = GetResource("registerLogoutPanel.emailEnteredLabel");
			confirmCancelLabel.Text = GetResource("registerLogoutPanel.confirmCancelLabel");
			
			confirmCancelButton.Text = GetResource("registerLogoutPanel.confirmCancelButton.Text");
			confirmButton.Text = GetResource("registerLogoutPanel.confirmButton.Text");
            #endregion

            // Adding script to the textbox
			AddDefaultLoginAction();
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		private void ExtraWiringEvents()
		{
			registerButton.Click += new EventHandler(this.registerButton_Click);
            cancelButton.Click += new EventHandler(this.cancelButton_Click);
            confirmCancelButton.Click += new EventHandler(this.confirmCancelButton_Click);
			confirmButton.Click += new EventHandler(this.confirmButton_Click);

            registerConfirmHelpLabel.CloseButton.Click += new ImageClickEventHandler(helpCustomControlClose_Click);
            registerHelpLabel.CloseButton.Click += new ImageClickEventHandler(helpCustomControlClose_Click);
		}
		#endregion

        #region Public Properties

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
		/// Read only property, returning the confirmation password as entered by the user.
		/// </summary>
		public string ConfirmPassword
		{
			get
			{
				return HttpUtility.HtmlEncode(confirmationTxtBox.Text.Trim());
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

        #endregion

        #region Event handlers

        /// <summary>
		/// Performs number of checks: if the username and password are already stored, ask the
		/// user if they intended to logon.  If not, they should enter a different email address
		/// and password.
		/// if the username is not already stored, it and the password
		/// entered are stored (will then wait for user confirmation before persisting the new 
		/// user profile).  
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.Web.UI.ImageClickEventArgs</param>
		private void registerButton_Click(object sender, EventArgs e)
		{
            emailTxtBox.Text = emailTxtBox.Text.Trim();
            Page.Validate();
            messageLabel.Text = string.Empty;
			messageLabel.Visible = false;
		
			//Re-Written code here to validate username first and to add validation for invalid e-mail addresses
			if(Username.Length == 0)
			{
				messageLabel.Text = GetResource("loginPanel.emailRequiredValidator");
				messageLabel.ForeColor = Color.Red;
				messageLabel.Visible = true;
			}
			else if(!(new EmailAddress(Username).IsValid) || Username.Length > 255)
			{
				messageLabel.Text = GetResource("loginPanel.emailValidator");
				messageLabel.ForeColor = Color.Red;
				messageLabel.Visible = true;
			}
			// password, confirm password and username entered
			else if(Password.Length !=0 && ConfirmPassword.Length !=0 && Username.Length !=0)  
			{
				//Hack. Check password against regular expression because the validators are not working 
				System.Text.RegularExpressions.Regex expression = new System.Text.RegularExpressions.Regex(passwordValidator.ValidationExpression);
				if(!expression.IsMatch(Password))
				{
					messageLabel.Text = passwordValidator.ErrorMessage;
                    if (string.IsNullOrEmpty(messageLabel.Text))
                    {
                        messageLabel.Text = GetResource("registerPanel.messageLabel.passwordError");
                    }
					messageLabel.ForeColor = Color.Red;
					messageLabel.Visible = true;
				}
                // check password confirmation
                else if (!Password.Equals(ConfirmPassword))
                {
                    // reset password text boxes and advise user to re-enter 
                    confirmationTxtBox.Text = string.Empty;
                    passwordTxtBox.Text = string.Empty;
                    messageLabel.Text = GetResource("registerPanel.messageLabel.passwordError");
                    messageLabel.ForeColor = Color.Red;
                    messageLabel.Visible = true;
                }
                else if (Page.IsValid)// valid password and email 
                {
                    // Try to find user in profile system
                    user = new TDUser();

                    // if no user found for these details, create a new user profile
                    if (user.FetchUser(Username) == false)
                    {
                        try
                        {
                            //Now we know that the details are OK,
                            //show the correct controls:
                            registerLogoutPanel.Visible = true;
                            registerPanel.Visible = false;


                            // store TDUser details in session for retrieval later
                            TDSessionManager.Current.CurrentUser = user;

                            TDSessionManager.Current.UnsavedUsername = Username;
                            TDSessionManager.Current.UnsavedPassword = Password;
                            DisplayConfirmation();
                        }
                        catch (Exception exp)
                        {
                            //log and rethrow unexpected exception 
                            string msg = "Error occurred attempting to register user with username: " + Username;

                            OperationalEvent operationalEvent = new OperationalEvent
                                (TDEventCategory.Business, TDTraceLevel.Error, msg);
                            Logger.Write(operationalEvent);

                            throw new TDException(msg, exp, true, TDExceptionIdentifier.BTCRegisterUserFailed);
                        }
                    }
                    else // profile found for registration details entered 
                    {
                        messageLabel.Text = GetResource("registerPanel.messageLabel.duplicateUser");
                        messageLabel.ForeColor = Color.Red;
                        messageLabel.Visible = true;
                    }
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
		/// Handler for user confirmation of registration details.  Persists the user profile.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.Web.UI.ImageClickEventArgs</param>		
		private void confirmButton_Click(object sender, EventArgs e)
		{
			// Create a user using the info from session data
			TDUser user = new TDUser();
			user.CreateUser( TDSessionManager.Current.UnsavedUsername, TDSessionManager.Current.UnsavedPassword );

			// perform the update to persist the profile
			user.Update();

			// Indicate we have successfully logged on
			HttpContext.Current.Session["authenticated"] = true;
			HttpContext.Current.Session[SessionKey.Username.ID] = user.Username;

			// log login event
			LoginEvent le = new LoginEvent(TDSessionManager.Current.Session.SessionID);
			Logger.Write(le);

			//clear user from temp storage in session after cs update
			TDSessionManager.Current.UnsavedPassword = string.Empty;
			TDSessionManager.Current.UnsavedUsername = string.Empty;

			TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;

			this.Visible = false;
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
        }
        
		private void confirmCancelButton_Click(object sender, EventArgs e)
		{
			emailTxtBox.Text = string.Empty;
			TDSessionManager.Current.UnsavedPassword = string.Empty;
			TDSessionManager.Current.UnsavedUsername = string.Empty;
			emailAddressLabel.Text = string.Empty;
            registerPanel.Visible = true;

			//this.Visible = false;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{

			emailTxtBox.Text = string.Empty;
			passwordTxtBox.Text = string.Empty;
			confirmationTxtBox.Text = string.Empty;
			messageLabel.Text = string.Empty;

            TDSessionManager.Current.UnsavedPassword = string.Empty;
            TDSessionManager.Current.UnsavedUsername = string.Empty;
            
			//this.Visible = false;
		
		}

		/// <summary>
		/// Add client side event and its handler to the given text box.
		/// </summary>
		private void AddDefaultLoginAction()
		{
			UserExperienceEnhancementHelper.TakeDefaultRegisterAction(emailTxtBox, this.Page);
			UserExperienceEnhancementHelper.TakeDefaultRegisterAction(passwordTxtBox, this.Page);
			UserExperienceEnhancementHelper.TakeDefaultRegisterAction(confirmationTxtBox, this.Page);
		}

		private void helpCustomControlConfirm_Click(object sender, EventArgs e)
		{
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
		}

		private void helpCustomControlClose_Click(object sender, ImageClickEventArgs e)
		{
			if(ButtonClicked != null)
			{
				ButtonClicked(this, new System.EventArgs());
			}
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Private method to display registration confirmation
        /// elements of the logoutPanel.
        /// </summary>
        /// <param name="newUser"></param>
        private void DisplayConfirmation()
        {
            registerPanel.Visible = false;
            confirmButton.Visible = true;
            confirmCancelButton.Visible = true;
            emailEnteredLabel.Visible = true;
            emailAddressLabel.Visible = true;
            confirmCancelLabel.Visible = true;
            registerLogoutPanel.Visible = true;

            string email = TDSessionManager.Current.UnsavedUsername;

            if (email.Length > CHARACTER_LIMIT)
            {
                emailAddressLabel.Text = string.Empty;
                int remlen = CHARACTER_LIMIT;


                for (int i = 0; i < email.Length; i += CHARACTER_LIMIT)
                {
                    emailAddressLabel.Text += email.Substring(i, remlen) + "<br>";

                    remlen = email.Substring(i + remlen).Length;
                    if (remlen > CHARACTER_LIMIT)
                    {
                        remlen = CHARACTER_LIMIT;
                    }

                }
            }
            else
            {
                emailAddressLabel.Text = email;
            }
        }

        /// <summary>
        /// Called when the user is not logged in.  All
        /// controls required to permit the user to login
        /// are visible.  Logout button is hidden. 
        /// </summary>
        public void DisplayLogin()
        {
            registerLogoutPanel.Visible = false;
            registerPanel.Visible = true;
        }

        #endregion
    }
}
