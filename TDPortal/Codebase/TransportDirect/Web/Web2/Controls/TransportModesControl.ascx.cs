// ************************************************************************************************ 
// NAME                 : TransportModesControl.ascx.cs 
// AUTHOR               : Tolu Olomolaiye 
// DATE CREATED         : 24/08/2005 
// DESCRIPTION			: Shows an icon for each mode of transport required to complete a journey
// ************************************************************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TransportModesControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:18   mturner
//Initial revision.
//
//   Rev 1.8   Feb 23 2006 19:17:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.7.1.0   Jan 10 2006 15:27:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   Oct 25 2005 12:56:46   tolomolaiye
//Ammended literal separator
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Oct 18 2005 14:52:40   tolomolaiye
//Updated with fxcop comments
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 15 2005 14:41:52   jbroome
//Added "+" seperator between mode icons
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 05 2005 09:44:26   tolomolaiye
//Updates following code review and fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Sep 16 2005 16:12:38   tolomolaiye
//Check in for review
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Sep 14 2005 11:26:00   tolomolaiye
//Work in progress
//
//   Rev 1.1   Sep 05 2005 17:51:22   tolomolaiye
//Check-in for review
//
//   Rev 1.0   Sep 02 2005 10:54:58   tolomolaiye
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
	using System.Web;
	using System.Web.UI.WebControls;
	using TransportDirect.JourneyPlanning.CJPInterface;

	/// <summary>
	///	Shows an icon for each mode of transport.
	/// </summary>
	public partial class TransportModesControl : TDPrintableUserControl 
	{
		private ModeType[] transportModeTypes;

		/// <summary>
		/// Constructor sets local resource manager
		/// </summary>
		public TransportModesControl()
		{
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
		}

		/// <summary>
		/// Default Page_Load method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		/// <summary>
		/// PreRender method sets data source and binds repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPreRender(object sender, EventArgs e)
		{
			repeaterTransportModesControl.DataSource = transportModeTypes;
			repeaterTransportModesControl.DataBind();
		}


		/// <summary>
		/// Read/write property
		/// Array of mode types that will be
		/// bound to the repeater
		/// </summary>
		public ModeType[] DataSource
		{
			get	{return transportModeTypes;}
			set { transportModeTypes = value; }
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
			this.PreRender += new EventHandler(this.OnPreRender);
		}
		#endregion

	}
}
