// *********************************************** 
// NAME                 : FeedbackClaimControl.aspx.cs 
// AUTHOR               : Halim Ahad
// DATE CREATED         : 15/09/2003 
// DESCRIPTION          : Control to allow user to
//                        select feedback/claim/survey feedback type 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FeedbackClaimControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:20:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:36   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:32   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:24:20   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Sep 16 2003 10:17:28   hahad
//Added PVCS header code to controls

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using TransportDirect.Web.Support;

	/// <summary>
	///		Summary description for FeedbackClaimControl.
	/// </summary>
	public partial  class FeedbackClaimControl : TDUserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				feedbackClaimSelection.Items.Add("I would like to give feedback");
				feedbackClaimSelection.Items.Add("I would like to make a claim");
				feedbackClaimSelection.Items.Add("I would like to complete a survey form");
				initialSelectionLabel.Text = "Please select one of the options below:";

			}
			// Put user code to initialize the page here
		}

		public string FeedBackClaimText
		{
			get
			{
				return feedbackClaimSelection.SelectedItem.Text;
			}
			set
			{
				feedbackClaimSelection.SelectedItem.Text = value;
			}
		}

		public int FeedBackClaimIndex
		{
			get
			{
				return feedbackClaimSelection.SelectedIndex;
			}
			set
			{
				feedbackClaimSelection.SelectedIndex = value;
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

		}
		#endregion
	}
}
