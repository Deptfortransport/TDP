// *********************************************** 
// NAME                 : MapFindInformationLocationControl.ascx.cs 
// AUTHOR               : Atos Origin
// DATE CREATED         : 03/03/2004
// DESCRIPTION			:
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapFindInformationLocationControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:56   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 04 2008 08:45:00 apatel
//  CCN 0427 Changed layout of the controls
//
//   Rev 1.0   Nov 08 2007 13:16:22   mturner
//Initial revision.
//
//   Rev 1.14   Feb 23 2006 19:16:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.13.1.0   Jan 10 2006 15:26:14   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.13   Nov 14 2005 20:33:48   RHopkins
//Changed clientside JavaScript that handles visibility of map manipulation controls.
//Resolution for 3017: UEE: Location Maps - "Select new location" JavaScript error
//Resolution for 3017: UEE: Location Maps - "Select new location" JavaScript error
//
//   Rev 1.12   Nov 14 2005 18:03:54   RHopkins
//Changed clientside JavaScript that handles visibility of map manipulation controls.
//Resolution for 3017: UEE: Location Maps - "Select new location" JavaScript error
//
//   Rev 1.11   Nov 03 2005 16:16:50   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.10.1.0   Oct 19 2005 14:57:48   rhopkins
//TD089 ES020 Image Button Replacement - Replace ScriptableImageButtons and ordinary ImageButtons
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.10   Sep 15 2004 09:24:02   jbroome
//Complete re-design of Map tools navigation. Removal of unnecessary stages in screen flow.
//
//   Rev 1.9   May 26 2004 11:27:54   jbroome
//Fix for IR914 / IR826. Map Control, Find Information flow.
//
//   Rev 1.8   Apr 30 2004 13:34:14   jbroome
//DEL 5.4 Merge
//JavaScript Map Control
//
//   Rev 1.7   Apr 01 2004 18:11:04   CHosegood
//Del 5.2 map QA changes.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.6   Mar 14 2004 19:12:18   CHosegood
//DEL 5.2 Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.5   Mar 12 2004 15:57:48   CHosegood
//Checked in for integration *NOT COMPLETE*
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.4   Mar 10 2004 19:02:10   PNorell
//Updated for Map state.
//
//   Rev 1.3   Mar 10 2004 18:31:24   PNorell
//Updated stub to actually work.
//
//   Rev 1.2   Mar 10 2004 18:22:48   PNorell
//Stubbed metod for setting the component to be return journey.
//
//   Rev 1.1   Mar 08 2004 19:08:24   CHosegood
//Added PVCS header
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
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///		Summary description for MapFindInformationLocationControl.
	/// </summary>
	public partial  class MapFindInformationLocationControl : TDUserControl
	{
        #region Private Members
        private bool output = false;
		private bool usesReturnJourney = false;
        #endregion

        #region Protected Members

		

        #endregion

        #region Public Properties

		/// <summary>
        /// Exposes the Cancel button
        /// </summary>
        public TDButton ButtonCancel
        {
            get { return this.buttonFindCancel; }
        }

		/// <summary>
		/// Exposes the New Location button
		/// </summary>
		public TDButton ButtonSelectNewLocation 
		{
			get { return this.buttonSelectNewLocation; }
		}

        /// <summary>
        /// Get/Set if this is for the output page
        /// </summary>
        public bool OutputMap
        {
            get { return this.output; }
            set { this.output = value; }
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			buttonFindCancel.Text = GetResource("MapFindInformationLocationControl.buttonFindCancel.Text");
			buttonFindCancel.ToolTip = GetResource("MapFindInformationLocationControl.buttonFindCancel.AlternateText");

			buttonSelectNewLocation.Text = GetResource("MapFindInformationLocationControl.buttonSelectNewLocation.Text");
			buttonSelectNewLocation.ToolTip = GetResource("MapFindInformationLocationControl.buttonSelectNewLocation.AlternateText");

			literalInstructions.Text = Global.tdResourceManager.GetString("MapFindInformationLocationControl.literalInstructions.Text", TDCultureInfo.CurrentUICulture );

			imageMapError.AlternateText = Global.tdResourceManager.GetString("MapSelectLocationControl.imageMapError.AlternateText", TDCultureInfo.CurrentUICulture );
			imageMapError.ImageUrl = Global.tdResourceManager.GetString("MapSelectLocationControl.imageMapError.ImageUrl", TDCultureInfo.CurrentUICulture );
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender( EventArgs e ) 
        {
			this.DataBind();

			//Due to JavaScript functionality, control can be loaded but not displayed...
			if (MapState.State != StateEnum.FindInformation)
				this.Visible = false;
			else
				this.Visible = true;

			// ...however, make sure that client display is in sync with server state.
			// Inconsistencies can arise due to use of JavaScript on client.
			AlignClientWithServer();
			//Check for Javascript presence and register client script methods if appropriate
			EnableScriptableObjects();

            base.OnPreRender( e );
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
				buttonFindCancel.EnableClientScript = true;
				buttonSelectNewLocation.EnableClientScript = true;
				
				Page.ClientScript.RegisterStartupScript(typeof(MapFindInformationLocationControl), buttonFindCancel.ScriptName, scriptRepository.GetScript(buttonFindCancel.ScriptName, javaScriptDom));
				Page.ClientScript.RegisterStartupScript(typeof(MapFindInformationLocationControl), buttonSelectNewLocation.ScriptName, scriptRepository.GetScript(buttonSelectNewLocation.ScriptName, javaScriptDom));
			}
			else
			{
				// Else, make sure client script is disabled
				buttonFindCancel.EnableClientScript = false;
				buttonSelectNewLocation.EnableClientScript = false;
			}
		}
		
		///<summary>
		/// Make sure that the controls' style.display attribute corresponds to its current visiblilty.
		/// The client and server need to be kept in sync.
		///</summary>
		private void AlignClientWithServer()
		{
			if (this.Visible)
				this.mapFindInformationLocationControl.Attributes.Remove("style");
			else
			{
				this.Visible = true;
				this.mapFindInformationLocationControl.Attributes.Add("style", "display:none");
			}

			buttonFindCancel.Action = "return FindInformation_Cancel('"+this.Parent.ClientID+"', '" + output + "');";
			// Determine which view to display for SelectLocation control.
			string selectEnabled = MapState.SelectEnabled.ToString();
			buttonSelectNewLocation.Action = "return SelectLocation('"+this.Parent.ClientID+"', "+selectEnabled.ToLower()+")";

		}
		#endregion

	}
}
