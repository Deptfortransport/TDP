// *********************************************** 
// NAME                 : AmbiguousDropDownList.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 13/10/2003 
// DESCRIPTION  : Ambiguous drop down list
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmbiguousDropdownList.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:19:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:52   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:18   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:23:10   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Nov 18 2003 10:44:00   passuied
//changes to hopefully pass code review
//
//   Rev 1.0   Oct 13 2003 12:24:14   passuied
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
	///		Ambiguous drop down list
	/// </summary>
	public partial  class AmbiguousDropdownList : TDUserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Get property. Returns the DropDownList control
		/// </summary>
		public DropDownList Control
		{
			get
			{
				return list;
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
