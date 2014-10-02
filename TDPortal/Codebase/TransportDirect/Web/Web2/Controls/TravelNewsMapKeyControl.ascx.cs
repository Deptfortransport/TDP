// ************************************************************************************************ 
// NAME                 : TravelNewsMapKeyControl.ascx.cs 
// AUTHOR               : James Broome 
// DATE CREATED         : 22/09/2005
// DESCRIPTION			: Displays key for map symbols used in travel news maps
// ************************************************************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TravelNewsMapKeyControl.ascx.cs-arc  $
//
//   Rev 1.5   Oct 28 2010 13:24:34   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.4   Dec 19 2008 15:49:30   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 16 2008 16:30:24   mmodi
//Update to comply with screen design
//Resolution for 4780: Netscape-Travel news-Map key
//
//   Rev 1.2   Mar 31 2008 13:23:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:30   mturner
//Initial revision.
//
//   Rev 1.2   Mar 28 2006 11:09:06   build
//Automatically merged from branch for stream0024
//
//   Rev 1.1.1.0   Mar 03 2006 16:40:22   AViitanen
//Updated langstring keys for new map symbol images. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.1   Feb 23 2006 19:17:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:27:58   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Sep 23 2005 11:40:10   jbroome
//Initial revision.
//Resolution for 2793: Travel News Printable Map View

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///	Simple Html table structure containing text and images 
	///	for map key of symbols used in travel news maps.
	/// </summary>
	public partial class TravelNewsMapKeyControl : TDPrintableUserControl
	{


		private string hyperlinkUrl = string.Empty;

		/// <summary>
		/// Page load - sets up labels, images etc.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			labelKeyHeading.Text = GetResource("TravelNews.KeyOfSymbols"); 

			imageMajor.ImageUrl = GetResource("IncidentMapping.RoadIncident.ImageUrl");
			imageMajor.AlternateText = GetResource("IncidentMapping.RoadIncident.Alt");
			labelMajor.Text = GetResource("IncidentMapping.RoadIncident.Alt");

			imageNormal.ImageUrl = GetResource("IncidentMapping.Roadworks.ImageUrl");
			imageNormal.AlternateText = GetResource("IncidentMapping.Roadworks.Alt");
			labelNormal.Text = GetResource("IncidentMapping.Roadworks.Alt");

			imageFutureMajor.ImageUrl = GetResource("IncidentMapping.PTIncident.ImageUrl");
			imageFutureMajor.AlternateText = GetResource("IncidentMapping.PTIncident.Alt");
			labelFutureMajor.Text = GetResource("IncidentMapping.PTIncident.Alt");

			imageFutureNormal.ImageUrl = GetResource("IncidentMapping.PlannedRoadworks.ImageUrl");
			imageFutureNormal.AlternateText = GetResource("IncidentMapping.PlannedRoadworks.Alt");
			labelFutureNormal.Text = GetResource("IncidentMapping.PlannedRoadworks.Alt");
		}

		/// <summary>
		/// Read-write property used to set the url 
		/// property of the hyperlink
		/// </summary>
		public string HyperlinkUrl
		{
			get {return hyperlinkUrl;}
			set {hyperlinkUrl = value;}
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
