// *********************************************** 
// NAME                 : ExpandableMenuControl.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 07/12/2005
// DESCRIPTION			: The ExpandableMenuControl displays a collapsable tree of links for navigating
//							around the site
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ExpandableMenuControl.ascx.cs-arc  $
//
//   Rev 1.18   Dec 18 2012 16:54:52   dlane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.17   Feb 07 2012 12:44:54   dlane
//Check in part 1 for  BatchJourneyPlanner - edited classes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.16   Jul 28 2011 16:18:50   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.15   Feb 16 2010 17:53:08   mmodi
//Updated for International input page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.14   Nov 02 2009 17:49:00   mmodi
//Updated for Find map pages
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.13   Oct 26 2009 10:24:00   mmodi
//Added Opens new window image change
//
//   Rev 1.12   Oct 23 2009 09:05:00   apatel
//Stop info departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.11   Sep 21 2009 14:57:22   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.10   Sep 14 2009 10:55:26   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.9   Oct 29 2008 16:18:02   mmodi
//Corrected class value setting
//
//   Rev 1.8   Oct 14 2008 11:57:12   mmodi
//Manual merge for stream5014
//
//   Rev 1.7   Oct 08 2008 16:05:28   mturner
//Further XHTML compliance fix
//
//   Rev 1.6   Oct 08 2008 14:01:18   mturner
//Updated for XHTML compliance
//
//   Rev 1.5   Jul 24 2008 10:44:26   apatel
//Removed External Links tooltip and added (opens new window) text to the links
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.4   Jun 26 2008 14:04:08   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3.1.4   Oct 13 2008 10:40:32   mmodi
//Added page id for cycle mode
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.3   Sep 24 2008 14:37:42   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.2   Sep 17 2008 12:48:34   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.1   Sep 15 2008 10:56:00   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 20 2008 14:39:56   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 29 2008 15:39:34   mturner
//Fix for IR 5012 - Changed the menu's to be expanded on the LocationInformation and JourneyEmissions Compare Pages.
//Resolution for 5012: Incorrect left hand links on JourneyEmissionsCompare and LocationInformation Pages
//
//   Rev 1.2   Mar 31 2008 13:19:54   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Mar 17 2008 12:35:00 apatel
//  Rootlink databound event modified to show/hide title heading on mini home pages depending on the property set in property database table
//
//  Rev Devfactory Feb 29 2008 08:50:00 apatel
//  Added pages to switch left hand expanding menu in OnLoad even for FindFareDateSelection, FindFareTicketSelectionReturn 
//  and FindFareTicketSelectionSingles page.
//
//  Rev Devfactory Feb 29 2008 08:50:00 apatel
//  Added pages to switch left hand expanding menu in OnLoad even for RefineMap page.
//
//  Rev DevFactory Feb 24 2008 12:52:00 dgath
//  Added pages to switch block for expanding menus in OnLoad event.
//  Also alphabetised pages listed to make it easier to see if a page had already been added to the switch block.
//
//  Rev DevFactory Feb 23 2008 07:42:00 apatel
//  Modified code to make expanding menu expanding turned on/off depending on TDPage's IsMenuExpandable property
//
//  Rev DevFactory Feb 21 2008 08:30:00 apatel
// Changed control to render second level menu. Also moved registerJavascript method call from onLoad event
// to onPreRender event. Changes done to handle categories with empty links
//
//DEVFACTORY   Feb 13 2008 13:51:08   aahmed
//white labelling added expanded menus depending on what page user is on
//
//DEVFACTORY   Feb 10 2008 14:41:08   aahmed
//white labelling uncommented show menus for side menu to show expanded on homepage 
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:13:08   mturner
//Initial revision.
//
//   Rev 1.13   Sep 07 2007 14:55:14   mmodi
//Added external link check for root links
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.12   Sep 06 2007 18:15:28   mmodi
//Open external links in new window
//Resolution for 4493: Car journey details: Screen reader improvements
//
//   Rev 1.11   Sep 06 2007 15:14:12   mturner
//Added code to expand all left hand link sections when you are on the homepage.  IR4494 - Del 9.7 homepage changes
//
//   Rev 1.10   Mar 06 2007 12:29:54   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.9.1.0   Feb 19 2007 17:42:44   mmodi
//Added Journey Emission Compare pages
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.9   Nov 14 2006 09:59:34   rbroddle
//Merge for stream4220
//
//   Rev 1.8.1.1   Nov 12 2006 13:45:04   tmollart
//Added functionality for FindTrainCostInput.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.8.1.0   Oct 23 2006 12:25:44   kjosling
//Added logic so menu expands on FindTrainInput page
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.8   Feb 24 2006 14:45:32   RWilby
//Fix for merge stream3129.
//
//   Rev 1.7   Jan 09 2006 15:57:50   RGriffith
//Changes made in light of code review comments
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.6   Dec 23 2005 15:22:20   RGriffith
//FxCop suggested changes
//
//   Rev 1.5   Dec 23 2005 12:12:36   RGriffith
//Changes to output Javascript only if javascript is detected (or setting is not known)
//
//   Rev 1.4   Dec 20 2005 17:57:54   RGriffith
//Changes to make ExpandableMenu control w3 compliant
//
//   Rev 1.3   Dec 19 2005 14:57:14   RGriffith
//Added comments
//
//   Rev 1.2   Dec 19 2005 10:32:08   RGriffith
//Chagnes to have CategoryCssClass as a property that can be set in the HTML
//
//   Rev 1.1   Dec 14 2005 18:12:04   RGriffith
//Updates to expandable menu
//
//   Rev 1.0   Dec 13 2005 11:12:16   RGriffith
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
    using System.Collections.Generic;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Collections;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SuggestionLinkService;
	using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.Common.DatabaseInfrastructure.Content;
    using TD.ThemeInfrastructure;
    using TransportDirect.Common.PropertyService.Properties;

	/// <summary>
	///		Summary description for ExpandableMenuControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ExpandableMenuControl : TDUserControl
	{
		protected System.Web.UI.WebControls.Repeater rootLinkRepeater;
		protected TransportDirect.UserPortal.Web.Controls.ScriptableHyperlink rootLink;
		protected System.Web.UI.WebControls.Repeater subcategoryLinkRepeater;
		protected System.Web.UI.WebControls.HyperLink subcategoryLink;
        
                
		#region Private Attributes
		// Array used for determining contexts used on a given expandable menu control
		private ArrayList contexts = new ArrayList();
		// Array used to determine the categories that have been selected.
		private ArrayList expandedCategories = new ArrayList();
		private string scriptBlock = string.Empty;
		int categoriesExpanded = 0;

		// Repeaters for root category and subcategory lists
		private Repeater rootLinkList;
		private Repeater subcategoryLinkList;

		// Variable for determining the basechannel.
		private string appAddress = string.Empty;
		// Constant for determining the javascript filename.
		private const string scriptName = "ExpandableMenu";
		
		// Private Property variable for determining which css class categories to use.
		private string categoryCssClass;

        // Private Property variable for determining whether menu is expandable with javascript or static
        private bool isMenuExpandable = true;

        // Text resource added to links which open in a new window
        private string opensNewWindow = string.Empty;

		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new context to the ExpandableMenu. The list of contexts available when
		/// the control is rendered determine which links will be displayed on a page
		/// </summary>
		/// <param name="context">The Context to be added</param>
		public void AddContext(SuggestionLinkService.Context context)
		{
            if (!contexts.Contains(context))
			{
				contexts.Add(context);
			}
		}

		/// <summary>
		/// Adds a new category to the list of categories expanded when a page is loaded.
		/// </summary>
		/// <param name="categoryToExpand">The category name to add to the list of expanded categories</param>
		public void AddExpandedCategory(string categoryToExpand)
		{
			if (!expandedCategories.Contains(categoryToExpand))
			{
				expandedCategories.Add(categoryToExpand);
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Readonly property for determining which css class categories to use when rendering the control
		/// </summary>
		public string CategoryCssClass
		{
			get 
			{
                return "ExpandableMenu " + categoryCssClass;
			}
			set 
			{
				categoryCssClass = value;
			}
		}

        
		#endregion

		#region Private Methods
		/// <summary>
		/// Page load method initially ran when the control is loaded.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set the event handler for the categories repeater when it has ItemData bound to it
			this.categories.ItemDataBound +=new RepeaterItemEventHandler(categories_ItemDataBound);

			// Determine the base session name for the web address used in the links
			string strURL = TDPage.SessionChannelName;
			if(strURL != null)
			{
				appAddress = TDPage.getBaseChannelURL(TDPage.SessionChannelName);
			}

            TDPage page = (TDPage)this.Page;
            isMenuExpandable = page.IsMenuExpandable;

            // Set up the opens new window resource
            if (string.IsNullOrEmpty(GetResource("ExternalLinks.OpensNewWindowImage")))
            {
                opensNewWindow = string.Empty;
            }
            else
            {
                opensNewWindow = " " + GetResource("ExternalLinks.OpensNewWindowImage");
            }
            
		}

		/// <summary>
		/// Method to register the required javascript blocks that are initially required 
		/// when the page loads (including the registering of the associated javascript file)
		/// </summary>
		private void registerJavaScript()
		{
            // CCN 0427 if IsMenuExpandable false javascript not registered to make menu expandable.
            if (isMenuExpandable)
            {
                // Determine if javascript is support and determine the JavascriptDom value
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

                // Register the ExpandableMenu java script file on the page
                Page.ClientScript.RegisterClientScriptBlock(typeof(ExpandableMenuControl), "", scriptRepository.GetScript(scriptName, javaScriptDom));

                // Register the CollapseExpandedMenuBranch javascript method on the page to run 
                // when the page loads (if javascript is enabled)
                if (!Page.ClientScript.IsClientScriptBlockRegistered(scriptName))
                {
                    string scriptBlock = "<script language=\"javascript\" type=\"text/javascript\"><!-- \nCollapseExpandedMenuBranch('"
                        + expandableMenu.ClientID
                        + "'); \n//--></script>";

                    Page.ClientScript.RegisterStartupScript(typeof(ExpandableMenuControl), scriptName, scriptBlock);
                }
            }
		}

		/// <summary>
		/// Helper method. Processes the links by category
		/// </summary>
		/// <param name="linkItems">The list of links that will be added to the suggestion box at runtime</param>
		/// <returns>An array list of categories containing the links that will be rendered</returns>
		private static ArrayList ProcessLinks(SuggestionBoxLinkItem[] linkItems)
		{
			ArrayList categories = new ArrayList();
			ArrayList links = new ArrayList();
            List<string> subcategories = new List<string>();

			string lastCategory = linkItems[0].Category;

            foreach (SuggestionBoxLinkItem item in linkItems)
            {
                if (!item.IsRoot && item.IsSubRootLink)
                    if(!subcategories.Contains(item.Category))
                        subcategories.Add(item.Category);
            }

            
            
			// Add links into the Arraylist of links and the array list of Categories 
			// (where a new category is encountered)
			foreach(SuggestionBoxLinkItem item in linkItems)
			{
                if (!subcategories.Contains(item.Category))
                {
                    if (item.Category != lastCategory)
                    {
                        categories.Add(links.Clone());
                        links.Clear();
                        lastCategory = item.Category;
                    }
                    links.Add(item);
                }
			}
			if(links.Count > 0)
			{
				categories.Add(links.Clone());
			}
			return categories;
		}
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Method OnInit contains Auto generated code for initializing components
		/// </summary>
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

		#region Event Handlers
		/// <summary>
		/// Renders the ExpandableMenuControl
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnPreRender(EventArgs e)
		{
			if(contexts == null || contexts.Count > 0)
			{
				//Create new SuggestionBoxLinkCatalogue for determining the contexts/items to be used in the expandable menu
				SuggestionBoxLinkCatalogue catalogue
					= (SuggestionBoxLinkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.SuggestionLinkService];
				//Convert to array of contexts
				SuggestionLinkService.Context[] listOfContexts = (SuggestionLinkService.Context[])contexts.ToArray(typeof(SuggestionLinkService.Context));

                Theme theme = ThemeProvider.Instance.GetTheme();

				//Get the link data for the current set of contexts
                SuggestionBoxLinkItem[] items = catalogue.GetUniqueLinkByContext(listOfContexts, theme.Id);

                          
				//IF there is no link data, render the control invisible
				if(items == null || items.Length == 0)
				{
					this.Visible = false;
				}
				else
				{
                    //CCN 0427 needs to check if expandable category got a category which got no links
                    //as this will make page load script in rootrepeater item data bound method not to fire.
                    List<string> expandedCategoriesToRemove = new List<string>();
                    foreach (string category in expandedCategories)
                    {
                        int count = 0;
                        foreach (SuggestionBoxLinkItem item in items)
                        {
                            if (item.Category == category)
                            {
                                count++;
                                
                            }
                        }
                        if (count == 0)
                            expandedCategoriesToRemove.Add(category);
                    }
                    foreach (string categoryToRemove in expandedCategoriesToRemove)
                    {
                        expandedCategories.Remove(categoryToRemove);
                    }
					//although ordered by category, the data needs to be data-bindable. To this end, we 
					//offload the returned links into a hashtable, indexed by category. This is then bound
					//to the first repeater on the form. The nested repeater will display the links for
					//each category
					ArrayList data = ProcessLinks(items);

                    // CCN 0427 moved registerJavaScript method call from onLoad control event to 
                    // on prerender event. This is due to javascript error being thrown when there is no 
                    // menu for the page.

                    if (data.Count > 0)
                    {
                        // Register the javascripts on the page - if Javascript is enabled 
                        // (or setting is not known as default is now to assume javascript is on)
                        TDPage page = (TDPage)this.Page;
                        if ((page.IsJavascriptEnabled) || (!page.IsJavaScriptSettingKnown))
                        {
                            // CCN 0427 if IsMenuExpandable false javascript not registered to make menu expandable.
                            if (isMenuExpandable)
                            {
                                registerJavaScript();
                            }
                        }
                    }
					// Bind the data to the ExpandableMenu repeater
					categories.DataSource = data;
					this.DataBind();
				}
			}
			else
			{
				this.Visible = false;
			}
		}

		/// <summary>
		/// OnLoad method to register the javascript and determine which category should be expanded
		/// depending on the page the control is used on.
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			

			// Determine which category to expand depending on the page the control is accessed from
			switch(PageId)
			{
				case PageId.Home: 
                case PageId.SpecialNoticeBoard:
                    //made changes for white labelling
					//AddExpandedCategory("Home");
					// Added for IR4494 at request of DfT
					AddExpandedCategory("Plan a journey");
                    AddExpandedCategory("Find a place");
                    AddExpandedCategory("Live travel");
                    AddExpandedCategory("Tips and tools");
                    

					// End of code added for IR4494
					break;
				case PageId.FindTrainCostInput:
					AddExpandedCategory("Plan a journey");
					break;
                
                // added for white labeliing where Plan a Journey is an expanded category
                case PageId.AdjustFullItinerarySummary:
                case PageId.CarDetails:
                case PageId.CompareAdjustedJourney:
                case PageId.ExtendedFullItinerarySummary:
                case PageId.ExtendJourneyInput:
                case PageId.FindBusInput: 
                case PageId.FindCarInput:
                case PageId.FindCycleInput:
                case PageId.FindCoachInput:
                case PageId.FindFlightInput:
                case PageId.FindTrainInput:
                case PageId.FindTrunkInput:
                case PageId.FindInternationalInput:
                case PageId.HomePlanAJourney:
                case PageId.JourneyAccessibility:
                case PageId.JourneyAdjust:
                case PageId.JourneyDetails:
                case PageId.JourneyEmissions:
                case PageId.JourneyFares:
                case PageId.JourneyMap:
                case PageId.JourneyOverview:
                case PageId.JourneyPlannerInput:
                case PageId.JourneySummary:
                case PageId.ParkAndRide:
                case PageId.ParkAndRideInput:
                case PageId.RefineDetails:
                case PageId.RefineJourneyPlan:
                case PageId.ReplanFullItinerarySummary:
                case PageId.ServiceDetails:
                case PageId.TicketRetailers:
                case PageId.TicketRetailersHandOff:
                case PageId.TicketUpgrade:
                case PageId.VisitPlannerInput:
                case PageId.VisitPlannerResults:
                case PageId.RefineMap:
                case PageId.FindFareDateSelection:
                case PageId.FindFareTicketSelection:
                case PageId.FindFareTicketSelectionReturn:
                case PageId.FindFareTicketSelectionSingles:
                case PageId.JourneyReplanInputPage:
                case PageId.JourneyEmissionsCompareJourney:
                case PageId.ExtensionResultsSummary:
                case PageId.JourneyPlannerAmbiguity:
                case PageId.RefineTickets:
                case PageId.RetailerInformation:
                case PageId.CycleJourneyDetails:
                case PageId.CycleJourneySummary:
                case PageId.CycleJourneyMap:
                case PageId.StopServiceDetails:
                case PageId.FindNearestAccessibleStop:
                    AddExpandedCategory("Plan a journey");
                    break;

                // added for white labeliing where Plan a Find a place is an expanded category
                case PageId.FindStationInput:
                case PageId.FindStationMap:
                case PageId.FindStationResults:
                case PageId.HomeFindAPlace:
                case PageId.JourneyPlannerLocationMap:
                case PageId.NetworkMaps:
                case PageId.TrafficMap:
                case PageId.FindMapInput:
                case PageId.FindMapResult:
                    AddExpandedCategory("Find a place");
                    break;

                //added for white labeliing where Plan a Find a Live Travel is an expanded category
                case PageId.DepartureBoards:
                    AddExpandedCategory("Departure boards");
                    AddExpandedCategory("Live travel");
                    break;
                case PageId.HomeTravelInfo:
                case PageId.TravelNews:
                    AddExpandedCategory("Live travel");
                    break;

                //added for white labeliing where Plan a Find a Tips and Tools is an expanded category
                case PageId.FeedbackPage:
                    AddExpandedCategory("Provide Feedback");
                    AddExpandedCategory("Tips and tools");
                    break;
                case PageId.BusinessLinks:
                case PageId.ContactUsPage:
                case PageId.FeedbackViewer:
                case PageId.HomeTipsTools:
                case PageId.JourneyEmissionsCompare:
                case PageId.LogViewer:
                case PageId.ToolbarDownload:
                case PageId.VersionViewer:
                case PageId.FindEBCInput:
                case PageId.EBCJourneyDetails:
                case PageId.EBCJourneyMap:
                case PageId.BatchJourneyPlanner:
                    AddExpandedCategory("Tips and tools");
                    break;
                
                case PageId.FindCarParkInput:
                case PageId.FindCarParkResults:
                case PageId.FindCarParkMap:
                case PageId.CarParkInformation:
                case PageId.LocationInformation:
                case PageId.StopInformation:
                    AddExpandedCategory("Plan a journey");
                    AddExpandedCategory("Find a place");
                    break;

                default:
					break;
			}

			base.OnLoad(e);
		}

		/// <summary>
		/// Repeater ItemDataBound event handler. 
		/// Used at top level to split the categories and subcategories so that the
		/// lower level repeaters can interpret them as ScriptableHyperlinks or Hyperlinks appropriately
		/// </summary>
		private void categories_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			ArrayList categoryLinks = new ArrayList();
			ArrayList subcategoryLinks = new ArrayList();

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				// Get data item from repeater
				ArrayList links = (ArrayList)e.Item.DataItem;
				// Find the sub repeater controls
				rootLinkList = (Repeater)e.Item.FindControl("rootLinkRepeater");
				subcategoryLinkList = (Repeater)e.Item.FindControl("subcategoryLinkRepeater");
				HtmlControl subcategory = (HtmlControl)e.Item.FindControl("subcategory");
                HtmlControl category = (HtmlControl)e.Item.FindControl("category");
				
				// For each repeater item - determine if it is a top level link or a lower level link
				// and add to the relevant array list for further processing
				foreach (SuggestionBoxLinkItem item in links)
				{
					if (item.IsRoot && !item.IsSubRootLink)
					{
                        // CCN 0427 Added property to show/hide title heading menu item on mini home pages.
                        if (Properties.Current["ExpandableMenu.MiniHomePages.ShowHeading"].ToLower() == "false")
                        {
                            if ((item.Category == "Plan a journey"
                                || item.Category == "Find a place"
                                || item.Category == "Live travel"
                                || item.Category == "Tips and tools")
                                && (!contexts.Contains(SuggestionLinkService.Context.HomePageMenu)))
                            {
                                category.Visible = false;
                                rootLinkList.Visible = false;
                            }

                        }
                        categoryLinks.Add(item);
					}
					else
					{
						subcategoryLinks.Add(item);
					}
				}

				// If category links exist - bind them to the rootLinkList repeater
                if (categoryLinks.Count > 0)
                {
                    rootLinkList.ItemDataBound += new RepeaterItemEventHandler(rootLinkRepeater_ItemDataBound);
                    rootLinkList.DataSource = categoryLinks;
                    rootLinkList.DataBind();
                }
                else
                {
                    category.Visible = false;
                }

				// If sub category links exist - bind them to the subcategoryLinks repeater
				if (subcategoryLinks.Count > 0)
				{
					subcategory.Visible = true;
					subcategoryLinkList.ItemDataBound += new RepeaterItemEventHandler(subcategoryLinkRepeater_ItemDataBound);
					subcategoryLinkList.DataSource = subcategoryLinks;
					subcategoryLinkList.DataBind();
				} 
				else
				{
					subcategory.Visible = false;
				}
			}
		}

		/// <summary>
		/// Repeater ItemDataBound event handler. 
		/// Used to set the category level links as scriptableHyperlinks and attach the javascript 
		/// function to toggle the expanded status (where appropriate).
		/// </summary>
		private void rootLinkRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// Determine whether or not to enable Javascript 
			// (assum true if javascript setting not known)
			bool enableJavascript = false;
			TDPage page = (TDPage)this.Page;
			if ((page.IsJavascriptEnabled) || (!page.IsJavaScriptSettingKnown))
			{
				enableJavascript = true;
			}

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				// Get the data item and from the repeater and find the scriptable hyperlink.
				SuggestionBoxLinkItem item = (SuggestionBoxLinkItem)e.Item.DataItem;
				ScriptableHyperlink scriptableLink = (ScriptableHyperlink)e.Item.FindControl("rootLink");
                

				// Set the scriptable hyperlink text value from the stored procedure results
				scriptableLink.Text = item.GetLinkText();

				// Set the navigation URL to '#' if no url exists - otherwise set it to the
				// url retrieved from the database stored procedure
				if (item.GetUrl(appAddress) == appAddress) // Change when able to get the values as Null!
				{
					scriptableLink.NavigateUrl = "#";
                    // CCN 0427 if IsMenuExpandable false javascript not registered to make menu expandable.
					if (enableJavascript && isMenuExpandable)
					{
						// Determine javascript filename to be used with the scriptable hyperlink
						scriptableLink.ScriptName = scriptName;
						// Enable scripting
						scriptableLink.EnableClientScript = true;
						// Bind the scriptable hyperlink to the Toggle function in the ExpandableMenu javascript file
						scriptableLink.Action = 
							"return ToggleExpandedMenuBranch('" + subcategoryLinkList.Parent.ClientID + "', '" + scriptableLink.ClientID + "');";
					}
				} 
				else
				{
					scriptableLink.NavigateUrl = item.GetUrl(appAddress);
				}

                if (item.IsExternal)
                {
                    scriptableLink.Attributes["onclick"] = "window.open(this.href); return false";
                    scriptableLink.Text += opensNewWindow;
                }

                // CCN 0427 Added property to show/hide title heading menu item on mini home pages.
                if (Properties.Current["ExpandableMenu.MiniHomePages.ShowHeading"].ToLower() == "false")
                {
                    if ((item.Category == "Plan a journey"
                        || item.Category == "Find a place"
                        || item.Category == "Live travel"
                        || item.Category == "Tips and tools") 
                        && (!contexts.Contains(SuggestionLinkService.Context.HomePageMenu)))
                    {
                      
                       scriptableLink.Visible = false;
                    }
                }


				// Add toggle script block to execute when page first loads (for any expanded menus on 1st load)
                // CCN 0427 if IsMenuExpandable false javascript not registered to make menu expandable.
				if((enableJavascript)&& (expandedCategories.Count != 0) && isMenuExpandable )
				{
					if (expandedCategories.Contains(item.Category))
					{
                        scriptBlock = scriptBlock + "<script language=\"javascript\" type=\"text/javascript\"><!-- \nToggleExpandedMenuBranch('" + subcategoryLinkList.Parent.ClientID + "', '" + scriptableLink.ClientID + "'); \n//--></script>";
						categoriesExpanded ++;
					}
		
					if(categoriesExpanded == expandedCategories.Count)
					{
						Page.ClientScript.RegisterStartupScript(typeof(ExpandableMenuControl),"preLoad" + this.UniqueID, scriptBlock);
					}
				}
			}
		}

		/// <summary>
		/// Repeater ItemDataBound event handler. 
		/// Used to set the subcategory links as Hyperlinks and set their .NavigateUrl property
		/// </summary>
		private void subcategoryLinkRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
            bool enableJavascript = false;
            TDPage page = (TDPage)this.Page;
            if ((page.IsJavascriptEnabled) || (!page.IsJavaScriptSettingKnown))
            {
                enableJavascript = true;
            }

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//Find the hyperlink and set its attributes
				SuggestionBoxLinkItem item = (SuggestionBoxLinkItem)e.Item.DataItem;
				HyperLink link = (HyperLink)e.Item.FindControl("subcategoryLink");
                // Changes made to handle second level category links
                ScriptableHyperlink slink = (ScriptableHyperlink)e.Item.FindControl("subrootLink");
                Repeater subrootcategoryLinkRepeater = (Repeater)e.Item.FindControl("subrootcategoryLinkRepeater");
                HtmlControl subrootcategory = (HtmlControl)e.Item.FindControl("subrootcategory");

                if (!item.IsRoot && !item.IsSubRootLink)
                {
                    
                    link.Text = item.GetLinkText();
                    if (link.Text.Length == 0)
                    {
                        //Hide the hyperlink and DIV container 
                        e.Item.Visible = false;
                    }
                    else
                    {
                        link.NavigateUrl = item.GetUrl(appAddress);
                    }

                    if (item.IsExternal)
                    {
                        link.Attributes["onclick"] = "window.open(this.href); return false";
                        link.Text += opensNewWindow;
                    }
                }
                // if item is root and is also subroot, it means its a subcategory root
                else if (item.IsRoot && item.IsSubRootLink)
                {
                    //Get links for this root
                    List<SuggestionBoxLinkItem> sublinks = GetSubLinksForSubRoot(item.SuggestionLinkId);

                    //if subroot links are greater than zero bind it to subrootlink repeater 
                    if (sublinks.Count > 0)
                    {
                        slink.Visible = true;
                        subrootcategory.Visible = true;
                        subrootcategoryLinkRepeater.Visible = true;
                        link.Visible = false;

                        // CCN 0427 if menu is not expandable show sub root menus always expanded.
                        if (!isMenuExpandable)
                            subrootcategory.Attributes["style"] = "display:block;";

                        subrootcategoryLinkRepeater.ItemDataBound += new RepeaterItemEventHandler(subrootcategoryLinkRepeater_ItemDataBound);
                        subrootcategoryLinkRepeater.DataSource = sublinks;
                        subrootcategoryLinkRepeater.DataBind();


                        // Set the scriptable hyperlink text value from the stored procedure results
                        slink.Text = item.GetLinkText();

                        // Set the navigation URL to '#' if no url exists - otherwise set it to the
                        // url retrieved from the database stored procedure
                        if (item.GetUrl(appAddress) == appAddress) // Change when able to get the values as Null!
                        {
                            slink.NavigateUrl = "#";
                            // CCN 0427 if IsMenuExpandable false javascript not registered to make menu expandable.
                            if (enableJavascript && isMenuExpandable)
                            {
                                // Determine javascript filename to be used with the scriptable hyperlink
                                slink.ScriptName = scriptName;
                                // Enable scripting
                                slink.EnableClientScript = true;
                                // Bind the scriptable hyperlink to the Toggle function in the ExpandableMenu javascript file
                                slink.Action =
                                    "return ToggleExpandedMenuBranch('" + subrootcategoryLinkRepeater.Parent.ClientID + "', '" + slink.ClientID + "');";
                            }
                        }
                        else
                        {
                            // CCN 0427 if sublinks count is greater than zero render subroot link as blank
                            // this is due to subrootlink might have the url for the page and is used as related link.
                            if (sublinks.Count > 0)
                            {
                                slink.NavigateUrl = "#";
                                // CCN 0427 if IsMenuExpandable false javascript not registered to make menu expandable.
                                if (enableJavascript && isMenuExpandable)
                                {
                                    // Determine javascript filename to be used with the scriptable hyperlink
                                    slink.ScriptName = scriptName;
                                    // Enable scripting
                                    slink.EnableClientScript = true;
                                    // Bind the scriptable hyperlink to the Toggle function in the ExpandableMenu javascript file
                                    slink.Action =
                                        "return ToggleExpandedMenuBranch('" + subrootcategoryLinkRepeater.Parent.ClientID + "', '" + slink.ClientID + "');";
                                }
                            }
                            else
                                slink.NavigateUrl = item.GetUrl(appAddress);
                        }

                        if (item.IsExternal)
                        {
                            slink.Attributes["onclick"] = "window.open(this.href); return false";
                            slink.Text += opensNewWindow;
                        }

                        // Add toggle script block to execute when page first loads (for any expanded menus on 1st load)
                        // CCN 0427 if IsMenuExpandable false javascript not registered to make menu expandable.
                        if ((enableJavascript) && (expandedCategories.Count != 0) && isMenuExpandable)
                        {
                            if (expandedCategories.Contains(sublinks[0].Category))
                            {
                                scriptBlock = scriptBlock + "<script language=\"javascript\" type=\"text/javascript\"><!-- \nToggleExpandedMenuBranch('" + subrootcategoryLinkRepeater.Parent.ClientID + "', '" + slink.ClientID + "'); \n//--></script>";
                                categoriesExpanded++;
                            }

                            if (categoriesExpanded == expandedCategories.Count)
                            {
                                Page.ClientScript.RegisterStartupScript(typeof(ExpandableMenuControl), "preLoad" + this.UniqueID, scriptBlock);
                            }
                        }
                    }
                    else
                    {
                        link.Text = item.GetLinkText();
                        if (link.Text.Length == 0)
                        {
                            //Hide the hyperlink and DIV container 
                            e.Item.Visible = false;
                        }
                        else
                        {
                            link.NavigateUrl = item.GetUrl(appAddress);
                        }

                        if (item.IsExternal)
                        {
                            link.Attributes["onclick"] = "window.open(this.href); return false";
                            link.Text += opensNewWindow;
                        }
                    }

                }
			}
		}


        /// <summary>
		/// Repeater ItemDataBound event handler. 
		/// Used to set the subrootcategory links as Hyperlinks and set their .NavigateUrl property
		/// </summary>
        private void subrootcategoryLinkRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Find the hyperlink and set its attributes
                SuggestionBoxLinkItem item = (SuggestionBoxLinkItem)e.Item.DataItem;
                HyperLink link = (HyperLink)e.Item.FindControl("subrootcategoryLink");
                
                if (!item.IsRoot && item.IsSubRootLink)
                {

                    link.Text = item.GetLinkText();
                    if (link.Text.Length == 0)
                    {
                        //Hide the hyperlink and DIV container 
                        e.Item.Visible = false;
                    }
                    else
                    {
                        link.NavigateUrl = item.GetUrl(appAddress);
                    }

                    if (item.IsExternal)
                    {
                        link.Attributes["onclick"] = "window.open(this.href); return false";
                        link.Text += opensNewWindow;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the sub root links (secont level links) for sub root.
        /// </summary>
        /// <param name="subRootLinkId">suggestion link id for the sub root</param>
        /// <returns>list of suggestion links for sub root.</returns>
        private List<SuggestionBoxLinkItem> GetSubLinksForSubRoot(int subRootLinkId)
        {
            List<SuggestionBoxLinkItem> sublinks = new List<SuggestionBoxLinkItem>();

            SuggestionBoxLinkCatalogue catalogue
                    = (SuggestionBoxLinkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.SuggestionLinkService];
            //Convert to array of contexts
            SuggestionLinkService.Context[] listOfContexts = (SuggestionLinkService.Context[])contexts.ToArray(typeof(SuggestionLinkService.Context));

            Theme theme = ThemeProvider.Instance.GetTheme();

            //Get the link data for the current set of contexts
            SuggestionBoxLinkItem[] items = catalogue.GetUniqueLinkByContext(listOfContexts, theme.Id);

            foreach (SuggestionBoxLinkItem item in items)
            {
                if (item.IsSubRootLink && item.SubRootLinkId == subRootLinkId)
                    sublinks.Add(item);
            }

            return sublinks;

        }
		#endregion
	}
}
