// *********************************************** 
// NAME                 : TravelDetailsSaveOptionControl.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 08/10/2003 
// DESCRIPTION          : TDUserControl providing save option 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TravelDetailsSaveOptionControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:23:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:24   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:17:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.1   Jan 30 2006 14:41:26   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2.1.0   Jan 10 2006 15:27:52   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Oct 24 2003 11:42:24   passuied
//Build 2 for ambiguity page first working version
//
//   Rev 1.1   Oct 15 2003 14:32:52   esevern
//sets checkbox false on first page load
//
//   Rev 1.0   Oct 09 2003 09:46:26   esevern
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
	///	Permits the user to opt to save their travel detail information
	/// </summary>
	public partial  class TravelDetailsSaveOptionControl : TDUserControl
	{


		/// <summary>
		/// Sets all instruction/label/error message text in the 
		/// current language
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// save details
			saveCheckbox.Text = Global.tdResourceManager.GetString("TravelDetailsSaveOptionControl.saveCheckbox", TDCultureInfo.CurrentUICulture);
		}

		public bool SaveDetails
		{
			get
			{
				return saveCheckbox.Checked;
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
