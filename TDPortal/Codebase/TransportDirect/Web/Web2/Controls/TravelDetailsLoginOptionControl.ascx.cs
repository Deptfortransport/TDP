// *********************************************** 
// NAME                 : TravelDetailsLoginOptionControl.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 08/10/2003 
// DESCRIPTION          : TDUserControl providing user instructions 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TravelDetailsLoginOptionControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:23:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:24   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:17:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.1   Jan 30 2006 14:41:26   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0.1.0   Jan 10 2006 15:27:52   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Oct 09 2003 09:46:40   esevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///	Instructs the user to login or register if they wish to save
	///	travel detail information
	/// </summary>
	public partial  class TravelDetailsLoginOptionControl : TDUserControl
	{

		/// <summary>
		/// Sets all instruction/label/error message text in the 
		/// current language
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//login register 
			loginRegisterLabel.Text = Global.tdResourceManager.GetString("TravelDetailsLoginOptionControl.loginRegisterLabel", TDCultureInfo.CurrentUICulture);
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
