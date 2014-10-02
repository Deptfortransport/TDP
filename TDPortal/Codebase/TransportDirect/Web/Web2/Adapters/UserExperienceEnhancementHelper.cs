// *********************************************** 
// NAME                 : UserExperienceEnhancementHelper.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 05/09/2005 
// DESCRIPTION  		: Helper functions for User Experirnce Enhancement functionalities.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/UserExperienceEnhancementHelper.cs-arc  $ 
//
//   Rev 1.5   Jul 20 2010 12:03:58   mmodi
//Added method to perform default Register action
//Resolution for 5010: Cannot submit new user registration details using "Enter" key
//
//   Rev 1.4   Nov 17 2009 17:59:00   mmodi
//Added new map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Sep 21 2009 14:57:06   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Mar 31 2008 12:59:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:32   mturner
//Initial revision.
//
//   Rev 1.3   Oct 06 2006 14:11:52   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.2.1.0   Sep 14 2006 16:40:02   tmollart
//Updated enum so Enter key works on FindCarPark input page.
//Resolution for 4173: Car Parking: Pressing <Enter> key does not submit search
//
//   Rev 1.2   Feb 23 2006 19:16:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:17:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Nov 11 2005 13:33:18   tolomolaiye
//Allow enter key to be used as the next button for Visit Planner
//Resolution for 2956: Visit Planner (CG): DEL 7.2 Using enter key for Next button doesn't work
//
//   Rev 1.0   Sep 16 2005 17:38:34   Schand
//Initial revision.
//
//   Rev 1.7   Sep 14 2005 13:49:08   Schand
//DN079 UEE 
//Updated Comments.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.6   Sep 13 2005 14:47:24   Schand
//DN079 UEE Default Navigation on enter key.
//Updated TakeDefaultAction.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.5   Sep 13 2005 10:14:26   Schand
//DN079
//Updated Login method().
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.4   Sep 12 2005 14:16:00   Schand
//DN079 UEE
//Updates for FxCop
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.3   Sep 12 2005 12:27:52   Schand
//// DN079 UEE
//Minor updates for FxCop
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.2   Sep 09 2005 14:05:06   Schand
//DN079 UEE Enter Key.
//Updated static method to accept Page.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.1   Sep 08 2005 14:50:14   NMoorhouse
//Updated for login controls
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.0   Sep 05 2005 18:06:24   Schand
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.ScriptRepository;   
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;   
//using System.Web.    

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// UserExperienceHelper class contains Helper functions for User Experirnce Enhancement functionalities.
	/// </summary>
	public sealed class UserExperienceEnhancementHelper
	{
		/// <summary>
		/// Private contructor.
		/// </summary>
 
		private UserExperienceEnhancementHelper()
		{
		}

		#region Public methods
		
		
		/// <summary>
		/// Add the client side onkeydown event and its handler to the given textbox
		/// </summary>
		/// <param name="textControl">Journey planner Input textbox</param>
		public static void TakeDefaultAction(TextBox textControl, Page page, PageId pageId)
		{	
			if ( ((TDPage)page).IsJavascriptEnabled && IsJourneyPlannerInputPage(pageId))
			{
				if (GetBrowserName =="IE")
				{
					textControl.Attributes.Add("onkeydown", "TakeDefaultAction()");					
				}
			}
		}
		
		/// <summary>
		/// Add the client side onkeydown event and its handler to the given textbox, for Logging in
		/// </summary>
		/// <param name="textControl">Journey planner Input textbox</param>
        public static void TakeDefaultLoginAction(TextBox textControl, Page page)
        {
            if (((TDPage)page).IsJavascriptEnabled)
            {
                textControl.Attributes.Add("onkeydown","TakeLoginAction(event)");
            }
        }

        /// <summary>
        /// Add the client side onkeydown event and its handler to the given textbox, for Registering
        /// </summary>
        /// <param name="textControl">Journey planner Input textbox</param>
        public static void TakeDefaultRegisterAction(TextBox textControl, Page page)
        {
            if (((TDPage)page).IsJavascriptEnabled)
            {
                textControl.Attributes.Add("onkeydown", "TakeRegisterAction(event)");
            }
        }

		/// <summary>
		/// Function to register client side script.
		/// </summary>
		/// <param name="page">Page to which scripts needs to be added</param>
		/// <param name="javaScriptFileName">Javascript file name</param>
		/// <param name="javaScriptDom">Type of Javascript DOM</param>
		public static void RegisterClientScript(Page page, string javaScriptFileName)
		{	TDPage tdPage = (TDPage)page;
			if (tdPage.IsJavascriptEnabled)
			{    string javaScriptDom = tdPage.JavascriptDom;
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
				// Output reference to necessary JavaScript file from the ScriptRepository
				page.ClientScript.RegisterClientScriptBlock(typeof(UserExperienceEnhancementHelper), javaScriptFileName, scriptRepository.GetScript(javaScriptFileName, javaScriptDom));                				
				
			}
		}

		
		/// <summary>
		/// Function to registers the client side script.
		/// </summary>
		/// <param name="page">Page to which scripts needs to be added</param>
		public static void AddClientForUserNavigationDefaultAction(Page page)
		{
			string javaScriptFileName = "UserNavigationDefaultAction";  			
			UserExperienceEnhancementHelper.RegisterClientScript(page, javaScriptFileName);  
		}
		
		#endregion

		#region Private Method
		/// <summary>
		/// Read Only property to return the name of the browser.
		/// </summary>
		/// <returns>Browser name as string</returns>
		private static string GetBrowserName
		{	
			get
			{
				HttpContext context = HttpContext.Current;
				HttpBrowserCapabilities browser =   context.Request.Browser;
				return	browser.Browser; 
			}
		}

		/// <summary>
		/// Function to indicate whether the given page is one of journey input planner pages. 
		/// </summary>
		/// <param name="pageName">The name of the Page</param>
		/// <returns>Returns true if the given page is one of journey input planner pages.</returns>
		private static bool IsJourneyPlannerInputPage(PageId pageId)
		{
			switch(pageId)
			{
				case PageId.FindCarInput:
				case PageId.FindCoachInput:
				case PageId.FindStationInput:
				case PageId.FindTrainInput:				
				case PageId.FindTrunkInput:
				case PageId.JourneyPlannerInput:
				case PageId.JourneyPlannerAmbiguity:
				case PageId.JourneyPlannerLocationMap:
				case PageId.TrafficMap: 
				case PageId.VisitPlannerInput:
				case PageId.FindCarParkInput:
                case PageId.FindCycleInput:
                case PageId.FindEBCInput:
                case PageId.FindMapInput:
					return true;
				default :
					return false;
 
			}
		}
		#endregion
	}
}
