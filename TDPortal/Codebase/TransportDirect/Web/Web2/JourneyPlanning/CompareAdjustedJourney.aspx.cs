// *********************************************** 
// NAME                 : CompareAdjustedJourney.aspx.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 19/09/2003
// DESCRIPTION			: The compare adjusted journey screen.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/CompareAdjustedJourney.aspx.cs-arc  $
//
//   Rev 1.6   Oct 28 2010 14:33:40   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Mar 29 2010 16:39:20   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.4   Jan 14 2009 14:19:52   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 15 2008 12:08:14   mmodi
//Renamed label to remove warning
//
//   Rev 1.2   Mar 31 2008 13:24:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:04   mturner
//Initial revision.
//
//Rev DevFatory Feb 14th 10:12:00 dgath
//buttonBackToOriginal and associated get resource / event handlers moved to this page from 
//JourneyDetailsCompareControl.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.28   Apr 07 2006 10:09:00   asinclair
//Added code to get header string
//
//   Rev 1.27   Apr 06 2006 16:51:08   asinclair
//Added subheading label
//Resolution for 3822: DN068 Adjust: Changes requested by Ben
//
//   Rev 1.26   Apr 04 2006 10:33:56   AViitanen
//Initialised resource manager
//Resolution for 3763: DN068 Adjust: Skip link text on Adjusted Journey Details page
//
//   Rev 1.25   Mar 20 2006 18:14:38   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Mar 13 2006 15:52:14   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.2.2   Mar 08 2006 21:08:10   pcross
//FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.2.1   Mar 03 2006 16:00:54   pcross
//Skip links
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.2.0   Mar 02 2006 15:12:38   pcross
//Layout changes to fit in with new adjust screens
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.23   Feb 24 2006 09:56:24   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.22   Feb 10 2006 15:08:44   build
//Automatically merged from branch for stream3180
//
//   Rev 1.21.1.0   Dec 06 2005 11:00:24   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.21   Nov 23 2005 14:19:30   ralonso
//Fixed problem with Print Friendly and Save buttons
//Resolution for 3171: UEE: Adjust - Incorrect button text displayed for Adjusted journey details screen
//
//   Rev 1.20   Nov 09 2005 12:31:30   build
//Automatically merged from branch for stream2818
//
//   Rev 1.19.1.0   Oct 14 2005 15:30:20   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.19   Sep 02 2005 17:32:02   asinclair
//Fixed errors
//
//   Rev 1.18   Sep 02 2005 16:41:16   asinclair
//Now using new error control
//
//   Rev 1.17   Oct 15 2004 12:38:40   jgeorge
//Changed to take account of new JourneyPlanStateData object and changes to JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.16   Apr 03 2004 14:53:30   AWindley
//DEL5.2 QA Changes: Resolution for 692
//
//   Rev 1.15   Mar 12 2004 09:36:06   PNorell
//Updated text for adjusted journeys
//
//   Rev 1.14   Nov 25 2003 18:25:04   COwczarek
//SCR#145: CSS style does not appear correctly in Mozilla browsers
//Resolution for 145: CSS style does not appear correctly in Mozilla browsers
//
//   Rev 1.13   Nov 17 2003 15:08:42   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.12   Nov 07 2003 11:22:26   PNorell
//Test
//
//   Rev 1.11   Nov 05 2003 12:27:06   kcheung
//Added headers
//
//   Rev 1.10   Nov 04 2003 13:38:48   kcheung
//Corrected n*mespace to Web.Templates
//
//   Rev 1.9   Oct 21 2003 15:00:26   kcheung
//Cosmetic corrections for FXCOP
//
//   Rev 1.8   Oct 15 2003 17:11:30   kcheung
//Fixed image printer hyperlink so that Alt text now appears
//
//   Rev 1.7   Oct 10 2003 12:52:20   kcheung
//Page Title read from lang strings.
//
//   Rev 1.6   Oct 03 2003 14:57:42   PNorell
//Added file header
using System;
using TransportDirect.Common.ResourceManager;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

using TransportDirect.UserPortal.Web.Controls;

using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for CompareAdjustedJourney.
	/// </summary>
	/// 
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class CompareAdjustedJourney : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyPageButton;
		protected JourneyDetailsCompareControl JourneyDetailsCompareControl1;

		protected ErrorDisplayControl errorDisplayControl;

		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;
	
		/// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public CompareAdjustedJourney()
		{
			this.pageId = PageId.CompareAdjustedJourney;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#region Properties
		/// <summary>
		/// Exposes the Print button control that links to a printer-friendly page.
		/// The Page ID of the printer-friendly page is the current page ID prefixed with "Printable".
		/// If no printer-friendly page is available then the Print button will not be shown.
		/// </summary>
		public TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl PrinterFriendlyPageButton
		{
			get { return printerFriendlyPageButton; }
			set { printerFriendlyPageButton = value; }
		}
		#endregion

		/// <summary>
		/// Page Load - sets up the page.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			JourneyDetailsCompareControl1.MyPageId = pageId;
			
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

			// Put user code to initialize the page here
			AsyncCallState acs = TDSessionManager.Current.AsyncCallState;
			if( acs != null && acs.Status == AsyncCallStatus.NoResults )
			{
				ITDJourneyResult result = TDSessionManager.Current.AmendedJourneyResult;				

				ArrayList errorsList = new ArrayList();
				bool arriveBefore = TDSessionManager.Current.CurrentAdjustState.AmendedJourneyRequest.OutwardArriveBefore;
				foreach( CJPMessage mess in result.CJPMessages )
				{

					if(mess.Type == ErrorsType.Warning)
					{
						errorDisplayControl.Type = ErrorsDisplayType.Warning;
					}
					string text = mess.MessageText;

					if( text == null || text.Length == 0 )
					{
						string errResource = mess.MessageResourceId;
						if( mess.MessageResourceId == JourneyControlConstants.JourneyWebNoResults )
						{			
								
							errResource = arriveBefore ? JourneyControlConstants.AdjustJourneyNoEarlierTime : JourneyControlConstants.AdjustJourneyNoLaterTime ; // Set it to a more suitable journey control constant
						}
						text = Global.tdResourceManager.GetString(errResource, TDCultureInfo.CurrentUICulture);
					}

					errorsList.Add( text );

				}

				errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));
				errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString(CultureInfo.InvariantCulture);

				if (errorDisplayControl.ErrorStrings.Length > 0)
				{
					errorMessagePanel.Visible = true;
					errorDisplayControl.Visible = true;
				}
				else
				{
					errorMessagePanel.Visible = false;
					errorDisplayControl.Visible = false ;
				}

				// Clear the error messages.
				result.ClearMessages();
			}
			else
			{
				errorMessagePanel.Visible = false;
				errorDisplayControl.Visible = false ;
			}

			labelSubheading.Text = GetResource("CompareAdjustedJourney.InstructionalText.Text");
			if (errorMessagePanel.Visible)
				labelSubheading.Visible = false;

            headerLabel.Text = GetResource("CompareAdjustedJourney.Header");

            this.PageTitle = GetResource("JourneyPlanner.CompareAdjustedJourneyPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextCompareAdjustedJourney);
            expandableMenuControl.AddExpandedCategory("Related links");
            //buttonBackToOriginal.Visible = !Printable;
            buttonBackToOriginal.Text = Global.tdResourceManager.GetString(
            "JourneyDetailsCompareControl.buttonBackToOriginal.Text", TDCultureInfo.CurrentUICulture);
            buttonBackToOriginal.ToolTip = Global.tdResourceManager.GetString(
            "JourneyDetailsCompareControl.buttonBackToOriginal.AlternateText", TDCultureInfo.CurrentUICulture);
        
        }

		/// <summary>
		/// Pre render event handling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			SetupSkipLinksAndScreenReaderText();
		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("CompareAdjustedJourney.imageMainContentSkipLink.AlternateText");
		
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
            this.buttonBackToOriginal.Click += new EventHandler(this.buttonBackToOriginal_Click);
		}
		#endregion


        /// <summary>
        /// Handler for the Back Button
        /// </summary>
        private void buttonBackToOriginal_Click(object sender, EventArgs e)
        {
            // Return to the journey adjust input page for outward / return as appropriate
            TDCurrentAdjustState adjustState = TDSessionManager.Current.CurrentAdjustState;

            bool outward = adjustState.CurrentAmendmentType == TDAmendmentType.OutwardJourney;

            if (outward)
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
                    TransitionEvent.JourneyAdjustOutward;
            else
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
                    TransitionEvent.JourneyAdjustReturn;

        }

	}
}
