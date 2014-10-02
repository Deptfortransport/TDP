// *********************************************** 
// NAME                 : MapPlanJourneyLocationControl.ascx.cs 
// AUTHOR               : Atos Origin
// DATE CREATED         : 03/03/2004
// DESCRIPTION			:
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapPlanJourneyLocationControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:06   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 04 2008 08:50 apatel
//  CCN 0427 changed the layout of controls
//
//   Rev 1.0   Nov 08 2007 13:16:32   mturner
//Initial revision.
//
//   Rev 1.16   Feb 23 2006 19:16:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.15.1.0   Jan 10 2006 15:26:26   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.15   Nov 03 2005 16:11:06   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.14.1.0   Oct 19 2005 14:57:50   rhopkins
//TD089 ES020 Image Button Replacement - Replace ScriptableImageButtons and ordinary ImageButtons
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.14   Sep 15 2004 09:24:16   jbroome
//Complete re-design of Map tools navigation. Removal of unnecessary stages in screen flow.
//
//   Rev 1.13   Aug 19 2004 15:41:32   passuied
//Use of enum type MapMode.FromFindAInput to mimic what it is supposed to do for MapMode.FromJourneyInput
//Resolution for 1361: Maps no longer displays all gazetteer options
//Resolution for 1390: JourneyLocationMap. Wrong header displayed in FindA mode
//
//   Rev 1.12   Jun 10 2004 14:57:44   jgeorge
//IR998
//
//   Rev 1.11   May 26 2004 10:51:44   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.10   Apr 30 2004 13:29:56   jbroome
//DEL 5.4 Merge
//JavaScript Map Control
//
//   Rev 1.9   Apr 07 2004 09:54:54   CHosegood
//Message displayed is now context sensitive.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.8   Apr 06 2004 12:54:30   CHosegood
//Del 5.2 Map QA changes.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.7   Apr 01 2004 18:08:54   CHosegood
//Now goes back to the ambiguity page/input page as appropriate
//Resolution for 687: Current location button on map page does not return to ambiguity page
//
//   Rev 1.6   Mar 12 2004 16:41:04   COwczarek
//Set appropriate session parameters in button event handlers
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.5   Mar 10 2004 19:02:10   PNorell
//Updated for Map state.
//
//   Rev 1.4   Mar 10 2004 18:31:26   PNorell
//Updated stub to actually work.
//
//   Rev 1.3   Mar 10 2004 18:23:10   PNorell
//Added stub method for if the journey is return journey or not.
//
//   Rev 1.2   Mar 09 2004 15:51:20   CHosegood
//Changes for "Select from map"
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.1   Mar 08 2004 19:53:50   CHosegood
//Added PVCS header and TravelVia_Click method
//Resolution for 633: Del 5.2 Map Changes

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Control responsible for displaying/handling the
	/// plan a journey views on the Map pages.
	/// </summary>
	public partial  class MapPlanJourneyLocationControl : TDUserControl
	{
        #region Instance Members


        
		private bool usesReturnJourney = false;
        #endregion

        #region Page Properties
        #region Button Properties

        /// <summary>
        /// Get the buttonCancel button
        /// </summary>
        public TDButton ButtonPlanCancel
        {
            get { return this.buttonCancel; }
        }

		/// <summary>
		/// Get the buttonTravelFrom button
		/// </summary>
		public TDButton ButtonTravelFromLocation
		{
			get { return this.buttonTravelFrom; }
		}

		/// <summary>
		/// Get the buttonTravelTo button
		/// </summary>
		public TDButton ButtonTravelToLocation
		{
			get { return this.buttonTravelTo; }
		}

        #endregion
		/// <summary>
		/// Sets/Gets if the control should be handling outward or inward journey.
		/// </summary>
		public bool UsesReturnJourney
		{
			get { return usesReturnJourney; }

			set 
			{
				usesReturnJourney = value;
			}
		}

		/// <summary>
		/// Gets the mapstate to be used by the control
		/// </summary>
		public JourneyMapState MapState
		{
			get 
			{ 
				if( UsesReturnJourney )
				{
					return TDSessionManager.Current.ReturnJourneyMapState;
				}
				return TDSessionManager.Current.JourneyMapState;
			}
		}

        #endregion

        #region Lifecycle event handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			labelCurrentLocationInstructions.Text = Global.tdResourceManager.GetString("MapPlanJourneyLocationControl.labelCurrentLocationInstructions.Text", TDCultureInfo.CurrentUICulture );

			buttonTravelFrom.ToolTip = GetResource("MapPlanJourneyLocationControl.buttonTravelFrom.AlternateText");
			buttonTravelFrom.Text = GetResource("MapPlanJourneyLocationControl.buttonTravelFrom.Text");

			buttonTravelTo.ToolTip = GetResource("MapPlanJourneyLocationControl.buttonTravelTo.AlternateText");
			buttonTravelTo.Text = GetResource("MapPlanJourneyLocationControl.buttonTravelTo.Text");

			buttonCancel.ToolTip = GetResource("MapPlanJourneyLocationControl.buttonCancel.AlternateText");
			buttonCancel.Text = GetResource("MapPlanJourneyLocationControl.buttonCancel.Text");
		}


        /// <summary>
        /// Event handler for PreRender event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender( EventArgs e ) 
        {

			//Due to JavaScript functionality, control can be loaded but not displayed...
			if (MapState.State != StateEnum.Plan)
				this.Visible = false;
			else
				this.Visible = true;

			// ...however, make sure that client display is in sync with server state.
			// Inconsistencies can arise due to use of JavaScript on client.
			AlignClientWithServer();
			//Check for Javascript presence and register client script methods if appropriate
			EnableScriptableObjects();
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
        /// Required method for Designer support - do not modify
        ///	the contents of this method with the code editor.
        /// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region JourneyMapState tracking methods
		
		///<summary>
		/// The EnableClientScript property of a scriptable control is set so that they
		/// output an action attribute when appropriate.
		/// If JavaScript is enabled then appropriate script blocks are added to the page.
		///</summary>
		protected void EnableScriptableObjects()
		{
			// Determine if JavaScript is supported
			bool javaScriptSupported = bool.Parse((string) Session[((TDPage)Page).Javascript_Support]);
			string javaScriptDom = ((TDPage)Page).JavascriptDom;
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

			
			if (javaScriptSupported)
			{
				// Register Client scripts, add script reference, ensure control is present.
				buttonCancel.EnableClientScript = true;
				Page.ClientScript.RegisterStartupScript(typeof(MapPlanJourneyLocationControl), buttonCancel.ScriptName, scriptRepository.GetScript(buttonCancel.ScriptName, javaScriptDom));
			}
			else
			{
				// Else, make sure client script is disabled
				buttonCancel.EnableClientScript = false;
			}
		}
		
		///<summary>
		/// Make sure that the controls' style.display attribute corresponds to its current visiblilty.
		/// The client and server need to be kept in sync.
		///</summary>
		private void AlignClientWithServer()
		{
			if (this.Visible)
				this.mapPlanJourneyLocationControl.Attributes.Remove("style");
			else
			{
				this.Visible = true;
				this.mapPlanJourneyLocationControl.Attributes.Add("style", "display:none");
			}

			buttonCancel.Action = "return Plan_Cancel('"+this.Parent.ClientID+"');";
		}
		#endregion

	}
}
