// *********************************************** 
// NAME                 : BusinessLinks.aspx.cs
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 22/11/2005
// DESCRIPTION			: BusinessLinks page, allowing users to 
// select a template to insert a link to JourneyPlanning on their 
// own website.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Tools/BusinessLinks.aspx.cs-arc  $
//
//   Rev 1.10   Mar 22 2013 10:49:28   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.9   Jul 28 2011 16:21:26   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.8   Oct 29 2010 09:25:38   apatel
//Updated to change soft content to show transportdirect pdf helpsheets
//Resolution for 5627: BusinessLink page content change
//
//   Rev 1.7   Feb 26 2010 16:14:56   PScott
//Meta tag and title changes on numerous pages
//RS71001 
//SCR 5408
//Resolution for 5408: Meta tags
//
//   Rev 1.6   Jan 07 2009 08:50:26   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   Jul 24 2008 13:45:52   apatel
//External links added text "(opens new window)"
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.4   Jul 24 2008 10:44:36   apatel
//Removed External Links tooltip and added (opens new window) text to the links
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.3   Jun 26 2008 14:05:12   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:27:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:58   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.10   Jun 13 2006 09:28:12   mmodi
//Added label to reflect updates to templates and text
//Resolution for 4116: English and Welsh translations - Business Links
//
//   Rev 1.9   Feb 24 2006 12:37:24   RWilby
//Fix for merge stream3129. Added using reference to TransportDirect.Common.ResourceManager namespace.
//
//   Rev 1.8   Jan 23 2006 18:29:32   jbroome
//Udpated screen flow based on location selection
//Resolution for 3488: Business Links:  Inconsistant behaviour of the Back button from the ambiguity page when location is unresolved
//
//   Rev 1.7   Jan 05 2006 17:45:56   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.6   Dec 20 2005 14:57:54   jbroome
//Udpated navigation for Back button when on location screen
//
//   Rev 1.5   Dec 16 2005 16:23:30   jbroome
//Updated labels, hyperlinks etc after final soft content copy
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.4   Dec 09 2005 12:19:20   tolomolaiye
//Ensure the HTML text in the test area is always visible
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.3   Dec 05 2005 11:48:42   tolomolaiye
//Corrected spelling mistakes in some resource links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.2   Dec 02 2005 17:07:26   tolomolaiye
//Updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
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
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.CommonWeb.Helpers;

using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Template which displays the Business Links functionality
	/// </summary>
	public partial class BusinessLinks : TDPage
	{
		protected TriStateLocationControl2 businessLinksTriStateLocationControl;
		protected BusinessLinkTemplateSelectControl BusinessLinkTemplateSelectControl1;

		private LocationSelectControlType locationControlType;	
		private BusinessLinkState linkState;
		private LocationSearch theSearch;
		private TDLocation theLocation;

		/// <summary>
		/// Constructor sets Page Id and Local resource manager
		/// </summary>
		public BusinessLinks() : base()
		{
			pageId = PageId.BusinessLinks;
			this.LocalResourceManager = TDResourceManager.TOOLS_TIPS_RM;
		}

		/// <summary>
		/// Page Load initialises BusinessLinkState 
		/// and sets up resource strings
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            PageTitle = GetResource("BusinessLinks.AppendPageTitle");	

			ITDSessionManager sessionManager = TDSessionManager.Current;
			linkState = sessionManager.BusinessLinkState;
			IBusinessLinksTemplateCatalogue linkCatalogue = (IBusinessLinksTemplateCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.BusinessLinksTemplateCatalogue];

				// If LinkState does not exist, initialise with default values
				if (sessionManager.BusinessLinkState == null)
				{
					linkState = new BusinessLinkState();
					sessionManager.BusinessLinkState = linkState;
					linkState.CurrentStage =  BusinessLinksStage.Introduction;
				}

			// When initialised (e.g. after clicking Finish), the 
			// template type is null - need to reset to default template.
			if (linkState.TemplateType == null)
				linkState.TemplateType =  linkCatalogue.GetDefault();

			BusinessLinkTemplateSelectControl1.SelectedTemplate = linkState.TemplateType;

			//For the Introduction panel
			labelBrochure1.Text = GetResource("BusinessLinks.labelBrochure1.Text");
			labelBrochure2.Text = GetResource("BusinessLinks.labelBrochure2.Text");
			labelBrochure3.Text = GetResource("BusinessLinks.labelBrochure3.Text");
            labelBrochure4.Text = GetResource("BusinessLinks.labelBrochure4.Text");

            hyperlinkBrochure.Text = GetResource("BusinessLinks.hyperlinkBrochure.Text");
            hyperlinkTechnicalGuide1.Text = GetResource("BusinessLinks.hyperlinkTechnicalGuide.Text");
            hyperlinkHelpSheets.Text = GetResource("BusinessLinks.hyperlinkHelpSheets.Text");
						
			// Get PDF locations from properties service
			hyperlinkBrochure.NavigateUrl = Properties.Current["BusinessLinks.HyperlinkURL.PDFBrochure"];
			hyperlinkTechnicalGuide1.NavigateUrl = Properties.Current["BusinessLinks.HyperlinkURL.PDFTechnicalGuide"];
            hyperlinkHelpSheets.NavigateUrl = GetResource("BusinessLinks.hyperlinkHelpSheets.NavigateUrl");            

			hyperlinkTechnicalGuide2.Text = hyperlinkTechnicalGuide1.Text;
			hyperlinkTechnicalGuide2.NavigateUrl = hyperlinkTechnicalGuide1.NavigateUrl;
			hyperlinkTechnicalGuide3.Text = hyperlinkTechnicalGuide1.Text;
			hyperlinkTechnicalGuide3.NavigateUrl = hyperlinkTechnicalGuide1.NavigateUrl;
			hyperlinkTechnicalGuide4.Text = hyperlinkTechnicalGuide1.Text;
			hyperlinkTechnicalGuide4.NavigateUrl = hyperlinkTechnicalGuide1.NavigateUrl;

			labelTermsConditions.Text = GetResource("BusinessLinks.TermsConditionsSubheading.Text");
			labelAgreeingNote.Text = GetResource("BusinesLinks.AgreeTermsNote.Text");

			buttonNext.Text = GetResource("BusinesLinks.buttonNext.Text");

			//For the Location /Ambiguity panel
			labelStep1.Text = GetResource("BusinessLinks.Step1.Text");
			labelChooseLocation.Text = GetResource("BusinessLinks.ChooseLocation.Text");

			buttonNextLocation.Text = GetResource ("BusinesLinks.buttonNext.Text");
			//buttonLocationBack.Text = GetResource ("BusinessLinks.buttonBack.Text");

			//Used to set up and populate the Location Selection Control
			theSearch = linkState.LocationSearch;
			theLocation = linkState.Location;

			//image properties
			imageBusinessLinksExample.ImageUrl = GetResource("BusinessLinks.imageBusinessLinksExample.ImageUrl");
			imageBusinessLinksExample.AlternateText = GetResource("BusinessLinks.imageBusinessLinksExample.AltText");
            
            //additional business link example images used in steps 1, 2 and 3
            imageBusinessLinksExample1.ImageUrl = GetResource("BusinessLinks.imageBusinessLinksExample1.ImageUrl");
            imageBusinessLinksExample1.AlternateText = GetResource("BusinessLinks.imageBusinessLinksExample1.AltText");

            imageBusinessLinksExample2.ImageUrl = GetResource("BusinessLinks.imageBusinessLinksExample2.ImageUrl");
            imageBusinessLinksExample2.AlternateText = GetResource("BusinessLinks.imageBusinessLinksExample2.AltText");

            imageBusinessLinksExample3.ImageUrl = GetResource("BusinessLinks.imageBusinessLinksExample3.ImageUrl");
            imageBusinessLinksExample3.AlternateText = GetResource("BusinessLinks.imageBusinessLinksExample3.AltText");

			hyperlinkGetAdobe.NavigateUrl = GetResource("BusinessLinks.hyperlinkGetAdobe.NavigateUrl");
			imageAdobe.ImageUrl = GetResource("BusinessLinks.imageAdobe.ImageUrl");
			imageAdobe.AlternateText = GetResource("BusinessLinks.imageAdobe.AltText");

			locationControlType =  new LocationSelectControlType(TDJourneyParameters.ControlType.Default);
	
			businessLinksTriStateLocationControl.Populate(DataServiceType.BusinessLinksLocation, CurrentLocationType.None, ref theSearch, ref theLocation, ref locationControlType, true, true, false, true); 

			//For the Template panel
			labelTemplateStep1.Text = GetResource ("BusinessLinks.labelTemplateStep1.Text");
			labelTemplateStep2.Text = GetResource ("BusinessLinks.labelTemplateStep2.Text");
			labelLocationChosenIs.Text = GetResource ("BusinessLinks.labelLocationChosenIs.Text");
			
			labelChooseTemplate.Text = GetResource ("BusinessLinks.labelChooseTemplate.Text");
		
			labelStep2Note1.Text = GetResource ("BusinessLinks.labelStep2Note1.Text");
			labelStep2Note2.Text = GetResource ("BusinessLinks.labelStep2Note2.Text");
			labelStep2Note3.Text = GetResource ("BusinessLinks.labelStep2Note3.Text");
			
			buttonNextTemplate.Text = GetResource ("BusinesLinks.buttonNext.Text");
			//buttonTemplateBack.Text = GetResource ("BusinessLinks.buttonBack.Text");

			//For the TemplateDownload / Code panel
			labelCodeSnippet.Text = GetResource ("BusinessLinks.labelCodeSnippet.Text");
			buttonCodeFinish.Text = GetResource ("BusinessLinks.buttonCodeFinish.Text");
			//buttonCodeBack.Text = GetResource ("BusinessLinks.buttonBack.Text");
            buttonBack.Text = GetResource("BusinessLinks.buttonBack.Text");

            imageBusinessLinks.ImageUrl = GetResource("HomeTipsTools.imageLinkToUs.ImageUrl");
            imageBusinessLinks.AlternateText = " ";

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextBusinessLinks);
            expandableMenuControl.AddExpandedCategory("Related links");

		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			buttonNext.Click += new EventHandler(buttonNext_Click);

			buttonNextLocation.Click += new EventHandler(buttonNextLocation_Click);
			//buttonLocationBack.Click += new EventHandler(buttonLocationBack_Click);

			buttonNextTemplate.Click += new EventHandler(buttonNextTemplate_Click);
			//buttonTemplateBack.Click +=new EventHandler(buttonTemplateBack_Click);

			//buttonCodeBack.Click += new EventHandler(buttonCodeBack_Click);
			buttonCodeFinish.Click += new EventHandler(buttonCodeFinish_Click);

            buttonBack.Click += new EventHandler(buttonBack_Click);

			businessLinksTriStateLocationControl.NewLocation +=new EventHandler(businessLinksTriStateLocationControl_NewLocation);
			
			BusinessLinkTemplateSelectControl1.TemplateSelectionChanged += new EventHandler(BusinessLinkTemplateSelectControl1_TemplateSelectionChanged);
		}

		/// <summary>
		/// OnPreRender event
		/// Sets visibility of controls
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
            labelBusinessLinksHeader.Text = GetResource("BusinessLinks.BusinessLinksHeader.Text");
			switch (linkState.CurrentStage)
			{
				case BusinessLinksStage.Introduction:
					panelIntroduction.Visible = true;
					panelLocationSelection.Visible = false;
					panelTemplateSelection.Visible = false;
					panelTemplateDownload.Visible = false;
                    buttonBack.Visible = false;
					labelBusinessLinksSubheading.Visible = true;
					labelBusinessLinksSubheading.Text = GetResource("BusinessLinks.BusinessLinksSubheading.Text");
					areaTermsConditions.InnerHtml = GetResource("BusinessLinks.TermsConditions.Text");
					break;

				case BusinessLinksStage.LocationSelection:
					panelIntroduction.Visible = false;
					panelLocationSelection.Visible = true;
					panelTemplateSelection.Visible = false;
					panelTemplateDownload.Visible = false;
                    buttonBack.Visible = true;
					labelStep1.Text = GetResource("BusinessLinks.BusinessLinksHeader.Location.Text");
					labelBusinessLinksSubheading.Visible = false;

					// Set screen reader label text on location control, if location unspecified
					if (businessLinksTriStateLocationControl.LocationControl is LocationSelectControl2)
					{
						((LocationSelectControl2)businessLinksTriStateLocationControl.LocationControl).TypeInstruction.Text = GetResource("BusinessLinks.LocationControl.SRText");
					}
					break;

				case BusinessLinksStage.TemplateSelection:
					panelIntroduction.Visible = false;
					panelLocationSelection.Visible = false;
					panelTemplateSelection.Visible = true;
					panelTemplateDownload.Visible = false;
                    buttonBack.Visible = true;
					labelChosenLocation.Text = linkState.Location.Description;
					labelTemplateStep1.Text = GetResource("BusinessLinks.BusinessLinksHeader.ChooseTemplate.Text");
					labelBusinessLinksSubheading.Visible = false;
					break;

				case BusinessLinksStage.Result:
					LandingPageHelper lpHelper = new LandingPageHelper();

					panelIntroduction.Visible = false;
					panelLocationSelection.Visible = false;
					panelTemplateSelection.Visible = false;
					panelTemplateDownload.Visible = true;
                    buttonBack.Visible = true;
					labelTemplateStep2.Text = GetResource("BusinessLinks.BusinessLinksHeader.Code.Text");
					labelBusinessLinksSubheading.Visible = false;
					boxTemplateCode.Text = lpHelper.GetBusinessLinksHtml(linkState.Location, BusinessLinkTemplateSelectControl1.SelectedTemplate);
					break;
			}

			base.OnPreRender(e); 
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraEventWireUp();
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

		/// <summary>
		/// Method resets location and location search objects used in the 
		/// tristatelocation control, then repopulates the control.
		/// </summary>
		private void ResetLocation()
		{
			theLocation = new TDLocation();
			theSearch = new LocationSearch();
			
			theSearch.ClearSearch();

			locationControlType =  new LocationSelectControlType(TDJourneyParameters.ControlType.Default);	
			
			businessLinksTriStateLocationControl.Reset();
			businessLinksTriStateLocationControl.Populate(DataServiceType.BusinessLinksLocation, CurrentLocationType.None, ref theSearch, ref theLocation, ref locationControlType, true, true, false, true); 
		
		}

		/// <summary>
		/// Click Event for the Next button on Introduction Panel - displays Location 
		/// selection panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNext_Click(object sender, EventArgs e)
		{
			linkState.CurrentStage = BusinessLinksStage.LocationSelection;
		}

		/// <summary>
		/// Click Event for the Next button on the Location Selection / Ambiguity panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNextLocation_Click(object sender, EventArgs e)
		{
			linkState.LocationSubmitted = true;

			businessLinksTriStateLocationControl.Search(true);

			if(theLocation.Status == TDLocationStatus.Valid)
			{
				linkState.CurrentStage = BusinessLinksStage.TemplateSelection;
			}
		}

		/// <summary>
		/// Click Event for the Back button on the Location Selection / Ambiguity panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonLocationBack_Click(object sender, EventArgs e)
		{
			ResetLocation();

			// If user has tried to submit location (i.e. clicked next) then
			// just blank out location, else return to intro screen
			if (linkState.LocationSubmitted)
			{
				linkState.CurrentStage = BusinessLinksStage.LocationSelection;
			}
			else
			{
				linkState.CurrentStage = BusinessLinksStage.Introduction;
			}

			// Reset flag for correct screen flow
			linkState.LocationSubmitted = false;
		}

		/// <summary>
		/// Click Event for the Next button on the Template Selection panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNextTemplate_Click(object sender, EventArgs e)
		{
			linkState.TemplateType = BusinessLinkTemplateSelectControl1.SelectedTemplate;
			linkState.CurrentStage = BusinessLinksStage.Result;
		}

		/// <summary>
		/// Click Event for the Back button on the Template Selection panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonTemplateBack_Click(object sender, EventArgs e)
		{
			ResetLocation();
			linkState.CurrentStage = BusinessLinksStage.LocationSelection;
			// Reset flag for correct screen flow
			linkState.LocationSubmitted = false;
		}

		/// <summary>
		/// Click Event for the Back button on the Code download panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCodeBack_Click(object sender, EventArgs e)
		{
            linkState.CurrentStage = BusinessLinksStage.TemplateSelection;
		}

        /// <summary>
        /// Click Event for the Back button on page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, EventArgs e)
        {

            switch (linkState.CurrentStage)
            {
                case BusinessLinksStage.LocationSelection:
                    ResetLocation();

                    // If user has tried to submit location (i.e. clicked next) then
                    // just blank out location, else return to intro screen
                    if (linkState.LocationSubmitted)
                    {
                        linkState.CurrentStage = BusinessLinksStage.LocationSelection;
                    }
                    else
                    {
                        linkState.CurrentStage = BusinessLinksStage.Introduction;
                    }

                    // Reset flag for correct screen flow
                    linkState.LocationSubmitted = false;
                    break;

                case BusinessLinksStage.TemplateSelection:
                    ResetLocation();
                    linkState.CurrentStage = BusinessLinksStage.LocationSelection;
                    // Reset flag for correct screen flow
                    linkState.LocationSubmitted = false;
                    break;

                case BusinessLinksStage.Result:
                    linkState.CurrentStage = BusinessLinksStage.TemplateSelection;
                    break;

                default:
                    linkState.CurrentStage = BusinessLinksStage.Introduction;
                    break;

            }

        }

		/// <summary>
		/// Click Event for the Finish button on the Code download panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCodeFinish_Click(object sender, EventArgs e)
		{
			linkState.Clear();
		}

		/// <summary>
		/// Clear the details in the location control and reset the control to unspecified
		/// </summary>
		private void businessLinksTriStateLocationControl_NewLocation(object sender, EventArgs e)
		{
			theLocation.Status = TDLocationStatus.Unspecified;
			locationControlType =  new LocationSelectControlType(TDJourneyParameters.ControlType.Default);	
			theSearch.ClearAll();
			businessLinksTriStateLocationControl.Reset();
			businessLinksTriStateLocationControl.Populate(DataServiceType.BusinessLinksLocation, CurrentLocationType.None, ref theSearch, ref theLocation, ref locationControlType, true, true, false, true); 
		}

		/// <summary>
		/// Method handles the event raised by the BusinessLinkTemplateSelectControl
		/// when the selection changes. Updates the session state with the new template.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BusinessLinkTemplateSelectControl1_TemplateSelectionChanged(object sender, EventArgs e)
		{
			linkState.TemplateType = BusinessLinkTemplateSelectControl1.SelectedTemplate;
		}
	}
}
