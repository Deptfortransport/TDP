// *********************************************** 
// NAME                 : FeedbackDetailsEmailControl.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 04/01/2007
// DESCRIPTION          : Control to allow user to enter feedback details and emaail address
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackDetailsEmailControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 16 2009 13:27:20   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:44   mturner
//Initial revision.
//
//   Rev 1.2   Jan 18 2007 14:50:50   mmodi
//Added code to set email address value if user logged on
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.1   Jan 12 2007 14:13:44   mmodi
//Updated code as part of development
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 08 2007 10:22:46   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///		Summary description for FeedbackDetailsEmailControl.
	/// </summary>
	public partial class FeedbackDetailsEmailControl : TDUserControl
	{
		#region Controls


		#endregion

		#region Page_Load, Page_PreRender

		/// <summary>
		/// Page_Load
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DetailsLabel.Text = GetResource("FeedbackDetailsEmailControl.DetailsLabel");
			FillDetailsLabel.Text = GetResource("FeedbackDetailsEmailControl.FillDetailsLabel");
			EmailAddressLabel.Text = GetResource("FeedbackDetailsEmailControl.EmailAddressLabel");
            RegularExpressionValidator1.ErrorMessage = GetResource("FeedbackDetailsEmailControl.EmailValidator");
		}

		/// <summary>
		/// Page PreRender
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{			
			// If user is logged on, then display their email address in the textbox
			if ((TDSessionManager.Current.Authenticated) && (EmailAddress == string.Empty))
				EmailAddress = TDSessionManager.Current.CurrentUser.Username.ToString().Trim();			
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Get and Set properties of CommentsTextBox
		/// </summary>
		public string UserComments
		{
			get { return HttpUtility.HtmlEncode(CommentsTextBox.Text.Trim());  }
			set	{ CommentsTextBox.Text = HttpUtility.HtmlEncode(value);	}

		}

		/// <summary>
		/// Get and Set properties of EmailTextBox
		/// </summary>
		public string EmailAddress
		{
			get	{ return HttpUtility.HtmlEncode(EmailTextBox.Text.Trim());  }
			set	{ EmailTextBox.Text = HttpUtility.HtmlEncode(value); }
		}

		/// <summary>
		/// Get/Sets the Details text box error visibility
		/// </summary>
		public bool UserCommentsErrorVisible
		{
			get{ return FillDetailsLabel.Visible; }
			set{ FillDetailsLabel.Visible = value; }
		}

		/// <summary>
		/// Initialises the controls to their default state
		/// </summary>
		public void Initialise()
		{
			UserComments = string.Empty;
			EmailAddress = string.Empty;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
