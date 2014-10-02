// *********************************************** 
// NAME                 : JourneyEmissionsCompare.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 02/02/2007
// DESCRIPTION			: Page to display the CO2 emissions comparison for public transport modes 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx.cs-arc  $
//
//   Rev 1.9   Jul 28 2011 16:20:26   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.8   Jul 19 2010 16:15:58   mmodi
//Added fix for page landing from Word 2003
//
//Resolution for 5077: CO2 page landing not working from Word 2003
//
//   Rev 1.7   Feb 24 2010 13:25:28   mmodi
//Changed emissions control ID to following update to the Emissions UnitsSwitch js file
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   May 20 2009 13:39:28   mmodi
//Updated code to limit length of distance to prevent overflow error.
//
//   Rev 1.5   Jan 30 2009 10:44:20   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.4   Dec 19 2008 14:33:24   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 29 2008 15:42:14   mturner
//Fix for IR5012 - Changed to show the Tips and Tools left hand links rather than the Journey Planning links.
//
//   Rev 1.2   Mar 31 2008 13:24:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:58   mturner
//Initial revision.
//
//   Rev 1.11   Sep 11 2007 12:01:12   mmodi
//Get label text from resources
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.10   Sep 11 2007 11:23:56   mmodi
//Added expandable links control for Information section
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.9   Sep 07 2007 16:22:52   mmodi
//Added ExpandleLinkControl for related links
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.8   Sep 03 2007 15:25:12   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.7   Apr 12 2007 11:17:26   mmodi
//Updated rounding of value entered and adding it to session properrty
//Resolution for 4383: CO2: Rounding of Distance on CO2 emissions compare panel
//
//   Rev 1.6   Apr 04 2007 14:48:34   mmodi
//Removed internal links
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.5   Mar 29 2007 22:16:36   asinclair
//Added new external link
//
//   Rev 1.4   Mar 22 2007 14:09:28   tmollart
//Modifications to rounding on mileages.
//
//   Rev 1.3   Mar 09 2007 16:28:18   mmodi
//Updated related links strings
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.2   Mar 05 2007 17:19:06   mmodi
//Updated to use JourneyEmisionRelatedLinksControl
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.1   Feb 28 2007 15:51:38   mmodi
//Converted distance to metres before validating
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.0   Feb 20 2007 16:51:22   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

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
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for JourneyEmissionsCompare.
	/// </summary>
	public partial class JourneyEmissionsCompare : TDPage
	{
		#region Controls

		// Page Controls
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl informationLinksControl;
		protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
		protected HeaderControl headerControl;
        		
		// Main Content controls
		
		protected PrinterFriendlyPageButtonControl printerFriendlyControl;
        
		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsDistanceInputControl journeyEmissionsDistanceInputControl;
        protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsCompareControl journeyEmissionsCompareControlOutward;

		// Skip links
		#endregion

		#region Private variables

        bool switchingSession = false;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public JourneyEmissionsCompare()
		{
			this.pageId = PageId.JourneyEmissionsCompare;
		}

		#endregion

		#region Page_Load, Page_PreRender

		/// <summary>
		/// Page_Load
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			PageTitle =  GetResource("JourneyEmissionsCompare.AppendPageTitle")  
				+ GetResource("JourneyPlanner.DefaultPageTitle");	
			#region UEE, Navigation links, and Information panels

			// Client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

			// Left hand navigation menu set up
			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);

			// Set up client link for bookmark on expandable menu
			clientLink.BookmarkTitle = GetResource("JourneyEmissionsCompare.clientLink.BookmarkTitle");
			clientLink.LinkText = GetResource("JourneyEmissionsCompare.clientLink.LinkText");

			// Determine url to save as bookmark
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			
			string baseChannel = string.Empty;
			
			if (TDPage.SessionChannelName != null)
			{
				baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);
			}
			
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyEmissionsCompare);
			
			string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			string url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
			
			clientLink.BookmarkUrl = url;

			// Set up related links control
			labelInformationLinks.Text = GetResource("JourneyEmissionsCompare.InformationLinks.Text");
			informationLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.JourneyEmissionsCompareInfo);
			
			#endregion

            imageJourneyEmissionsCompare.ImageUrl = GetResource("HomeTipsTools.imageJourneyEmissionsPT.ImageUrl");
            imageJourneyEmissionsCompare.AlternateText = " ";

			// Set up page
			labelTitle.Text = GetResource("JourneyEmissionsCompare.Title");			
			buttonNext.Text = GetResource("JourneyEmissionsDistanceInputControl.Next");

			// Set Help button url
			helpJourneyEmissions.HelpUrl = GetResource("JourneyEmissionsCompare.HelpPageUrl");
		
			// Set printable for controls
            journeyEmissionsCompareControlOutward.NonPrintable = true;

            // Used for Landing from Word 2003
            // If the session id in the url doesn't match the current session id.
            if (this.Request.QueryString["SID"] != null && TDSessionManager.Current.Session.SessionID != this.Request.QueryString["SID"])
            {
                //Call method to copy the data from the url's session to the current session.
                TDSessionSerializer tdSS = new TDSessionSerializer();
                tdSS.UpdateToDeferredSession(TDSessionManager.Current.Session.SessionID, this.Request.QueryString["SID"]);

                switchingSession = true;
            }

            // check if landing page request
            if (TDSessionManager.Current.Session[SessionKey.LandingPageCheck] == true
                || (TDSessionManager.Current.JourneyEmissionsPageState.IsLandingPageActive))
            {
                journeyEmissionsCompareControlOutward.PageLandingActive = true;
                TDSessionManager.Current.Session[SessionKey.LandingPageCheck] = false;
            }

			// Skip links
			imageMainContentSkipLink1.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.AlternateText = GetResource("JourneyEmissionsCompare.imageMainContentSkipLink.AlternateText");

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyEmissionsCompare);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Page PreRender
		/// </summary>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			SetControlVisibility();

			// Set the params to pass for units for Printer Page
			int  units = TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceUnit;

			if (units == 1)
			{
				printerFriendlyControl.UrlParams = "Units=miles";				
			}
			else
			{
				printerFriendlyControl.UrlParams = "Units=kms";
			}

            // Used for JPLanding from Word 2003
            // If the session id in the url doesn't match the current session id.
            if (switchingSession)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = this.Request.QueryString["SID"];
            }
		}


        protected string GetSpacerText()
        {
            return GetResource("JourneyEmissions.imageSpacer.AlternateText");
        }

		#endregion

		#region Private methods

		/// <summary>
		/// Sets the visibility of the controls dependent on the page state
		/// </summary>
		private void SetControlVisibility()
		{
			// Set visibility of the controls
			if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState == JourneyEmissionsCompareState.InputDefault)
			{
				printerFriendlyControl.Visible = false;
				journeyEmissionsDistanceInputControl.Visible = true;
                journeyEmissionsCompareControlOutward.Visible = false;
				EmissionsInformationHtmlPlaceholderControl.Visible = true;
			}
			else if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState == JourneyEmissionsCompareState.InputCompare)
			{
				printerFriendlyControl.Visible = true;
				journeyEmissionsDistanceInputControl.Visible = true;
                journeyEmissionsCompareControlOutward.Visible = true;
				EmissionsInformationHtmlPlaceholderControl.Visible = false;
			}
			else
			{
				// Place in the default state
				printerFriendlyControl.Visible = false;
				journeyEmissionsDistanceInputControl.Visible = true;
                journeyEmissionsCompareControlOutward.Visible = false;
				EmissionsInformationHtmlPlaceholderControl.Visible = true;
			}


            if (journeyEmissionsCompareControlOutward.PageLandingActive)
            {
                printerFriendlyControl.Visible = true;
                journeyEmissionsDistanceInputControl.Visible = true;
                journeyEmissionsCompareControlOutward.Visible = true;
                EmissionsInformationHtmlPlaceholderControl.Visible = false;
                ProcessButtonNextClick();
                if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceValid == false)
                {
                    printerFriendlyControl.Visible = false;
                    journeyEmissionsCompareControlOutward.Visible = false;
                    EmissionsInformationHtmlPlaceholderControl.Visible = true;
                }
                if (TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan] == false)
                {
                    TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan] = true;
                    printerFriendlyControl.Visible = false;
                    journeyEmissionsCompareControlOutward.Visible = false;
                    EmissionsInformationHtmlPlaceholderControl.Visible = true;
                }


            }



		}


        /// <summary>
        /// Validates the journey distance entered
        /// </summary>
        /// <returns></returns>
        private bool ValidateJourneyDistance()
        {
            bool valid = false;


            try
            {
                double distance;

                // If in Page Landing mode, then use the distance set by the CO2LandingPage. Only for the first page
                // load. Subsequent actions on page should then carry on as normal, not needing to use the original
                // value from the landing page.
                if ((journeyEmissionsCompareControlOutward.PageLandingActive) && (!Page.IsPostBack))
                {
                    journeyEmissionsDistanceInputControl.JourneyDistanceText = TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceToDisplay;
                }

                distance = Convert.ToDouble(journeyEmissionsDistanceInputControl.JourneyDistanceText);
                string journeyDistance;

                // Stop the user entering a value less than the minimum regardless of unit type
                int minJourneyDistance = Convert.ToInt32(Properties.Current["JourneyEmissions.MinDistance"]);
                if (distance < minJourneyDistance)
                {
                    valid = false;
                }
                else
                {

                    // Convert the distance into metres before validating
                    if (journeyEmissionsDistanceInputControl.JourneyDistanceUnit == 1)
                    {
                        journeyDistance = MeasurementConversion.Convert(distance, ConversionType.MilesToKilometres);
                        distance = Math.Round(Convert.ToDouble(journeyDistance)) * 1000;
                    }
                    else
                    {
                        distance = distance * 1000;
                    }

                    valid = JourneyEmissionsHelper.JourneyDistanceValid(distance);

                    // if this is a plane only landing journey then issue error if under the minimum distance
                    if (journeyEmissionsCompareControlOutward.PageLandingActive)
                    {
                        if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModePlane
                            && !(TDSessionManager.Current.JourneyEmissionsPageState.LandingModeAll
                                || TDSessionManager.Current.JourneyEmissionsPageState.LandingModeSmallCar
                                || TDSessionManager.Current.JourneyEmissionsPageState.LandingModeLargeCar
                                || TDSessionManager.Current.JourneyEmissionsPageState.LandingModeCoach
                                || TDSessionManager.Current.JourneyEmissionsPageState.LandingModeTrain))
                        {
                            valid = (valid && JourneyEmissionsHelper.ShowAir(Convert.ToDecimal(distance)));
                        }
                    }

                    journeyEmissionsDistanceInputControl.JourneyDistanceValid = valid;

                }

                return valid;
            }
            catch
            {
                return false;
            }
        }


		#endregion

		#region Event Handlers

		/// <summary>
		/// Click event for next button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void ButtonNextClick(object sender, EventArgs e)
		{
			ProcessButtonNextClick();
		}

		private void ProcessButtonNextClick()
		{
            if (ValidateJourneyDistance())
			{
                double distance = Convert.ToDouble(journeyEmissionsDistanceInputControl.JourneyDistanceText);
				string journeyDistance;
				
				// Convert the distance into metres before saving
				if (journeyEmissionsDistanceInputControl.JourneyDistanceUnit == 1)
				{
					journeyDistance = MeasurementConversion.Convert(distance, ConversionType.MileageToMetres);
				}
				else
				{
					distance = distance * 1000;
					journeyDistance = distance.ToString();
				}

				// Set the journey distance values to the session
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistance = journeyDistance;
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceToDisplay = journeyEmissionsDistanceInputControl.JourneyDistanceText;
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceUnit =  journeyEmissionsDistanceInputControl.JourneyDistanceUnit;
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceValid = true;

				// Set the properties for JourneyEmissionsCompare control
                journeyEmissionsCompareControlOutward.JourneyEmissionsCompareMode = JourneyEmissionsCompareMode.DistanceDefault;
                journeyEmissionsCompareControlOutward.JourneyDistance = Convert.ToDecimal(journeyDistance);
                journeyEmissionsCompareControlOutward.JourneyDistanceToDisplay = journeyEmissionsDistanceInputControl.JourneyDistanceText;
				
				// Ensure the compare control shows the same road units as selected in the Distance input control
				if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceUnit == 1)
				{
                    journeyEmissionsCompareControlOutward.RoadUnits = RoadUnitsEnum.Miles;
				}
				else
				{
                    journeyEmissionsCompareControlOutward.RoadUnits = RoadUnitsEnum.Kms;
				}


				// Update the page state
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState = JourneyEmissionsCompareState.InputCompare;
			}
			else
			{
				// Update session to indicate journey distance is not valid
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceValid = false;
			}

			// Reload this page, which allows the controls to be reloaded in their (new) states
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissionsCompare;
		}

		/// <summary>
		/// Event handler for default action
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void DefaultActionClick(object sender, EventArgs e)
		{
			ImageClickEventArgs imageEventArgs = new ImageClickEventArgs(0,0);
			ButtonNextClick(sender, imageEventArgs); 
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
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			headerControl.DefaultActionEvent +=  new EventHandler(this.DefaultActionClick);
            buttonNext.Click += new EventHandler(this.ButtonNextClick);
		}
		#endregion
	}
}
