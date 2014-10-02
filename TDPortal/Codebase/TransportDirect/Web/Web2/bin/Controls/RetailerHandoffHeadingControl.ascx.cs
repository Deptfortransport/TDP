// *********************************************** 
// NAME                 : RetailerHandoffHeadingControl.aspx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 28/02/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RetailerHandoffHeadingControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:22:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:26   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:17:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:27:04   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Mar 21 2005 17:01:00   jgeorge
//FxCop changes
//
//   Rev 1.0   Mar 04 2005 11:51:22   jgeorge
//Initial revision.

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
	///	Displays relevant information at the top of the retailer handoff page
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RetailerHandoffHeadingControl : TDUserControl, ILanguageHandlerIndependent
	{

		#region Private members and constants

		private const string resourceKeyTitle = "RetailerHandoffHeadingControl.labelDiscountsTitle";
		private const string resourceKeyRailcardHeading = "JourneyFareHeadingControl.railCardLabel";
		private const string resourceKeyCoachcardHeading = "JourneyFareHeadingControl.coachCardLabel";

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets local resource manager.
		/// </summary>
		public RetailerHandoffHeadingControl()
		{
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Page event handlers

		/// <summary>
		/// Handler for the load event. Sets the text in static controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set up label text
			labelDiscountsTitle.Text = GetResource(resourceKeyTitle );
			labelRailcard.Text = GetResource(resourceKeyRailcardHeading );
			labelCoachcard.Text = GetResource(resourceKeyCoachcardHeading );
		}

		#endregion

		#region Public properties

		/// Display name of specified railcard. Set to string.Empty to display "None"
		/// </summary>
		public string RailCardName 
		{
			set { railcardName.Text = value; }
			get { return railcardName.Text; }
		}

		/// <summary>
		/// Display name of specified coachcard. Set to string.Empty to display "None"
		/// </summary>
		public string CoachCardName 
		{
			set { coachcardName.Text = value; }
			get { return coachcardName.Text; }
		}

		#endregion

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
