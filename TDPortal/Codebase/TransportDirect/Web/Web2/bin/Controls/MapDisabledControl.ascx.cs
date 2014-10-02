// *********************************************** 
// NAME                 : MapDisabledControl.ascx
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 17/02/2004 
// DESCRIPTION  : Disabled Map Web user control. Used to display a
// "greyed out" map control when location to display is unresolved.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapDisabledControl.ascx.cs-arc  $: 
//
//   Rev 1.2   Mar 31 2008 13:21:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:20   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.1   Jan 30 2006 14:41:14   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2.1.0   Jan 10 2006 15:26:12   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Mar 03 2004 13:40:50   COwczarek
//Change class name
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.1   Feb 20 2004 09:46:10   asinclair
//Del 5.2 - Disabled map control
//
//   Rev 1.0   Feb 17 2004 10:28:38   asinclair
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
    ///	Disabled Map Web user control. Used to display a
    /// "greyed out" map control when location to display is unresolved.
	/// </summary>
	public partial  class MapDisabledControl : TDUserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			labelMapAppear.Text = Global.tdResourceManager.GetString(
				"MapDisabledControl.labelMapAppear", TDCultureInfo.CurrentUICulture);
			
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
