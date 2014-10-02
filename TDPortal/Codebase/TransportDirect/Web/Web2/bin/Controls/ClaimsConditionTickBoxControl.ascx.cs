// *********************************************** 
// NAME                 : ClaimsConditionTickBoxControl.ascx.cs 
// AUTHOR               : Halim Ahad
// DATE CREATED         : 25/09/2003 
// DESCRIPTION          : Control allowing users to 
//                        submit Yes/No Claim Conditions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ClaimsConditionTickBoxControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:50   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:28   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:23:54   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Sep 30 2003 16:07:40   hahad
//Changed Tick boxes to RadioButtons
//
//   Rev 1.1   Sep 30 2003 10:41:08   hahad
//Initial Revision
//
//   Rev 1.0   Sep 29 2003 18:02:46   hahad
//Initial Revision
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;

	/// <summary>
	///		Summary description for ClaimsConditionTickBoxControl.
	/// </summary>
	public partial  class ClaimsConditionTickBoxControl : TDUserControl
	{
		

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				//Need to set lanaguage control here
				Condition1RadioButtonList.Items.Add("No");
				Condition1RadioButtonList.Items.Add("Yes");
				Condition2RadioButtonList.Items.Add("No");
				Condition2RadioButtonList.Items.Add("Yes");
				Condition3RadioButtonList.Items.Add("No");
				Condition3RadioButtonList.Items.Add("Yes");

				//Need to set lanaguage control here
				Condition1Label.Text = "I am submitting the claim within two weeks (14 days) of taking the journey";
				Condition2Label.Text = "I have proof that the incorrect information was shown on the Transport Direct website. e.g. a screen shot or a print out";
				Condition3Label.Text = "I have copies of my 'proof of travel' e.g. tickets, credit cards recipets for tickets, receipts for alternative travel taken";

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
