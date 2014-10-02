// ***********************************************
// NAME 		: TDOnTheMove.aspx.cs
// AUTHOR 		: Richard Scott
// DATE CREATED : 17/03/05
// DESCRIPTION 	: Page to simulate what Transport Direct would look like on a mobile
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/TDOnTheMove/TDOnTheMove.aspx.cs-arc  $
//
//   Rev 1.5   Sep 14 2009 10:55:32   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.4   Jan 30 2009 10:44:30   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.3   Jan 14 2009 12:29:20   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:27:12   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory feb 05 2008 14:37:00 aahmed
//  added side menus and other CCN compliant changes
//
//   Rev 1.0   Nov 08 2007 13:31:34   mturner
//Initial revision.
//
//   Rev 1.8   Sep 03 2007 15:25:36   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.7   Jul 12 2006 16:48:10   CRees
//Fixes following code review
//Resolution for 2502: Mobile - web - Synchronising English/Welsh in 2 panels
//
//   Rev 1.6   Jul 12 2006 14:43:46   SCraddock
//Corrected scope problem
//Resolution for 2502: Mobile - web - Synchronising English/Welsh in 2 panels
//
//   Rev 1.5   Jul 10 2006 18:34:16   scraddock
//Added language value to interactivehelpurl value to get language specific page 
//Resolution for 2502: Mobile - web - Synchronising English/Welsh in 2 panels
//
//   Rev 1.4   Feb 24 2006 10:17:36   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.3   Feb 10 2006 15:09:30   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.1   Jan 09 2006 16:48:48   RGriffith
//Changes made in light of code review comments
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for TDOnTheMove.
	/// </summary>
	public partial class TDOnTheMove : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
    	public static readonly string TDMobileUIInteractiveHelp  = "TDOnTheMove.TDMobileUI.URL";
		private string interactiveHelpURL;

		public TDOnTheMove() : base()
		{
			pageId = PageId.TDOnTheMove;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//provide the TDMobileUI interactive help URL to the frame
			InteractiveHelpURL = GetInner();

			PageTitle = GetResource("TDOnTheMove.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            
            // Populate the left hand navigation menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            expandableMenuControl.AddExpandedCategory("Tips and tools");
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextHomeTools);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		private string GetInner()
		{
			IPropertyProvider pp = Properties.Current;

            string link = pp[TDMobileUIInteractiveHelp].ToString();

            //Note that we need to adjust the language if we're in Welsh:
            switch (CurrentLanguage.Value)
            {
                case Language.Welsh:
                    link = link.Replace(@"/en/", @"/cy/");
                    break;
                case Language.English:
                default:
                    //Do nothing
                    break;
            }

            if (TDSessionManager.Current.IsStopInformationMode)
            {
                if (!string.IsNullOrEmpty(TDSessionManager.Current.InputPageState.StopInfromationTDOnMoveUrl))
                {
                    link = TDSessionManager.Current.InputPageState.StopInfromationTDOnMoveUrl;
                }
            }

            return link;
		}

		public string InteractiveHelpURL
		{
			get {return interactiveHelpURL; }
			set { interactiveHelpURL = value; }
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
