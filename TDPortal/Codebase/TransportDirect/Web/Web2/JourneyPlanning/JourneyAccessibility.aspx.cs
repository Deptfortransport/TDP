// *********************************************** 
// NAME                 : JourneyAccessibility.aspx
// AUTHOR               : James Broome
// DATE CREATED         : 08/09/2005 
// DESCRIPTION			: Displays transport accessibility links
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyAccessibility.aspx.cs-arc  $ 
//
//   Rev 1.7   Feb 25 2010 11:03:24   mmodi
//Read accessability modes from session if set
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Jan 12 2009 15:43:14   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   Oct 09 2008 15:21:06   mmodi
//Updated to check for modelist count
//Resolution for 5141: Cycle Planner - "Server Error" is displayed when user clicks on 'accessibility issues relating to the types of vehicles used in this journey' link at the bottom of details, map and summary page in 'Notes' section
//
//   Rev 1.4   Jul 24 2008 13:45:50   apatel
//External links added text "(opens new window)"
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.3   Jun 26 2008 14:04:26   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:24:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:52   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.6   Mar 10 2006 16:43:24   JFrank
//Made the title on the accessibility info page visible.
//
//   Rev 1.5   Feb 23 2006 18:52:18   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.4   Feb 10 2006 15:08:36   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.0   Nov 30 2005 14:45:24   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.3   Nov 14 2005 14:47:50   ralonso
//ButtonBack.Text was using the FindStationInput.commandBack resource. Now it is using its own. Lanstring files modified accordingly
//
//   Rev 1.2   Nov 03 2005 17:02:34   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.1.1.0   Oct 12 2005 10:32:06   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.2   Oct 07 2005 17:33:24   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.1   Jul 20 2005 15:33:24   NMoorhouse
//Apply Review Comments
//Resolution for 2556: DEL 8 Stream: Accessibility Links
//
//   Rev 1.0   Jun 29 2005 11:12:54   jbroome
//Initial revision.
//Resolution for 2556: DEL 8 Stream: Accessibility Links

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
using System.IO;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page displays transport accessibility links
	/// </summary>
	public partial class JourneyAccessibility : TDPage
	{
		#region Instance variables
		
		private ArrayList modeList = new ArrayList();
		

		protected TransportDirect.UserPortal.Web.Controls.JourneyAccessibilityLinksControl linksControl;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		#endregion

		/// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public JourneyAccessibility() : base()
		{
			pageId = PageId.JourneyAccessibility;
		}

        /// <summary>
        /// Page Load - sets up text, urls and correct header tabs.
        /// Populates the links control with transport modes used in 
        /// current journey in session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Back button properties
			buttonBack.Text = GetResource("JourneyAccessibility.commandBack.Text");
					
			IExternalLinks externalLinks = ExternalLinks.Current;
			ExternalLinkDetail linkDetail = externalLinks[ExternalLinksKeys.AccessibilityInformation_BeforeTravel];

			// If link is valid, then show footer label text
			if ((linkDetail != null) && (linkDetail.IsValid))
			{
				// Create hyperlink control
				StringWriter sw = new StringWriter();
				HtmlTextWriter htw = new HtmlTextWriter(sw);
				HyperLink link = new HyperLink();
			
				link.Text = GetResource("JourneyAccessibility.labelFooter.LinkText");
				link.NavigateUrl = linkDetail.Url;
				link.Target = "_blank";
                
				// Render link into underlying string writer
				link.RenderControl(htw);	
			
				// Set up label footer
				labelFooter.Visible = true;
				// sw contains the hyperlink string
                labelFooter.Text = string.Format(GetResource("JourneyAccessibility.labelFooter.Text"),
                    sw.ToString(), GetResource("ExternalLinks.OpensNewWindowText"));
			}
			else
			{
				// If link is invalid or cannot be found, then do not display
				labelFooter.Visible = false;
			}

			// Set the modeList ArrayList with the modes used
			GetTransportModesUsed();
			
			if ((modeList.Count < 1)
                ||
                (modeList.Count == 1 && modeList[0].ToString() == ModeType.Walk.ToString()))
			{
				linksControl.Visible = false;
			}

			// Populate the linksControl with the modes used
			linksControl.TransportModes = (ModeType[]) modeList.ToArray(typeof(ModeType));

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyAccessibility);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Method accesses the correct session/itinerary location to retreive
		/// the current journey results and obtain the modes of transport involved.
		/// </summary>
		private void GetTransportModesUsed()
		{
			// Has the user extended the journey?
			bool itineraryExists = ((TDItineraryManager.Current != null) && (TDItineraryManager.Current.Length > 0));
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            // Did calling page set up any modes to be shown
            if ((inputPageState.AccessabilityModeTypes != null) && (inputPageState.AccessabilityModeTypes.Length > 0))
            {
                AddTransportModes(inputPageState.AccessabilityModeTypes);
            }
			// If so, are we viewing the full itinerary?
			else if (itineraryExists && TDItineraryManager.Current.FullItinerarySelected)
			{
				// Get the modes used for each journey in the itinerary
				for (int i=0; i<TDItineraryManager.Current.Length; i++)
				{
					TDJourneyViewState viewState = TDItineraryManager.Current.SpecificJourneyViewState(i);
					ITDJourneyResult result = TDItineraryManager.Current.SpecificJourneyResult(i);

					// Outward Journeys
					if (viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
					{
						AddCarMode();
					}
					else if (result.OutwardPublicJourneyCount > 0)
					{
						int outwardJourney = viewState.SelectedOutwardJourneyID;
						ModeType[] outwardModes = result.OutwardPublicJourney(outwardJourney).GetUsedModes();
						AddTransportModes(outwardModes);
					}

					// Return journeys
					if (TDItineraryManager.Current.ReturnExists)
					{
						if (viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
						{
							AddCarMode();
						}
						else if (result.ReturnPublicJourneyCount > 0)
						{
							int returnJourney = viewState.SelectedReturnJourneyID;
							ModeType[] returnModes = result.ReturnPublicJourney(returnJourney).GetUsedModes();
							AddTransportModes(returnModes);
						}
					}
				}
			}
			else
			{
				// Access journey result from itinerary/session
				ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;
				// If there are outward journeys present?
				if (TDItineraryManager.Current.OutwardExists)
				{
					if (TDItineraryManager.Current.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
					{
						AddCarMode();
					}
					else if (result.OutwardPublicJourneyCount > 0)
					{
						int outwardJourney = TDItineraryManager.Current.SelectedOutwardJourneyID;
						ModeType[] outwardModes = result.OutwardPublicJourney(outwardJourney).GetUsedModes();
						AddTransportModes(outwardModes);
					}
				}
				// Are there return journeys present?
				if (TDItineraryManager.Current.ReturnExists)
				{
					if (TDItineraryManager.Current.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
					{
						AddCarMode();
					}
					else if (result.ReturnPublicJourneyCount > 0)
					{
						int returnJourney = TDItineraryManager.Current.SelectedReturnJourneyID;
						ModeType[] returnModes = result.ReturnPublicJourney(returnJourney).GetUsedModes();
						AddTransportModes(returnModes);
					}
				}
			}
		}

		/// <summary>
		/// Method adds an array of Transport Modes to the 
		/// array list, if they do not already exist.
		/// </summary>
		/// <param name="modes"></param>
		private void AddTransportModes(ModeType[] modes)
		{
			foreach (ModeType mode in modes)
			{
				if (!modeList.Contains(mode))
				{
					modeList.Add(mode);
				}
			}
		}

		/// <summary>
		/// Adds the car transport mode to the array list.
		/// </summary>
		private void AddCarMode()
		{
			ModeType[] carMode = new ModeType[1] {ModeType.Car};
			AddTransportModes(carMode);
		}

		/// <summary>
		/// Handler for the back button click event
		/// Accesses the referring page from the query string parameter.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonBack_Click(object sender, EventArgs e)
		{
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyAccessibilityBack;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			AddExtraWireEvent();
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

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void AddExtraWireEvent()
		{
			// Setting up the Button Event handlers
			buttonBack.Click += new EventHandler(this.ButtonBack_Click);
		}
		#endregion
	}
}
