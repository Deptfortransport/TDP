// *********************************************** 
// NAME                 : FeebackCommentsControl.aspx.cs 
// AUTHOR               : Esther Severn 
// DATE CREATED         : 03/07/2003 
// DESCRIPTION			: A custom user control to 
// allow users to enter text 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackCommentsControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:20:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:40   mturner
//Initial revision.
//
//   Rev 1.14   Feb 23 2006 19:16:32   build
//Automatically merged from branch for stream3129
//
//   Rev 1.13.1.1   Jan 30 2006 14:41:04   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.13.1.0   Jan 10 2006 15:24:22   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.13   Aug 14 2003 12:28:18   hahad
//Amended Documentation Comments
//
//   Rev 1.12   Aug 13 2003 14:35:18   hahad
//Secure Coding practise implemented
//
//   Rev 1.11   Aug 07 2003 12:59:12   hahad
//Further Commenting
//
//   Rev 1.10   Aug 07 2003 11:22:14   hahad
//Documentation Comments added
//
//   Rev 1.9   Aug 05 2003 11:25:46   hahad
//Comments Updated
//
//   Rev 1.8   Aug 05 2003 11:20:34   hahad
//Comments Updated
//
//   Rev 1.7   Jul 24 2003 15:53:40   AWindley
//added a RequiredFieldValidator
//
//   Rev 1.6   Jul 24 2003 14:10:54   hahad
//Changed txtBoxComments & commetsLabel in FeedbackCommentsControl from protect to Private (FXcop)
//
//   Rev 1.5   Jul 24 2003 13:27:44   hahad
//Member names are Pascal-cased, Instead of member name 'label', use 'Label'.Instead of member name 'comments', use 'Comments' (FXcop)
//
//   Rev 1.4   Jul 23 2003 13:04:48   AWindley
//updated label to be language sensitive
//
//   Rev 1.3   Jul 11 2003 12:04:10   ESevern
//now extends TDUserControl not TDControl

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	using TransportDirect.Web.Support;

	/// <summary>
	/// User Control that gains feedback via a textbox, the textbox can not be empty or exceed 256 chars in length
	/// </summary>
	public partial  class FeedbackCommentsControl : TDUserControl
	{
		//Textbox, Validator & Label for CustomControl

		/// <summary>
		/// Page Load event for FeedbackCommentsControl, sets language context
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//Label Text is Language sensitive - this is retrieved from the resource manager after a lookup of FeedbackCommentsControl.commentsLabel
			commentsLabel.Text = Global.tdResourceManager.GetString("FeedbackCommentsControl.commentsLabel", TDCultureInfo.CurrentUICulture);
			//Validator Text is Language sensitive - this is retrieved from the resource manager after a lookup of FeedbackCommentsControl.commentsReqdError
			commentsRequiredFieldValidator.ErrorMessage = Global.tdResourceManager.GetString("FeedbackCommentsControl.commentsReqdError", TDCultureInfo.CurrentUICulture);
			//Set Language sensitive error message for the Custom Validator
			commentsLengthValidator.ErrorMessage = Global.tdResourceManager.GetString("FeedbackCommentsControl.commentsLengthError", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Sets or returns the string contents of the comments
		/// text area
		/// </summary>
		/// 
		//get & set properties for txtBoxComments (return typr: Text)
		public string Comments 
		{
			get 
			{
				return txtBoxComments.Text;
			}
			set 
			{
				txtBoxComments.Text = value;
			}
		}

		/// <summary>
		/// Sets or returns the string contents of the comments
		/// label
		/// </summary>
		/// 
		//get & set properties for commentsLabel (return type: Text)
		public string Label 
		{
			get 
			{
				return commentsLabel.Text;
			}
			set 
			{
				commentsLabel.Text = value;
			}
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.commentsLengthValidator.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.commentsLengthValidator_ServerValidate);

		}
		#endregion

		//Custom Validator for the textbox which checks that the number of characters does not exceed 256 chars
		/// <summary>
		/// Custom Validator for Comment TextBox Length
		/// </summary>
		/// <param name="source"></param>
		/// <param name="args"></param>
		private void commentsLengthValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
			//Get length of comment
			int localCommentLength = Comments.Length;
			//fire validation if character count exceeds 256 chars
			if (localCommentLength > 256)
			{
				args.IsValid = false;
			}
				//All other selections from the DropDownList are valid
			else args.IsValid = true;
		
		}

	

	}
}
