// *********************************************** 
// NAME                 : PrintablePageInfoControl.ascx.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 23/03/2005
// DESCRIPTION          : Displays basic information on printable pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PrintablePageInfoControl.ascx.cs-arc  $
//
//   Rev 1.3   Dec 17 2008 11:39:56   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:22:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:02   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:17:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:26:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Mar 29 2005 15:21:36   tmollart
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for PrintablePageInfoControl.
	/// </summary>
	public partial class PrintablePageInfoControl : TDUserControl
	{
		//Controls

		#region Event Handlers

		protected void Page_Load(object sender, System.EventArgs e)
		{
			labelDateTitle.Text = GetResource("PrintableJourneyMap.labelDateTimeTitle");
			labelUsernameTitle.Text = GetResource("PrintableJourneyMap.labelUsernameTitle");
			labelJourneyReferenceNumberTitle.Text = GetResource("PrintableJourneySummary.labelReferenceNumberTitle");
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

		#endregion

		#region Public Properties

		/// <summary>
		/// Write only. Set displayed date.
		/// </summary>
		public string Date
		{
			set{labelDate.Text = value;}
		}

		/// <summary>
		/// Write only. Set displayed username.
		/// </summary>
		public string UserName
		{
			set{labelUsername.Text = value;}
		}

		/// <summary>
		/// Write only. Set displayed journey reference number;
		/// </summary>
		public string JourneyReferenceNumber
		{
			set{labelJourneyReferenceNumber.Text = value;}
		}

		/// <summary>
		/// Write only. Controls visibility of the username controls.
		/// </summary>
		public bool UserNameVisible
		{
			set
			{
				labelUsernameTitle.Visible = value;
				labelUsername.Visible = value;
			}
		}

		/// <summary>
		/// Write only. Controls visibility of the journey reference 
		/// number title and actual label.
		/// </summary>
		public bool JourneyReferenceNumberVisible
		{
			set
			{
				labelJourneyReferenceNumberTitle.Visible = value;
				labelJourneyReferenceNumber.Visible = value;
			}
		}
																	 
		#endregion
	}
}
