//******************************************************************************
//NAME			: TDResourceManager.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 01/07/2003
//DESCRIPTION	: TDResourceManager class inherited from ResourceManager. 
//Initially adding methods that handle language translation.  
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ResourceManager/TDResourceManager.cs-arc  $
//
//   Rev 1.3   Aug 04 2009 14:13:32   mmodi
//Added method to return a resource string, forcing it to use the CultureInfo provided
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.2   Jul 07 2008 13:16:52   mturner
//Added CLS compliance attribute to remove compiler warnings
//
//   Rev 1.1   Mar 10 2008 15:25:40   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.0   Nov 08 2007 12:45:44   mturner
//Initial revision.
//
//  Devfactory  Feb 18 2008 sbarker
//  Tweak to include the request url whem getting resources
//
//   Rev 1.6   Mar 13 2006 17:21:36   NMoorhouse
//stream3353 - added new resource "RefineJourney"
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 27 2006 11:14:14   AViitanen
//Updated VisitPlanner resource manager.
//
//   Rev 1.4   Feb 24 2006 14:51:32   RWilby
//Fix for merge stream3129.
//
//   Rev 1.3   Jan 31 2006 16:10:10   mdambrine
//Fix fo the welsh language
//
//   Rev 1.2   Jan 16 2006 16:20:18   mdambrine
//added journeyplannerservice key
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 11 2006 12:13:14   mdambrine
//Added the welsh resourcefiles and the batch creation files
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 10 2006 16:02:46   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.11   Nov 01 2005 15:12:14   build
//Automatically merged from branch for stream2638
//
//   Rev 1.10.1.1   Sep 28 2005 17:15:56   MTillett
//Create new help button control and lang strings to match SD008
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.10.1.0   Sep 21 2005 11:24:32   asinclair
//New branch for 2638 with Del 7.1
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.10   Aug 19 2005 14:07:06   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.9.1.1   Jul 18 2005 15:09:08   RPhilpott
//Change JourneyResults RM constant.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.9.1.0   Jul 08 2005 13:59:52   rgreenwood
//DN062: Added Resource Manager for OnBoard Facilities
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.9   Feb 10 2005 13:55:52   rgeraghty
//Added constant for fares and ticket retailer resource manager
//
//   Rev 1.8   Feb 10 2005 09:24:22   COwczarek
//Add constants that identify different resource managers
//Resolution for 1921: DEL 7 Development : Find A Fare Ticket Selection
//
//   Rev 1.7   Sep 22 2003 16:17:04   PNorell
//Ensured that the base href is added to the links when it is a CMS posting.
//
//   Rev 1.6   Jul 18 2003 16:27:38   JMorrissey
//Moved methods SetTextOnControls and SetLanguageCulture to the new LanguageHandler class
//
//   Rev 1.5   Jul 17 2003 10:39:02   ALole
//Updated SetThreadLanguageCulture method to use CustomDateTimeInfoManager to internationalize Date/Time text.
//
//   Rev 1.4   Jul 11 2003 14:55:08   JMorrissey
//Updated comments in code
//
//   Rev 1.3   Jul 09 2003 15:23:36   JMorrissey
//Added GetChannelLanguage method

using System;
using System.Reflection;
using System.Resources;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using System.Threading;
using System.Web;

[assembly: CLSCompliant(true)]
namespace TransportDirect.Common.ResourceManager
{

	/// <summary>
	/// Summary description for TDResourceManager, which inherits from ResourceManager
	/// This class contains look up style methods to retrieve language resources.
	/// </summary>
	public class TDResourceManager
	{	
	
		/// <summary>
		/// Identifies the resource manager for langStrings
		/// </summary>
		public const string LANG_STRINGS = "langStrings";

		/// <summary>
		/// Identifies the resource manager for Tools and Tips pages and controls
		/// </summary>
		public const string TOOLS_TIPS_RM = "Tools";

		/// <summary>
		/// Identifies the resource manager for Find A Fare pages and controls
		/// </summary>
		public const string FIND_A_FARE_RM = "FindAFare";

		/// <summary>
		/// Identifies the resource manager for Fares and ticket retailers pages and controls
		/// </summary>
		public const string FARES_AND_TICKETS_RM = "FaresAndTickets";

		/// <summary>
		/// Identifies the resource manager for Fares and ticket retailers pages and controls
		/// </summary>
		public const string JOURNEY_RESULTS_RM = "JourneyResults";

		/// <summary>
		/// Identifies the resource manager for User Survey pages and controls
		/// </summary>
		public const string USER_SURVEY_RM = "UserSurveyStrings";

		/// <summary>
		/// Identifies the resource manager for Visit Planner pages and controls
		/// </summary>
		public const string VISIT_PLANNER_RM = "VisitPlanner";

		/// <summary>
		/// Identifies the resource manager for Journey Planner Services pages and controls
		/// </summary>
		public const string JOURNEY_PLANNER_SERVICE_RM = "JourneyPlannerService";

		/// <summary>
		/// Identifies the resource manager for Refine (Extend, Replan and Adjust) pages and controls
		/// </summary>
		public const string REFINE_JOURNEY_RM = "RefineJourney";

		/// <summary>
		/// Initializes a new instance of the TDResourceManager that looks up resources in satellite assemblies based on information from the specified Type
		/// </summary>
		/// <param name="resourceSource"></param>
		
		/// <summary>
		///	Prefix added to key values referencing resource manager instances in system cache,
		/// required to make them unique
		/// </summary>
		private const string RESOURCE_MANAGER_KEY_PREFIX = "RM_";

        private string collectionName = "";
		/// <summary>
		/// Initializes a new instance of the TDResourceManager that looks up resources in satellite assemblies based on information from the specified Type
		/// </summary>
		/// <param name="resourceSource"></param>
		public TDResourceManager(Type resourceSource)
		{
			///use base class constructor 
            collectionName = resourceSource.ToString();
		}

		/// <summary>
		/// Initializes a new instance of the TDResourceManager class that looks up resources contained in files derived from the specified root name using the given Assembly
		/// </summary>
		/// <param name="baseName"></param>
		/// <param name="assembly"></param>
		public TDResourceManager(string baseName)
		{
			///use base class constructor 
            collectionName = baseName;
		}
/*
		/// <summary>
		/// Initializes a new instance of the TDResourceManager class that looks up resources contained in files derived from the specified root name using the given Assembly
		/// </summary>
		/// <param name="baseName"></param>
		/// <param name="assembly"></param>
		/// <param name="usingResourceSet"></param>
		public TDResourceManager(string baseName,Assembly assemblyName,Type usingResourceSet) : base(baseName,assemblyName,usingResourceSet)
		{
			///use base class constructor 
		}
        */

		/// <summary>
		/// Obtains a resource manager from the system cache for the specified name.
		/// If not found, an instance is created and added to the cache with a finite lifetime.
		/// </summary>
		/// <param name="name">The name of the resource manager</param>
		/// <returns>A resource manager for the specified name</returns>
		public static TDResourceManager GetResourceManagerFromCache(string name) 
		{
			return new TDResourceManager(name);
		}
        
        /// <summary>
		/// Provides an alternative way to access resources that are not  in the current local resource
		/// manager. This method calls GetResourceManagerFromCache to retrieve the correct resource manager
		/// </summary>
		/// <param name="resourceFilename">The filename of the resource manager</param>
		/// <param name="key">The key in the resource manager file to retrieve</param>
		/// <returns>A string representing the value of the key from the resource file</returns>
		public string GetString(string resourceFilename, string key)
		{
            return ContentProvider.Instance["General"].GetControlProperties().GetPropertyValue(resourceFilename, key);
		}

		/// <summary>
		/// This method overrides the getstring of the resourcemanager and get the language specific
		/// resource. This method is normally not needed but because of the unsupported welsh language
		/// this passing of the thread is needed.
		/// </summary>
		/// <param name="name">The key to look for in the resourcefiles</param>
		/// <returns></returns>
        /// 
        public string GetString(string name, System.Globalization.CultureInfo info)
        {
            try
            {
                return ContentProvider.Instance["General"].GetControlProperties().GetPropertyValue(collectionName, name);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// This method overrides the getstring of the resourcemanager and get the language specific
        /// resource. This bool flag is used to force ignoring the CurrentLanguage global class and use the 
        /// language from the CultureInfo provided.
        /// </summary>
        /// <param name="name">The key to look for in the resourcefiles</param>
        /// <returns></returns>
        public string GetString(string name, System.Globalization.CultureInfo info, bool useCultureInfo)
        {
            try
            {
                Language language = CurrentLanguage.ParseCulture(info.Name);

                return ContentProvider.Instance["General"].GetControlProperties(language).GetPropertyValue(collectionName, name);
            }
            catch
            {
                return null;
            }
        }

		public string GetString(string name)
		{
            return ContentProvider.Instance["General"].GetControlProperties().GetPropertyValue(collectionName, name);
			//return base.GetString(name, TDCultureInfo.CurrentUICulture);
		}

        public string GetContentDatabaseString(string page, string name)
        {
            return ContentProvider.Instance["ContentDatabase"].GetControlProperties().GetPropertyValue(page, name);
        }
       
        /// <summary>
        /// Uses HttpContext to get the current request Uri.
        /// </summary>
        /// <returns></returns>
        private Uri GetRequestUri()
        {
            return HttpContext.Current.Request.Url;
        }
	}
}
