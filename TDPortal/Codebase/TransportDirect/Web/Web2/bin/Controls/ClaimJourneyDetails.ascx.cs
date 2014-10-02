// *********************************************** 
// NAME                 : ClaimJourneyDetails.aspx.cs 
// AUTHOR               : Andrew Toner & Halim Ahad
// DATE CREATED         : 01/10/2003 
// DESCRIPTION          : Control to validate user 
//						  Claim input for journey details
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ClaimJourneyDetails.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:48   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:16:28   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:23:52   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Mar 09 2004 12:44:14   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.3   Oct 07 2003 17:54:50   AToner
//Made sizing dynamic
//
//   Rev 1.2   Oct 05 2003 16:51:14   hahad
//Added Correct Namespace details

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.DataServices;


	/// <summary>
	///		Summary description for ClaimJourneyDetails.
	/// </summary>
	public partial  class ClaimJourneyDetails : TDUserControl
	{
		protected System.Web.UI.WebControls.Label labelMandatoryJourneyDetails;
		protected System.Web.UI.WebControls.Image imgJourneyDetails;
		protected System.Web.UI.WebControls.Label labelJourneyDetails;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}


		public string JourneyDetails
		{
			get { return textJourneyDetails.Text; }
			set { textJourneyDetails.Text = value; }
		}

		public bool ValidJourneyDetails
		{
			set	{ SetFieldStatus( "JourneyDetails", value ); }
		}

		private void SetFieldStatus( string FieldName, bool Valid )
		{
			try
			{
				System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)FindControl( "imgError" + FieldName );
				System.Web.UI.WebControls.TextBox text = (System.Web.UI.WebControls.TextBox)FindControl( "text" + FieldName );
				image.Visible = !Valid;
				if( Valid )
				{
					text.BorderStyle = BorderStyle.NotSet;
					text.BorderColor = Color.Empty;
				}
				else
				{
					text.BorderStyle = BorderStyle.Solid;
					text.BorderColor = Color.Red;
				}
			}
			catch
			{
				// TODO: Trying to set an unknown field
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
