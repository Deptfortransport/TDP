// *********************************************** 
// NAME                 : AmendSaveSendMessageControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 22/10/2003 
// DESCRIPTION          : Displays a message label for AmendSendSave 
//							collection of controls
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendMessageControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:19:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:10   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:20   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:23:26   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Nov 25 2003 11:03:02   passuied
//No change.
//Resolution for 347: Strange behaviour on login control
//
//   Rev 1.0   Oct 22 2003 16:11:54   esevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///	Contains a label for the display of confirmation messages for the 
	///	AmendSendSave controls, for example, when a user has saved a favourite
	///	journey or emailed their password.
	/// </summary>
	public partial  class AmendSaveSendMessageControl : TDUserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		/// <summary>
		/// Read only property, returing confirmation message label
		/// </summary>
		public Label MessageLabel 
		{
			get 
			{
				return confirmationLabel;
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
