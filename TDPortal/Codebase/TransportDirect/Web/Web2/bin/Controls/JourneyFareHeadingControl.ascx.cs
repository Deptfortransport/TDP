// *********************************************** 
// NAME                 : JourneyFareHeadingControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 05/01/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyFareHeadingControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:36   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:16:50   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:25:42   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Mar 21 2005 16:58:36   jgeorge
//FxCop changes
//
//   Rev 1.2   Mar 04 2005 09:26:54   jgeorge
//Changed to use GetResource instead of resourceManager.GetString
//
//   Rev 1.1   Feb 22 2005 17:31:14   jgeorge
//Interim check-in
//
//   Rev 1.0   Jan 18 2005 11:44:38   jgeorge
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Displays the journey and discount card information. Note - viewstate is disabled, so 
	///	properties will have to be set every time
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyFareHeadingControl : TDUserControl, ILanguageHandlerIndependent
	{
		#region Control declarations


		#endregion

		#region Private members and constants

		private const string resourceKeyTitle = "JourneyFareHeadingControl.labelFaresTitle";
		private const string resourceKeyRailcardHeading = "JourneyFareHeadingControl.railCardLabel";
		private const string resourceKeyCoachcardHeading = "JourneyFareHeadingControl.coachCardLabel";
		private const string resourceKeyTo = "JourneyFareHeadingControl.Seperator";

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets local resource manager
		/// </summary>
		public JourneyFareHeadingControl()
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
			labelFaresTitle.Text = GetResource(resourceKeyTitle);
			labelRailcard.Text = GetResource(resourceKeyRailcardHeading);
			labelCoachcard.Text = GetResource(resourceKeyCoachcardHeading);
			labelTo.Text = GetResource(resourceKeyTo);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Name of the origin location
		/// </summary>
		public string OriginLocation
		{
			set { originName.Text = value; }
			get { return originName.Text; }
		}

		/// <summary>
		/// Name of the destination location
		/// </summary>
		public string DestinationLocation
		{
			set { destinationName.Text = value; }
			get { return destinationName.Text; }
		}

		/// <summary>
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
			this.EnableViewState = false;

		}
		#endregion
	}
}
